namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Valores_CatalogosNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("SCH_CATALOGOS.catalogos", "valor_numerico", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("SCH_CATALOGOS.catalogos", "valor_binario", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("SCH_CATALOGOS.catalogos", "valor_binario", c => c.Boolean(nullable: false));
            AlterColumn("SCH_CATALOGOS.catalogos", "valor_numerico", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
