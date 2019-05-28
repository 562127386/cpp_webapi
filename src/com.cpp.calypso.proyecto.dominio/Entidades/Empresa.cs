using System;
using Abp.Domain.Entities;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    [Serializable]
    public class Empresa : Entity, ISoftDelete
    {
        public TipoDeIdentificacion TipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public bool Estado { get; set; }
        public string Telefono { get; set; }
        public TipoDeSociedad TipoSociedad { get; set; }
        public string Observaciones { get; set; }
        public bool EsPrincipal { get; set; }
        public TipoDeContribuyente TipoContribuyente { get; set; }
        public string CodigoSap { get; set; }

        public DateTime? fs { get; set; }

        public DateTime? fr { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }

        public string uid { get; set; }
    }

    public enum TipoDeIdentificacion
    {
        RUC, // Ruc = 1
        Cedula, // Cedula = 2
        Passaporte, // Passaporte = 3
    }

    public enum TipoDeSociedad
    {
        Normal,
        Especial,
    }

    public enum TipoDeContribuyente
    {
        Normal,
        Especial,
    }
}
