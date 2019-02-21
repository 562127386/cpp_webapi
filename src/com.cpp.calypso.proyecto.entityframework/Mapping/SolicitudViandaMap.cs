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
    public class SolicitudViandaMap : EntityTypeConfiguration<SolicitudVianda>
    {
        public SolicitudViandaMap()
        {
            ToTable("solicitudes_viandas", "SCH_SERVICIOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(p => p.solicitante)
                .WithMany()
                .HasForeignKey(s => s.solicitante_id)
                .WillCascadeOnDelete(false);

            HasRequired(p => p.disciplina)
                .WithMany()
                .HasForeignKey(s => s.disciplina_id)
                .WillCascadeOnDelete(false);

            HasRequired(p => p.locacion)
                .WithMany()
                .HasForeignKey(s => s.LocacionId)
                .WillCascadeOnDelete(false);



            HasRequired(p => p.tipo_comida)
                .WithMany()
                .HasForeignKey(s => s.tipo_comida_id)
                .WillCascadeOnDelete(false);

            HasRequired(p => p.area)
                .WithMany()
                .HasForeignKey(s => s.area_id)
                .WillCascadeOnDelete(false);

            HasOptional(p => p.anotador)
                .WithMany()
                .HasForeignKey(c => c.anotador_id)
                .WillCascadeOnDelete(false);

            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.LocacionId).HasColumnName("locacion_id");
        }
    }
}
