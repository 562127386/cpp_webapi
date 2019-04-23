using com.cpp.calypso.proyecto.dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class ProveedorMap : EntityTypeConfiguration<Proveedor>
    {
        public ProveedorMap()
        {
            ToTable("proveedores", "SCH_PROVEEDORES");

            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.Id).HasColumnName("id");
            Property(d => d.CodigoSap).HasColumnName("codigo_sap");
            Property(d => d.Coordenadas).HasColumnName("coordenadas");
            Property(d => d.EsExterno).HasColumnName("es_externo");
            Property(d => d.Estado).HasColumnName("estado");
            Property(d => d.Identificacion).HasColumnName("identificacion");
            Property(d => d.RazonSocial).HasColumnName("razon_social");
            Property(d => d.TipoIdentificacion).HasColumnName("tipo_identificacion");
            Property(d => d.Usuario).HasColumnName("usuario");

            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.Version).HasColumnName("m_version");
            Property(d => d.Ref).HasColumnName("m_ref");

        }
    }
}
