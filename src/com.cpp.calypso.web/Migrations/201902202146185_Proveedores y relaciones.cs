namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Proveedoresyrelaciones : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SCH_PROVEEDORES.proveedores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        tipo_identificacion = c.Int(nullable: false),
                        identificacion = c.String(nullable: false, maxLength: 20),
                        razon_social = c.String(nullable: false, maxLength: 100),
                        estado = c.Int(nullable: false),
                        es_externo = c.Int(nullable: false),
                        coordenadas = c.String(maxLength: 50),
                        codigo_sap = c.String(maxLength: 30),
                        usuario = c.String(maxLength: 60),
                        vigente = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Proveedor_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "SCH_PROVEEDORES.contratos_proveedores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        proveedor_id = c.Int(nullable: false),
                        empresa_id = c.Int(nullable: false),
                        codigo = c.String(nullable: false, maxLength: 30),
                        objeto = c.String(nullable: false, maxLength: 500),
                        fecha_inicio = c.DateTime(nullable: false),
                        fecha_fin = c.DateTime(nullable: false),
                        plazo_pago = c.Int(nullable: false),
                        monto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        orden_compra = c.String(nullable: false, maxLength: 10),
                        estado = c.Int(nullable: false),
                        documentacion_id = c.Int(),
                        vigente = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ContratoProveedor_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SCH_PROYECTOS.empresas", t => t.empresa_id, cascadeDelete: true)
                .ForeignKey("SCH_PROVEEDORES.proveedores", t => t.proveedor_id, cascadeDelete: true)
                .Index(t => t.proveedor_id)
                .Index(t => t.empresa_id);
            
            CreateTable(
                "SCH_PROYECTOS.empresas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        tipo_identificacion = c.Int(nullable: false),
                        identificacion = c.String(nullable: false, maxLength: 20),
                        razon_social = c.String(nullable: false, maxLength: 100),
                        direccion = c.String(nullable: false, maxLength: 255),
                        correo = c.String(nullable: false, maxLength: 100),
                        estado = c.Boolean(nullable: false),
                        telefono = c.String(nullable: false, maxLength: 15),
                        tipo_sociedad = c.Int(nullable: false),
                        observaciones = c.String(maxLength: 800),
                        es_principal = c.Boolean(nullable: false),
                        tipo_contribuyente = c.Int(nullable: false),
                        vigente1 = c.Boolean(nullable: false),
                        codigo_sap = c.String(maxLength: 200),
                        vigente = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Empresa_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "SCH_SERVICIOS.tipos_opciones_comidas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        contrato_id = c.Int(nullable: false),
                        opcion_comida_id = c.Int(nullable: false),
                        costo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        tipo_comida_id = c.Int(nullable: false),
                        vigente = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TipoOpcionComida_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SCH_PROVEEDORES.contratos_proveedores", t => t.contrato_id, cascadeDelete: true)
                .ForeignKey("SCH_CATALOGOS.catalogos", t => t.opcion_comida_id)
                .ForeignKey("SCH_CATALOGOS.catalogos", t => t.tipo_comida_id)
                .Index(t => t.contrato_id)
                .Index(t => t.opcion_comida_id)
                .Index(t => t.tipo_comida_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("SCH_SERVICIOS.tipos_opciones_comidas", "tipo_comida_id", "SCH_CATALOGOS.catalogos");
            DropForeignKey("SCH_SERVICIOS.tipos_opciones_comidas", "opcion_comida_id", "SCH_CATALOGOS.catalogos");
            DropForeignKey("SCH_SERVICIOS.tipos_opciones_comidas", "contrato_id", "SCH_PROVEEDORES.contratos_proveedores");
            DropForeignKey("SCH_PROVEEDORES.contratos_proveedores", "proveedor_id", "SCH_PROVEEDORES.proveedores");
            DropForeignKey("SCH_PROVEEDORES.contratos_proveedores", "empresa_id", "SCH_PROYECTOS.empresas");
            DropIndex("SCH_SERVICIOS.tipos_opciones_comidas", new[] { "tipo_comida_id" });
            DropIndex("SCH_SERVICIOS.tipos_opciones_comidas", new[] { "opcion_comida_id" });
            DropIndex("SCH_SERVICIOS.tipos_opciones_comidas", new[] { "contrato_id" });
            DropIndex("SCH_PROVEEDORES.contratos_proveedores", new[] { "empresa_id" });
            DropIndex("SCH_PROVEEDORES.contratos_proveedores", new[] { "proveedor_id" });
            DropTable("SCH_SERVICIOS.tipos_opciones_comidas",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TipoOpcionComida_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("SCH_PROYECTOS.empresas",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Empresa_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("SCH_PROVEEDORES.contratos_proveedores",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ContratoProveedor_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("SCH_PROVEEDORES.proveedores",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Proveedor_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
