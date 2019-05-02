using com.cpp.calypso.proyecto.dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class ServicioProveedorMap : EntityTypeConfiguration<ServicioProveedor>
    {
        public ServicioProveedorMap()
        {
            ToTable("servicios_proveedores", "SCH_SERVICIOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.Id).HasColumnName("id");
            Property(d => d.ServicioId).HasColumnName("servicio_id");
            Property(d => d.ProveedorId).HasColumnName("proveedor_id");
            Property(d => d.Estado).HasColumnName("estado");
            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.Version).HasColumnName("m_version");
            Property(d => d.Ref).HasColumnName("m_ref");
        }
    }
}
