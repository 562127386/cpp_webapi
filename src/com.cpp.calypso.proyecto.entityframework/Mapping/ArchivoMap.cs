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
    public class ArchivoMap : EntityTypeConfiguration<Archivo>
    {
        public ArchivoMap()
        {
            ToTable("archivos", "SCH_PROYECTOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.Id).HasColumnName("id");
            Property(d => d.Codigo).HasColumnName("codigo");
            Property(d => d.Nombre).HasColumnName("nombre");
            Property(d => d.FechaRegistro).HasColumnName("fecha_registro");
            Property(d => d.Hash).HasColumnName("hash");
            Property(d => d.TipoContenido).HasColumnName("contenido");
            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.Version).HasColumnName("m_version");
            Property(d => d.Ref).HasColumnName("m_ref");
        }
    }
}
