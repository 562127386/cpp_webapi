namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Hospedaje_DetalleReservasNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("SCH_SERVICIOS.detalles_reservas", "origen_consumo_id", c => c.Int());
            AlterColumn("SCH_SERVICIOS.detalles_reservas", "liquidacion_detalle_id", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("SCH_SERVICIOS.detalles_reservas", "liquidacion_detalle_id", c => c.Int(nullable: false));
            AlterColumn("SCH_SERVICIOS.detalles_reservas", "origen_consumo_id", c => c.Int(nullable: false));
        }
    }
}
