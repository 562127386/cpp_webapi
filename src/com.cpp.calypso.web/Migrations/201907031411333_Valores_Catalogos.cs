namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Valores_Catalogos : DbMigration
    {
        public override void Up()
        {
            AddColumn("SCH_CATALOGOS.catalogos", "valor_texto", c => c.String());
            AddColumn("SCH_CATALOGOS.catalogos", "valor_numerico", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("SCH_CATALOGOS.catalogos", "valor_fecha", c => c.DateTime());
            AddColumn("SCH_CATALOGOS.catalogos", "valor_binario", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("SCH_CATALOGOS.catalogos", "valor_binario");
            DropColumn("SCH_CATALOGOS.catalogos", "valor_fecha");
            DropColumn("SCH_CATALOGOS.catalogos", "valor_numerico");
            DropColumn("SCH_CATALOGOS.catalogos", "valor_texto");
        }
    }
}
