using com.cpp.calypso.proyecto.dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class RequisitoColaboradorMap : EntityTypeConfiguration<RequisitoColaborador>
    {
        public RequisitoColaboradorMap()
        {
            ToTable("requisitos_colaboradores", "SCH_ACCESOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.Id).HasColumnName("id");
            Property(d => d.RequisitoId).HasColumnName("requisito_id");
            Property(d => d.NombreRequisito).HasColumnName("nombre_requisito");
            Property(d => d.Obligatorio).HasColumnName("obligatorio");
            Property(d => d.ColaboradorId).HasColumnName("colaborador_id");
            Property(d => d.FechaCaducidad).HasColumnName("fecha_caducidad");
            Property(d => d.Cumple).HasColumnName("cumple");
            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.Version).HasColumnName("m_version");
            Property(d => d.Ref).HasColumnName("m_ref");

        }
    }
}
