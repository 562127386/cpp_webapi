using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.proyecto.aplicacion.Dto;
using com.cpp.calypso.proyecto.aplicacion.Interfaces;
using com.cpp.calypso.proyecto.dominio.Entidades;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.cpp.calypso.proyecto.aplicacion.Service
{
    public class SolicitudViandaAsyncBaseCrudAppService :
        AsyncBaseCrudAppService<SolicitudVianda, SolicitudViandaDto, PagedAndFilteredResultRequestDto>,
        ISolicitudViandaAsyncBaseCrudAppService
    {
        public SolicitudViandaAsyncBaseCrudAppService(IBaseRepository<SolicitudVianda> repository) : base(repository)
        {
            Repository = repository;
        }

        public IBaseRepository<SolicitudVianda> Repository { get; }

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

        public JArray GenerarRegistrosMovil(Dictionary<int, int> diccionario, List<SolicitudVianda> registros)
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
                SolicitudVianda instancia = JsonToObject(obj);

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

        public List<SolicitudVianda> GetRegistros(int version, List<int> usuarios)
        {
            var registros = Repository.GetAll()
                .Where(o => o.Version > version)
                .ToList()
                ;
            return registros;
        }

        public JObject ObjectToJson(SolicitudVianda entidad)
        {
            JObject objJson = new JObject();

            objJson.Add("m_id", entidad.Id);
            if (entidad.Ref != null)
            {
                objJson.Add("m_ref", entidad.Ref);
            }
            objJson.Add("m_version", entidad.Version);
            objJson.Add("vigente", GetStringFromBool(entidad.IsDeleted == false));


            objJson.Add("solicitante_id", entidad.SolicitanteId);
            objJson.Add("locacion_id", entidad.LocacionId);
            objJson.Add("tipo_operacion_comida_id", entidad.TipoComidaId);
            objJson.Add("disciplina_id", entidad.DisciplinaId);
            objJson.Add("fecha_solicitud", GetStringFromDateTime(entidad.FechaSolicitud));
            objJson.Add("fecha_alcance", GetStringFromDateTime(entidad.FechaAlcancce));
            objJson.Add("pedido_viandas", entidad.PedidoViandas);
            objJson.Add("total_pedido", entidad.TotalPedido);
            objJson.Add("consumido", entidad.Consumido);
            objJson.Add("consumido_justificado", entidad.ConsumoJustificado);
            objJson.Add("total_consumido", entidad.TotalConsumido);
            objJson.Add("estado", (int)entidad.Estado);
            objJson.Add("solicitud_original_id", entidad.SolicitudOriginalId);
            objJson.Add("referencia_ubicacion", entidad.ReferenciaUbicacion);
            objJson.Add("area_id", entidad.AreaId);
            objJson.Add("observaciones", entidad.SolicitudOriginalId);




            return objJson;
        }

        public SolicitudVianda JsonToObject(JObject json)
        {

            var entity = new SolicitudVianda();
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

            entity.SolicitanteId = (int)json["solicitante_id"];
            entity.LocacionId = (int)json["locacion_id"];
            entity.TipoComidaId = (int)json["tipo_operacion_comida_id"];
            entity.DisciplinaId = (int)json["disciplina_id"];
            entity.FechaSolicitud = GetDateTimeFromString((string)json["fecha_solicitud"]);
            entity.FechaAlcancce = GetDateTimeFromString((string)json["fecha_alcance"]);
            entity.PedidoViandas = (int)json["pedido_viandas"];
            entity.TotalPedido = (int)json["total_pedido"];
            entity.Consumido = (int)json["consumido"];
            entity.ConsumoJustificado = (int)json["consumido_justificado"];
            entity.TotalConsumido = (int)json["total_consumido"];
            //Falta Por Justificar
            entity.Estado = (SolicitudViandaEstado)Enum.ToObject(typeof(SolicitudViandaEstado), (int)json["estado"]);
            entity.SolicitudOriginalId = (int)json["solicitud_original_id"];
            entity.ReferenciaUbicacion = (string)json["referencia_ubicacion"];
            entity.AreaId = (int)json["area_id"];
            entity.Observaciones = (string)json["observaciones"];
            entity.AnotadorId = (int)json["anotador_id"];
            entity.HoraEntregaRestaurante = GetDateTimeFromString((string)json["hora_entrega_restaurante"]);
            entity.HoraEntregaTransportista = GetDateTimeFromString((string)json["hora_entrega_transportista"]);
            //Fata Origen
            entity.AlcanceViandas = (int)json["alcance_viandas"];
            //Falta Total Recibido
            //Falta Hora Entrega Anotador





            return entity;
        }
    }
}
