using com.cpp.calypso.proyecto.dominio.Servicios_Proveedores;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;


namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class TipoCatalogoMap : EntityTypeConfiguration<TipoCatalogo>
    {
        public TipoCatalogoMap()
        {
            ToTable("tipos_catalogos", "SCH_CATALOGOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.IsDeleted).HasColumnName("vigente");
        }
    }
}
