using com.cpp.calypso.proyecto.dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class SincronizacionLogMap : EntityTypeConfiguration<SincronizacionLog>
    {
        public SincronizacionLogMap()
        {
            ToTable("sincronizaciones_log", "SCH_RESPALDOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.Id).HasColumnName("id");
            Property(d => d.UsuarioId).HasColumnName("usuario_id");
            Property(d => d.Log).HasColumnName("log");
            Property(d => d.FechaRegistro).HasColumnName("fecha_registro");
        }
    }
}
