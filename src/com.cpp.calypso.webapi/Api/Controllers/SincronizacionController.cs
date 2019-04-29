﻿using com.cpp.calypso.framework;
using com.cpp.calypso.proyecto.aplicacion.Interfaces;
using com.cpp.calypso.webapi.Api.Controllers.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
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
            IDetalleDistribucionAsyncBaseCrudAppService detalleDistribucionSync
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
                }
            }
            catch (Exception exception)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var base64Encode = System.Text.Encoding.UTF8.GetBytes(respuesta.ToString());
            return Convert.ToBase64String(base64Encode);
        }
    }
}