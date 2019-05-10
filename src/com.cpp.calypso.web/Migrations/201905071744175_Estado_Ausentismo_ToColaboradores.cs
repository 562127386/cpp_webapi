namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Estado_Ausentismo_ToColaboradores : DbMigration
    {
        public override void Up()
        {
            AddColumn("SCH_RRHH.colaboradores", "tiene_ausentismo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("SCH_RRHH.colaboradores", "tiene_ausentismo");
        }
    }
}
