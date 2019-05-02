namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Schema_ServicioRol : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "SCH_PROYECTOS.servicio_rol", newSchema: "SCH_SERVICIOS");
        }
        
        public override void Down()
        {
            MoveTable(name: "SCH_SERVICIOS.servicio_rol", newSchema: "SCH_PROYECTOS");
        }
    }
}
