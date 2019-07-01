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
    public class ConsumoTransporteAsyncBaseCrudAppService : AsyncBaseCrudAppService<ConsumoTransporte, ConsumoTransporteDto, PagedAndFilteredResultRequestDto>, IConsumoTransporteAsyncBaseCrudAppService
    {
        private readonly IBaseRepository<OperacionDiariaRuta> _operacionDiariaRutaRepository;

        public IBaseRepository<ConsumoTransporte> Repository { get; }

        public ConsumoTransporteAsyncBaseCrudAppService(
            IBaseRepository<ConsumoTransporte> repository,
            IBaseRepository<OperacionDiariaRuta> operacionDiariaRutaRepository
            ) : base(repository)
        {
            Repository = repository;
            _operacionDiariaRutaRepository = operacionDiariaRutaRepository;
        }

        public JArray Sync(int version, JArray registrosJson, List<int> usuarios)
        {
            var diccionario = Sincronizar(version, registrosJson, usuarios);
            var registros = GetRegistros(version, usuarios, diccionario);

            var json = GenerarRegistrosMovil(diccionario, registros);
            return json;

        }

        public Dictionary<int, int> Sincronizar(int version, JArray registrosJson, List<int> usuariosId)
        {
            //Aplicar los cambios que vienen del movil y generar el binding de Ids SqlServer vs SqlLite
            var lKeyBinding = SincronizacionLocal(registrosJson);

            return lKeyBinding;
        }

        public JArray GenerarRegistrosMovil(Dictionary<int, int> diccionario, List<ConsumoTransporte> registros)
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
                ConsumoTransporte instancia = JsonToObject(obj);

                Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                instancia.Version = unixTimestamp;

                var operacionDiaria = _operacionDiariaRutaRepository.GetAll()
                    .Where(o => o.Ref == instancia.OperacionDiariaRutaRef)
                    .FirstOrDefault()
                    ;

                if(operacionDiaria != null)
                    instancia.OperacionDiariaRutaId = operacionDiaria.Id;

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

        public List<ConsumoTransporte> GetRegistros(int version, List<int> usuarios, Dictionary<int, int> diccionario)
        {
            var solicitudesViandasEnMovil = diccionario.Keys.ToList();

            var registros = Repository.GetAll()
                    .Where(o => o.Version > version)
                    .Where(o => o.IsDeleted || o.IsDeleted == false)
                    .Where(o => solicitudesViandasEnMovil.Contains(o.Id))
                    .ToList()
                ;
            return registros;
        }

        public JObject ObjectToJson(ConsumoTransporte entidad)
        {
            JObject objJson = new JObject();

            objJson.Add("m_id", entidad.Id);
            if (entidad.Ref != null)
            {
                objJson.Add("m_ref", entidad.Ref);
            }
            objJson.Add("m_version", entidad.Version);
            objJson.Add("vigente", GetStringFromBool(entidad.IsDeleted == false));

            //objJson.Add("tipo", entidad.TipoContenido);
            objJson.Add("tipo_consumo", entidad.TipoConsumo);

            objJson.Add("operacion_diaria_ruta_id", entidad.OperacionDiariaRutaId);
            objJson.Add("operacion_diaria_ruta_ref", entidad.OperacionDiariaRutaRef);
            objJson.Add("fecha_embarque", GetStringFromDateTime(entidad.FechaEmbarque));
            objJson.Add("fecha_desembarque", GetStringFromDateTime(entidad.FechaDesembarque));
            objJson.Add("coordenada_x_embarque", entidad.CoordenadaXEmbarque);
            objJson.Add("coordenada_y_embarque", entidad.CoordenadaYEmbarque);
            objJson.Add("coordenada_x_desembarque", entidad.CoordenadaXDesembarque);
            objJson.Add("coordenada_y_desembarque", entidad.CoordenadaYDesembarque);
            objJson.Add("colaborador_id", entidad.ColaboradorId);
            objJson.Add("huella", entidad.Huella);

            return objJson;
        }

        public ConsumoTransporte JsonToObject(JObject json)
        {

            var entity = new ConsumoTransporte();
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


            //entity.TipoContenido = (string)json["tipo"];

            var operacionDiariaEsNull = json.GetValue("operacion_diaria_ruta_id").Type == JTokenType.Null;
            if (!operacionDiariaEsNull)
                entity.OperacionDiariaRutaId = (int)json["operacion_diaria_ruta_id"];

            entity.TipoConsumo = (string)json["tipo_consumo"];
            entity.OperacionDiariaRutaRef = (string)json["operacion_diaria_ruta_ref"];
            entity.FechaEmbarque = GetDateTimeFromString((string)json["fecha_embarque"]);

            var fechaDesembarqueEsNull = json.GetValue("fecha_desembarque").Type == JTokenType.Null;
            if (!fechaDesembarqueEsNull)
                entity.FechaDesembarque = GetDateTimeFromString((string)json["fecha_desembarque"]);

            var cordenadaXEmbarqueEsNull = json.GetValue("coordenada_x_embarque").Type == JTokenType.Null;
            if (!cordenadaXEmbarqueEsNull)
                entity.CoordenadaXEmbarque = (decimal)json["coordenada_x_embarque"];

            var coordenadaYEmbarqueEsNull = json.GetValue("coordenada_y_embarque").Type == JTokenType.Null;
            if (!coordenadaYEmbarqueEsNull)
                entity.CoordenadaYEmbarque = (decimal)json["coordenada_y_embarque"];

            var coordenadaXDesembarqueEsNull = json.GetValue("coordenada_x_desembarque").Type == JTokenType.Null;
            if (!coordenadaXDesembarqueEsNull)
                entity.CoordenadaXDesembarque = (decimal)json["coordenada_x_desembarque"];

            var coordenadaYDesembarqueEsNull = json.GetValue("coordenada_y_desembarque").Type == JTokenType.Null;
            if (!coordenadaYDesembarqueEsNull)
                entity.CoordenadaYDesembarque = (decimal)json["coordenada_y_desembarque"];

            var coolaboradorEsNull = json.GetValue("colaborador_id").Type == JTokenType.Null;
            if (!coolaboradorEsNull)
                entity.ColaboradorId = (int)json["colaborador_id"];


            var huellaEsNull = json.GetValue("huella").Type == JTokenType.Null;
            if (!huellaEsNull)
                entity.Huella = (string)json["huella"];



            return entity;
        }
    }
}
