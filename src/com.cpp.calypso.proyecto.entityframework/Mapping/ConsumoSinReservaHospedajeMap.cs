using com.cpp.calypso.proyecto.dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class ConsumoSinReservaHospedajeMap : EntityTypeConfiguration<ConsumoSinReservaHospedaje>
    {
        public ConsumoSinReservaHospedajeMap()
        {
            ToTable("consumos_sin_reserva_hospedaje", "SCH_SERVICIOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.Version).HasColumnName("m_version");
            Property(d => d.Ref).HasColumnName("m_ref");

            Property(d => d.ProveedorId).HasColumnName("proveedor_id");
            Property(d => d.ColaboradorId).HasColumnName("colaborador_id");
            Property(d => d.IdentificacionColaborador).HasColumnName("identificacion_colaborador");
            Property(d => d.Nombres).HasColumnName("nombres");
            Property(d => d.PrimerApellido).HasColumnName("primer_apellido");
            Property(d => d.SegundoApellido).HasColumnName("segundo_apellido");
            Property(d => d.OrigenConsumoId).HasColumnName("origen_consumo_id");
            Property(d => d.TipoHabitacionId).HasColumnName("tipo_habitacion_id");
            Property(d => d.FechaRegistro).HasColumnName("fecha_registro");
            Property(d => d.NumeroHabitacion).HasColumnName("numero_habitacion");
            Property(d => d.Autorizacion).HasColumnName("autorizacion");
        }
    }
}
