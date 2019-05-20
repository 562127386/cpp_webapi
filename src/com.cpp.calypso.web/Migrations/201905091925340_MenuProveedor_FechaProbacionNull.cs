namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MenuProveedor_FechaProbacionNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("SCH_PROVEEDORES.menus_proveedor", "fecha_aprobacion", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("SCH_PROVEEDORES.menus_proveedor", "fecha_aprobacion", c => c.DateTime(nullable: false));
        }
    }
}
