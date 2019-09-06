namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TiposComidaPermitidos_Rename : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "SCH_RRHH.colaboradores", name: "TipoComidaPermitidos", newName: "tipos_comida_permitidos");
        }
        
        public override void Down()
        {
            RenameColumn(table: "SCH_RRHH.colaboradores", name: "tipos_comida_permitidos", newName: "TipoComidaPermitidos");
        }
    }
}
