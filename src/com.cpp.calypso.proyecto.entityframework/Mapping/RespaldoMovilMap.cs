using com.cpp.calypso.proyecto.dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class RespaldoMovilMap : EntityTypeConfiguration<RespaldoMovil>
    {
        public RespaldoMovilMap()
        {
            ToTable("respaldos_movil", "SCH_RESPALDOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.Id).HasColumnName("id");
            Property(d => d.Estado).HasColumnName("estado");
            Property(d => d.FechaRegistro).HasColumnName("fecha_registro");
            Property(d => d.Json).HasColumnName("json");
            Property(d => d.UserId).HasColumnName("user_id");
        }
    }
}
