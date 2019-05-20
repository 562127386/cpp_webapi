using com.cpp.calypso.proyecto.dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class LugarMap : EntityTypeConfiguration<Lugar>
    {
        public LugarMap()
        {
            ToTable("lugares", "SCH_TRANSPORTES");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            Property(d => d.Codigo).HasColumnName("codigo");
            Property(d => d.Nombre).HasColumnName("nombre");
            Property(d => d.Descripcion).HasColumnName("descripcion");
            Property(d => d.Latitud).HasColumnName("latitud");
            Property(d => d.Longitud).HasColumnName("longitud");


            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.Version).HasColumnName("m_version");
            Property(d => d.Ref).HasColumnName("m_ref");
            Property(d => d.FechaCreacion).HasColumnName("fecha_creacion");
            Property(d => d.UsuarioCreador).HasColumnName("usuario_creador");
            Property(d => d.UltimaModificacion).HasColumnName("ultima_modificacion");
            Property(d => d.UsuarioUltimaModificacion).HasColumnName("usuario_ultima_modificacion");
            Property(d => d.FechaEliminacion).HasColumnName("fecha_eliminacion");
            Property(d => d.UltimaModificacion).HasColumnName("ultima_modificacion");
        }
    }
}
