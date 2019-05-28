namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cambios_BDD : DbMigration
    {
        public override void Up()
        {
            AddColumn("SCH_CATALOGOS.tipos_catalogos", "uid", c => c.String());
            AddColumn("SCH_ACCESOS.accesos_temporal", "uid", c => c.String());
            AddColumn("SCH_PROYECTOS.archivos", "uid", c => c.String());
            AddColumn("SCH_CATALOGOS.catalogos", "uid", c => c.String());
            AddColumn("SCH_TRANSPORTES.choferes", "uid", c => c.String());
            AddColumn("SCH_PROVEEDORES.proveedores", "uid", c => c.String());
            AddColumn("SCH_PROVEEDORES.contratos_proveedores", "uid", c => c.String());
            AddColumn("SCH_PROYECTOS.empresas", "uid", c => c.String());
            AddColumn("SCH_RRHH.colaboradores", "uid", c => c.String());
            AddColumn("SCH_CATALOGOS.locaciones", "uid", c => c.String());
            AddColumn("SCH_CATALOGOS.zonas", "uid", c => c.String());
            AddColumn("SCH_SERVICIOS.consumos", "uid", c => c.String());
            AddColumn("SCH_SERVICIOS.consumos_qr", "uid", c => c.String());
            AddColumn("SCH_TRANSPORTES.consumos_transporte", "uid", c => c.String());
            AddColumn("SCH_TRANSPORTES.operaciones_diarias_rutas", "uid", c => c.String());
            AddColumn("SCH_TRANSPORTES.operaciones_diarias", "uid", c => c.String());
            AddColumn("SCH_TRANSPORTES.vehiculos", "uid", c => c.String());
            AddColumn("SCH_TRANSPORTES.rutas_horarios_vehiculos", "uid", c => c.String());
            AddColumn("SCH_TRANSPORTES.rutas_horarios", "uid", c => c.String());
            AddColumn("SCH_TRANSPORTES.rutas", "uid", c => c.String());
            AddColumn("SCH_TRANSPORTES.lugares", "uid", c => c.String());
            AddColumn("SCH_SERVICIOS.consumos_viandas", "uid", c => c.String());
            AddColumn("SCH_SERVICIOS.solicitudes_viandas", "uid", c => c.String());
            AddColumn("SCH_SERVICIOS.consumos_viandas_qr", "uid", c => c.String());
            AddColumn("SCH_SERVICIOS.detalles_distribuciones", "uid", c => c.String());
            AddColumn("SCH_SERVICIOS.distribuciones_viandas", "uid", c => c.String());
            AddColumn("SCH_PROVEEDORES.menus_proveedor", "uid", c => c.String());
            AddColumn("SCH_SERVICIOS.opciones_comidas", "uid", c => c.String());
            AddColumn("SCH_ACCESOS.registros_accesos", "uid", c => c.String());
            AddColumn("SCH_ACCESOS.requisitos_colaboradores", "uid", c => c.String());
            AddColumn("SCH_SERVICIOS.servicios_proveedores", "uid", c => c.String());
            AddColumn("SCH_SERVICIOS.servicio_rol", "uid", c => c.String());
            AddColumn("SCH_SERVICIOS.tipos_acciones_empresa", "uid", c => c.String());
            AddColumn("SCH_SERVICIOS.tipos_opciones_comidas", "uid", c => c.String());
            AddColumn("SCH_USUARIOS.usuarios_movil", "uid", c => c.String());
            AlterColumn("SCH_PROVEEDORES.proveedores", "usuario", c => c.Int(nullable: false));
            AlterColumn("SCH_RRHH.colaboradores", "empleado_id_sap", c => c.Int());
            AlterColumn("SCH_RRHH.colaboradores", "meta4", c => c.Int());
            AlterColumn("SCH_RRHH.colaboradores", "remuneracion_mensual", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("SCH_RRHH.colaboradores", "numero_hijos", c => c.Int());
            AlterColumn("SCH_RRHH.colaboradores", "validacion_cedula", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("SCH_RRHH.colaboradores", "validacion_cedula", c => c.Boolean(nullable: false));
            AlterColumn("SCH_RRHH.colaboradores", "numero_hijos", c => c.Int(nullable: false));
            AlterColumn("SCH_RRHH.colaboradores", "remuneracion_mensual", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("SCH_RRHH.colaboradores", "meta4", c => c.Int(nullable: false));
            AlterColumn("SCH_RRHH.colaboradores", "empleado_id_sap", c => c.Int(nullable: false));
            AlterColumn("SCH_PROVEEDORES.proveedores", "usuario", c => c.String());
            DropColumn("SCH_USUARIOS.usuarios_movil", "uid");
            DropColumn("SCH_SERVICIOS.tipos_opciones_comidas", "uid");
            DropColumn("SCH_SERVICIOS.tipos_acciones_empresa", "uid");
            DropColumn("SCH_SERVICIOS.servicio_rol", "uid");
            DropColumn("SCH_SERVICIOS.servicios_proveedores", "uid");
            DropColumn("SCH_ACCESOS.requisitos_colaboradores", "uid");
            DropColumn("SCH_ACCESOS.registros_accesos", "uid");
            DropColumn("SCH_SERVICIOS.opciones_comidas", "uid");
            DropColumn("SCH_PROVEEDORES.menus_proveedor", "uid");
            DropColumn("SCH_SERVICIOS.distribuciones_viandas", "uid");
            DropColumn("SCH_SERVICIOS.detalles_distribuciones", "uid");
            DropColumn("SCH_SERVICIOS.consumos_viandas_qr", "uid");
            DropColumn("SCH_SERVICIOS.solicitudes_viandas", "uid");
            DropColumn("SCH_SERVICIOS.consumos_viandas", "uid");
            DropColumn("SCH_TRANSPORTES.lugares", "uid");
            DropColumn("SCH_TRANSPORTES.rutas", "uid");
            DropColumn("SCH_TRANSPORTES.rutas_horarios", "uid");
            DropColumn("SCH_TRANSPORTES.rutas_horarios_vehiculos", "uid");
            DropColumn("SCH_TRANSPORTES.vehiculos", "uid");
            DropColumn("SCH_TRANSPORTES.operaciones_diarias", "uid");
            DropColumn("SCH_TRANSPORTES.operaciones_diarias_rutas", "uid");
            DropColumn("SCH_TRANSPORTES.consumos_transporte", "uid");
            DropColumn("SCH_SERVICIOS.consumos_qr", "uid");
            DropColumn("SCH_SERVICIOS.consumos", "uid");
            DropColumn("SCH_CATALOGOS.zonas", "uid");
            DropColumn("SCH_CATALOGOS.locaciones", "uid");
            DropColumn("SCH_RRHH.colaboradores", "uid");
            DropColumn("SCH_PROYECTOS.empresas", "uid");
            DropColumn("SCH_PROVEEDORES.contratos_proveedores", "uid");
            DropColumn("SCH_PROVEEDORES.proveedores", "uid");
            DropColumn("SCH_TRANSPORTES.choferes", "uid");
            DropColumn("SCH_CATALOGOS.catalogos", "uid");
            DropColumn("SCH_PROYECTOS.archivos", "uid");
            DropColumn("SCH_ACCESOS.accesos_temporal", "uid");
            DropColumn("SCH_CATALOGOS.tipos_catalogos", "uid");
        }
    }
}
