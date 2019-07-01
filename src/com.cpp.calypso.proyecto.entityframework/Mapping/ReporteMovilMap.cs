using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using com.cpp.calypso.proyecto.dominio.Entidades;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class ReporteMovilMap : EntityTypeConfiguration<ReporteMovil>
    {
        public ReporteMovilMap()
        {
            ToTable("reportes_movil", "SCH_SERVICIOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            Property(d => d.Codigo).HasColumnName("codigo");
            Property(d => d.Nombre).HasColumnName("nombre");
            Property(d => d.Parametros).HasColumnName("parametros");
            Property(d => d.CatalogoServicioId).HasColumnName("catalogo_servicio_id");
            Property(d => d.Rol).HasColumnName("rol");
        }
    }
}
