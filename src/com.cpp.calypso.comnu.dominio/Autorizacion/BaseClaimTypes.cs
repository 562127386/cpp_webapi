using System.Security.Claims;

namespace com.cpp.calypso.comun.dominio
{
    /// <summary>
    /// Claim type names.
    /// </summary>
    public static class BaseClaimTypes
    {
        /// <summary>
        /// UserId.
        /// Default: <see cref="ClaimTypes.Name"/>
        /// </summary>
        public static string UserName { get; set; } = ClaimTypes.Name;

        /// <summary>
        /// UserId.
        /// Default: <see cref="ClaimTypes.NameIdentifier"/>
        /// </summary>
        public static string UserId { get; set; } = ClaimTypes.NameIdentifier;

        /// <summary>
        /// UserId.
        /// Default: <see cref="ClaimTypes.Role"/>
        /// </summary>
        public static string Role { get; set; } = ClaimTypes.Role;

        /// <summary>
        /// ModuloId.
        /// Claims, para modulo ID autentificado en la sesion del usuario
        /// Default: http://www.calypso.com/identity/claims/moduloId
        /// </summary>
        public static string ModuloId { get; set; } = "http://www.calypso.com/identity/claims/moduloId";

     }
}
