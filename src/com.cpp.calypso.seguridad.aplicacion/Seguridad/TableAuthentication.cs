using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;
using System.Linq;

namespace com.cpp.calypso.seguridad.aplicacion
{
    /// <summary>
    /// Auttentificacion, con usuarios registrados en una base de datos. 
    /// </summary>
    public class TableAuthentication : IAuthentication
    {
        private readonly IBaseRepository<Usuario> repositoryUsuario;

        public TableAuthentication(IBaseRepository<Usuario> repositoryUsuario)
        {
            this.repositoryUsuario = repositoryUsuario;
        }

        public bool Authenticate(string username, string password)
        {
            Guard.AgainstNullOrEmptyString(username, "username");
            Guard.AgainstNullOrEmptyString(username, "password");

            var usuario = repositoryUsuario.GetAll()
                        .Where(u => u.Cuenta.ToUpper() == username.ToUpper())
                        .SingleOrDefault();

            //Validar usuario si existen
            if (usuario == null)
                return false;

            //2. Validar Clave si es valida
            var passwordHash = HashPassword(password);
            if (usuario.Password.Equals(passwordHash)) {

                return true;
            }
            
            return false;
        }

        private string HashPassword(string password)
        {
            //TODO: pendiente definir mecanismo de hash de clave

            return password;
        }

        public bool IsCompareStrictUserName()
        {
            return true;
        }
    }

   
}