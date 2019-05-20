using com.cpp.calypso.proyecto.dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class OperacionDiariaRutaMap : EntityTypeConfiguration<OperacionDiariaRuta>
    {
        public OperacionDiariaRutaMap()
        {
            ToTable("operaciones_diarias_rutas", "SCH_TRANSPORTES");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            Property(d => d.RutaHorarioVehiculoId).HasColumnName("ruta_horario_vehiculo_id");
            Property(d => d.RutaHorarioVehiculoRef).HasColumnName("ruta_horario_vehiculo_ref");
            Property(d => d.OperacionDiariaId).HasColumnName("operacion_diaria_id");
            Property(d => d.OperacionDiariaRef).HasColumnName("operacion_diaria_ref");
            Property(d => d.FechaInicio).HasColumnName("fecha_inicio");
            Property(d => d.FechaFin).HasColumnName("fecha_fin");


            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.Version).HasColumnName("m_version");
            Property(d => d.Ref).HasColumnName("m_ref");
        }
    }
}
