namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SolicitudViandas_AreaNull : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("SCH_SERVICIOS.solicitudes_viandas", "area_id", "SCH_CATALOGOS.catalogos");
            DropIndex("SCH_SERVICIOS.solicitudes_viandas", new[] { "area_id" });
            AlterColumn("SCH_SERVICIOS.solicitudes_viandas", "area_id", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("SCH_SERVICIOS.solicitudes_viandas", "area_id", c => c.Int(nullable: false));
            CreateIndex("SCH_SERVICIOS.solicitudes_viandas", "area_id");
            AddForeignKey("SCH_SERVICIOS.solicitudes_viandas", "area_id", "SCH_CATALOGOS.catalogos", "id");
        }
    }
}
