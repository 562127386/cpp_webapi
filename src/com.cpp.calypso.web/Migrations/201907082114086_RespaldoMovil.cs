namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RespaldoMovil : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SCH_RESPALDOS.respaldos_movil",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        user_id = c.Int(nullable: false),
                        json = c.String(),
                        fecha_registro = c.DateTime(nullable: false),
                        estado = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("SCH_RESPALDOS.respaldos_movil");
        }
    }
}
