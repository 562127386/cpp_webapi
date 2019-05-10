namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CamposSincronizacion_Henry : DbMigration
    {
        public override void Up()
        {
            AddColumn("SCH_CATALOGOS.tipos_catalogos", "FS", c => c.DateTime());
            AddColumn("SCH_CATALOGOS.tipos_catalogos", "FR", c => c.DateTime());
            AddColumn("SCH_ACCESOS.accesos_temporal", "FS", c => c.DateTime());
            AddColumn("SCH_ACCESOS.accesos_temporal", "FR", c => c.DateTime());
            AddColumn("SCH_PROYECTOS.archivos", "FS", c => c.DateTime());
            AddColumn("SCH_PROYECTOS.archivos", "FR", c => c.DateTime());
            AddColumn("SCH_CATALOGOS.catalogos", "FS", c => c.DateTime());
            AddColumn("SCH_CATALOGOS.catalogos", "FR", c => c.DateTime());
            AddColumn("SCH_RRHH.colaboradores", "FS", c => c.DateTime());
            AddColumn("SCH_RRHH.colaboradores", "FR", c => c.DateTime());
            AddColumn("SCH_PROYECTOS.empresas", "FS", c => c.DateTime());
            AddColumn("SCH_PROYECTOS.empresas", "FR", c => c.DateTime());
            AddColumn("SCH_CATALOGOS.locaciones", "FS", c => c.DateTime());
            AddColumn("SCH_CATALOGOS.locaciones", "FR", c => c.DateTime());
            AddColumn("SCH_CATALOGOS.zonas", "FS", c => c.DateTime());
            AddColumn("SCH_CATALOGOS.zonas", "FR", c => c.DateTime());
            AddColumn("SCH_SERVICIOS.consumos", "FS", c => c.DateTime());
            AddColumn("SCH_SERVICIOS.consumos", "FR", c => c.DateTime());
            AddColumn("SCH_PROVEEDORES.proveedores", "FS", c => c.DateTime());
            AddColumn("SCH_PROVEEDORES.proveedores", "FR", c => c.DateTime());
            AddColumn("SCH_PROVEEDORES.contratos_proveedores", "FS", c => c.DateTime());
            AddColumn("SCH_PROVEEDORES.contratos_proveedores", "FR", c => c.DateTime());
            AddColumn("SCH_SERVICIOS.consumos_qr", "FS", c => c.DateTime());
            AddColumn("SCH_SERVICIOS.consumos_qr", "FR", c => c.DateTime());
            AddColumn("SCH_SERVICIOS.consumos_viandas", "FS", c => c.DateTime());
            AddColumn("SCH_SERVICIOS.consumos_viandas", "FR", c => c.DateTime());
            AddColumn("SCH_SERVICIOS.solicitudes_viandas", "FS", c => c.DateTime());
            AddColumn("SCH_SERVICIOS.solicitudes_viandas", "FR", c => c.DateTime());
            AddColumn("SCH_SERVICIOS.consumos_viandas_qr", "FS", c => c.DateTime());
            AddColumn("SCH_SERVICIOS.consumos_viandas_qr", "FR", c => c.DateTime());
            AddColumn("SCH_SERVICIOS.detalles_distribuciones", "FS", c => c.DateTime());
            AddColumn("SCH_SERVICIOS.detalles_distribuciones", "FR", c => c.DateTime());
            AddColumn("SCH_SERVICIOS.distribuciones_viandas", "FS", c => c.DateTime());
            AddColumn("SCH_SERVICIOS.distribuciones_viandas", "FR", c => c.DateTime());
            AddColumn("SCH_PROVEEDORES.menus_proveedor", "FS", c => c.DateTime());
            AddColumn("SCH_PROVEEDORES.menus_proveedor", "FR", c => c.DateTime());
            AddColumn("SCH_SERVICIOS.opciones_comidas", "FS", c => c.DateTime());
            AddColumn("SCH_SERVICIOS.opciones_comidas", "FR", c => c.DateTime());
            AddColumn("SCH_ACCESOS.registros_accesos", "FS", c => c.DateTime());
            AddColumn("SCH_ACCESOS.registros_accesos", "FR", c => c.DateTime());
            AddColumn("SCH_ACCESOS.requisitos_colaboradores", "FS", c => c.DateTime());
            AddColumn("SCH_ACCESOS.requisitos_colaboradores", "FR", c => c.DateTime());
            AddColumn("SCH_SERVICIOS.servicios_proveedores", "FS", c => c.DateTime());
            AddColumn("SCH_SERVICIOS.servicios_proveedores", "FR", c => c.DateTime());
            AddColumn("SCH_SERVICIOS.servicio_rol", "FS", c => c.DateTime());
            AddColumn("SCH_SERVICIOS.servicio_rol", "FR", c => c.DateTime());
            AddColumn("SCH_SERVICIOS.tipos_acciones_empresa", "FS", c => c.DateTime());
            AddColumn("SCH_SERVICIOS.tipos_acciones_empresa", "FR", c => c.DateTime());
            AddColumn("SCH_SERVICIOS.tipos_opciones_comidas", "FS", c => c.DateTime());
            AddColumn("SCH_SERVICIOS.tipos_opciones_comidas", "FR", c => c.DateTime());
            AddColumn("SCH_USUARIOS.usuarios_movil", "FS", c => c.DateTime());
            AddColumn("SCH_USUARIOS.usuarios_movil", "FR", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("SCH_USUARIOS.usuarios_movil", "FR");
            DropColumn("SCH_USUARIOS.usuarios_movil", "FS");
            DropColumn("SCH_SERVICIOS.tipos_opciones_comidas", "FR");
            DropColumn("SCH_SERVICIOS.tipos_opciones_comidas", "FS");
            DropColumn("SCH_SERVICIOS.tipos_acciones_empresa", "FR");
            DropColumn("SCH_SERVICIOS.tipos_acciones_empresa", "FS");
            DropColumn("SCH_SERVICIOS.servicio_rol", "FR");
            DropColumn("SCH_SERVICIOS.servicio_rol", "FS");
            DropColumn("SCH_SERVICIOS.servicios_proveedores", "FR");
            DropColumn("SCH_SERVICIOS.servicios_proveedores", "FS");
            DropColumn("SCH_ACCESOS.requisitos_colaboradores", "FR");
            DropColumn("SCH_ACCESOS.requisitos_colaboradores", "FS");
            DropColumn("SCH_ACCESOS.registros_accesos", "FR");
            DropColumn("SCH_ACCESOS.registros_accesos", "FS");
            DropColumn("SCH_SERVICIOS.opciones_comidas", "FR");
            DropColumn("SCH_SERVICIOS.opciones_comidas", "FS");
            DropColumn("SCH_PROVEEDORES.menus_proveedor", "FR");
            DropColumn("SCH_PROVEEDORES.menus_proveedor", "FS");
            DropColumn("SCH_SERVICIOS.distribuciones_viandas", "FR");
            DropColumn("SCH_SERVICIOS.distribuciones_viandas", "FS");
            DropColumn("SCH_SERVICIOS.detalles_distribuciones", "FR");
            DropColumn("SCH_SERVICIOS.detalles_distribuciones", "FS");
            DropColumn("SCH_SERVICIOS.consumos_viandas_qr", "FR");
            DropColumn("SCH_SERVICIOS.consumos_viandas_qr", "FS");
            DropColumn("SCH_SERVICIOS.solicitudes_viandas", "FR");
            DropColumn("SCH_SERVICIOS.solicitudes_viandas", "FS");
            DropColumn("SCH_SERVICIOS.consumos_viandas", "FR");
            DropColumn("SCH_SERVICIOS.consumos_viandas", "FS");
            DropColumn("SCH_SERVICIOS.consumos_qr", "FR");
            DropColumn("SCH_SERVICIOS.consumos_qr", "FS");
            DropColumn("SCH_PROVEEDORES.contratos_proveedores", "FR");
            DropColumn("SCH_PROVEEDORES.contratos_proveedores", "FS");
            DropColumn("SCH_PROVEEDORES.proveedores", "FR");
            DropColumn("SCH_PROVEEDORES.proveedores", "FS");
            DropColumn("SCH_SERVICIOS.consumos", "FR");
            DropColumn("SCH_SERVICIOS.consumos", "FS");
            DropColumn("SCH_CATALOGOS.zonas", "FR");
            DropColumn("SCH_CATALOGOS.zonas", "FS");
            DropColumn("SCH_CATALOGOS.locaciones", "FR");
            DropColumn("SCH_CATALOGOS.locaciones", "FS");
            DropColumn("SCH_PROYECTOS.empresas", "FR");
            DropColumn("SCH_PROYECTOS.empresas", "FS");
            DropColumn("SCH_RRHH.colaboradores", "FR");
            DropColumn("SCH_RRHH.colaboradores", "FS");
            DropColumn("SCH_CATALOGOS.catalogos", "FR");
            DropColumn("SCH_CATALOGOS.catalogos", "FS");
            DropColumn("SCH_PROYECTOS.archivos", "FR");
            DropColumn("SCH_PROYECTOS.archivos", "FS");
            DropColumn("SCH_ACCESOS.accesos_temporal", "FR");
            DropColumn("SCH_ACCESOS.accesos_temporal", "FS");
            DropColumn("SCH_CATALOGOS.tipos_catalogos", "FR");
            DropColumn("SCH_CATALOGOS.tipos_catalogos", "FS");
        }
    }
}
