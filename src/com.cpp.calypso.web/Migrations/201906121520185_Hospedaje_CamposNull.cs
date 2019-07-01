namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Hospedaje_CamposNull : DbMigration
    {
        public override void Up()
        {
            DropIndex("SCH_SERVICIOS.espacios_habitaciones", new[] { "habitacion_id" });
            DropIndex("SCH_SERVICIOS.detalles_reservas_qr", new[] { "reserva_hotel_qr_id" });
            AlterColumn("SCH_SERVICIOS.espacios_habitaciones", "habitacion_id", c => c.Int());
            AlterColumn("SCH_SERVICIOS.detalles_reservas_qr", "reserva_hotel_qr_id", c => c.Int());
            CreateIndex("SCH_SERVICIOS.espacios_habitaciones", "habitacion_id");
            CreateIndex("SCH_SERVICIOS.detalles_reservas_qr", "reserva_hotel_qr_id");
        }
        
        public override void Down()
        {
            DropIndex("SCH_SERVICIOS.detalles_reservas_qr", new[] { "reserva_hotel_qr_id" });
            DropIndex("SCH_SERVICIOS.espacios_habitaciones", new[] { "habitacion_id" });
            AlterColumn("SCH_SERVICIOS.detalles_reservas_qr", "reserva_hotel_qr_id", c => c.Int(nullable: false));
            AlterColumn("SCH_SERVICIOS.espacios_habitaciones", "habitacion_id", c => c.Int(nullable: false));
            CreateIndex("SCH_SERVICIOS.detalles_reservas_qr", "reserva_hotel_qr_id");
            CreateIndex("SCH_SERVICIOS.espacios_habitaciones", "habitacion_id");
        }
    }
}
