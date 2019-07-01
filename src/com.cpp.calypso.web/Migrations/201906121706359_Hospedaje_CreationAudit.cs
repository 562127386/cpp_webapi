namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Hospedaje_CreationAudit : DbMigration
    {
        public override void Up()
        {
            AddColumn("SCH_SERVICIOS.detalles_reservas", "creator_user_id", c => c.Long());
            AddColumn("SCH_SERVICIOS.detalles_reservas", "creation_time", c => c.DateTime(nullable: false));
            AddColumn("SCH_SERVICIOS.reservas_hoteles", "creator_user_id", c => c.Long());
            AddColumn("SCH_SERVICIOS.reservas_hoteles", "creation_time", c => c.DateTime(nullable: false));
            AddColumn("SCH_SERVICIOS.espacios_habitaciones", "creator_user_id", c => c.Long());
            AddColumn("SCH_SERVICIOS.espacios_habitaciones", "creation_time", c => c.DateTime(nullable: false));
            AddColumn("SCH_SERVICIOS.habitaciones", "creator_user_id", c => c.Long());
            AddColumn("SCH_SERVICIOS.habitaciones", "creation_time", c => c.DateTime(nullable: false));
            AddColumn("SCH_SERVICIOS.reservas_hoteles_qr", "creator_user_id", c => c.Long());
            AddColumn("SCH_SERVICIOS.reservas_hoteles_qr", "creation_time", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("SCH_SERVICIOS.reservas_hoteles_qr", "creation_time");
            DropColumn("SCH_SERVICIOS.reservas_hoteles_qr", "creator_user_id");
            DropColumn("SCH_SERVICIOS.habitaciones", "creation_time");
            DropColumn("SCH_SERVICIOS.habitaciones", "creator_user_id");
            DropColumn("SCH_SERVICIOS.espacios_habitaciones", "creation_time");
            DropColumn("SCH_SERVICIOS.espacios_habitaciones", "creator_user_id");
            DropColumn("SCH_SERVICIOS.reservas_hoteles", "creation_time");
            DropColumn("SCH_SERVICIOS.reservas_hoteles", "creator_user_id");
            DropColumn("SCH_SERVICIOS.detalles_reservas", "creation_time");
            DropColumn("SCH_SERVICIOS.detalles_reservas", "creator_user_id");
        }
    }
}
