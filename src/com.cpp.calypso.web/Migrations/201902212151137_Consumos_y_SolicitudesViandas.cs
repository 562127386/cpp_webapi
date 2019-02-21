namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Consumos_y_SolicitudesViandas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SCH_RRHH.colaboradores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        catalogo_tipo_identificacion_id = c.Int(nullable: false),
                        catalogo_genero_id = c.Int(nullable: false),
                        catalogo_etnia_id = c.Int(nullable: false),
                        catalogo_estado_civil_id = c.Int(nullable: false),
                        catalogo_codigo_siniestro_id = c.Int(),
                        catalogo_formacion_educativa_id = c.Int(),
                        catalogo_grupo_personal_id = c.Int(nullable: false),
                        catalogo_destino_estancia_id = c.Int(nullable: false),
                        catalogo_sitio_trabajo_id = c.String(),
                        catalogo_area_id = c.Int(nullable: false),
                        catalogo_cargo_id = c.Int(nullable: false),
                        catalogo_vinculo_laboral_id = c.Int(nullable: false),
                        catalogo_clase_id = c.Int(nullable: false),
                        catalogo_plan_beneficios_id = c.Int(nullable: false),
                        catalogo_plan_salud_id = c.Int(nullable: false),
                        catalogo_cobertura_dependiente_id = c.Int(nullable: false),
                        catalogo_planes_beneficios_id = c.Int(nullable: false),
                        catalogo_asociacion_id = c.Int(nullable: false),
                        catalogo_apto_medico_id = c.Int(nullable: false),
                        catalogo_division_personal_id = c.Int(nullable: false),
                        catalogo_subdivision_personal_id = c.Int(nullable: false),
                        catalogo_tipo_contrato_id = c.Int(nullable: false),
                        catalogo_clase_contrato_id = c.Int(nullable: false),
                        catalogo_funcion_id = c.Int(nullable: false),
                        catalogo_tipo_nomina_id = c.Int(nullable: false),
                        catalogo_periodo_nomina_id = c.Int(nullable: false),
                        catalogo_forma_pago_id = c.Int(nullable: false),
                        catalogo_grupo_id = c.Int(nullable: false),
                        catalogo_subgrupo_id = c.Int(nullable: false),
                        catalogo_banco_id = c.Int(nullable: false),
                        catalogo_tipo_cuenta_id = c.Int(nullable: false),
                        empresa_id = c.Int(nullable: false),
                        pais_pais_nacimiento_id = c.Int(nullable: false),
                        locacion_trabajo_id = c.Int(nullable: false),
                        usuario_id = c.Int(nullable: false),
                        contrato_id = c.Int(),
                        turno_id = c.Int(nullable: false),
                        horario_desde = c.Time(nullable: false, precision: 7),
                        horario_hasta = c.Time(nullable: false, precision: 7),
                        empleado_id_sap = c.Int(nullable: false),
                        candidato_id_sap = c.Int(nullable: false),
                        numero_identificacion = c.String(maxLength: 25),
                        nombres = c.String(),
                        fecha_ingreso = c.DateTime(),
                        primer_apellido = c.String(),
                        segundo_apellido = c.String(),
                        fecha_nacimiento = c.DateTime(),
                        fecha_matrimonio = c.DateTime(),
                        meta4 = c.Int(nullable: false),
                        posicion = c.String(),
                        fecha_caducidad_contrato = c.DateTime(),
                        ejecutor_obra = c.Boolean(),
                        remuneracion_mensual = c.Decimal(nullable: false, precision: 18, scale: 2),
                        numero_cuenta = c.String(),
                        numero_legajo_temporal = c.String(),
                        numero_legajo_definitivo = c.String(),
                        numero_hijos = c.Int(nullable: false),
                        estado = c.String(),
                        registro_masivo = c.Boolean(nullable: false),
                        catalogo_encuadre_id = c.Int(nullable: false),
                        catalogo_encargado_personal_id = c.Int(nullable: false),
                        validacion_cedula = c.Boolean(nullable: false),
                        nombres_apellidos = c.String(),
                        catalogo_codigo_incapacidad_id = c.Int(),
                        catalogo_sector_id = c.Int(),
                        catalogo_via_pago_id = c.Int(),
                        fecha_alta = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Colaborador_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "SCH_SERVICIOS.consumos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        proveedor_id = c.Int(nullable: false),
                        colaborador_id = c.Int(nullable: false),
                        fecha = c.DateTime(nullable: false),
                        tipo_comida_id = c.Int(nullable: false),
                        opcion_comida = c.Int(nullable: false),
                        observacion = c.String(nullable: false, maxLength: 500),
                        origen_consumo_id = c.Int(nullable: false),
                        vigente = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Consumo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SCH_RRHH.colaboradores", t => t.colaborador_id)
                .ForeignKey("SCH_CATALOGOS.catalogos", t => t.opcion_comida)
                .ForeignKey("SCH_PROVEEDORES.proveedores", t => t.proveedor_id, cascadeDelete: true)
                .ForeignKey("SCH_CATALOGOS.catalogos", t => t.tipo_comida_id)
                .Index(t => t.proveedor_id)
                .Index(t => t.colaborador_id)
                .Index(t => t.tipo_comida_id)
                .Index(t => t.opcion_comida);
            
            CreateTable(
                "SCH_SERVICIOS.consumos_qr",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        proveedor_id = c.Int(nullable: false),
                        tipo_comida_id = c.Int(nullable: false),
                        opcion_comida = c.Int(nullable: false),
                        cedula = c.String(nullable: false, maxLength: 15),
                        fecha_consumo = c.DateTime(nullable: false),
                        usuario_generador = c.Int(nullable: false),
                        origen_consumo_id = c.Int(nullable: false),
                        vigente = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ConsumoQr_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SCH_CATALOGOS.catalogos", t => t.opcion_comida)
                .ForeignKey("SCH_PROVEEDORES.proveedores", t => t.proveedor_id, cascadeDelete: true)
                .ForeignKey("SCH_CATALOGOS.catalogos", t => t.tipo_comida_id)
                .Index(t => t.proveedor_id)
                .Index(t => t.tipo_comida_id)
                .Index(t => t.opcion_comida);
            
            CreateTable(
                "SCH_SERVICIOS.consumos_viandas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        solicitud_vianda_id = c.Int(nullable: false),
                        colaborador_id = c.Int(nullable: false),
                        fecha_consumo_vianda = c.DateTime(nullable: false),
                        en_sitio = c.Int(nullable: false),
                        observaciones = c.String(nullable: false, maxLength: 500),
                        origen_consumo_id = c.Int(nullable: false),
                        vigente = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ConsumoVianda_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SCH_RRHH.colaboradores", t => t.colaborador_id)
                .ForeignKey("SCH_SERVICIOS.solicitudes_viandas", t => t.solicitud_vianda_id)
                .Index(t => t.solicitud_vianda_id)
                .Index(t => t.colaborador_id);
            
            CreateTable(
                "SCH_SERVICIOS.solicitudes_viandas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        solicitante_id = c.Int(nullable: false),
                        locacion_id = c.Int(nullable: false),
                        tipo_comida_id = c.Int(nullable: false),
                        disciplina_id = c.Int(nullable: false),
                        area_id = c.Int(nullable: false),
                        fecha_solicitud = c.DateTime(nullable: false),
                        fecha_alcancce = c.DateTime(),
                        pedido_viandas = c.Int(nullable: false),
                        alcance_viandas = c.Int(nullable: false),
                        total_pedido = c.Int(nullable: false),
                        consumido = c.Int(nullable: false),
                        consumo_justificado = c.Int(nullable: false),
                        total_consumido = c.Int(nullable: false),
                        estado = c.Int(nullable: false),
                        solicitud_original_id = c.Int(),
                        referencia_ubicacion = c.String(maxLength: 200),
                        observaciones = c.String(maxLength: 255),
                        anotador_id = c.Int(),
                        hora_entrega_restaurante = c.DateTime(),
                        hora_entrega_transportista = c.DateTime(),
                        vigente = c.Boolean(nullable: false),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SolicitudVianda_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SCH_RRHH.colaboradores", t => t.anotador_id)
                .ForeignKey("SCH_CATALOGOS.catalogos", t => t.area_id)
                .ForeignKey("SCH_CATALOGOS.catalogos", t => t.disciplina_id)
                .ForeignKey("SCH_CATALOGOS.locaciones", t => t.locacion_id)
                .ForeignKey("SCH_RRHH.colaboradores", t => t.solicitante_id)
                .ForeignKey("SCH_CATALOGOS.catalogos", t => t.tipo_comida_id)
                .Index(t => t.solicitante_id)
                .Index(t => t.locacion_id)
                .Index(t => t.tipo_comida_id)
                .Index(t => t.disciplina_id)
                .Index(t => t.area_id)
                .Index(t => t.anotador_id);
            
            CreateTable(
                "SCH_CATALOGOS.locaciones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        codigo = c.Int(nullable: false),
                        zona_id = c.Int(nullable: false),
                        nombre = c.String(nullable: false, maxLength: 200),
                        vigente = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Locacion_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SCH_CATALOGOS.zonas", t => t.zona_id, cascadeDelete: true)
                .Index(t => t.zona_id);
            
            CreateTable(
                "SCH_CATALOGOS.zonas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        codigo = c.String(nullable: false, maxLength: 10),
                        nombre = c.String(nullable: false, maxLength: 60),
                        descripcion = c.String(nullable: false, maxLength: 400),
                        vigente = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Zona_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "SCH_SERVICIOS.consumos_viandas_qr",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        solicitud_vianda_id = c.Int(nullable: false),
                        identificacion = c.String(nullable: false, maxLength: 15),
                        fecha_consumo_vianda = c.DateTime(nullable: false),
                        origen_consumo_id = c.Int(nullable: false),
                        vigente = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ConsumoViandaQr_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SCH_SERVICIOS.solicitudes_viandas", t => t.solicitud_vianda_id)
                .Index(t => t.solicitud_vianda_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("SCH_SERVICIOS.consumos_viandas_qr", "solicitud_vianda_id", "SCH_SERVICIOS.solicitudes_viandas");
            DropForeignKey("SCH_SERVICIOS.consumos_viandas", "solicitud_vianda_id", "SCH_SERVICIOS.solicitudes_viandas");
            DropForeignKey("SCH_SERVICIOS.solicitudes_viandas", "tipo_comida_id", "SCH_CATALOGOS.catalogos");
            DropForeignKey("SCH_SERVICIOS.solicitudes_viandas", "solicitante_id", "SCH_RRHH.colaboradores");
            DropForeignKey("SCH_SERVICIOS.solicitudes_viandas", "locacion_id", "SCH_CATALOGOS.locaciones");
            DropForeignKey("SCH_CATALOGOS.locaciones", "zona_id", "SCH_CATALOGOS.zonas");
            DropForeignKey("SCH_SERVICIOS.solicitudes_viandas", "disciplina_id", "SCH_CATALOGOS.catalogos");
            DropForeignKey("SCH_SERVICIOS.solicitudes_viandas", "area_id", "SCH_CATALOGOS.catalogos");
            DropForeignKey("SCH_SERVICIOS.solicitudes_viandas", "anotador_id", "SCH_RRHH.colaboradores");
            DropForeignKey("SCH_SERVICIOS.consumos_viandas", "colaborador_id", "SCH_RRHH.colaboradores");
            DropForeignKey("SCH_SERVICIOS.consumos_qr", "tipo_comida_id", "SCH_CATALOGOS.catalogos");
            DropForeignKey("SCH_SERVICIOS.consumos_qr", "proveedor_id", "SCH_PROVEEDORES.proveedores");
            DropForeignKey("SCH_SERVICIOS.consumos_qr", "opcion_comida", "SCH_CATALOGOS.catalogos");
            DropForeignKey("SCH_SERVICIOS.consumos", "tipo_comida_id", "SCH_CATALOGOS.catalogos");
            DropForeignKey("SCH_SERVICIOS.consumos", "proveedor_id", "SCH_PROVEEDORES.proveedores");
            DropForeignKey("SCH_SERVICIOS.consumos", "opcion_comida", "SCH_CATALOGOS.catalogos");
            DropForeignKey("SCH_SERVICIOS.consumos", "colaborador_id", "SCH_RRHH.colaboradores");
            DropIndex("SCH_SERVICIOS.consumos_viandas_qr", new[] { "solicitud_vianda_id" });
            DropIndex("SCH_CATALOGOS.locaciones", new[] { "zona_id" });
            DropIndex("SCH_SERVICIOS.solicitudes_viandas", new[] { "anotador_id" });
            DropIndex("SCH_SERVICIOS.solicitudes_viandas", new[] { "area_id" });
            DropIndex("SCH_SERVICIOS.solicitudes_viandas", new[] { "disciplina_id" });
            DropIndex("SCH_SERVICIOS.solicitudes_viandas", new[] { "tipo_comida_id" });
            DropIndex("SCH_SERVICIOS.solicitudes_viandas", new[] { "locacion_id" });
            DropIndex("SCH_SERVICIOS.solicitudes_viandas", new[] { "solicitante_id" });
            DropIndex("SCH_SERVICIOS.consumos_viandas", new[] { "colaborador_id" });
            DropIndex("SCH_SERVICIOS.consumos_viandas", new[] { "solicitud_vianda_id" });
            DropIndex("SCH_SERVICIOS.consumos_qr", new[] { "opcion_comida" });
            DropIndex("SCH_SERVICIOS.consumos_qr", new[] { "tipo_comida_id" });
            DropIndex("SCH_SERVICIOS.consumos_qr", new[] { "proveedor_id" });
            DropIndex("SCH_SERVICIOS.consumos", new[] { "opcion_comida" });
            DropIndex("SCH_SERVICIOS.consumos", new[] { "tipo_comida_id" });
            DropIndex("SCH_SERVICIOS.consumos", new[] { "colaborador_id" });
            DropIndex("SCH_SERVICIOS.consumos", new[] { "proveedor_id" });
            DropTable("SCH_SERVICIOS.consumos_viandas_qr",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ConsumoViandaQr_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("SCH_CATALOGOS.zonas",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Zona_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("SCH_CATALOGOS.locaciones",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Locacion_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("SCH_SERVICIOS.solicitudes_viandas",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SolicitudVianda_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("SCH_SERVICIOS.consumos_viandas",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ConsumoVianda_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("SCH_SERVICIOS.consumos_qr",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ConsumoQr_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("SCH_SERVICIOS.consumos",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Consumo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("SCH_RRHH.colaboradores",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Colaborador_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
