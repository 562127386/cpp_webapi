using com.cpp.calypso.proyecto.dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class EmpresaMap : EntityTypeConfiguration<Empresa>
    {
        public EmpresaMap()
        {
            ToTable("empresas", "SCH_PROYECTOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.Id).HasColumnName("id");
            Property(d => d.Identificacion).HasColumnName("identificacion");
            Property(d => d.CodigoSap).HasColumnName("codigo_sap");
            Property(d => d.Correo).HasColumnName("correo");
            Property(d => d.Direccion).HasColumnName("direccion");
            Property(d => d.EsPrincipal).HasColumnName("es_principal");
            Property(d => d.Estado).HasColumnName("estado");
            Property(d => d.Observaciones).HasColumnName("observaciones");
            Property(d => d.RazonSocial).HasColumnName("razon_social");
            Property(d => d.Telefono).HasColumnName("telefono");
            Property(d => d.TipoContribuyente).HasColumnName("tipo_contribuyente");
            Property(d => d.TipoIdentificacion).HasColumnName("tipo_identificacion");
            Property(d => d.TipoSociedad).HasColumnName("tipo_sociedad");

            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.Version).HasColumnName("m_version");
            Property(d => d.Ref).HasColumnName("m_ref");

        }
    }
}
