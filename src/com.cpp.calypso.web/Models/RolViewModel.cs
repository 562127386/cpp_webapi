using System;
using com.cpp.calypso.comun.dominio;

namespace com.cpp.calypso.web
{

    public class RolViewModel : IViewModel, IEquatable<RolViewModel>
    {

        public string CodigoRol { get; set; }

        public string NombreRol { get; set; }

        public override int GetHashCode()
        {
            return CodigoRol.GetHashCode();
        }

        public bool Equals(RolViewModel other)
        {
            if (other == null) return false;
            return (this.CodigoRol.Equals(other.CodigoRol));

        }
    }
}
