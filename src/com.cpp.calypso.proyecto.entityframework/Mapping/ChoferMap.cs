using com.cpp.calypso.proyecto.dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class ChoferMap : EntityTypeConfiguration<Chofer>
    {
        public ChoferMap()
        {
            ToTable("choferes", "SCH_TRANSPORTES");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.ProveedorId).HasColumnName("proveedor_id");
            Property(d => d.CatalogoTipoIdentificacionId).HasColumnName("catalogo_tipo_identificacion");
            Property(d => d.NumeroIdentificacion).HasColumnName("numero_identificacion");
            Property(d => d.ApellidosNombres).HasColumnName("apellidos_nombres");
            Property(d => d.Nombres).HasColumnName("nombres");
            Property(d => d.Apellidos).HasColumnName("apellidos");
            Property(d => d.CatalogoGeneroId).HasColumnName("catalogo_genero_id");
            Property(d => d.CatalogoEstadoCivilId).HasColumnName("catalogo_estado_civil_id");
            Property(d => d.FechaNacimiento).HasColumnName("fecha_nacimiento");
            Property(d => d.Celular).HasColumnName("celular");
            Property(d => d.Mail).HasColumnName("mail");
            Property(d => d.Estado).HasColumnName("estado");
            Property(d => d.FechaEstado).HasColumnName("fecha_estado");

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
