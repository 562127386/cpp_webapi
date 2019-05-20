using com.cpp.calypso.proyecto.dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class RutaHorarioVehiculoMap : EntityTypeConfiguration<RutaHorarioVehiculo>
    {
        public RutaHorarioVehiculoMap()
        {
            ToTable("rutas_horarios_vehiculos", "SCH_TRANSPORTES");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.RutaHorarioId).HasColumnName("ruta_horario_id");
            Property(d => d.VehiculoId).HasColumnName("vehiculo_id");
            Property(d => d.FechaDesde).HasColumnName("fecha_desde");
            Property(d => d.FechaHasta).HasColumnName("fecha_hasta");
            Property(d => d.HoraSalida).HasColumnName("hora_salida");
            Property(d => d.HoraLlegada).HasColumnName("hora_llegada");
            Property(d => d.Duracion).HasColumnName("duracion");
            Property(d => d.Observacion).HasColumnName("observacion");
            Property(d => d.Estado).HasColumnName("estado");
            Property(d => d.FechaAsignacion).HasColumnName("fecha_asignacion");
            Property(d => d.UsuarioAsignacion).HasColumnName("usuario_asignacion");
            Property(d => d.FechaDesasignacion).HasColumnName("fecha_desasignacion");
            Property(d => d.UsuarioDesasignacion).HasColumnName("usuario_desasignacion");
            Property(d => d.CreaOperacion).HasColumnName("crea_operacion");

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
