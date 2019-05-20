namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Entidades_Transporte : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SCH_TRANSPORTES.choferes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        proveedor_id = c.Int(nullable: false),
                        catalogo_tipo_identificacion = c.Int(),
                        numero_identificacion = c.String(),
                        apellidos_nombres = c.String(),
                        nombres = c.String(),
                        apellidos = c.String(),
                        catalogo_genero_id = c.Int(),
                        catalogo_estado_civil_id = c.Int(),
                        fecha_nacimiento = c.DateTime(),
                        celular = c.String(),
                        mail = c.String(),
                        estado = c.Int(nullable: false),
                        fecha_estado = c.DateTime(),
                        fecha_creacion = c.DateTime(),
                        ultima_modificacion = c.DateTime(),
                        fecha_eliminacion = c.DateTime(),
                        UsuarioEliminacion = c.Int(),
                        usuario_creador = c.Int(),
                        usuario_ultima_modificacion = c.Int(),
                        fs = c.DateTime(),
                        fr = c.DateTime(),
                        m_version = c.Int(nullable: false),
                        m_ref = c.String(),
                        vigente = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Chofer_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SCH_CATALOGOS.catalogos", t => t.catalogo_estado_civil_id)
                .ForeignKey("SCH_CATALOGOS.catalogos", t => t.catalogo_genero_id)
                .ForeignKey("SCH_CATALOGOS.catalogos", t => t.catalogo_tipo_identificacion)
                .ForeignKey("SCH_PROVEEDORES.proveedores", t => t.proveedor_id)
                .Index(t => t.proveedor_id)
                .Index(t => t.catalogo_tipo_identificacion)
                .Index(t => t.catalogo_genero_id)
                .Index(t => t.catalogo_estado_civil_id);
            
            CreateTable(
                "SCH_TRANSPORTES.lugares",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        codigo = c.String(),
                        nombre = c.String(),
                        descripcion = c.String(),
                        latitud = c.String(),
                        longitud = c.String(),
                        fecha_creacion = c.DateTime(),
                        ultima_modificacion = c.DateTime(),
                        usuario_creador = c.Int(),
                        usuario_ultima_modificacion = c.Int(),
                        fecha_eliminacion = c.DateTime(),
                        UsuarioEliminacion = c.Int(),
                        fs = c.DateTime(),
                        fr = c.DateTime(),
                        m_version = c.Int(nullable: false),
                        m_ref = c.String(),
                        vigente = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Lugar_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "SCH_TRANSPORTES.operaciones_diarias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        vehiculo_id = c.Int(),
                        chofer_id = c.Int(),
                        fecha_inicio = c.DateTime(),
                        fecha_fin = c.DateTime(),
                        kilometraje_inicio = c.Int(nullable: false),
                        kilometraje_final = c.Int(nullable: false),
                        observacion = c.String(),
                        estado = c.String(),
                        fs = c.DateTime(),
                        fr = c.DateTime(),
                        m_version = c.Int(nullable: false),
                        m_ref = c.String(),
                        vigente = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_OperacionDiaria_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SCH_TRANSPORTES.vehiculos", t => t.vehiculo_id)
                .Index(t => t.vehiculo_id);
            
            CreateTable(
                "SCH_TRANSPORTES.vehiculos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        proveedor_id = c.Int(nullable: false),
                        codigo = c.String(),
                        catalogo_tipo_vehiculo = c.Int(),
                        numero_placa = c.String(),
                        marca = c.String(),
                        capacidad = c.Int(nullable: false),
                        anio_fabricacion = c.Int(nullable: false),
                        color = c.String(),
                        fecha_vencimiento_matricula = c.DateTime(),
                        estado = c.String(),
                        fecha_estado = c.DateTime(),
                        fecha_creacion = c.DateTime(),
                        ultima_modificacion = c.DateTime(),
                        fecha_eliminacion = c.DateTime(),
                        usuario_eliminacion = c.Int(),
                        usuario_creador = c.Int(),
                        usuario_ultima_modificacion = c.Int(),
                        fs = c.DateTime(),
                        fr = c.DateTime(),
                        m_version = c.Int(nullable: false),
                        m_ref = c.String(),
                        vigente = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Vehiculo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SCH_CATALOGOS.catalogos", t => t.catalogo_tipo_vehiculo)
                .ForeignKey("SCH_PROVEEDORES.proveedores", t => t.proveedor_id)
                .Index(t => t.proveedor_id)
                .Index(t => t.catalogo_tipo_vehiculo);
            
            CreateTable(
                "SCH_TRANSPORTES.operaciones_diarias_rutas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ruta_horario_vehiculo_id = c.Int(nullable: false),
                        operacion_diaria_id = c.Int(nullable: false),
                        ruta_horario_vehiculo_ref = c.String(),
                        operacion_diaria_ref = c.String(),
                        fecha_inicio = c.DateTime(),
                        fecha_fin = c.DateTime(),
                        fs = c.DateTime(),
                        fr = c.DateTime(),
                        m_version = c.Int(nullable: false),
                        m_ref = c.String(),
                        vigente = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_OperacionDiariaRuta_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SCH_TRANSPORTES.operaciones_diarias", t => t.operacion_diaria_id)
                .ForeignKey("SCH_TRANSPORTES.rutas_horarios_vehiculos", t => t.ruta_horario_vehiculo_id)
                .Index(t => t.ruta_horario_vehiculo_id)
                .Index(t => t.operacion_diaria_id);
            
            CreateTable(
                "SCH_TRANSPORTES.rutas_horarios_vehiculos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ruta_horario_id = c.Int(nullable: false),
                        vehiculo_id = c.Int(),
                        fecha_desde = c.DateTime(),
                        fecha_hasta = c.DateTime(),
                        hora_salida = c.Time(nullable: false, precision: 7),
                        hora_llegada = c.Time(nullable: false, precision: 7),
                        duracion = c.Int(nullable: false),
                        observacion = c.String(),
                        estado = c.Int(nullable: false),
                        fecha_asignacion = c.DateTime(),
                        usuario_asignacion = c.Int(),
                        fecha_desasignacion = c.DateTime(),
                        usuario_desasignacion = c.Int(),
                        crea_operacion = c.Boolean(nullable: false),
                        fecha_creacion = c.DateTime(),
                        ultima_modificacion = c.DateTime(),
                        usuario_creador = c.Int(),
                        usuario_ultima_modificacion = c.Int(),
                        fecha_eliminacion = c.DateTime(),
                        UsuarioEliminacion = c.Int(),
                        fs = c.DateTime(),
                        fr = c.DateTime(),
                        m_version = c.Int(nullable: false),
                        m_ref = c.String(),
                        vigente = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_RutaHorarioVehiculo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SCH_TRANSPORTES.rutas_horarios", t => t.ruta_horario_id)
                .ForeignKey("SCH_TRANSPORTES.vehiculos", t => t.vehiculo_id)
                .Index(t => t.ruta_horario_id)
                .Index(t => t.vehiculo_id);
            
            CreateTable(
                "SCH_TRANSPORTES.rutas_horarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ruta_id = c.Int(nullable: false),
                        horario = c.Time(nullable: false, precision: 7),
                        fecha_creacion = c.DateTime(),
                        ultima_modificacion = c.DateTime(),
                        usuario_creador = c.Int(),
                        usuario_ultima_modificacion = c.Int(),
                        fecha_eliminacion = c.DateTime(),
                        usuario_eliminacion = c.Int(),
                        fs = c.DateTime(),
                        fr = c.DateTime(),
                        m_version = c.Int(nullable: false),
                        m_ref = c.String(),
                        vigente = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_RutaHorario_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SCH_TRANSPORTES.rutas", t => t.ruta_id)
                .Index(t => t.ruta_id);
            
            CreateTable(
                "SCH_TRANSPORTES.rutas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        codigo = c.String(),
                        nombre = c.String(),
                        descripcion = c.String(),
                        origen_id = c.Int(nullable: false),
                        destino_id = c.Int(nullable: false),
                        distancia = c.Decimal(nullable: false, precision: 18, scale: 2),
                        fecha_creacion = c.DateTime(),
                        ultima_modificacion = c.DateTime(),
                        fecha_eliminacion = c.DateTime(),
                        usuario_eliminacion = c.Int(),
                        usuario_creador = c.Int(),
                        usuario_ultima_modificacion = c.Int(),
                        fs = c.DateTime(),
                        fr = c.DateTime(),
                        m_version = c.Int(nullable: false),
                        m_ref = c.String(),
                        vigente = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Ruta_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SCH_TRANSPORTES.lugares", t => t.destino_id)
                .ForeignKey("SCH_TRANSPORTES.lugares", t => t.origen_id)
                .Index(t => t.origen_id)
                .Index(t => t.destino_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("SCH_TRANSPORTES.operaciones_diarias_rutas", "ruta_horario_vehiculo_id", "SCH_TRANSPORTES.rutas_horarios_vehiculos");
            DropForeignKey("SCH_TRANSPORTES.rutas_horarios_vehiculos", "vehiculo_id", "SCH_TRANSPORTES.vehiculos");
            DropForeignKey("SCH_TRANSPORTES.rutas_horarios_vehiculos", "ruta_horario_id", "SCH_TRANSPORTES.rutas_horarios");
            DropForeignKey("SCH_TRANSPORTES.rutas_horarios", "ruta_id", "SCH_TRANSPORTES.rutas");
            DropForeignKey("SCH_TRANSPORTES.rutas", "origen_id", "SCH_TRANSPORTES.lugares");
            DropForeignKey("SCH_TRANSPORTES.rutas", "destino_id", "SCH_TRANSPORTES.lugares");
            DropForeignKey("SCH_TRANSPORTES.operaciones_diarias_rutas", "operacion_diaria_id", "SCH_TRANSPORTES.operaciones_diarias");
            DropForeignKey("SCH_TRANSPORTES.operaciones_diarias", "vehiculo_id", "SCH_TRANSPORTES.vehiculos");
            DropForeignKey("SCH_TRANSPORTES.vehiculos", "proveedor_id", "SCH_PROVEEDORES.proveedores");
            DropForeignKey("SCH_TRANSPORTES.vehiculos", "catalogo_tipo_vehiculo", "SCH_CATALOGOS.catalogos");
            DropForeignKey("SCH_TRANSPORTES.choferes", "proveedor_id", "SCH_PROVEEDORES.proveedores");
            DropForeignKey("SCH_TRANSPORTES.choferes", "catalogo_tipo_identificacion", "SCH_CATALOGOS.catalogos");
            DropForeignKey("SCH_TRANSPORTES.choferes", "catalogo_genero_id", "SCH_CATALOGOS.catalogos");
            DropForeignKey("SCH_TRANSPORTES.choferes", "catalogo_estado_civil_id", "SCH_CATALOGOS.catalogos");
            DropIndex("SCH_TRANSPORTES.rutas", new[] { "destino_id" });
            DropIndex("SCH_TRANSPORTES.rutas", new[] { "origen_id" });
            DropIndex("SCH_TRANSPORTES.rutas_horarios", new[] { "ruta_id" });
            DropIndex("SCH_TRANSPORTES.rutas_horarios_vehiculos", new[] { "vehiculo_id" });
            DropIndex("SCH_TRANSPORTES.rutas_horarios_vehiculos", new[] { "ruta_horario_id" });
            DropIndex("SCH_TRANSPORTES.operaciones_diarias_rutas", new[] { "operacion_diaria_id" });
            DropIndex("SCH_TRANSPORTES.operaciones_diarias_rutas", new[] { "ruta_horario_vehiculo_id" });
            DropIndex("SCH_TRANSPORTES.vehiculos", new[] { "catalogo_tipo_vehiculo" });
            DropIndex("SCH_TRANSPORTES.vehiculos", new[] { "proveedor_id" });
            DropIndex("SCH_TRANSPORTES.operaciones_diarias", new[] { "vehiculo_id" });
            DropIndex("SCH_TRANSPORTES.choferes", new[] { "catalogo_estado_civil_id" });
            DropIndex("SCH_TRANSPORTES.choferes", new[] { "catalogo_genero_id" });
            DropIndex("SCH_TRANSPORTES.choferes", new[] { "catalogo_tipo_identificacion" });
            DropIndex("SCH_TRANSPORTES.choferes", new[] { "proveedor_id" });
            DropTable("SCH_TRANSPORTES.rutas",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Ruta_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("SCH_TRANSPORTES.rutas_horarios",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_RutaHorario_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("SCH_TRANSPORTES.rutas_horarios_vehiculos",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_RutaHorarioVehiculo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("SCH_TRANSPORTES.operaciones_diarias_rutas",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_OperacionDiariaRuta_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("SCH_TRANSPORTES.vehiculos",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Vehiculo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("SCH_TRANSPORTES.operaciones_diarias",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_OperacionDiaria_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("SCH_TRANSPORTES.lugares",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Lugar_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("SCH_TRANSPORTES.choferes",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Chofer_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
