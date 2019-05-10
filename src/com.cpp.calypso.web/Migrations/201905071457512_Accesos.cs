namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Accesos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SCH_ACCESOS.accesos_temporal",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        identificacion = c.String(nullable: false),
                        fecha_acceso = c.DateTime(nullable: false),
                        usuario_creador_id = c.Int(nullable: false),
                        m_version = c.Int(nullable: false),
                        m_ref = c.String(),
                        vigente = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AccesoTemporal_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "SCH_ACCESOS.registros_accesos",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        colaborador_id = c.Int(nullable: false),
                        fecha_acceso = c.DateTime(nullable: false),
                        tipo_acceso_id = c.Int(nullable: false),
                        m_version = c.Int(nullable: false),
                        m_ref = c.String(),
                        vigente = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_RegistroAcceso_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("SCH_RRHH.colaboradores", t => t.colaborador_id)
                .Index(t => t.colaborador_id);
            
            CreateTable(
                "SCH_ACCESOS.requisitos_colaboradores",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        requisito_id = c.Int(nullable: false),
                        nombre_requisito = c.String(nullable: false),
                        obligatorio = c.Boolean(nullable: false),
                        colaborador_id = c.Int(nullable: false),
                        fecha_caducidad = c.DateTime(nullable: false),
                        cumple = c.Boolean(nullable: false),
                        m_version = c.Int(nullable: false),
                        m_ref = c.String(),
                        vigente = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_RequisitoColaborador_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("SCH_RRHH.colaboradores", t => t.colaborador_id)
                .Index(t => t.colaborador_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("SCH_ACCESOS.requisitos_colaboradores", "colaborador_id", "SCH_RRHH.colaboradores");
            DropForeignKey("SCH_ACCESOS.registros_accesos", "colaborador_id", "SCH_RRHH.colaboradores");
            DropIndex("SCH_ACCESOS.requisitos_colaboradores", new[] { "colaborador_id" });
            DropIndex("SCH_ACCESOS.registros_accesos", new[] { "colaborador_id" });
            DropTable("SCH_ACCESOS.requisitos_colaboradores",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_RequisitoColaborador_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("SCH_ACCESOS.registros_accesos",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_RegistroAcceso_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("SCH_ACCESOS.accesos_temporal",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AccesoTemporal_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
