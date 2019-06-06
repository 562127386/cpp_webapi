using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using Abp.Runtime.Validation;
using System;
using Abp.Extensions;
using System.Threading.Tasks;

namespace com.cpp.calypso.comun.dominio
{
    public abstract class BaseUsuario : 
        FullAuditedEntity, IUsuario, IUserLockout, IPassword, IPasswordReset
    {

        //public int Id { get; set; }

        [Obligado]
        [LongitudMayor(60)]
        public virtual string Cuenta { get; set; }

        [Obligado]
        [LongitudMayor(80)]
        [Display(Name = "Identificación")]
        public virtual string Identificacion { get; set; }


        [Obligado]
        [LongitudMayor(80)]
        public virtual string Nombres { get; set; }


        [Obligado]
        [LongitudMayor(80)]
        public virtual string Apellidos { get; set; }

        [Obligado]
        [LongitudMayor(80)]
        [EmailAddress(ErrorMessage = "Correo Electrónico Inválido")]
        public virtual string Correo { get; set; }

        [NotMapped]
        public virtual string EstadoNombre { get { return Estado == EstadoUsuario.Activo ? "Activo" : "Inactivo"; } set { } }


        [Obligado]
        public virtual EstadoUsuario Estado { get; set; }

        /// <summary>
        /// Listado de Roles que posee el Usuario
        /// </summary>
        [DisableValidation]
        public virtual ICollection<Rol> Roles { get; set; }

        /// <summary>
        /// Listado de Modulos que posee el Usuario
        /// </summary>
        [DisableValidation]
        public virtual ICollection<Modulo> Modulos { get; set; }


        /// <summary>
        /// Codigo para resetear clave
        /// Si existe este codigo, en el login obligar a cambiar la clave.
        /// Se utiliza este codigo, para enviar un correo al usuario para ingresar y cambiar su clave. 
        /// </summary>
        [LongitudMayor(328)]
        public virtual string PasswordResetCode { get; protected set; }

        public virtual void SetNewPasswordResetCode()
        {
            PasswordResetCode = Guid.NewGuid().ToString("N").Truncate(328);
        }

        public virtual void ClearPasswordResetCode(IPasswordReset usuario) {

            this.PasswordResetCode = string.Empty;
            return;
        }


        public virtual Task<string> GetPasswordResetCode(IPasswordReset usuario)
        {
            return Task.FromResult(usuario.PasswordResetCode);
        }

        #region Bloqueo de Usuario

        /// <summary>
        /// Fecha de finalizacion de bloque (UTC)
        /// </summary>
        public virtual DateTime? FechaFinalizacionBloqueUtc { get; set; }

        /// <summary>
        /// Cantidad de accesos fallidos
        /// </summary>
        public virtual int CantidadAccesoFallido { get; set; }

        /// <summary>
        /// Bloque habilitado
        /// </summary>
        public virtual bool BloqueoHabilitado { get; set; }

        #endregion

 
        [Obligado]
        [LongitudMayor(128)]
        public virtual string Password { get; set; }

        public string uid { get; set; }


        public override string ToString()
        {
            return string.Format("{0} {1} ", Nombres, Apellidos);
        }

    }
}
