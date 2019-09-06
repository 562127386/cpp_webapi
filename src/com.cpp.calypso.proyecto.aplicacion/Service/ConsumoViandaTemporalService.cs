using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.proyecto.aplicacion.Dto;
using com.cpp.calypso.proyecto.aplicacion.Interfaces;
using com.cpp.calypso.proyecto.dominio.Entidades;
using Newtonsoft.Json.Linq;

namespace com.cpp.calypso.proyecto.aplicacion.Service
{
    public class  ConsumoViandaTemporalAsyncBaseCrudAppService : AsyncBaseCrudAppService<ConsumoViandaTemporal, ConsumoViandaTemporalDto, PagedAndFilteredResultRequestDto>, IConsumoViandaTemporalAsyncBaseCrudAppService
    {
        private readonly IBaseRepository<SolicitudViandaTemporal> _solicitudViandaTemporalRepository;

        public ConsumoViandaTemporalAsyncBaseCrudAppService(
            IBaseRepository<ConsumoViandaTemporal> repository,
            IBaseRepository<SolicitudViandaTemporal> solicitudViandaTemporalRepository
            ) : base(repository)
        {
            _solicitudViandaTemporalRepository = solicitudViandaTemporalRepository;
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

        public JArray GenerarRegistrosMovil(Dictionary<int, int> diccionario, List<ConsumoViandaTemporal> registros)
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
                ConsumoViandaTemporal instancia = JsonToObject(obj);

                Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                instancia.Version = unixTimestamp;

                var solicitudTemporal = _solicitudViandaTemporalRepository
                    .GetAll()
                    .FirstOrDefault(o => o.Ref == instancia.SolicitudViandaTemporalRef)
                    ;

                if(solicitudTemporal != null)
                    instancia.SolicitudViandaTemporalId = solicitudTemporal.Id;

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

        public List<ConsumoViandaTemporal> GetRegistros(int version, List<int> usuarios, Dictionary<int, int> diccionario)
        {
            var solicitudesViandasEnMovil = diccionario.Keys.ToList();

            var registros = Repository.GetAll()
                    .Where(o => o.Version > version)
                    .Where(o => solicitudesViandasEnMovil.Contains(o.Id))
                    .ToList()
                ;
            return registros;
        }

        public JObject ObjectToJson(ConsumoViandaTemporal entidad)
        {
            JObject objJson = new JObject();

            objJson.Add("m_id", entidad.Id);

            objJson.Add("m_version", entidad.Version);
            objJson.Add("vigente", GetStringFromBool(entidad.IsDeleted == false));

            objJson.Add("solicitud_vianda_temporal_id", entidad.SolicitudViandaTemporalId);
            objJson.Add("fecha_consumo", GetStringFromDateTime(entidad.FechaConsumo));
            objJson.Add("colaborador_id", entidad.ColaboradorId);
            objJson.Add("origen_consumo_id", entidad.OrigenConsumoId == OrigenConsumo.Cedula ? 1
                : entidad.OrigenConsumoId == OrigenConsumo.Qr ? 2
                : 3);
            objJson.Add("solicitud_vianda_temporal_ref", entidad.SolicitudViandaTemporalRef);

            return objJson;
        }

        public ConsumoViandaTemporal JsonToObject(JObject json)
        {

            var entity = new ConsumoViandaTemporal();
            if (json.GetValue("m_id").Type == JTokenType.Null)
                json["m_id"] = 0;

            if (json.Property("m_id") != null && (int)json.Property("m_id") != 0)
            {
                int id = (int)json["m_id"];
                entity = Repository.Get(id);
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

            var solicitudViandaTemporalId = json.GetValue("solicitud_vianda_temporal_id").Type == JTokenType.Null;
            if (!solicitudViandaTemporalId)
                entity.SolicitudViandaTemporalId = (int)json["solicitud_vianda_temporal_id"];

            var fechaConsumo = json.GetValue("fecha_consumo").Type == JTokenType.Null;
            if (!fechaConsumo)
                entity.FechaConsumo = GetDateTimeFromString((string)json["fecha_consumo"]);

            entity.ColaboradorId = (int)json["colaborador_id"];

            int origenConsumoJson = (int)json["origen_consumo_id"];
            entity.OrigenConsumoId = origenConsumoJson == 1 ? OrigenConsumo.Cedula
                : origenConsumoJson == 2 ? OrigenConsumo.Qr
                : OrigenConsumo.Huella;
           
            entity.SolicitudViandaTemporalRef = (string)json["solicitud_vianda_temporal_ref"];


            return entity;
        }
    }
}
