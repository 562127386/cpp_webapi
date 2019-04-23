using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using com.cpp.calypso.proyecto.dominio.Entidades;


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

            Property(d => d.Id).HasColumnName("id");
            Property(d => d.Codigo).HasColumnName("codigo");
            Property(d => d.Nombre).HasColumnName("nombre");
            Property(d => d.TipoOrdenamiento).HasColumnName("tipo_ordenamiento");

            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.Version).HasColumnName("m_version");
            Property(d => d.Ref).HasColumnName("m_ref");
        }
    }
}
