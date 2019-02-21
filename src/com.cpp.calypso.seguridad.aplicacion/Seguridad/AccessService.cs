//using com.cpp.calypso.comun.dominio;
//using System.Threading.Tasks;

//namespace com.cpp.calypso.seguridad.aplicacion
//{
//    public class AccessService : IAccessService<Usuario>
//    {
//        private readonly LoginManager<Usuario, Modulo, Rol> LoginManager;

//        public AccessService(LoginManager<Usuario, Modulo, Rol> loginManager)
//        {
//            this.LoginManager = loginManager;
//        }

//        public async Task<LoginResult<Usuario>> AccessAsync(UsuarioDto usuario, ModuloDto modulo)
//        {
//            return await LoginManager.AccessAsync(usuario.Id, modulo.Id);
//        }

//        public async Task<LoginResult<Usuario>> AccessAsync(Usuario usuario, Modulo modulo)
//        {
//            return await LoginManager.AccessAsync(usuario, modulo);
//        }
//    }
//}
