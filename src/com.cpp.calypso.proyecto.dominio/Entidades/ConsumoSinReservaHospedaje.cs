using Abp.Domain.Entities;
using System;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    [Serializable]
    public class ConsumoSinReservaHospedaje : Entity, ISoftDelete
    {
        public int ProveedorId { get; set; }

        public Proveedor Proveedor { get; set; }

        public int? ColaboradorId { get; set; }

        public Colaborador Colaborador { get; set; }

        public string IdentificacionColaborador { get; set; }

        public string Nombres { get; set; }

        public string PrimerApellido { get; set; }

        public string SegundoApellido { get; set; }

        public int? OrigenConsumoId { get; set; }

        public int TipoHabitacionId { get; set; }

        public Catalogo TipoHabitacion { get; set; }

        public DateTime? FechaRegistro { get; set; }

        public int NumeroHabitacion { get; set; }

        public int? Autorizacion { get; set; }



        public DateTime? fs { get; set; }

        public DateTime? fr { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }

        public string uid { get; set; }
    }
}
