using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.cpp.calypso.web
{
    
    /// <summary>
    /// Listado de Funcionalidades que se auditan... 
    /// 
    /// Funcionalidades / Accciones
    /// </summary>
    public static class FUNCIONALIDADES_AUDITORIA {

        public static List<IFUNCIONALIDAD_GESTION_USUARIOS> Funcionalidades = new List<IFUNCIONALIDAD_GESTION_USUARIOS>();

        static FUNCIONALIDADES_AUDITORIA() {
            Funcionalidades.Add(new FUNCIONALIDAD_GESTION_USUARIOS());

            //Funcionalidades.Add(new OTRO());
            
        
        }
    }

    public  class FUNCIONALIDAD_GESTION_USUARIOS : IFUNCIONALIDAD_GESTION_USUARIOS
    {

         public const string Nombre =  @"Seguridad\Gestion Usuarios";

         private List<string> _Acciones = new List<string>();
         public List<string> Acciones
         {
             get { return _Acciones; }
             set { _Acciones = value;  } 
         }


         public const string ACCION_LOGIN = "Login";
         public const string ACCION_LOGOUT = "Logout";


         public  FUNCIONALIDAD_GESTION_USUARIOS()
         {
             Acciones.Add(ACCION_LOGIN);
             Acciones.Add(ACCION_LOGOUT);
        }

         public override string ToString()
         {
             return Nombre;
         }

    }
     
     
}