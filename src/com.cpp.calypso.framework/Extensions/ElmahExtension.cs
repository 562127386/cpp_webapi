using Elmah;
using System;
using System.Web;

namespace com.cpp.calypso.framework
{


    /// <summary>
    /// Wrapper Elmah para asp.net, wcf
    /// </summary>
    public static class ElmahExtension
    {
        public static void LogToElmah(Exception ex)
        {
            if (HttpContext.Current != null && HttpContext.Current.ApplicationInstance != null)
            {
                //TODO: Existe un problema a pesar que HttpContext.Current, el HttpContext.Current.ApplicationInstance  es nulo
                //HttpContext.Current.ApplicationInstance 
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            else
            {
                //TODO: JSA, REVISAR COMO APLICAR ELMAH EN CONSOLA, OPCION 1 NO REGISTRA SEGUN EL CORREO PERMITE ENVIAR CORREOS, LA OPCION 2 REGISTRA EN LA BASE DE DATOS
                //INVESTIGAR 
                //http://stackoverflow.com/questions/841451/using-elmah-in-a-console-application

                //OPCION 1
                //if (httpApplication == null) InitNoContext();
                //ErrorSignal.Get(httpApplication).Raise(ex);

                //OPCION 2
                ErrorLog errorLog = ErrorLog.GetDefault(null);
                //TODO: Configurar el nombre de la aplicacion
                errorLog.ApplicationName = "Syllabus"; // ErrorHandling.Application;
                errorLog.Log(new Elmah.Error(ex));
            }
        }

         
    }
}
