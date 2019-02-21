using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace com.cpp.calypso.seguridad.aplicacion
{
    public interface IUsuarioService  : 
        IAsyncBaseCrudAppService<Usuario, UsuarioDto, PagedAndFilteredResultRequestDto, CrearUsuarioDto>
    {
      
        Task<UsuarioDto> Get(string cuenta);

       
        /// <summary>
        /// Recuperar clave.
        /// </summary>
        /// <param name="correoElectronicoCuenta"></param>
        /// <returns></returns>
        Task<LoginResult<Usuario>> RecoverPasswordAsync(string correoElectronicoCuenta);


        Task<LoginResult<Usuario>> ResetPassword(int id);


        Task<IdentityResult> ChangePassword(string password, string newPassword);


        /// <summary>
        /// Actualizar mi informacion. (Usuario autentificado)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<UsuarioDto> Update(MyUsuarioDto input);
        
 

    }
}