using com.cpp.calypso.proyecto.dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class MenuProveedorMap : EntityTypeConfiguration<MenuProveedor>
    {
        public MenuProveedorMap()
        {
            ToTable("menus_proveedor", "SCH_PROVEEDORES");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.Id).HasColumnName("id");
            Property(d => d.Aprobado).HasColumnName("aprobado");
            Property(d => d.Descripcion).HasColumnName("descripcion");
            Property(d => d.DocumentacionId).HasColumnName("documentacion_id");
            Property(d => d.FechaAprobacion).HasColumnName("fecha_aprobacion");
            Property(d => d.FechaFinal).HasColumnName("fecha_final");
            Property(d => d.FechaInicial).HasColumnName("fecha_inicial");
            Property(d => d.ProveedorId).HasColumnName("proveedor_id");
            Property(d => d.DocumentacionRef).HasColumnName("documentacion_ref");

            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.Version).HasColumnName("m_version");
            Property(d => d.Ref).HasColumnName("m_ref");

            
        }
    }
}
