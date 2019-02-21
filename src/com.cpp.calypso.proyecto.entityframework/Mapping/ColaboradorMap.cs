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
    public class ColaboradorMap : EntityTypeConfiguration<Colaborador>
    {

        public ColaboradorMap()
        {
            ToTable("colaboradores", "SCH_RRHH");

            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            Property(d => d.ContratoId).HasColumnName("contrato_id");
        }
    }
}
