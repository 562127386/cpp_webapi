using com.cpp.calypso.proyecto.dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class TipoAccionEmpresaMap : EntityTypeConfiguration<TipoAccionEmpresa>
    {
        public TipoAccionEmpresaMap()
        {
            ToTable("tipos_acciones_empresa", "SCH_SERVICIOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.Id).HasColumnName("id");
            Property(d => d.AccionId).HasColumnName("accion_id");
            Property(d => d.EmpresaId).HasColumnName("empresa_id");
            Property(d => d.TipoComidaId).HasColumnName("tipo_comida_id");
            Property(d => d.HoraDesde).HasColumnName("hora_desde");
            Property(d => d.HoraHasta).HasColumnName("hora_hasta");

            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.Version).HasColumnName("m_version");
            Property(d => d.Ref).HasColumnName("m_ref");


        }
    }
}
