namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrigenesDeConsumosNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("SCH_SERVICIOS.consumos", "origen_consumo_id", c => c.Int());
            AlterColumn("SCH_SERVICIOS.consumos_qr", "origen_consumo_id", c => c.Int());
            AlterColumn("SCH_SERVICIOS.consumos_viandas", "origen_consumo_id", c => c.Int());
            AlterColumn("SCH_SERVICIOS.consumos_viandas_qr", "origen_consumo_id", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("SCH_SERVICIOS.consumos_viandas_qr", "origen_consumo_id", c => c.Int(nullable: false));
            AlterColumn("SCH_SERVICIOS.consumos_viandas", "origen_consumo_id", c => c.Int(nullable: false));
            AlterColumn("SCH_SERVICIOS.consumos_qr", "origen_consumo_id", c => c.Int(nullable: false));
            AlterColumn("SCH_SERVICIOS.consumos", "origen_consumo_id", c => c.Int(nullable: false));
        }
    }
}
