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
    public class DetalleReservaQrAsyncBaseCrudAppService : AsyncBaseCrudAppService<DetalleReservaQr, DetalleReservaQrDto, PagedAndFilteredResultRequestDto>, IDetalleReservaQrAsyncBaseCrudAppService
    {
        private readonly IBaseRepository<ReservaHotelQr> _reservaHotelQrRepository;

        public DetalleReservaQrAsyncBaseCrudAppService(
            IBaseRepository<DetalleReservaQr> repository,
            IBaseRepository<ReservaHotelQr> reservaHotelQrRepository
            ) : base(repository)
        {
            Repository = repository;
            _reservaHotelQrRepository = reservaHotelQrRepository;
        }

        public IBaseRepository<DetalleReservaQr> Repository { get; }

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

        public JArray GenerarRegistrosMovil(Dictionary<int, int> diccionario, List<DetalleReservaQr> registros)
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
                DetalleReservaQr instancia = JsonToObject(obj);

                Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

                instancia.Version = unixTimestamp;

                var reservaQr = _reservaHotelQrRepository.GetAll()
                    .Where(o => o.Ref == instancia.ReservaHotelQrRef)
                    .FirstOrDefault();

                if (reservaQr != null)
                    instancia.ReservaHotelQrId = reservaQr.Id;

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

        public List<DetalleReservaQr> GetRegistros(int version, List<int> usuarios)
        {
            var registros = Repository.GetAll()
                .Where(o => o.Version > version)
                .ToList()
                ;
            return registros;
        }

        public JObject ObjectToJson(DetalleReservaQr entidad)
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
            objJson.Add("reserva_hotel_qr_id", entidad.ReservaHotelQrId);
            objJson.Add("reserva_hotel_qr_ref", entidad.ReservaHotelQrRef);
            objJson.Add("origen_consumo_id", entidad.OrigenConsumoId);
            objJson.Add("fecha_reserva", GetStringFromDate(entidad.FechaReserva));
            objJson.Add("fecha_consumo", GetStringFromDate(entidad.FechaConsumo));
            objJson.Add("consumido", entidad.Consumido ? 1 : 0);
            objJson.Add("facturado", entidad.Facturado ? 1 : 0);
            objJson.Add("liquidacion_detalle_id", entidad.LiquidacionDetalleId);


            return objJson;
        }

        public DetalleReservaQr JsonToObject(JObject json)
        {
            var entity = new DetalleReservaQr();
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
            if (json.GetValue("reserva_hotel_qr_id").Type != JTokenType.Null)
                entity.ReservaHotelQrId = (int)json["reserva_hotel_qr_id"];
            entity.ReservaHotelQrRef = (string)json["reserva_hotel_qr_ref"];

            entity.OrigenConsumoId = (int)json["origen_consumo_id"];
            entity.FechaReserva = GetDateFromString((string)json["fecha_reserva"]);

            if (json.GetValue("fecha_consumo").Type != JTokenType.Null)
                entity.FechaConsumo = GetDateFromString((string)json["fecha_consumo"]);

            entity.Consumido = (int)json["consumido"] == 1 ? true : false;
            entity.Facturado = (int)json["facturado"] == 1 ? true : false;
            entity.LiquidacionDetalleId = (int)json["liquidacion_detalle_id"];


            return entity;
        }
    }
}
