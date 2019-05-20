namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RutaHorarioVehiculo_Estado : DbMigration
    {
        public override void Up()
        {
            AlterColumn("SCH_TRANSPORTES.rutas_horarios_vehiculos", "estado", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("SCH_TRANSPORTES.rutas_horarios_vehiculos", "estado", c => c.Int(nullable: false));
        }
    }
}
