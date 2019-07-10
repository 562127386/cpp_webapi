namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SincronizacionesLog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SCH_RESPALDOS.sincronizaciones_log",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        usuario_id = c.Int(nullable: false),
                        log = c.String(),
                        fecha_registro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("SCH_RESPALDOS.sincronizaciones_log");
        }
    }
}
