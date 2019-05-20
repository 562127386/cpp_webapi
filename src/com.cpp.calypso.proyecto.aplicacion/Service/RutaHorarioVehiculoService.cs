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
    public class RutaHorarioVehiculoAsyncBaseCrudAppService : AsyncBaseCrudAppService<RutaHorarioVehiculo, RutaHorarioVehiculoDto, PagedAndFilteredResultRequestDto>, IRutaHorarioVehiculoAsyncBaseCrudAppService
    {
        public IBaseRepository<RutaHorarioVehiculo> Repository { get; }

        public RutaHorarioVehiculoAsyncBaseCrudAppService(
            IBaseRepository<RutaHorarioVehiculo> repository
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

        public JArray GenerarRegistrosMovil(Dictionary<int, int> diccionario, List<RutaHorarioVehiculo> registros)
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
                RutaHorarioVehiculo instancia = JsonToObject(obj);

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

        public List<RutaHorarioVehiculo> GetRegistros(int version, List<int> usuarios)
        {
            var registros = Repository.GetAll()
                .Where(o => o.Version > version)
                .ToList()
                ;
            return registros;
        }

        public JObject ObjectToJson(RutaHorarioVehiculo entidad)
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
            objJson.Add("ruta_id", entidad.RutaHorarioId);
            objJson.Add("vehiculo_id", entidad.VehiculoId);
            objJson.Add("fecha_desde", GetStringFromDate(entidad.FechaDesde));
            objJson.Add("fecha_hasta", GetStringFromDate(entidad.FechaHasta));
            objJson.Add("hora_salida", GetStringFromTimeSpan(entidad.HoraSalida));
            objJson.Add("duracion", entidad.Duracion);
            objJson.Add("hora_llegada", GetStringFromTimeSpan(entidad.HoraLlegada));
            objJson.Add("observacion", entidad.Observacion);
            objJson.Add("estado", entidad.Estado);
            objJson.Add("fecha_asignacion", GetStringFromDate(entidad.FechaAsignacion));
            objJson.Add("usuario_asignacion", entidad.UsuarioAsignacion);
            objJson.Add("fecha_desasignacion", GetStringFromDate(entidad.FechaDesasignacion));
            objJson.Add("usuario_desasignacion", entidad.UsuarioDesasignacion);
            objJson.Add("crea_operacion", entidad.CreaOperacion ? 1 : 0);

            return objJson;
        }

        public RutaHorarioVehiculo JsonToObject(JObject json)
        {

            var entity = new RutaHorarioVehiculo();
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

            entity.RutaHorarioId = (int)json["ruta_id"];
            entity.VehiculoId = (int)json["vehiculo_id"];
            entity.FechaDesde = GetDateFromString((string)json["fecha_desde"]);
            entity.FechaHasta = GetDateFromString((string)json["fecha_hasta"]);
            entity.HoraSalida = GetTimeSpanFromString((string)json["hora_salida"]);
            entity.Duracion = (int)json["duracion"];
            entity.HoraLlegada = GetTimeSpanFromString((string)json["hora_llegada"]);
            entity.Observacion = (string)json["observacion"];
            entity.Estado = (string)json["estado"];
            entity.FechaAsignacion = GetDateFromString((string)json["fecha_asignacion"]);
            entity.UsuarioAsignacion = (int)json["usuario_asignacion"];
            entity.FechaDesasignacion = GetDateFromString((string)json["fecha_desasignacion"]);
            entity.UsuarioDesasignacion = (int)json["usuario_desasignacion"];
            var creaOperacion = (int)json["crea_operacion"];
            entity.CreaOperacion = creaOperacion == 1 ? true : false;

            return entity;
        }
    }
}
