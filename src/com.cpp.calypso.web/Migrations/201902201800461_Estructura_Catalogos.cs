namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Estructura_Catalogos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SCH_CATALOGOS.tipos_catalogos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        nombre = c.String(nullable: false, maxLength: 200),
                        codigo = c.String(nullable: false, maxLength: 50),
                        tipo_ordenamiento = c.String(nullable: false, maxLength: 3),
                        editable = c.Boolean(nullable: false),
                        vigente = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TipoCatalogo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "SCH_CATALOGOS.catalogos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        nombre = c.String(nullable: false, maxLength: 200),
                        codigo = c.String(nullable: false, maxLength: 20),
                        descripcion = c.String(nullable: false, maxLength: 800),
                        tipo_catalogo_id = c.Int(nullable: false),
                        predeterminado = c.Boolean(nullable: false),
                        ordinal = c.Int(nullable: false),
                        vigente = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Catalogo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SCH_CATALOGOS.tipos_catalogos", t => t.tipo_catalogo_id, cascadeDelete: true)
                .Index(t => t.tipo_catalogo_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("SCH_CATALOGOS.catalogos", "tipo_catalogo_id", "SCH_CATALOGOS.tipos_catalogos");
            DropIndex("SCH_CATALOGOS.catalogos", new[] { "tipo_catalogo_id" });
            DropTable("SCH_CATALOGOS.catalogos",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Catalogo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("SCH_CATALOGOS.tipos_catalogos",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TipoCatalogo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
