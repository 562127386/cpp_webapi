namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TiposComidaPermitidos_Colaborador : DbMigration
    {
        public override void Up()
        {
            AddColumn("SCH_RRHH.colaboradores", "TipoComidaPermitidos", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("SCH_RRHH.colaboradores", "TipoComidaPermitidos");
        }
    }
}
