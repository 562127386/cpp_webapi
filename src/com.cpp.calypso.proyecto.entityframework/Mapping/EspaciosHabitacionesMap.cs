using com.cpp.calypso.proyecto.dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class EspaciosHabitacionesMap : EntityTypeConfiguration<EspacioHabitacion>
    {
        public EspaciosHabitacionesMap()
        {
            ToTable("espacios_habitaciones", "SCH_SERVICIOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.HabitacionId).HasColumnName("habitacion_id");
            Property(d => d.HabitacionRef).HasColumnName("habitacion_ref");
            Property(d => d.CodigoEspacio).HasColumnName("codigo_espacio");
            Property(d => d.Estado).HasColumnName("estado");
            Property(d => d.Activo).HasColumnName("activo");
            Property(d => d.Observaciones).HasColumnName("observaciones");

            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.Version).HasColumnName("m_version");
            Property(d => d.Ref).HasColumnName("m_ref");
            Property(d => d.CreationTime).HasColumnName("creation_time");
            Property(d => d.CreatorUserId).HasColumnName("creator_user_id");
        }
    }
}
