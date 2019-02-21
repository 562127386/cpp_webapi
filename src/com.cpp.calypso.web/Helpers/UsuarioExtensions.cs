using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.comun.dominio;
using CommonServiceLocator;
using System.Web.Mvc;

namespace com.cpp.calypso.web
{
    public static class UsuarioExtensions
    {
       


        public static string GetIdentityUser(this Usuario usuario)
        {
            var CampoIdentidad = ManagerUser.CampoIdentidad;
            if (CampoIdentidad == CamposIdentidadUsuario.Id)
                return usuario.Id.ToString();

            if (CampoIdentidad == CamposIdentidadUsuario.UserName)
                return usuario.Cuenta;

            return null;
        }


        public static MvcHtmlString GetUserName(this HtmlHelper htmlHelper)
        {
                var application = ServiceLocator.Current.GetInstance<IApplication>();
                var usuario = application.GetCurrentUser();
                if (usuario != null)
                    return MvcHtmlString.Create(usuario.Nombres);

                return MvcHtmlString.Create(string.Empty);
        }


        
    }

    public enum CamposIdentidadUsuario
    {
        Id,
        UserName 
    }

    
}