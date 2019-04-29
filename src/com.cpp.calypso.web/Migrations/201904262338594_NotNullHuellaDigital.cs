namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotNullHuellaDigital : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "SCH_RRHH.colaboradores", name: "HuellaDigital", newName: "huella_digital");
            AlterColumn("SCH_RRHH.colaboradores", "huella_digital", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("SCH_RRHH.colaboradores", "huella_digital", c => c.String());
            RenameColumn(table: "SCH_RRHH.colaboradores", name: "huella_digital", newName: "HuellaDigital");
        }
    }
}
