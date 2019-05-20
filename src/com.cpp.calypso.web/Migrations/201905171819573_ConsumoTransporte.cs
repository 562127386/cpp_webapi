namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class ConsumoTransporte : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SCH_TRANSPORTES.consumos_transporte",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        tipo_consumo = c.String(),
                        operacion_diaria_ruta_id = c.Int(nullable: false),
                        operacion_diaria_ruta_ref = c.String(),
                        fecha_embarque = c.DateTime(),
                        fecha_desembarque = c.DateTime(),
                        coordenada_x_embarque = c.Decimal(nullable: false, precision: 18, scale: 2),
                        coordenada_y_embarque = c.Decimal(nullable: false, precision: 18, scale: 2),
                        coordenada_x_desembarque = c.Decimal(nullable: false, precision: 18, scale: 2),
                        coordenada_y_desembarque = c.Decimal(nullable: false, precision: 18, scale: 2),
                        colaborador_id = c.Int(nullable: false),
                        huella = c.String(),
                        fs = c.DateTime(),
                        fr = c.DateTime(),
                        m_version = c.Int(nullable: false),
                        m_ref = c.String(),
                        vigente = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ConsumoTransporte_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SCH_RRHH.colaboradores", t => t.colaborador_id)
                .ForeignKey("SCH_TRANSPORTES.operaciones_diarias_rutas", t => t.operacion_diaria_ruta_id)
                .Index(t => t.operacion_diaria_ruta_id)
                .Index(t => t.colaborador_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("SCH_TRANSPORTES.consumos_transporte", "operacion_diaria_ruta_id", "SCH_TRANSPORTES.operaciones_diarias_rutas");
            DropForeignKey("SCH_TRANSPORTES.consumos_transporte", "colaborador_id", "SCH_RRHH.colaboradores");
            DropIndex("SCH_TRANSPORTES.consumos_transporte", new[] { "colaborador_id" });
            DropIndex("SCH_TRANSPORTES.consumos_transporte", new[] { "operacion_diaria_ruta_id" });
            DropTable("SCH_TRANSPORTES.consumos_transporte",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ConsumoTransporte_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
