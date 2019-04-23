using com.cpp.calypso.framework;
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


        private readonly List<string> _orden = new List<string>();

        public SincronizacionController(
            IHandlerExcepciones manejadorExcepciones,
            ICatalogoAsyncBaseCrudAppService catalogoSync,
            ITipoCatalogoAsyncBaseCrudAppService tipoCatalogoSync
            ) : base(manejadorExcepciones)
        {
            _catalogoSync = catalogoSync;
            _tipoCatalogoSync = tipoCatalogoSync;
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