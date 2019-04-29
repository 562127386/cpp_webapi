namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameEmpresaId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "SCH_PROYECTOS.archivos", name: "tipo_contenido", newName: "contenido");
            RenameColumn(table: "SCH_SERVICIOS.tipos_acciones_empresa", name: "EmpresaId", newName: "empresa_id");
            RenameColumn(table: "SCH_SERVICIOS.tipos_acciones_empresa", name: "AccionId", newName: "accion_id");
            RenameIndex(table: "SCH_SERVICIOS.tipos_acciones_empresa", name: "IX_EmpresaId", newName: "IX_empresa_id");
            RenameIndex(table: "SCH_SERVICIOS.tipos_acciones_empresa", name: "IX_AccionId", newName: "IX_accion_id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "SCH_SERVICIOS.tipos_acciones_empresa", name: "IX_accion_id", newName: "IX_AccionId");
            RenameIndex(table: "SCH_SERVICIOS.tipos_acciones_empresa", name: "IX_empresa_id", newName: "IX_EmpresaId");
            RenameColumn(table: "SCH_SERVICIOS.tipos_acciones_empresa", name: "accion_id", newName: "AccionId");
            RenameColumn(table: "SCH_SERVICIOS.tipos_acciones_empresa", name: "empresa_id", newName: "EmpresaId");
            RenameColumn(table: "SCH_PROYECTOS.archivos", name: "contenido", newName: "tipo_contenido");
        }
    }
}
