using Abp.Domain.Entities;
using System;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    [Serializable]
    public class DetalleReserva : Entity, ISoftDelete
    {
        public int ReservaHotelId { get; set; }

        public ReservaHotel ReservaHotel { get; set; }

        public int? OrigenConsumoId { get; set; }

        public DateTime? FechaReserva { get; set; }

        public DateTime? FechaConsumo { get; set; }

        public bool Consumido { get; set; }

        public bool Facturado { get; set; }

        public int? LiquidacionDetalleId { get; set; }


        public DateTime? fs { get; set; }

        public DateTime? fr { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }

        public string uid { get; set; }

        public long? CreatorUserId { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
