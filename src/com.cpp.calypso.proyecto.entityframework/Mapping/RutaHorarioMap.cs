using com.cpp.calypso.proyecto.dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class RutaHorarioMap : EntityTypeConfiguration<RutaHorario>
    {
        public RutaHorarioMap()
        {
            ToTable("rutas_horarios", "SCH_TRANSPORTES");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.RutaId).HasColumnName("ruta_id");
            Property(d => d.Horario).HasColumnName("horario");

            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.Version).HasColumnName("m_version");
            Property(d => d.Ref).HasColumnName("m_ref");
            Property(d => d.FechaCreacion).HasColumnName("fecha_creacion");
            Property(d => d.UsuarioCreador).HasColumnName("usuario_creador");
            Property(d => d.UltimaModificacion).HasColumnName("ultima_modificacion");
            Property(d => d.UsuarioUltimaModificacion).HasColumnName("usuario_ultima_modificacion");
            Property(d => d.UsuarioEliminacion).HasColumnName("usuario_eliminacion");
            Property(d => d.FechaEliminacion).HasColumnName("fecha_eliminacion");
            Property(d => d.UltimaModificacion).HasColumnName("ultima_modificacion");
        }
    }
}
