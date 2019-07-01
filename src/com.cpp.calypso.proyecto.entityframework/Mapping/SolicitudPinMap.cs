using com.cpp.calypso.proyecto.dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class SolicitudPinMap : EntityTypeConfiguration<SolicitudPin>
    {
        public SolicitudPinMap()
        {
            ToTable("solicitudes_pin", "SCH_USUARIOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.Id).HasColumnName("id");
            Property(d => d.FechaSolicitud).HasColumnName("fecha_solicitud");
            Property(d => d.FechaEnvio).HasColumnName("fecha_envio");
            Property(d => d.UsuarioId).HasColumnName("usuario_id");
            Property(d => d.Estado).HasColumnName("estado");
            Property(d => d.Pin).HasColumnName("pin");
            Property(d => d.CorreoElectronico).HasColumnName("correo_electronico");
            Property(d => d.Error).HasColumnName("error");
        }
    }
}
