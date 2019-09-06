using com.cpp.calypso.framework;
using com.cpp.calypso.proyecto.aplicacion.Interfaces;
using com.cpp.calypso.webapi.Api.Controllers.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace com.cpp.calypso.webapi.Api.Controllers
{
    [AllowAnonymous]
    public class SincronizacionController : BaseApiController
    {
        private readonly ICatalogoAsyncBaseCrudAppService _catalogoSync;
        private readonly ITipoCatalogoAsyncBaseCrudAppService _tipoCatalogoSync;
        private readonly IArchivoAsyncBaseCrudAppService _archivoSync;
        private readonly IOpcionComidaAsyncBaseCrudAppService _opcionComidaSync;
        private readonly IEmpresaAsyncBaseCrudAppService _empresaSync;
        private readonly IProveedorAsyncBaseCrudAppService _proveedorSync;
        private readonly IContratoProveedorAsyncBaseCrudAppService _contratoProveedorSync;
        private readonly ITipoOpcionComidaAsyncBaseCrudAppService _tipoOpcionComidaSync;
        private readonly IColaboradorAsyncBaseCrudAppService _colaboradorSync;
        private readonly IMenuProveedorAsyncBaseCrudAppService _menuProveedorSync;
        private readonly ITipoAccionEmpresaAsyncBaseCrudAppService _tipoAccionEmpresaSync;
        private readonly ISolicitudViandaAsyncBaseCrudAppService _solicitudViandaSync;
        private readonly IDistribucionViandaAsyncBaseCrudAppService _distribucionViandaSync;
        private readonly IDetalleDistribucionAsyncBaseCrudAppService _detalleDistribucionSync;
        private readonly IServicioProveedorAsyncBaseCrudAppService _servicioProveedorSync;
        private readonly IServicioRolAsyncBaseCrudAppService _servicioRolSync;
        private readonly IUsuarioMovilAsyncBaseCrudAppService _usuarioMovilSync;
        private readonly IConsumoAsyncBaseCrudAppService _consumoSync;
        private readonly IConsumoQrAsyncBaseCrudAppService _consumoQrSync;
        private readonly IConsumoViandaAsyncBaseCrudAppService _consumoViandaSync;
        private readonly IConsumoViandaQrAsyncBaseCrudAppService _consumoViandaQrSync;
        private readonly IAccesoTemporalAsyncBaseCrudAppService _accesoTemporalSync;
        private readonly IRegistroAccesoAsyncBaseCrudAppService _registroAccesoSync;
        private readonly IRequisitoColaboradorAsyncBaseCrudAppService _requisitoColaboradorSync;
        private readonly IChoferAsyncBaseCrudAppService _choferSync;
        private readonly IVehiculoAsyncBaseCrudAppService _vehiculoSync;
        private readonly IRutaAsyncBaseCrudAppService _rutasSync;
        private readonly IRutaHorarioAsyncBaseCrudAppService _rutaHorarioSync;
        private readonly IRutaHorarioVehiculoAsyncBaseCrudAppService _rutaHorarioVehiculoSync;
        private readonly ILugarAsyncBaseCrudAppService _lugarSync;
        private readonly IConsumoTransporteAsyncBaseCrudAppService _consumoTransporteSync;
        private readonly IOperacionDiariaAsyncBaseCrudAppService _operacionDiariaSync;
        private readonly IOperacionDiariaRutaAsyncBaseCrudAppService _operacionDiariaRutaSync;
        private readonly IHabitacionAsyncBaseCrudAppService _habitacionSync;
        private readonly IEspacioHabitacionAsyncBaseCrudAppService _espacioHabitacionSync;
        private readonly IReservaHotelAsyncBaseCrudAppService _reservaHotelSync;
        private readonly IReservaHotelQrAsyncBaseCrudAppService _reservaHotelQrSync;
        private readonly IDetalleReservaAsyncBaseCrudAppService _detalleReservaSync;
        private readonly IDetalleReservaQrAsyncBaseCrudAppService _detalleReservaQrSync;
        private readonly IConsumoSinReservaHospedajeAsyncBaseCrudAppService _consumoSinReservaHospedajeSync;
        private readonly ISincronizacionLogAsyncBaseCrudAppService _sincronizacionLogService;
        private readonly ISolicitudViandaTemporalAsyncBaseCrudAppService _solicitudViandaTemporalSync;
        private readonly IConsumoViandaTemporalAsyncBaseCrudAppService _consumoViandaTemporalSync;
        private readonly IConsumoViandaQrTemporalAsyncBaseCrudAppService _consumoViandaQrTemporalSync;
        private readonly List<string> _orden = new List<string>();

        public SincronizacionController(
            IHandlerExcepciones manejadorExcepciones,
            ICatalogoAsyncBaseCrudAppService catalogoSync,
            ITipoCatalogoAsyncBaseCrudAppService tipoCatalogoSync,
            IArchivoAsyncBaseCrudAppService archivoSync,
            IOpcionComidaAsyncBaseCrudAppService opcionComidaSync,
            IEmpresaAsyncBaseCrudAppService empresaSync,
            IProveedorAsyncBaseCrudAppService proveedorSync,
            IContratoProveedorAsyncBaseCrudAppService contratoProveedorSync,
            ITipoOpcionComidaAsyncBaseCrudAppService tipoOpcionComidaSync,
            IColaboradorAsyncBaseCrudAppService colaboradorSync,
            IMenuProveedorAsyncBaseCrudAppService menuProveedorSync,
            ITipoAccionEmpresaAsyncBaseCrudAppService tipoAccionEmpresaSync,
            ISolicitudViandaAsyncBaseCrudAppService solicitudViandaSync,
            IDistribucionViandaAsyncBaseCrudAppService distribucionViandaSync,
            IDetalleDistribucionAsyncBaseCrudAppService detalleDistribucionSync,
            IServicioProveedorAsyncBaseCrudAppService servicioProveedorSync,
            IServicioRolAsyncBaseCrudAppService servicioRolSync,
            IUsuarioMovilAsyncBaseCrudAppService usuarioMovilSync,
            IConsumoAsyncBaseCrudAppService consumoSync,
            IConsumoQrAsyncBaseCrudAppService consumoQrSync,
            IConsumoViandaAsyncBaseCrudAppService consumoViandaSync,
            IConsumoViandaQrAsyncBaseCrudAppService consumoViandaQrSync,
            IAccesoTemporalAsyncBaseCrudAppService accesoTemporalSync,
            IRegistroAccesoAsyncBaseCrudAppService registroAccesoSync,
            IRequisitoColaboradorAsyncBaseCrudAppService requisitoColaboradorSync,
            IChoferAsyncBaseCrudAppService choferSync,
            IVehiculoAsyncBaseCrudAppService vehiculoSync,
            IRutaAsyncBaseCrudAppService rutasSync,
            IRutaHorarioAsyncBaseCrudAppService rutaHorarioSync,
            IRutaHorarioVehiculoAsyncBaseCrudAppService rutaHorarioVehiculoSync,
            ILugarAsyncBaseCrudAppService lugarSync,
            IConsumoTransporteAsyncBaseCrudAppService consumoTransporteSync,
            IOperacionDiariaAsyncBaseCrudAppService operacionDiariaSync,
            IOperacionDiariaRutaAsyncBaseCrudAppService operacionDiariaRutaSync,
            IHabitacionAsyncBaseCrudAppService habitacionSync,
            IEspacioHabitacionAsyncBaseCrudAppService espacioHabitacionSync,
            IReservaHotelAsyncBaseCrudAppService reservaHotelSync,
            IReservaHotelQrAsyncBaseCrudAppService reservaHotelQrSync,
            IDetalleReservaAsyncBaseCrudAppService detalleReservaSync,
            IDetalleReservaQrAsyncBaseCrudAppService detalleReservaQrSync,
            IConsumoSinReservaHospedajeAsyncBaseCrudAppService consumoSinReservaHospedajeSync,
            ISincronizacionLogAsyncBaseCrudAppService sincronizacionLogService,
            ISolicitudViandaTemporalAsyncBaseCrudAppService solicitudViandaTemporalSync,
            IConsumoViandaTemporalAsyncBaseCrudAppService consumoViandaTemporalSync,
            IConsumoViandaQrTemporalAsyncBaseCrudAppService consumoViandaQrTemporalSync
            ) : base(manejadorExcepciones)
        {
            _catalogoSync = catalogoSync;
            _tipoCatalogoSync = tipoCatalogoSync;
            _archivoSync = archivoSync;
            _opcionComidaSync = opcionComidaSync;
            _empresaSync = empresaSync;
            _proveedorSync = proveedorSync;
            _contratoProveedorSync = contratoProveedorSync;
            _tipoOpcionComidaSync = tipoOpcionComidaSync;
            _colaboradorSync = colaboradorSync;
            _menuProveedorSync = menuProveedorSync;
            _tipoAccionEmpresaSync = tipoAccionEmpresaSync;
            _solicitudViandaSync = solicitudViandaSync;
            _distribucionViandaSync = distribucionViandaSync;
            _detalleDistribucionSync = detalleDistribucionSync;
            _servicioProveedorSync = servicioProveedorSync;
            _servicioRolSync = servicioRolSync;
            _usuarioMovilSync = usuarioMovilSync;
            _consumoSync = consumoSync;
            _consumoQrSync = consumoQrSync;
            _consumoViandaSync = consumoViandaSync;
            _consumoViandaQrSync = consumoViandaQrSync;
            _accesoTemporalSync = accesoTemporalSync;
            _registroAccesoSync = registroAccesoSync;
            _requisitoColaboradorSync = requisitoColaboradorSync;
            _choferSync = choferSync;
            _vehiculoSync = vehiculoSync;
            _rutasSync = rutasSync;
            _rutaHorarioSync = rutaHorarioSync;
            _rutaHorarioVehiculoSync = rutaHorarioVehiculoSync;
            _lugarSync = lugarSync;
            _consumoTransporteSync = consumoTransporteSync;
            _operacionDiariaSync = operacionDiariaSync;
            _operacionDiariaRutaSync = operacionDiariaRutaSync;
            _habitacionSync = habitacionSync;
            _espacioHabitacionSync = espacioHabitacionSync;
            _reservaHotelSync = reservaHotelSync;
            _reservaHotelQrSync = reservaHotelQrSync;
            _detalleReservaSync = detalleReservaSync;
            _detalleReservaQrSync = detalleReservaQrSync;
            _consumoSinReservaHospedajeSync = consumoSinReservaHospedajeSync;
            _sincronizacionLogService = sincronizacionLogService;
            _solicitudViandaTemporalSync = solicitudViandaTemporalSync;
            _consumoViandaTemporalSync = consumoViandaTemporalSync;
            _consumoViandaQrTemporalSync = consumoViandaQrTemporalSync;
            _orden.Add("catalogos");
        }



        /**
         * Se recibe un objeto JSON con dos componentes, las versiones de tablas y los usuarios involucrados.
         * {
         *   vesiones: [
         *                {
         *                   nombre: "nombre_tabla", //nombre de la tabla
         *                   version: 00000,
         *                   cambios: [
         *                      { m_id:0, codigo: '', ..., lkey: 1 }
         *                   ]
         *                } , {
         *                  ... otra tabla
         *                }, 
         *                ... mas tablas
         *              ],
         *   usuarios: [ 1000, 2000, 20001 ] //Ids de usuarios, estos son IDs SqlServer
         * }
         **/
        [System.Web.Mvc.HttpPost]
        public string Index(SincronizacionModel req)
        {
            JArray respuesta = new JArray();

            try
            {
                var versionesBase64Bytes = Convert.FromBase64String(req.Versiones);
                var versiones = JArray.Parse(System.Text.Encoding.UTF8.GetString(versionesBase64Bytes));

                var usuariosBase64Bytes = Convert.FromBase64String(req.Usuarios);
                var usuarios = JArray.Parse(System.Text.Encoding.UTF8.GetString(usuariosBase64Bytes));
                List<int> usuariosIds = usuarios.ToObject<List<int>>();


                foreach (JObject tablaJson in versiones)
                {
                    string nombre = (string)tablaJson["nombre"];

                    int version = (int)tablaJson["version"];
                    JArray cambios = (JArray)tablaJson["cambios"];
                    JObject tablaResult = new JObject();

                    if (nombre == "tipos_catalogos")
                    {
                        var servicio = _tipoCatalogoSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "catalogos")
                    {
                        var servicio = _catalogoSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "archivos")
                    {
                        var servicio = _archivoSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "opciones_comidas")
                    {
                        var servicio = _opcionComidaSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "empresas")
                    {
                        var servicio = _empresaSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "proveedores")
                    {
                        var servicio = _proveedorSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "contratos_proveedores")
                    {
                        var servicio = _contratoProveedorSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "tipos_opciones_comidas")
                    {
                        var servicio = _tipoOpcionComidaSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "colaboradores")
                    {
                        var servicio = _colaboradorSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "menus_proveedor")
                    {
                        var servicio = _menuProveedorSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "tipos_acciones_empresas")
                    {
                        var servicio = _tipoAccionEmpresaSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "solicitudes_viandas")
                    {
                        var servicio = _solicitudViandaSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "distribuciones_viandas")
                    {
                        var servicio = _distribucionViandaSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "detalles_distribuciones")
                    {
                        var servicio = _detalleDistribucionSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "servicios_proveedor")
                    {
                        var servicio = _servicioProveedorSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "servicios_roles")
                    {
                        var servicio = _servicioRolSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "usuarios")
                    {
                        var servicio = _usuarioMovilSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "consumos")
                    {
                        var servicio = _consumoSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "consumos_qr")
                    {
                        var servicio = _consumoQrSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "consumos_viandas")
                    {
                        var servicio = _consumoViandaSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "consumos_viandas_qr")
                    {
                        var servicio = _consumoViandaQrSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "requisitos_colaboradores")
                    {
                        var servicio = _requisitoColaboradorSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "registros_accesos")
                    {
                        var servicio = _registroAccesoSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "accesos_temporal")
                    {
                        var servicio = _accesoTemporalSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "lugares")
                    {
                        var servicio = _lugarSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "choferes")
                    {
                        var servicio = _choferSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "vehiculos")
                    {
                        var servicio = _vehiculoSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "rutas")
                    {
                        var servicio = _rutasSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "rutas_horarios")
                    {
                        var servicio = _rutaHorarioSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "rutas_horarios_vehiculos")
                    {
                        var servicio = _rutaHorarioVehiculoSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "operaciones_diarias")
                    {
                        var servicio = _operacionDiariaSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "operaciones_diarias_rutas")
                    {
                        var servicio = _operacionDiariaRutaSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "consumos_transporte")
                    {
                        var servicio = _consumoTransporteSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "habitaciones")
                    {
                        var servicio = _habitacionSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "espacios_habitaciones")
                    {
                        var servicio = _espacioHabitacionSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "reservas_hoteles")
                    {
                        var servicio = _reservaHotelSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "reservas_hoteles_qr")
                    {
                        var servicio = _reservaHotelQrSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "detalles_reservas")
                    {
                        var servicio = _detalleReservaSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "detalles_reservas_qr")
                    {
                        var servicio = _detalleReservaQrSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "consumos_sin_reserva_hospedaje")
                    {
                        var servicio = _consumoSinReservaHospedajeSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    
                    if (nombre == "solicitudes_viandas_temporal")
                    {
                        var servicio = _solicitudViandaTemporalSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "consumos_viandas_temporal")
                    {
                        var servicio = _consumoViandaTemporalSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }

                    if (nombre == "consumos_viandas_qr_temporal")
                    {
                        var servicio = _consumoViandaQrTemporalSync;
                        var result = servicio.Sync(version, cambios, usuariosIds);
                        tablaResult.Add("nombre", nombre);
                        tablaResult.Add("registros", result);
                        respuesta.Add(tablaResult);
                    }


                }
            }
            catch (Exception exception)
            {

                //_sincronizacionLogService.CreateLog(exception.ToString());
                var message = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(exception.Message),
                    ReasonPhrase = "Internal server error"
                };
                throw new HttpResponseException(message);
            }

            var base64Encode = System.Text.Encoding.UTF8.GetBytes(respuesta.ToString());
            var base64String = Convert.ToBase64String(base64Encode);
            var base64EncodedBytes = System.Convert.FromBase64String(base64String);
            var original = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            return base64String;
        }

    }
}