using System;
using System.ComponentModel;

namespace com.cpp.calypso.comun.dominio
{
    [Serializable]
    public class UsuarioCriteria: ICriteria
    {
        [DisplayName("Identificación: ")]
        public string Identificacion { get; set; }

        [DisplayName("Cuenta: ")]
        public string Cuenta { get; set; }
    }
}
