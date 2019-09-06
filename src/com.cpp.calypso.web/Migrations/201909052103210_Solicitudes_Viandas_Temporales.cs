namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Solicitudes_Viandas_Temporales : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SCH_SERVICIOS.consumos_viandas_qr_temporal",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        solicitud_vianda_temporal_id = c.Int(nullable: false),
                        identificacion = c.String(),
                        fecha_consumo = c.DateTime(nullable: false),
                        origen_consumo_id = c.Int(),
                        solicitud_vianda_temporal_ref = c.String(),
                        fs = c.DateTime(),
                        fr = c.DateTime(),
                        m_version = c.Int(nullable: false),
                        vigente = c.Boolean(nullable: false),
                        uid = c.String(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ConsumoViandaQrTemporal_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("SCH_SERVICIOS.solicitudes_viandas_temporal", t => t.solicitud_vianda_temporal_id)
                .Index(t => t.solicitud_vianda_temporal_id);
            
            CreateTable(
                "SCH_SERVICIOS.solicitudes_viandas_temporal",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        fecha_solicitud = c.DateTime(nullable: false),
                        solicitante_id = c.Int(nullable: false),
                        tipo_comida = c.Int(nullable: false),
                        total_pedido = c.Int(nullable: false),
                        total_consumido = c.Int(nullable: false),
                        fs = c.DateTime(),
                        fr = c.DateTime(),
                        m_version = c.Int(nullable: false),
                        m_ref = c.String(),
                        vigente = c.Boolean(nullable: false),
                        uid = c.String(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SolicitudViandaTemporal_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("SCH_CATALOGOS.catalogos", t => t.tipo_comida)
                .Index(t => t.tipo_comida);
            
            CreateTable(
                "SCH_SERVICIOS.consumos_viandas_temporal",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        solicitud_vianda_temporal_id = c.Int(nullable: false),
                        fecha_consumo = c.DateTime(nullable: false),
                        colaborador_id = c.Int(nullable: false),
                        origen_consumo_id = c.Int(),
                        solicitud_vianda_temporal_ref = c.String(),
                        fs = c.DateTime(),
                        fr = c.DateTime(),
                        m_version = c.Int(nullable: false),
                        vigente = c.Boolean(nullable: false),
                        uid = c.String(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ConsumoViandaTemporal_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("SCH_SERVICIOS.solicitudes_viandas_temporal", t => t.solicitud_vianda_temporal_id)
                .Index(t => t.solicitud_vianda_temporal_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("SCH_SERVICIOS.consumos_viandas_temporal", "solicitud_vianda_temporal_id", "SCH_SERVICIOS.solicitudes_viandas_temporal");
            DropForeignKey("SCH_SERVICIOS.consumos_viandas_qr_temporal", "solicitud_vianda_temporal_id", "SCH_SERVICIOS.solicitudes_viandas_temporal");
            DropForeignKey("SCH_SERVICIOS.solicitudes_viandas_temporal", "tipo_comida", "SCH_CATALOGOS.catalogos");
            DropIndex("SCH_SERVICIOS.consumos_viandas_temporal", new[] { "solicitud_vianda_temporal_id" });
            DropIndex("SCH_SERVICIOS.solicitudes_viandas_temporal", new[] { "tipo_comida" });
            DropIndex("SCH_SERVICIOS.consumos_viandas_qr_temporal", new[] { "solicitud_vianda_temporal_id" });
            DropTable("SCH_SERVICIOS.consumos_viandas_temporal",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ConsumoViandaTemporal_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("SCH_SERVICIOS.solicitudes_viandas_temporal",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SolicitudViandaTemporal_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("SCH_SERVICIOS.consumos_viandas_qr_temporal",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ConsumoViandaQrTemporal_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
