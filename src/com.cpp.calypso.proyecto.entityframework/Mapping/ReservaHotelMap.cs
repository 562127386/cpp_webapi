using com.cpp.calypso.proyecto.dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class ReservaHotelMap : EntityTypeConfiguration<ReservaHotel>
    {
        public ReservaHotelMap()
        {
            ToTable("reservas_hoteles", "SCH_SERVICIOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            Property(d => d.EspacioHabitacionId).HasColumnName("espacio_Habitacion_id");
            Property(d => d.ColaboradorId).HasColumnName("colaborador_id");
            Property(d => d.FechaRegistro).HasColumnName("fecha_registro");
            Property(d => d.FechaDesde).HasColumnName("fecha_desde");
            Property(d => d.FechaHasta).HasColumnName("fecha_hasta");
            Property(d => d.Estado).HasColumnName("estado");
            Property(d => d.IdentificacionColaborador).HasColumnName("identificacion_colaborador");
            Property(d => d.Nombres).HasColumnName("nombres");
            Property(d => d.PrimerApellido).HasColumnName("primer_apellido");
            Property(d => d.SegundoApellido).HasColumnName("segundo_apellido");


            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.Version).HasColumnName("m_version");
            Property(d => d.Ref).HasColumnName("m_ref");
            Property(d => d.CreationTime).HasColumnName("creation_time");
            Property(d => d.CreatorUserId).HasColumnName("creator_user_id");
        }
    }
}
