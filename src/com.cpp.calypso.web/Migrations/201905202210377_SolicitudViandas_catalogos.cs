namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SolicitudViandas_catalogos : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("SCH_SERVICIOS.solicitudes_viandas", "anotador_id", "SCH_RRHH.colaboradores");
            DropForeignKey("SCH_SERVICIOS.solicitudes_viandas", "locacion_id", "SCH_CATALOGOS.catalogos");
            DropForeignKey("SCH_SERVICIOS.solicitudes_viandas", "solicitante_id", "SCH_RRHH.colaboradores");
            DropIndex("SCH_SERVICIOS.solicitudes_viandas", new[] { "solicitante_id" });
            DropIndex("SCH_SERVICIOS.solicitudes_viandas", new[] { "locacion_id" });
            DropIndex("SCH_SERVICIOS.solicitudes_viandas", new[] { "anotador_id" });
            AlterColumn("SCH_SERVICIOS.solicitudes_viandas", "locacion_id", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("SCH_SERVICIOS.solicitudes_viandas", "locacion_id", c => c.Int(nullable: false));
            CreateIndex("SCH_SERVICIOS.solicitudes_viandas", "anotador_id");
            CreateIndex("SCH_SERVICIOS.solicitudes_viandas", "locacion_id");
            CreateIndex("SCH_SERVICIOS.solicitudes_viandas", "solicitante_id");
            AddForeignKey("SCH_SERVICIOS.solicitudes_viandas", "solicitante_id", "SCH_RRHH.colaboradores", "id");
            AddForeignKey("SCH_SERVICIOS.solicitudes_viandas", "locacion_id", "SCH_CATALOGOS.catalogos", "id");
            AddForeignKey("SCH_SERVICIOS.solicitudes_viandas", "anotador_id", "SCH_RRHH.colaboradores", "id");
        }
    }
}
