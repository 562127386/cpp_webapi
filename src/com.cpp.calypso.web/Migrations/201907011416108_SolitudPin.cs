namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SolitudPin : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SCH_USUARIOS.solicitudes_pin",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        fecha_solicitud = c.DateTime(nullable: false),
                        fecha_envio = c.DateTime(),
                        usuario_id = c.Int(),
                        estado = c.String(),
                        pin = c.String(),
                        correo_electronico = c.String(),
                        error = c.String(),
                        fs = c.DateTime(),
                        fr = c.DateTime(),
                        uid = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("SCH_USUARIOS.solicitudes_pin");
        }
    }
}
