using com.cpp.calypso.proyecto.dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class VehiculoMap : EntityTypeConfiguration<Vehiculo>
    {
        public VehiculoMap()
        {
            ToTable("vehiculos", "SCH_TRANSPORTES");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.ProveedorId).HasColumnName("proveedor_id");
            Property(d => d.Codigo).HasColumnName("codigo");
            Property(d => d.CatalogoTipoVehiculoId).HasColumnName("catalogo_tipo_vehiculo");
            Property(d => d.NumeroPlaca).HasColumnName("numero_placa");
            Property(d => d.Marca).HasColumnName("marca");
            Property(d => d.Capacidad).HasColumnName("capacidad");
            Property(d => d.AnioFabricacion).HasColumnName("anio_fabricacion");
            Property(d => d.Color).HasColumnName("color");
            Property(d => d.FechaVencimientoMatricula).HasColumnName("fecha_vencimiento_matricula");
            Property(d => d.Estado).HasColumnName("estado");
            Property(d => d.FechaEstado).HasColumnName("fecha_estado");

            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.Version).HasColumnName("m_version");
            Property(d => d.Ref).HasColumnName("m_ref");
            Property(d => d.FechaCreacion).HasColumnName("fecha_creacion");
            Property(d => d.UsuarioCreador).HasColumnName("usuario_creador");
            Property(d => d.UltimaModificacion).HasColumnName("ultima_modificacion");
            Property(d => d.UsuarioUltimaModificacion).HasColumnName("usuario_ultima_modificacion");
            Property(d => d.UsuarioEliminacion).HasColumnName("usuario_eliminacion");
            Property(d => d.FechaEliminacion).HasColumnName("fecha_eliminacion");
            Property(d => d.UltimaModificacion).HasColumnName("ultima_modificacion");
        }
    }
}
