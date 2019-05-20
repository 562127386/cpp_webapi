using System;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.proyecto.aplicacion.Dto;
using com.cpp.calypso.proyecto.dominio.Entidades;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.proyecto.aplicacion.Interfaces;

namespace com.cpp.calypso.proyecto.aplicacion.Service
{
    public class OperacionDiariaAsyncBaseCrudAppService : AsyncBaseCrudAppService<OperacionDiaria, OperacionDiariaDto, PagedAndFilteredResultRequestDto>, IOperacionDiariaAsyncBaseCrudAppService
    {
        public IBaseRepository<OperacionDiaria> Repository { get; }

        public OperacionDiariaAsyncBaseCrudAppService(
            IBaseRepository<OperacionDiaria> repository
            ) : base(repository)
        {
            Repository = repository;
        }

        public JArray Sync(int version, JArray registrosJson, List<int> usuarios)
        {
            var diccionario = Sincronizar(version, registrosJson, usuarios);
            var registros = GetRegistros(version, usuarios);

            var json = GenerarRegistrosMovil(diccionario, registros);
            return json;

        }

        public Dictionary<int, int> Sincronizar(int version, JArray registrosJson, List<int> usuariosId)
        {
            //Aplicar los cambios que vienen del movil y generar el binding de Ids SqlServer vs SqlLite
            var lKeyBinding = SincronizacionLocal(registrosJson);

            return lKeyBinding;
        }

        public JArray GenerarRegistrosMovil(Dictionary<int, int> diccionario, List<OperacionDiaria> registros)
        {
            //IList<TEntityDto> registros = GetRegistros(version, usuariosId);

            JArray registroJson = new JArray();
            foreach (var entidad in registros)
            {

                int Id = entidad.Id;
                var objJson = ObjectToJson(entidad);


                //En el caso de registros nuevos y actualizados desde movil hay que reflejar el bindgin
                //con el ID local
                if (diccionario.ContainsKey(Id))
                {
                    objJson.Add("lkey", diccionario[Id]);
                }
                registroJson.Add(objJson);
            }

            return registroJson;
        }

        public Dictionary<int, int> SincronizacionLocal(JArray registrosJson)
        {

            var lKeyBinding = new Dictionary<int, int>();

            foreach (JObject obj in registrosJson)
            {

                //Recuperamos la instacia de la entidad concreta
                OperacionDiaria instancia = JsonToObject(obj);

                Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                instancia.Version = unixTimestamp;


                if (instancia.Id == 0)
                {
                    try
                    {
                        var created = Repository.InsertAndGetId(instancia);

                        //Vinculamos el ID SqlServer al Id Sqlite
                        lKeyBinding.Add(created, obj.GetValue("lkey").Value<int>());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);

                    }
                }
                else
                {

                    //Si la instancia ha sido persistida realizamos un update
                    Repository.Update(instancia);

                    //Vinculamos el ID SqlServer al Id Sqlite
                    lKeyBinding.Add(instancia.Id, obj.GetValue("lkey").Value<int>());
                }
            }
            return lKeyBinding;
        }

        public List<OperacionDiaria> GetRegistros(int version, List<int> usuarios)
        {
            var registros = Repository.GetAll()
                .Where(o => o.Version > version)
                .ToList()
                ;
            return registros;
        }

        public JObject ObjectToJson(OperacionDiaria entidad)
        {
            JObject objJson = new JObject();

            objJson.Add("m_id", entidad.Id);
            if (entidad.Ref != null)
            {
                objJson.Add("m_ref", entidad.Ref);
            }
            objJson.Add("m_version", entidad.Version);
            objJson.Add("vigente", GetStringFromBool(entidad.IsDeleted == false));

            //objJson.Add("fecha_registro", GetStringFromDateTime(entidad.FechaRegistro));
            objJson.Add("chofer_id", entidad.ChoferId);
            objJson.Add("fecha_inicio", GetStringFromDateTime(entidad.FechaInicio));
            objJson.Add("fecha_fin", GetStringFromDateTime(entidad.FechaFin));
            objJson.Add("vehiculo_id", entidad.VehiculoId);
            objJson.Add("kilometraje_inicio", entidad.KilometrajeInicio);
            objJson.Add("kilometraje_final", entidad.KilometrajeFinal);
            objJson.Add("observacion", entidad.Observacion);
            objJson.Add("estado", entidad.Estado);

            return objJson;
        }

        public OperacionDiaria JsonToObject(JObject json)
        {

            var entity = new OperacionDiaria();
            if (json.GetValue("m_id").Type == JTokenType.Null)
                json["m_id"] = 0;

            if (json.Property("m_id") != null && (int)json.Property("m_id") != 0)
            {
                int id = (int)json["m_id"];
                entity = Repository.Get(id);
            }

            //Si el obj viene con referencia UUID se carga.
            if (json.Property("m_ref") != null)
            {
                entity.Ref = (string)json["m_ref"];
            }

            //Se encera la version
            entity.Version = 0;
            if (entity.Id == 0)
            {
                entity.IsDeleted = false;
            }
            else
            {
                entity.IsDeleted = (bool)json["vigente"] == false;
            }



            //entity.FechaRegistro = GetDateTimeFromString((string)json["fecha_registro"]);
            entity.ChoferId = (int)json["chofer_id"];
            entity.VehiculoId = (int)json["vehiculo_id"];

            if (json.GetValue("fecha_inicio").Type != JTokenType.Null)
                entity.FechaInicio = GetDateTimeFromString((string)json["fecha_inicio"]);

            if (json.GetValue("fecha_fin").Type != JTokenType.Null)
                entity.FechaFin = GetDateTimeFromString((string)json["fecha_fin"]);

            if (json.GetValue("kilometraje_inicio").Type != JTokenType.Null)
                entity.KilometrajeInicio = (int)json["kilometraje_inicio"];

            if (json.GetValue("kilometraje_final").Type != JTokenType.Null)
                entity.KilometrajeFinal = (int)json["kilometraje_final"];


            entity.Observacion = (string)json["observacion"];
            entity.Estado = (string)json["estado"];

            return entity;
        }
    }
}
