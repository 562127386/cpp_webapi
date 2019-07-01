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
    public class ReservaHotelQrAsyncBaseCrudAppService : AsyncBaseCrudAppService<ReservaHotelQr, ReservaHotelQrDto, PagedAndFilteredResultRequestDto>, IReservaHotelQrAsyncBaseCrudAppService
    {
        public ReservaHotelQrAsyncBaseCrudAppService(
            IBaseRepository<ReservaHotelQr> repository
            ) : base(repository)
        {
            Repository = repository;
        }

        public IBaseRepository<ReservaHotelQr> Repository { get; }

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

        public JArray GenerarRegistrosMovil(Dictionary<int, int> diccionario, List<ReservaHotelQr> registros)
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
                ReservaHotelQr instancia = JsonToObject(obj);

                Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                Int32 unixTimestamp2 = (Int32)(DateTime.UtcNow.Subtract(new DateTime())).TotalSeconds;
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

        public List<ReservaHotelQr> GetRegistros(int version, List<int> usuarios)
        {
            var registros = Repository.GetAll()
                .Where(o => o.Version > version)
                .ToList()
                ;
            return registros;
        }

        public JObject ObjectToJson(ReservaHotelQr entidad)
        {
            JObject objJson = new JObject();

            objJson.Add("m_id", entidad.Id);
            if (entidad.Ref != null)
            {
                objJson.Add("m_ref", entidad.Ref);
            }
            objJson.Add("m_version", entidad.Version);
            objJson.Add("vigente", GetStringFromBool(entidad.IsDeleted == false));

            //objJson.Add("proveedor_id", entidad.ProveedorId);
            objJson.Add("espacio_habitacion_id", entidad.EspacioHabitacionId);
            objJson.Add("identificacion_colaborador", entidad.IdentificacionColaborador);
            objJson.Add("fecha_registro", GetStringFromDate(entidad.FechaRegistro));
            objJson.Add("fecha_desde", GetStringFromDate(entidad.FechaDesde));
            objJson.Add("fecha_hasta", GetStringFromDate(entidad.FechaHasta));
            objJson.Add("creation_time", GetStringFromDateTime(entidad.CreationTime));
            objJson.Add("creator_user_id", entidad.CreatorUserId);


            return objJson;
        }

        public ReservaHotelQr JsonToObject(JObject json)
        {
            var entity = new ReservaHotelQr();

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

            //entity.ProveedorId = (int)json["proveedor_id"];
            entity.EspacioHabitacionId = (int)json["espacio_habitacion_id"];
            entity.IdentificacionColaborador = (string)json["identificacion_colaborador"];
            entity.FechaRegistro = GetDateFromString((string)json["fecha_registro"]);
            entity.FechaDesde = GetDateFromString((string)json["fecha_desde"]);
            entity.FechaHasta = GetDateFromString((string)json["fecha_hasta"]);

            if (json.GetValue("creator_user_id").Type != JTokenType.Null)
                entity.CreatorUserId = (long)json["creator_user_id"];

            entity.CreationTime = GetDateTimeFromString((string)json["creation_time"]);


            return entity;
        }
    }
}
