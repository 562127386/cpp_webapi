﻿using com.cpp.calypso.proyecto.dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class RegistroAccesoMap : EntityTypeConfiguration<RegistroAcceso>
    {
        public RegistroAccesoMap()
        {
            ToTable("registros_accesos", "SCH_ACCESOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.Id).HasColumnName("id");
            Property(d => d.ColaboradorId).HasColumnName("colaborador_id");
            Property(d => d.FechaAcceso).HasColumnName("fecha_acceso");
            Property(d => d.TipoAccesoId).HasColumnName("tipo_acceso_id");
            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.Version).HasColumnName("m_version");
            Property(d => d.Ref).HasColumnName("m_ref");

        }
    }
}
