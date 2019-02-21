using System.Dynamic;

namespace com.cpp.calypso.seguridad.aplicacion
{
    /// <summary>
    /// 
    /// </summary>
    public class NotificacionUsuarioCreado : DynamicObject
    {
        public string PasswordRestCode { get;   set; }
        public string Nombres { get;   set; }
        public string Usuario { get;   set; }
        public string Enlace { get; internal set; }
        public string Password { get; internal set; }
    }
}