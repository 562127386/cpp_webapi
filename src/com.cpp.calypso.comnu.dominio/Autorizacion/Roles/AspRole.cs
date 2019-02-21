using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace com.cpp.calypso.comun.dominio
{
    /// <summary>
    /// Implements 'IRole' of ASP.NET Identity Framework.
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public abstract class AspRole<TUser> : BaseRol, IRole<int>
         where TUser : AspUser<TUser>
    {

        [NotMapped]
        public string Name { get => Nombre; set => Nombre = value; }
    }
        

}
