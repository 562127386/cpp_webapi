using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.cpp.calypso.proyecto.dominio.Entidades;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class UsuarioMovilMap : EntityTypeConfiguration<UsuarioMovil>
    {
        public UsuarioMovilMap()
        {
            ToTable("usuarios_movil", "SCH_USUARIOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.Id).HasColumnName("id");
            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.Version).HasColumnName("m_version");
            Property(d => d.Ref).HasColumnName("m_ref");
            Property(d => d.ActivoMovil).HasColumnName("activo_movil");
            Property(d => d.ApellidosNombres).HasColumnName("apellidos_nombre");
            Property(d => d.FechaCreacion).HasColumnName("fecha_creacion");
            Property(d => d.Pin).HasColumnName("pin");
            Property(d => d.UltimoAcceso).HasColumnName("ultimo_acceso");
            Property(d => d.Username).HasColumnName("username");
            Property(d => d.UsuarioId).HasColumnName("usuario_id");
            Property(d => d.Rol).HasColumnName("rol");
        }
    }
}
