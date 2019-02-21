using Abp;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace com.cpp.calypso.comun.dominio
{
    /// <summary>
    /// Implements 'TUser' of ASP.NET Identity Framework.
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public abstract class AspUser<TUsuario> : BaseUsuario, IUser<int>
        where TUsuario : BaseUsuario
    {
        protected AspUser()
        {
            SecurityStamp = SequentialGuidGenerator.Instance.Create().ToString();
        }

        

        [NotMapped]
        public string UserName
        {
            get => Cuenta;
            set => Cuenta = value;
        }

        

        /// <summary>
        /// Valor aleatorio que debe cambiar cada vez que se modifican las credenciales 
        /// de un usuario (cambio de contraseña, eliminación de inicio de sesión).
        /// </summary>
        [LongitudMayor(128)]
        public string SecurityStamp { get;  set; }
    }
}
