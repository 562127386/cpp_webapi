namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SolicitudReportesMovil : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SCH_SERVICIOS.reportes_movil",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        codigo = c.String(),
                        nombre = c.String(),
                        parametros = c.String(),
                        catalogo_servicio_id = c.Int(nullable: false),
                        rol = c.String(),
                        fs = c.DateTime(),
                        fr = c.DateTime(),
                        uid = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SCH_CATALOGOS.catalogos", t => t.catalogo_servicio_id)
                .Index(t => t.catalogo_servicio_id);
            
            CreateTable(
                "SCH_SERVICIOS.solicitudes_reportes_movil",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        codigo_reporte = c.String(),
                        fecha_solicitud = c.DateTime(nullable: false),
                        fecha_envio = c.DateTime(),
                        usuario_id = c.Int(nullable: false),
                        estado = c.String(),
                        parametros = c.String(),
                        correo_electronico = c.String(),
                        error = c.String(),
                        fs = c.DateTime(),
                        fr = c.DateTime(),
                        uid = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("SCH_RRHH.colaboradores", "validacion_cedula", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("SCH_SERVICIOS.reportes_movil", "catalogo_servicio_id", "SCH_CATALOGOS.catalogos");
            DropIndex("SCH_SERVICIOS.reportes_movil", new[] { "catalogo_servicio_id" });
            AlterColumn("SCH_RRHH.colaboradores", "validacion_cedula", c => c.Boolean());
            DropTable("SCH_SERVICIOS.solicitudes_reportes_movil");
            DropTable("SCH_SERVICIOS.reportes_movil");
        }
    }
}
