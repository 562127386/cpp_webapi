namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Usuario_uid : DbMigration
    {
        public override void Up()
        {
            AddColumn("SCH_USUARIOS.usuarios", "uid", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("SCH_USUARIOS.usuarios", "uid");
        }
    }
}
