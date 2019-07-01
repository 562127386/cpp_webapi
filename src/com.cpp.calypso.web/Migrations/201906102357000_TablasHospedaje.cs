namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class TablasHospedaje : DbMigration
    {
        public override void Up()
        {
            DropIndex("SCH_RRHH.colaboradores", new[] { "locacion_trabajo_id" });
            CreateTable(
                "SCH_SERVICIOS.consumos_sin_reserva_hospedaje",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        proveedor_id = c.Int(nullable: false),
                        colaborador_id = c.Int(nullable: false),
                        identificacion_colaborador = c.String(),
                        nombres = c.String(),
                        primer_apellido = c.String(),
                        segundo_apellido = c.String(),
                        origen_consumo_id = c.Int(),
                        tipo_habitacion_id = c.Int(nullable: false),
                        fecha_registro = c.DateTime(),
                        numero_habitacion = c.Int(nullable: false),
                        autorizacion = c.Int(nullable: false),
                        fs = c.DateTime(),
                        fr = c.DateTime(),
                        m_version = c.Int(nullable: false),
                        m_ref = c.String(),
                        vigente = c.Boolean(nullable: false),
                        uid = c.String(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ConsumoSinReservaHospedaje_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SCH_RRHH.colaboradores", t => t.colaborador_id)
                .ForeignKey("SCH_PROVEEDORES.proveedores", t => t.proveedor_id)
                .ForeignKey("SCH_CATALOGOS.catalogos", t => t.tipo_habitacion_id)
                .Index(t => t.proveedor_id)
                .Index(t => t.colaborador_id)
                .Index(t => t.tipo_habitacion_id);
            
            CreateTable(
                "SCH_SERVICIOS.detalles_reservas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        reserva_hotel_id = c.Int(nullable: false),
                        origen_consumo_id = c.Int(nullable: false),
                        fecha_reserva = c.DateTime(),
                        fecha_consumo = c.DateTime(),
                        consumido = c.Boolean(nullable: false),
                        facturado = c.Boolean(nullable: false),
                        liquidacion_detalle_id = c.Int(nullable: false),
                        fs = c.DateTime(),
                        fr = c.DateTime(),
                        m_version = c.Int(nullable: false),
                        m_ref = c.String(),
                        vigente = c.Boolean(nullable: false),
                        uid = c.String(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DetalleReserva_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SCH_SERVICIOS.reservas_hoteles", t => t.reserva_hotel_id)
                .Index(t => t.reserva_hotel_id);
            
            CreateTable(
                "SCH_SERVICIOS.reservas_hoteles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        espacio_Habitacion_id = c.Int(nullable: false),
                        colaborador_id = c.Int(nullable: false),
                        fecha_registro = c.DateTime(),
                        fecha_desde = c.DateTime(),
                        fecha_hasta = c.DateTime(),
                        estado = c.Boolean(nullable: false),
                        identificacion_colaborador = c.String(),
                        nombres = c.String(),
                        primer_apellido = c.String(),
                        segundo_apellido = c.String(),
                        fs = c.DateTime(),
                        fr = c.DateTime(),
                        m_version = c.Int(nullable: false),
                        m_ref = c.String(),
                        vigente = c.Boolean(nullable: false),
                        uid = c.String(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ReservaHotel_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SCH_RRHH.colaboradores", t => t.colaborador_id)
                .ForeignKey("SCH_SERVICIOS.espacios_habitaciones", t => t.espacio_Habitacion_id)
                .Index(t => t.espacio_Habitacion_id)
                .Index(t => t.colaborador_id);
            
            CreateTable(
                "SCH_SERVICIOS.espacios_habitaciones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        habitacion_id = c.Int(nullable: false),
                        habitacion_ref = c.String(),
                        codigo_espacio = c.String(),
                        estado = c.Boolean(nullable: false),
                        activo = c.Boolean(nullable: false),
                        observaciones = c.String(),
                        fs = c.DateTime(),
                        fr = c.DateTime(),
                        m_version = c.Int(nullable: false),
                        m_ref = c.String(),
                        vigente = c.Boolean(nullable: false),
                        uid = c.String(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_EspacioHabitacion_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SCH_SERVICIOS.habitaciones", t => t.habitacion_id)
                .Index(t => t.habitacion_id);
            
            CreateTable(
                "SCH_SERVICIOS.habitaciones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        proveedor_id = c.Int(nullable: false),
                        numero_habitacion = c.String(),
                        tipo_habitacion_id = c.Int(nullable: false),
                        capacidad = c.Int(nullable: false),
                        estado = c.Boolean(nullable: false),
                        aprobado = c.Boolean(nullable: false),
                        fecha_aprobacion = c.DateTime(),
                        fs = c.DateTime(),
                        fr = c.DateTime(),
                        m_version = c.Int(nullable: false),
                        m_ref = c.String(),
                        vigente = c.Boolean(nullable: false),
                        uid = c.String(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Habitacion_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SCH_PROVEEDORES.proveedores", t => t.proveedor_id)
                .ForeignKey("SCH_CATALOGOS.catalogos", t => t.tipo_habitacion_id)
                .Index(t => t.proveedor_id)
                .Index(t => t.tipo_habitacion_id);
            
            CreateTable(
                "SCH_SERVICIOS.detalles_reservas_qr",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        reserva_hotel_qr_id = c.Int(nullable: false),
                        reserva_hotel_qr_ref = c.String(),
                        origen_consumo_id = c.Int(nullable: false),
                        fecha_reserva = c.DateTime(),
                        fecha_consumo = c.DateTime(),
                        consumido = c.Boolean(nullable: false),
                        facturado = c.Boolean(nullable: false),
                        liquidacion_detalle_id = c.Int(nullable: false),
                        fs = c.DateTime(),
                        fr = c.DateTime(),
                        m_version = c.Int(nullable: false),
                        m_ref = c.String(),
                        vigente = c.Boolean(nullable: false),
                        uid = c.String(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DetalleReservaQr_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SCH_SERVICIOS.reservas_hoteles_qr", t => t.reserva_hotel_qr_id)
                .Index(t => t.reserva_hotel_qr_id);
            
            CreateTable(
                "SCH_SERVICIOS.reservas_hoteles_qr",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        espacio_habitacion_id = c.Int(nullable: false),
                        identificacion_colaborador = c.String(),
                        fecha_registro = c.DateTime(),
                        fecha_desde = c.DateTime(),
                        fecha_hasta = c.DateTime(),
                        fs = c.DateTime(),
                        fr = c.DateTime(),
                        m_version = c.Int(nullable: false),
                        m_ref = c.String(),
                        vigente = c.Boolean(nullable: false),
                        uid = c.String(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ReservaHotelQr_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SCH_SERVICIOS.espacios_habitaciones", t => t.espacio_habitacion_id)
                .Index(t => t.espacio_habitacion_id);
            
            AlterColumn("SCH_PROVEEDORES.proveedores", "usuario", c => c.Int());
            AlterColumn("SCH_RRHH.colaboradores", "pais_pais_nacimiento_id", c => c.Int());
            AlterColumn("SCH_RRHH.colaboradores", "locacion_trabajo_id", c => c.Int());
            AlterColumn("SCH_RRHH.colaboradores", "usuario_id", c => c.Int());
            AlterColumn("SCH_RRHH.colaboradores", "turno_id", c => c.Int());
            AlterColumn("SCH_RRHH.colaboradores", "horario_desde", c => c.Time(precision: 7));
            AlterColumn("SCH_RRHH.colaboradores", "horario_hasta", c => c.Time(precision: 7));
            AlterColumn("SCH_RRHH.colaboradores", "candidato_id_sap", c => c.Int());
            AlterColumn("SCH_RRHH.colaboradores", "registro_masivo", c => c.Boolean());
            AlterColumn("SCH_SERVICIOS.distribuciones_viandas", "hora_asigancion_transporte", c => c.DateTime());
            CreateIndex("SCH_RRHH.colaboradores", "locacion_trabajo_id");
        }
        
        public override void Down()
        {
            DropForeignKey("SCH_SERVICIOS.detalles_reservas_qr", "reserva_hotel_qr_id", "SCH_SERVICIOS.reservas_hoteles_qr");
            DropForeignKey("SCH_SERVICIOS.reservas_hoteles_qr", "espacio_habitacion_id", "SCH_SERVICIOS.espacios_habitaciones");
            DropForeignKey("SCH_SERVICIOS.detalles_reservas", "reserva_hotel_id", "SCH_SERVICIOS.reservas_hoteles");
            DropForeignKey("SCH_SERVICIOS.reservas_hoteles", "espacio_Habitacion_id", "SCH_SERVICIOS.espacios_habitaciones");
            DropForeignKey("SCH_SERVICIOS.espacios_habitaciones", "habitacion_id", "SCH_SERVICIOS.habitaciones");
            DropForeignKey("SCH_SERVICIOS.habitaciones", "tipo_habitacion_id", "SCH_CATALOGOS.catalogos");
            DropForeignKey("SCH_SERVICIOS.habitaciones", "proveedor_id", "SCH_PROVEEDORES.proveedores");
            DropForeignKey("SCH_SERVICIOS.reservas_hoteles", "colaborador_id", "SCH_RRHH.colaboradores");
            DropForeignKey("SCH_SERVICIOS.consumos_sin_reserva_hospedaje", "tipo_habitacion_id", "SCH_CATALOGOS.catalogos");
            DropForeignKey("SCH_SERVICIOS.consumos_sin_reserva_hospedaje", "proveedor_id", "SCH_PROVEEDORES.proveedores");
            DropForeignKey("SCH_SERVICIOS.consumos_sin_reserva_hospedaje", "colaborador_id", "SCH_RRHH.colaboradores");
            DropIndex("SCH_SERVICIOS.reservas_hoteles_qr", new[] { "espacio_habitacion_id" });
            DropIndex("SCH_SERVICIOS.detalles_reservas_qr", new[] { "reserva_hotel_qr_id" });
            DropIndex("SCH_SERVICIOS.habitaciones", new[] { "tipo_habitacion_id" });
            DropIndex("SCH_SERVICIOS.habitaciones", new[] { "proveedor_id" });
            DropIndex("SCH_SERVICIOS.espacios_habitaciones", new[] { "habitacion_id" });
            DropIndex("SCH_SERVICIOS.reservas_hoteles", new[] { "colaborador_id" });
            DropIndex("SCH_SERVICIOS.reservas_hoteles", new[] { "espacio_Habitacion_id" });
            DropIndex("SCH_SERVICIOS.detalles_reservas", new[] { "reserva_hotel_id" });
            DropIndex("SCH_SERVICIOS.consumos_sin_reserva_hospedaje", new[] { "tipo_habitacion_id" });
            DropIndex("SCH_SERVICIOS.consumos_sin_reserva_hospedaje", new[] { "colaborador_id" });
            DropIndex("SCH_SERVICIOS.consumos_sin_reserva_hospedaje", new[] { "proveedor_id" });
            DropIndex("SCH_RRHH.colaboradores", new[] { "locacion_trabajo_id" });
            AlterColumn("SCH_SERVICIOS.distribuciones_viandas", "hora_asigancion_transporte", c => c.DateTime(nullable: false));
            AlterColumn("SCH_RRHH.colaboradores", "registro_masivo", c => c.Boolean(nullable: false));
            AlterColumn("SCH_RRHH.colaboradores", "candidato_id_sap", c => c.Int(nullable: false));
            AlterColumn("SCH_RRHH.colaboradores", "horario_hasta", c => c.Time(nullable: false, precision: 7));
            AlterColumn("SCH_RRHH.colaboradores", "horario_desde", c => c.Time(nullable: false, precision: 7));
            AlterColumn("SCH_RRHH.colaboradores", "turno_id", c => c.Int(nullable: false));
            AlterColumn("SCH_RRHH.colaboradores", "usuario_id", c => c.Int(nullable: false));
            AlterColumn("SCH_RRHH.colaboradores", "locacion_trabajo_id", c => c.Int(nullable: false));
            AlterColumn("SCH_RRHH.colaboradores", "pais_pais_nacimiento_id", c => c.Int(nullable: false));
            AlterColumn("SCH_PROVEEDORES.proveedores", "usuario", c => c.Int(nullable: false));
            DropTable("SCH_SERVICIOS.reservas_hoteles_qr",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ReservaHotelQr_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("SCH_SERVICIOS.detalles_reservas_qr",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DetalleReservaQr_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("SCH_SERVICIOS.habitaciones",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Habitacion_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("SCH_SERVICIOS.espacios_habitaciones",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_EspacioHabitacion_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("SCH_SERVICIOS.reservas_hoteles",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ReservaHotel_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("SCH_SERVICIOS.detalles_reservas",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DetalleReserva_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("SCH_SERVICIOS.consumos_sin_reserva_hospedaje",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ConsumoSinReservaHospedaje_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            CreateIndex("SCH_RRHH.colaboradores", "locacion_trabajo_id");
        }
    }
}
