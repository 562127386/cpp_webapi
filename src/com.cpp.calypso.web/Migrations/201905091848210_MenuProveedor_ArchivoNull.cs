namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MenuProveedor_ArchivoNull : DbMigration
    {
        public override void Up()
        {
            DropIndex("SCH_PROVEEDORES.menus_proveedor", new[] { "documentacion_id" });
            AlterColumn("SCH_PROVEEDORES.menus_proveedor", "documentacion_id", c => c.Int());
            CreateIndex("SCH_PROVEEDORES.menus_proveedor", "documentacion_id");
        }
        
        public override void Down()
        {
            DropIndex("SCH_PROVEEDORES.menus_proveedor", new[] { "documentacion_id" });
            AlterColumn("SCH_PROVEEDORES.menus_proveedor", "documentacion_id", c => c.Int(nullable: false));
            CreateIndex("SCH_PROVEEDORES.menus_proveedor", "documentacion_id");
        }
    }
}
