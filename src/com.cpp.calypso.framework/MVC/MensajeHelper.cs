using System;
using System.Web.Mvc;

namespace com.cpp.calypso.framework
{

    /// <summary>
    /// Tipo de Mensaje, para visualizar al usuario
    /// </summary>
    public enum TipoMensaje
    {
        Alerta = 1,
        Informativo = 2,
        Error = 3,
        Correcto = 4
    }


    public class MensajeHelper
    {
        public MensajeHelper() { 
        
        }
       

        public TipoMensaje Tipo { get; set; }
        public string Texto { get; set; }
    }

    public static class TipoMensajeExtensions
    {

        public static string ShowAlert(this HtmlHelper htmlHelper, string tipoMensaje)
        {
            if (string.IsNullOrWhiteSpace(tipoMensaje))
                return string.Empty;

            TipoMensaje tipo;

            if (Enum.TryParse(tipoMensaje, out tipo))
            {
                return ShowAlert(htmlHelper, tipo);
            }
            else {
                return string.Empty;
            }

        }



        public static string ShowAlert(this HtmlHelper htmlHelper, TipoMensaje tipo)
        {
 
            string css = "alert-success";
            
            switch (tipo){
                case TipoMensaje.Alerta:
                    css =  "alert-warning";
                    break;
                case TipoMensaje.Correcto:
                    css = "alert-success";
                    break;
                case TipoMensaje.Error:
                    css = "alert-danger";
                    break;
                case TipoMensaje.Informativo:
                    css = "alert-info";
                    break;
            }

            return css;

        }
    }

}
