﻿using com.cpp.calypso.proyecto.dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class OpcionComidaMap : EntityTypeConfiguration<OpcionComida>
    {
        public OpcionComidaMap()
        {
            ToTable("opciones_comidas", "SCH_SERVICIOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.Id).HasColumnName("id");
            Property(d => d.Costo).HasColumnName("costo");
            Property(d => d.Nombre).HasColumnName("nombre");
            Property(d => d.Opcion).HasColumnName("opcion_comida");

            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.Version).HasColumnName("m_version");
            Property(d => d.Ref).HasColumnName("m_ref");


        }
    }
}
