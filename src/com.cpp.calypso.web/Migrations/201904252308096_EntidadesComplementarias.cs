namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class EntidadesComplementarias : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("SCH_USUARIOS.AuditoriaPropiedad", "AuditEntryID", "SCH_USUARIOS.AuditoriaEntidad");
            DropForeignKey("SCH_USUARIOS.acciones", "FuncionalidadId", "SCH_USUARIOS.funcionalidades");
            DropForeignKey("SCH_USUARIOS.funcionalidades", "ModuloId", "SCH_USUARIOS.modulos");
            DropForeignKey("SCH_USUARIOS.permisos", "RolId", "SCH_USUARIOS.roles");
            DropForeignKey("SCH_USUARIOS.permisos", "AccionId", "SCH_USUARIOS.acciones");
            DropForeignKey("SCH_USUARIOS.menuitems", "MenuId", "SCH_USUARIOS.menus");
            DropForeignKey("SCH_USUARIOS.menus", "ModuloId", "SCH_USUARIOS.modulos");
            DropForeignKey("SCH_USUARIOS.parametro_opciones", "ParametroId", "SCH_USUARIOS.parametros");
            DropForeignKey("SCH_RRHH.colaboradores", "locacion_trabajo_id", "SCH_CATALOGOS.locaciones");
            CreateTable(
                "SCH_PROYECTOS.archivos",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        codigo = c.String(),
                        nombre = c.String(),
                        fecha_registro = c.DateTime(nullable: false),
                        hash = c.String(),
                        tipo_contenido = c.String(),
                        m_version = c.Int(nullable: false),
                        m_ref = c.String(),
                        vigente = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Archivo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "SCH_SERVICIOS.detalles_distribuciones",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        DistribucionViandaId = c.Int(nullable: false),
                        SolicitudViandaId = c.Int(nullable: false),
                        total_asignado = c.Int(nullable: false),
                        total_entregado = c.Int(nullable: false),
                        m_version = c.Int(nullable: false),
                        m_ref = c.String(),
                        vigente = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DetalleDistribucion_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("SCH_SERVICIOS.distribuciones_viandas", t => t.DistribucionViandaId)
                .ForeignKey("SCH_SERVICIOS.solicitudes_viandas", t => t.SolicitudViandaId)
                .Index(t => t.DistribucionViandaId)
                .Index(t => t.SolicitudViandaId);
            
            CreateTable(
                "SCH_SERVICIOS.distribuciones_viandas",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ProveedorId = c.Int(nullable: false),
                        tipo_comida_id = c.Int(nullable: false),
                        estado = c.Int(nullable: false),
                        fecha = c.DateTime(nullable: false),
                        total_pedido = c.Int(nullable: false),
                        total_entregado_transporte = c.Int(nullable: false),
                        total_justificado = c.Int(nullable: false),
                        total_liquidado = c.Int(nullable: false),
                        liquidado = c.Int(nullable: false),
                        conductor_asignado_id = c.Int(nullable: false),
                        hora_asigancion_transporte = c.DateTime(nullable: false),
                        m_version = c.Int(nullable: false),
                        m_ref = c.String(),
                        vigente = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DistribucionVianda_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("SCH_RRHH.colaboradores", t => t.conductor_asignado_id)
                .ForeignKey("SCH_PROVEEDORES.proveedores", t => t.ProveedorId)
                .ForeignKey("SCH_CATALOGOS.catalogos", t => t.tipo_comida_id)
                .Index(t => t.ProveedorId)
                .Index(t => t.tipo_comida_id)
                .Index(t => t.conductor_asignado_id);
            
            CreateTable(
                "SCH_PROVEEDORES.menus_proveedor",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        proveedor_id = c.Int(nullable: false),
                        fecha_inicial = c.DateTime(nullable: false),
                        fecha_final = c.DateTime(nullable: false),
                        aprobado = c.Boolean(nullable: false),
                        fecha_aprobacion = c.DateTime(nullable: false),
                        descripcion = c.String(),
                        documentacion_id = c.Int(nullable: false),
                        documentacion_ref = c.String(),
                        m_version = c.Int(nullable: false),
                        m_ref = c.String(),
                        vigente = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("SCH_PROYECTOS.archivos", t => t.documentacion_id)
                .ForeignKey("SCH_PROVEEDORES.proveedores", t => t.proveedor_id)
                .Index(t => t.proveedor_id)
                .Index(t => t.documentacion_id);
            
            CreateTable(
                "SCH_SERVICIOS.opciones_comidas",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        opcion_comida = c.Int(nullable: false),
                        nombre = c.String(),
                        costo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        m_version = c.Int(nullable: false),
                        m_ref = c.String(),
                        vigente = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_OpcionComida_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "SCH_SERVICIOS.tipos_acciones_empresa",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        EmpresaId = c.Int(nullable: false),
                        tipo_comida_id = c.Int(nullable: false),
                        AccionId = c.Int(nullable: false),
                        hora_desde = c.DateTime(nullable: false),
                        hora_hasta = c.DateTime(nullable: false),
                        m_version = c.Int(nullable: false),
                        m_ref = c.String(),
                        vigente = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TipoAccionEmpresa_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("SCH_CATALOGOS.catalogos", t => t.AccionId)
                .ForeignKey("SCH_PROYECTOS.empresas", t => t.EmpresaId)
                .ForeignKey("SCH_CATALOGOS.catalogos", t => t.tipo_comida_id)
                .Index(t => t.EmpresaId)
                .Index(t => t.tipo_comida_id)
                .Index(t => t.AccionId);
            
            AddForeignKey("SCH_USUARIOS.AuditoriaPropiedad", "AuditEntryID", "SCH_USUARIOS.AuditoriaEntidad", "AuditEntryID");
            AddForeignKey("SCH_USUARIOS.acciones", "FuncionalidadId", "SCH_USUARIOS.funcionalidades", "Id");
            AddForeignKey("SCH_USUARIOS.funcionalidades", "ModuloId", "SCH_USUARIOS.modulos", "Id");
            AddForeignKey("SCH_USUARIOS.permisos", "RolId", "SCH_USUARIOS.roles", "Id");
            AddForeignKey("SCH_USUARIOS.permisos", "AccionId", "SCH_USUARIOS.acciones", "Id");
            AddForeignKey("SCH_USUARIOS.menuitems", "MenuId", "SCH_USUARIOS.menus", "Id");
            AddForeignKey("SCH_USUARIOS.menus", "ModuloId", "SCH_USUARIOS.modulos", "Id");
            AddForeignKey("SCH_USUARIOS.parametro_opciones", "ParametroId", "SCH_USUARIOS.parametros", "Id");
            AddForeignKey("SCH_RRHH.colaboradores", "locacion_trabajo_id", "SCH_CATALOGOS.locaciones", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("SCH_RRHH.colaboradores", "locacion_trabajo_id", "SCH_CATALOGOS.locaciones");
            DropForeignKey("SCH_USUARIOS.parametro_opciones", "ParametroId", "SCH_USUARIOS.parametros");
            DropForeignKey("SCH_USUARIOS.menus", "ModuloId", "SCH_USUARIOS.modulos");
            DropForeignKey("SCH_USUARIOS.menuitems", "MenuId", "SCH_USUARIOS.menus");
            DropForeignKey("SCH_USUARIOS.permisos", "AccionId", "SCH_USUARIOS.acciones");
            DropForeignKey("SCH_USUARIOS.permisos", "RolId", "SCH_USUARIOS.roles");
            DropForeignKey("SCH_USUARIOS.funcionalidades", "ModuloId", "SCH_USUARIOS.modulos");
            DropForeignKey("SCH_USUARIOS.acciones", "FuncionalidadId", "SCH_USUARIOS.funcionalidades");
            DropForeignKey("SCH_USUARIOS.AuditoriaPropiedad", "AuditEntryID", "SCH_USUARIOS.AuditoriaEntidad");
            DropForeignKey("SCH_SERVICIOS.tipos_acciones_empresa", "tipo_comida_id", "SCH_CATALOGOS.catalogos");
            DropForeignKey("SCH_SERVICIOS.tipos_acciones_empresa", "EmpresaId", "SCH_PROYECTOS.empresas");
            DropForeignKey("SCH_SERVICIOS.tipos_acciones_empresa", "AccionId", "SCH_CATALOGOS.catalogos");
            DropForeignKey("SCH_PROVEEDORES.menus_proveedor", "proveedor_id", "SCH_PROVEEDORES.proveedores");
            DropForeignKey("SCH_PROVEEDORES.menus_proveedor", "documentacion_id", "SCH_PROYECTOS.archivos");
            DropForeignKey("SCH_SERVICIOS.detalles_distribuciones", "SolicitudViandaId", "SCH_SERVICIOS.solicitudes_viandas");
            DropForeignKey("SCH_SERVICIOS.detalles_distribuciones", "DistribucionViandaId", "SCH_SERVICIOS.distribuciones_viandas");
            DropForeignKey("SCH_SERVICIOS.distribuciones_viandas", "tipo_comida_id", "SCH_CATALOGOS.catalogos");
            DropForeignKey("SCH_SERVICIOS.distribuciones_viandas", "ProveedorId", "SCH_PROVEEDORES.proveedores");
            DropForeignKey("SCH_SERVICIOS.distribuciones_viandas", "conductor_asignado_id", "SCH_RRHH.colaboradores");
            DropIndex("SCH_SERVICIOS.tipos_acciones_empresa", new[] { "AccionId" });
            DropIndex("SCH_SERVICIOS.tipos_acciones_empresa", new[] { "tipo_comida_id" });
            DropIndex("SCH_SERVICIOS.tipos_acciones_empresa", new[] { "EmpresaId" });
            DropIndex("SCH_PROVEEDORES.menus_proveedor", new[] { "documentacion_id" });
            DropIndex("SCH_PROVEEDORES.menus_proveedor", new[] { "proveedor_id" });
            DropIndex("SCH_SERVICIOS.distribuciones_viandas", new[] { "conductor_asignado_id" });
            DropIndex("SCH_SERVICIOS.distribuciones_viandas", new[] { "tipo_comida_id" });
            DropIndex("SCH_SERVICIOS.distribuciones_viandas", new[] { "ProveedorId" });
            DropIndex("SCH_SERVICIOS.detalles_distribuciones", new[] { "SolicitudViandaId" });
            DropIndex("SCH_SERVICIOS.detalles_distribuciones", new[] { "DistribucionViandaId" });
            DropTable("SCH_SERVICIOS.tipos_acciones_empresa",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TipoAccionEmpresa_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("SCH_SERVICIOS.opciones_comidas",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_OpcionComida_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("SCH_PROVEEDORES.menus_proveedor");
            DropTable("SCH_SERVICIOS.distribuciones_viandas",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DistribucionVianda_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("SCH_SERVICIOS.detalles_distribuciones",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DetalleDistribucion_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("SCH_PROYECTOS.archivos",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Archivo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            AddForeignKey("SCH_RRHH.colaboradores", "locacion_trabajo_id", "SCH_CATALOGOS.locaciones", "id", cascadeDelete: true);
            AddForeignKey("SCH_USUARIOS.parametro_opciones", "ParametroId", "SCH_USUARIOS.parametros", "Id", cascadeDelete: true);
            AddForeignKey("SCH_USUARIOS.menus", "ModuloId", "SCH_USUARIOS.modulos", "Id", cascadeDelete: true);
            AddForeignKey("SCH_USUARIOS.menuitems", "MenuId", "SCH_USUARIOS.menus", "Id", cascadeDelete: true);
            AddForeignKey("SCH_USUARIOS.permisos", "AccionId", "SCH_USUARIOS.acciones", "Id", cascadeDelete: true);
            AddForeignKey("SCH_USUARIOS.permisos", "RolId", "SCH_USUARIOS.roles", "Id", cascadeDelete: true);
            AddForeignKey("SCH_USUARIOS.funcionalidades", "ModuloId", "SCH_USUARIOS.modulos", "Id", cascadeDelete: true);
            AddForeignKey("SCH_USUARIOS.acciones", "FuncionalidadId", "SCH_USUARIOS.funcionalidades", "Id", cascadeDelete: true);
            AddForeignKey("SCH_USUARIOS.AuditoriaPropiedad", "AuditEntryID", "SCH_USUARIOS.AuditoriaEntidad", "AuditEntryID", cascadeDelete: true);
        }
    }
}
