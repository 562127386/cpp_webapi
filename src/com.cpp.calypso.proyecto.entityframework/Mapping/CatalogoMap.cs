using com.cpp.calypso.proyecto.dominio.Servicios_Proveedores;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class CatalogoMap : EntityTypeConfiguration<Catalogo>
    {
        public CatalogoMap()
        {
            ToTable("catalogos", "SCH_CATALOGOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.TipoCatalogoId).HasColumnName("tipo_catalogo_id");
        }
    }
}
