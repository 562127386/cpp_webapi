namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Servicios_Rol_Proveedores : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SCH_SERVICIOS.servicios_proveedores",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        servicio_id = c.Int(nullable: false),
                        proveedor_id = c.Int(nullable: false),
                        estado = c.Int(nullable: false),
                        m_version = c.Int(nullable: false),
                        m_ref = c.String(),
                        vigente = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ServicioProveedor_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("SCH_PROVEEDORES.proveedores", t => t.proveedor_id)
                .ForeignKey("SCH_CATALOGOS.catalogos", t => t.servicio_id)
                .Index(t => t.servicio_id)
                .Index(t => t.proveedor_id);
            
            CreateTable(
                "SCH_PROYECTOS.servicio_rol",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        servicio_id = c.Int(nullable: false),
                        rol = c.String(),
                        m_version = c.Int(nullable: false),
                        m_ref = c.String(),
                        vigente = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ServicioRol_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("SCH_CATALOGOS.catalogos", t => t.servicio_id)
                .Index(t => t.servicio_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("SCH_PROYECTOS.servicio_rol", "servicio_id", "SCH_CATALOGOS.catalogos");
            DropForeignKey("SCH_SERVICIOS.servicios_proveedores", "servicio_id", "SCH_CATALOGOS.catalogos");
            DropForeignKey("SCH_SERVICIOS.servicios_proveedores", "proveedor_id", "SCH_PROVEEDORES.proveedores");
            DropIndex("SCH_PROYECTOS.servicio_rol", new[] { "servicio_id" });
            DropIndex("SCH_SERVICIOS.servicios_proveedores", new[] { "proveedor_id" });
            DropIndex("SCH_SERVICIOS.servicios_proveedores", new[] { "servicio_id" });
            DropTable("SCH_PROYECTOS.servicio_rol",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ServicioRol_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("SCH_SERVICIOS.servicios_proveedores",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ServicioProveedor_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
