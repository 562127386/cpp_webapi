using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace com.cpp.calypso.webapi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );



            ////Route normal
            //config.Routes.MapHttpRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Inicio", action = "Index", id = UrlParameter.Optional }
            //);

            // Route to index.html
            config.Routes.MapHttpRoute(
                name: "Default",
                routeTemplate: "{id}",
                defaults: new { id = "swagger/ui/index" });

            //Aplicar autentificacion a todos los controladores
            config.Filters.Add(new AuthorizeAttribute());

            //Mecanismo de Verificar Token, para autentificar
            config.MessageHandlers.Add(new TokenValidationHandler());


        }
    }
}
