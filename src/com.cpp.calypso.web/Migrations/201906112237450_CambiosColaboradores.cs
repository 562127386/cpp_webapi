namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CambiosColaboradores : DbMigration
    {
        public override void Up()
        {
            AddColumn("SCH_RRHH.colaboradores", "tiene_derecho_alimentacion", c => c.Boolean(nullable: false));
            AddColumn("SCH_RRHH.colaboradores", "tiene_derecho_hospedaje", c => c.Boolean(nullable: false));
            AddColumn("SCH_RRHH.colaboradores", "tiene_derecho_transporte", c => c.Boolean(nullable: false));
            AddColumn("SCH_RRHH.colaboradores", "tiene_acceso", c => c.Boolean(nullable: false));
            AddColumn("SCH_RRHH.colaboradores", "observaciones_acceso", c => c.String());
            AlterColumn("SCH_RRHH.colaboradores", "huella_digital", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("SCH_RRHH.colaboradores", "huella_digital", c => c.String(nullable: false));
            DropColumn("SCH_RRHH.colaboradores", "observaciones_acceso");
            DropColumn("SCH_RRHH.colaboradores", "tiene_acceso");
            DropColumn("SCH_RRHH.colaboradores", "tiene_derecho_transporte");
            DropColumn("SCH_RRHH.colaboradores", "tiene_derecho_hospedaje");
            DropColumn("SCH_RRHH.colaboradores", "tiene_derecho_alimentacion");
        }
    }
}
