

using com.cpp.calypso.comun.dominio;
using System;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace com.cpp.calypso.webapi
{
   

    public class JWTToken<TUser>
        where TUser : AspUser<TUser>
    {
        private string ProveedorJWT;
        private string AudenciaJWT;
        private string ClaveSimetrica;
        private int TiempoCaducidad = 60;

        public JWTToken()
        {

            ProveedorJWT = ConfigurationManager.AppSettings[Constantes.API_SEGURIDAD_JWT_PROVEEDOR_EMISION];
            AudenciaJWT = ConfigurationManager.AppSettings[Constantes.API_SEGURIDAD_JWT_AUDIENCIAS];
            ClaveSimetrica = ConfigurationManager.AppSettings[Constantes.API_SEGURIDAD_JWT_CLAVE_SIMENTRICA];

            var timeStr = ConfigurationManager.AppSettings[Constantes.API_SEGURIDAD_JWT_TOKEN_TIEMPO_CADUCIDAD_MINUTOS];
            if (!string.IsNullOrWhiteSpace(timeStr)) {
                int.TryParse(timeStr, out TiempoCaducidad);
            }

            
        }

        public IApplication Application { get; }

        public string CreateToken(TUser user)
        {
            //TODO: Configuraciones
            
            //Set issued at date
            DateTime issuedAt = DateTime.UtcNow;
            //set the time when it expires
            DateTime expires = DateTime.UtcNow.AddMinutes(TiempoCaducidad);

            //http://stackoverflow.com/questions/18223868/how-to-encrypt-jwt-security-token
            var tokenHandler = new JwtSecurityTokenHandler();

     
            var now = DateTime.UtcNow;
            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(ClaveSimetrica));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            //CreateClaims
            //create a identity and add claims to the user which we want to log in
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(CreateClaims(user));


            //create the jwt
            var token =
                (JwtSecurityToken)
                    tokenHandler.CreateJwtSecurityToken(
                        issuer: ProveedorJWT,
                        audience: AudenciaJWT,
                        subject: claimsIdentity, 
                        notBefore: issuedAt,
                        expires: expires, 
                        signingCredentials: signingCredentials);


            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        private IEnumerable<Claim> CreateClaims(TUser user)
        {
            //Roles
            string[] roles = user.Roles.Select(r => r.Codigo).ToArray();

            //Modulos
            string[] modules = user.Modulos.Select(m => m.Codigo).ToArray();

            //Usuario Autentificado
            string usuarioJson = JsonConvert.SerializeObject(MapTo(user));

            var list = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Cuenta),
                new Claim(ClaimTypes.Role, string.Join(",",roles)),
                 new Claim(Constantes.API_CLAIM_USUARIO_MODULO, string.Join(",",modules)),
                 new Claim(Constantes.API_CLAIM_USUARIO_AUTENTIFICADO, usuarioJson)
            };

            return list;
        }

        private UsuarioAutentificado MapTo(TUser usuario)
        {

            //Establecer Informacion 
            //1.
            var usuarioAutentificado = new UsuarioAutentificado();
            usuarioAutentificado.Id = usuario.Id;
            usuarioAutentificado.Cuenta = usuario.Cuenta;
            usuarioAutentificado.Correo = usuario.Correo;
            usuarioAutentificado.Apellidos = usuario.Apellidos;
            usuarioAutentificado.Identificacion = usuario.Identificacion;
            usuarioAutentificado.Nombres = usuario.Nombres;

            foreach (var rol in usuario.Roles)
            {
                var rolAutentificado = new RolAutentificado();
                rolAutentificado.Id = rol.Id;
                rolAutentificado.Codigo = rol.Codigo;
                rolAutentificado.EsAdministrador = rol.EsAdministrador;
                rolAutentificado.Nombre = rol.Nombre;

                usuarioAutentificado.Roles.Add(rolAutentificado);
            }

            foreach (var modulo in usuario.Modulos)
            {
                var moduloAutentificado = new ModuloAutentificado();
                moduloAutentificado.Id = modulo.Id;
                moduloAutentificado.Codigo = modulo.Codigo;
                moduloAutentificado.Nombre = modulo.Nombre;
                usuarioAutentificado.Modulos.Add(moduloAutentificado);
            }
   

            return usuarioAutentificado;
        }
    }
}