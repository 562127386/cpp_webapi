using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.cpp.calypso.proyecto.dominio;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class ContratoProveedorMap : EntityTypeConfiguration<ContratoProveedor>
    {
        public ContratoProveedorMap()
        {
            ToTable("contratos_proveedores", "SCH_PROVEEDORES");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            // Relationships
            this.HasRequired(t => t.Proveedor)
                .WithMany(t => t.contratos)
                .HasForeignKey(d => d.ProveedorId);

            Property(d => d.ProveedorId).HasColumnName("proveedor_id");
            Property(d => d.EmpresaId).HasColumnName("empresa_id");
            Property(d => d.IsDeleted).HasColumnName("vigente");
        }
    }
}
