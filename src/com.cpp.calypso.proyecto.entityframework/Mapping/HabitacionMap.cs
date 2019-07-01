using com.cpp.calypso.proyecto.dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{

    public class HabitacionMap : EntityTypeConfiguration<Habitacion>
    {
        public HabitacionMap()
        {
            ToTable("habitaciones", "SCH_SERVICIOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            Property(d => d.ProveedorId).HasColumnName("proveedor_id");
            Property(d => d.NumeroHabitacion).HasColumnName("numero_habitacion");
            Property(d => d.TipoHabitacionId).HasColumnName("tipo_habitacion_id");
            Property(d => d.Capacidad).HasColumnName("capacidad");
            Property(d => d.Estado).HasColumnName("estado");
            Property(d => d.Aprobado).HasColumnName("aprobado");
            Property(d => d.FechaAprobacion).HasColumnName("fecha_aprobacion");

            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.Version).HasColumnName("m_version");
            Property(d => d.Ref).HasColumnName("m_ref");
            Property(d => d.CreationTime).HasColumnName("creation_time");
            Property(d => d.CreatorUserId).HasColumnName("creator_user_id");
        }
    }
}
