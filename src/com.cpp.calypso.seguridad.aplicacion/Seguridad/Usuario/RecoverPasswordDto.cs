using System;
using System.Dynamic;

namespace com.cpp.calypso.seguridad.aplicacion
{
    /// <summary>
    /// 
    /// </summary>
    public class RecoverPasswordDto : DynamicObject
    {
        public string PasswordRestCode { get;   set; }
        public string Nombres { get;   set; }
        public string Usuario { get;   set; }
        public string Enlace { get; internal set; }
    }


    public class ChangePasswordDto : DynamicObject
    {
    
        public string Nombres { get; set; }
        public string Usuario { get; set; }
        public DateTime Fecha { get; internal set; }
    }
}