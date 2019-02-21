using com.cpp.calypso.comun.dominio;
using System.Configuration;

namespace com.cpp.calypso.web
{
    public  class ManagerUser
    {
        //<!--Nombre del campo para mantener la identidad del usuario. Valores posibles. ID, UserName-->
        private const string CLAVE_CONFIGURACION_CAMPO_IDENTIDAD = "Seguridad.Usuario.Identidad.Campo";

        private static CamposIdentidadUsuario? _CampoIdentidad = null;

        public static CamposIdentidadUsuario CampoIdentidad
        {
            get
            {

                if (!_CampoIdentidad.HasValue)
                {
                    var aux = ConfigurationManager.AppSettings[ManagerUser.CLAVE_CONFIGURACION_CAMPO_IDENTIDAD];
                    if (string.IsNullOrWhiteSpace(aux))
                    {
                        string error = string.Format("No se puede iniciar el sistema, no existe la entrada [0] en la configuración correspondiente al campo utilizado en la identidiad. Ejemplo web.config", ManagerUser.CLAVE_CONFIGURACION_CAMPO_IDENTIDAD);
                        throw new GenericException(error, error);
                    }

                    if (aux.ToUpper() == CamposIdentidadUsuario.Id.ToString().ToUpper())
                    {
                        _CampoIdentidad = CamposIdentidadUsuario.Id;

                    }
                    else if (aux.ToUpper() == CamposIdentidadUsuario.UserName.ToString().ToUpper())
                    {
                        _CampoIdentidad = CamposIdentidadUsuario.UserName;

                    }
                    else
                    {
                        string error = string.Format("No se puede iniciar el sistema, la entrada de configuracion correspondiente al campo de identididad tiene un valor diferente a 'Id' o 'UserName', su valor es [0]", aux);
                        throw new GenericException(error, error);
                    }
                }
                return _CampoIdentidad.Value;
            }
        }
    }
}