using com.cpp.calypso.comun.dominio;
using CommonServiceLocator;
using System.Web.Mvc;

namespace com.cpp.calypso.web
{
    public static class SecurityExtensions
    {

         /// <summary>
         /// Verificar si existe una autentificacion activa 
         /// </summary>
         /// <param name="htmlHelper"></param>
         /// <returns></returns>
         public static bool IsAuthenticated(this HtmlHelper htmlHelper)
         {
             var app = ServiceLocator.Current.GetInstance<IApplication>();

             return app.IsAuthenticated();
         }
    }
}
