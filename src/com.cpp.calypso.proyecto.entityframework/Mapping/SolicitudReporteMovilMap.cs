using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using com.cpp.calypso.proyecto.dominio.Entidades;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class SolicitudReporteMovilMap : EntityTypeConfiguration<SolicitudReporteMovil>
    {
        public SolicitudReporteMovilMap()
        {
            ToTable("solicitudes_reportes_movil", "SCH_SERVICIOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            Property(d => d.CodigoReporte).HasColumnName("codigo_reporte");
            Property(d => d.FechaEnvio).HasColumnName("fecha_envio");
            Property(d => d.FechaSolicitud).HasColumnName("fecha_solicitud");
            Property(d => d.UsuarioId).HasColumnName("usuario_id");
            Property(d => d.Estado).HasColumnName("estado");
            Property(d => d.Parametros).HasColumnName("parametros");
            Property(d => d.CorreoElectronico).HasColumnName("correo_electronico");
            Property(d => d.Error).HasColumnName("error");
        }
    }
}
