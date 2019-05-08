using com.cpp.calypso.proyecto.dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class ContratoProveedorMap : EntityTypeConfiguration<ContratoProveedor>
    {
        public ContratoProveedorMap()
        {
            ToTable("contratos_proveedores", "SCH_PROVEEDORES");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("id");
            
            Property(d => d.Codigo).HasColumnName("codigo");
            Property(d => d.DocumentacionId).HasColumnName("documentacion_id");
            Property(d => d.Estado).HasColumnName("estado");
            Property(d => d.FechaFin).HasColumnName("fecha_fin");
            Property(d => d.FechaInicio).HasColumnName("fecha_inicio");
            Property(d => d.Monto).HasColumnName("monto");
            Property(d => d.Objeto).HasColumnName("objeto");
            Property(d => d.OrdenCompra).HasColumnName("orden_compra");
            Property(d => d.PlazoPago).HasColumnName("plazo_pago");
            Property(d => d.ProveedorId).HasColumnName("proveedor_id");
            Property(d => d.EmpresaId).HasColumnName("empresa_id");

            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.Version).HasColumnName("m_version");
            Property(d => d.Ref).HasColumnName("m_ref");


            this.HasRequired(t => t.Proveedor)
                .WithMany(t => t.Contratos)
                .HasForeignKey(d => d.ProveedorId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.Empresa)
                .WithMany()
                .HasForeignKey(d => d.EmpresaId)
                .WillCascadeOnDelete(false);
        }
    }
}
