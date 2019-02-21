using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.cpp.calypso.proyecto.dominio;

namespace com.cpp.calypso.proyecto.entityframework
{
    public class ProveedorMap : EntityTypeConfiguration<Proveedor>
    {
        public ProveedorMap()
        {
            ToTable("proveedores", "SCH_PROVEEDORES");

            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.IsDeleted).HasColumnName("vigente");

        }
    }
}
