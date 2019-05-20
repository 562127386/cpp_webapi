using com.cpp.calypso.proyecto.dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class OperacionDiariaMap : EntityTypeConfiguration<OperacionDiaria>
    {
        public OperacionDiariaMap()
        {
            ToTable("operaciones_diarias", "SCH_TRANSPORTES");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.VehiculoId).HasColumnName("vehiculo_id");
            Property(d => d.ChoferId).HasColumnName("chofer_id");
            Property(d => d.FechaInicio).HasColumnName("fecha_inicio");
            Property(d => d.FechaFin).HasColumnName("fecha_fin");
            Property(d => d.KilometrajeInicio).HasColumnName("kilometraje_inicio");
            Property(d => d.KilometrajeFinal).HasColumnName("kilometraje_final");
            Property(d => d.Observacion).HasColumnName("observacion");
            Property(d => d.Estado).HasColumnName("estado");

            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.Version).HasColumnName("m_version");
            Property(d => d.Ref).HasColumnName("m_ref");
        }
    }
}
