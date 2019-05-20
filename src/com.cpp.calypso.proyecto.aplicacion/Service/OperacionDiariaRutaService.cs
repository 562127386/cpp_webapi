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
    public class OperacionDiariaRutaAsyncBaseCrudAppService : AsyncBaseCrudAppService<OperacionDiariaRuta, OperacionDiariaRutaDto, PagedAndFilteredResultRequestDto>, IOperacionDiariaRutaAsyncBaseCrudAppService
    {
        private readonly IBaseRepository<RutaHorarioVehiculo> _rutaHorarioVehiculoRepository;
        private readonly IBaseRepository<OperacionDiaria> _operacionDiariaRepository;

        public IBaseRepository<OperacionDiariaRuta> Repository { get; }

        public OperacionDiariaRutaAsyncBaseCrudAppService(
            IBaseRepository<OperacionDiariaRuta> repository,
            IBaseRepository<RutaHorarioVehiculo> rutaHorarioVehiculoRepository,
            IBaseRepository<OperacionDiaria> operacionDiariaRepository
            ) : base(repository)
        {
            Repository = repository;
            _rutaHorarioVehiculoRepository = rutaHorarioVehiculoRepository;
            _operacionDiariaRepository = operacionDiariaRepository;
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

        public JArray GenerarRegistrosMovil(Dictionary<int, int> diccionario, List<OperacionDiariaRuta> registros)
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
                OperacionDiariaRuta instancia = JsonToObject(obj);

                Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                instancia.Version = unixTimestamp;

                var operacionDiaria = _operacionDiariaRepository.GetAll()
                    .Where(o => o.Ref == instancia.OperacionDiariaRef)
                    .FirstOrDefault();
                    ;

                if(operacionDiaria != null)
                    instancia.OperacionDiariaId = operacionDiaria.Id;

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

        public List<OperacionDiariaRuta> GetRegistros(int version, List<int> usuarios)
        {
            var registros = Repository.GetAll()
                .Where(o => o.Version > version)
                .ToList()
                ;
            return registros;
        }

        public JObject ObjectToJson(OperacionDiariaRuta entidad)
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
            objJson.Add("ruta_horario_vehiculo_id", entidad.RutaHorarioVehiculoId);
            objJson.Add("operacion_diaria_id", entidad.OperacionDiariaId);
            objJson.Add("ruta_horario_vehiculo_ref", entidad.RutaHorarioVehiculoRef);
            objJson.Add("operacion_diaria_ref", entidad.OperacionDiariaRef);
            objJson.Add("fecha_inicio", GetStringFromDateTime(entidad.FechaInicio));
            objJson.Add("fecha_fin", GetStringFromDateTime(entidad.FechaFin));


            return objJson;
        }

        public OperacionDiariaRuta JsonToObject(JObject json)
        {

            var entity = new OperacionDiariaRuta();
            if(json.GetValue("m_id").Type == JTokenType.Null)
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

            var rutaHorarioVehiculoEsNull = json.GetValue("ruta_horario_vehiculo_id").Type == JTokenType.Null;
            if (!rutaHorarioVehiculoEsNull)
                entity.RutaHorarioVehiculoId = (int)json["ruta_horario_vehiculo_id"];

            var operacionDiariaEsNull = json.GetValue("operacion_diaria_id").Type == JTokenType.Null;
            if (!operacionDiariaEsNull)
                entity.OperacionDiariaId = (int)json["operacion_diaria_id"];

            var rutaHorarioVehiculoIdEsNull = json.GetValue("ruta_horario_vehiculo_ref").Type == JTokenType.Null;
            if (!rutaHorarioVehiculoIdEsNull)
                entity.RutaHorarioVehiculoRef = (string)json["ruta_horario_vehiculo_ref"];


            entity.OperacionDiariaRef = (string)json["operacion_diaria_ref"];

            var fechaInicioEsNull = json.GetValue("fecha_inicio").Type == JTokenType.Null;
            if (!fechaInicioEsNull)
                entity.FechaInicio = GetDateTimeFromString((string)json["fecha_inicio"]);

            var fechaFinEsNull = json.GetValue("fecha_fin").Type == JTokenType.Null;
            if (!fechaFinEsNull)
                entity.FechaFin = GetDateTimeFromString((string)json["fecha_fin"]);


            return entity;
        }
    }
}
