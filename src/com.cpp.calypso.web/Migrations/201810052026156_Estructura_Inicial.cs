namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Estructura_Inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SCH_USUARIOS.funcionalidades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codigo = c.String(nullable: false, maxLength: 15),
                        Nombre = c.String(nullable: false, maxLength: 80),
                        Descripcion = c.String(maxLength: 255),
                        Controlador = c.String(nullable: false),
                        Estado = c.Int(nullable: false),
                        ModuloId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SCH_USUARIOS.modulos", t => t.ModuloId, cascadeDelete: true)
                .Index(t => t.Codigo, unique: true, name: "UX_FUN_SIS_ID_FUN_CODIGO")
                .Index(t => t.ModuloId);
            
            CreateTable(
                "SCH_USUARIOS.acciones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codigo = c.String(nullable: false, maxLength: 60),
                        Nombre = c.String(nullable: false, maxLength: 80),
                        FuncionalidadId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SCH_USUARIOS.funcionalidades", t => t.FuncionalidadId, cascadeDelete: true)
                .Index(t => new { t.FuncionalidadId, t.Codigo }, unique: true, name: "UX_ACC_FUN_ID_ACC_CODIGO");
            
            CreateTable(
                "SCH_USUARIOS.modulos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codigo = c.String(nullable: false, maxLength: 60),
                        Nombre = c.String(nullable: false, maxLength: 80),
                        Descripcion = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Codigo, unique: true);
            
            CreateTable(
                "SCH_USUARIOS.usuarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SecurityStamp = c.String(maxLength: 128),
                        Cuenta = c.String(nullable: false, maxLength: 60),
                        Identificacion = c.String(nullable: false, maxLength: 80),
                        Nombres = c.String(nullable: false, maxLength: 80),
                        Apellidos = c.String(nullable: false, maxLength: 80),
                        Correo = c.String(nullable: false, maxLength: 80),
                        Estado = c.Int(nullable: false),
                        PasswordResetCode = c.String(maxLength: 328),
                        FechaFinalizacionBloqueUtc = c.DateTime(),
                        CantidadAccesoFallido = c.Int(nullable: false),
                        BloqueoHabilitado = c.Boolean(nullable: false),
                        Password = c.String(nullable: false, maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Usuario_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Cuenta, unique: true);
            
            CreateTable(
                "SCH_USUARIOS.roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EsAdministrador = c.Boolean(nullable: false),
                        EsExterno = c.Boolean(nullable: false),
                        Url = c.String(maxLength: 255),
                        Parametros = c.String(),
                        Codigo = c.String(nullable: false, maxLength: 15),
                        Nombre = c.String(nullable: false, maxLength: 80),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Codigo, unique: true);
            
            CreateTable(
                "SCH_USUARIOS.permisos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RolId = c.Int(nullable: false),
                        AccionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SCH_USUARIOS.acciones", t => t.AccionId, cascadeDelete: true)
                .ForeignKey("SCH_USUARIOS.roles", t => t.RolId, cascadeDelete: true)
                .Index(t => t.RolId)
                .Index(t => t.AccionId);
            
            CreateTable(
                "SCH_USUARIOS.menus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(maxLength: 255),
                        ModuloId = c.Int(nullable: false),
                        Codigo = c.String(nullable: false, maxLength: 15),
                        Nombre = c.String(nullable: false, maxLength: 80),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SCH_USUARIOS.modulos", t => t.ModuloId, cascadeDelete: true)
                .Index(t => t.ModuloId)
                .Index(t => t.Codigo, unique: true, name: "UX_MEN_SIS_ID_MEN_CODIGO");
            
            CreateTable(
                "SCH_USUARIOS.menuitems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(maxLength: 255),
                        Url = c.String(maxLength: 255),
                        Estado = c.Int(nullable: false),
                        TipoId = c.Int(nullable: false),
                        Orden = c.Int(nullable: false),
                        Icono = c.String(),
                        MenuId = c.Int(nullable: false),
                        PadreId = c.Int(),
                        FuncionalidadId = c.Int(),
                        Codigo = c.String(nullable: false, maxLength: 15),
                        Nombre = c.String(nullable: false, maxLength: 80),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SCH_USUARIOS.menus", t => t.MenuId, cascadeDelete: true)
                .ForeignKey("SCH_USUARIOS.menuitems", t => t.PadreId)
                .Index(t => new { t.MenuId, t.Codigo }, unique: true, name: "UX_MIT_MEN_ID_MIT_CODIGO")
                .Index(t => t.PadreId);
            
            CreateTable(
                "SCH_USUARIOS.parametros",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codigo = c.String(nullable: false, maxLength: 60),
                        Nombre = c.String(nullable: false, maxLength: 80),
                        Descripcion = c.String(maxLength: 255),
                        Categoria = c.Int(nullable: false),
                        Tipo = c.Int(nullable: false),
                        Valor = c.String(nullable: false),
                        EsEditable = c.Boolean(nullable: false),
                        TieneOpciones = c.Boolean(nullable: false),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Codigo, unique: true, name: "UX_PAR_SIS_ID_PAR_CODIGO");
            
            CreateTable(
                "SCH_USUARIOS.parametro_opciones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Valor = c.String(nullable: false, maxLength: 30),
                        Texto = c.String(nullable: false, maxLength: 255),
                        ParametroId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SCH_USUARIOS.parametros", t => t.ParametroId, cascadeDelete: true)
                .Index(t => t.ParametroId);
            
            CreateTable(
                "SCH_USUARIOS.sessiones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UsuarioId = c.Int(),
                        Cuenta = c.String(maxLength: 50),
                        CreationTime = c.DateTime(nullable: false),
                        ClientIpAddress = c.String(maxLength: 64),
                        ClientName = c.String(maxLength: 128),
                        BrowserInfo = c.String(maxLength: 512),
                        ModuloId = c.Int(),
                        Result = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "SCH_USUARIOS.views",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Model = c.String(nullable: false, maxLength: 510),
                        Arch = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "SCH_USUARIOS.auditorias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        Identificacion = c.String(),
                        Usuario = c.String(),
                        Funcionalidad = c.String(),
                        Accion = c.String(),
                        Mensaje = c.String(),
                        IpAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "SCH_USUARIOS.usuario_modulo",
                c => new
                    {
                        usuarioId = c.Int(nullable: false),
                        moduloId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.usuarioId, t.moduloId })
                .ForeignKey("SCH_USUARIOS.usuarios", t => t.usuarioId, cascadeDelete: true)
                .ForeignKey("SCH_USUARIOS.modulos", t => t.moduloId, cascadeDelete: true)
                .Index(t => t.usuarioId)
                .Index(t => t.moduloId);
            
            CreateTable(
                "SCH_USUARIOS.usuario_rol",
                c => new
                    {
                        usuarioId = c.Int(nullable: false),
                        rolId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.usuarioId, t.rolId })
                .ForeignKey("SCH_USUARIOS.usuarios", t => t.usuarioId, cascadeDelete: true)
                .ForeignKey("SCH_USUARIOS.roles", t => t.rolId, cascadeDelete: true)
                .Index(t => t.usuarioId)
                .Index(t => t.rolId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("SCH_USUARIOS.parametro_opciones", "ParametroId", "SCH_USUARIOS.parametros");
            DropForeignKey("SCH_USUARIOS.menus", "ModuloId", "SCH_USUARIOS.modulos");
            DropForeignKey("SCH_USUARIOS.menuitems", "PadreId", "SCH_USUARIOS.menuitems");
            DropForeignKey("SCH_USUARIOS.menuitems", "MenuId", "SCH_USUARIOS.menus");
            DropForeignKey("SCH_USUARIOS.funcionalidades", "ModuloId", "SCH_USUARIOS.modulos");
            DropForeignKey("SCH_USUARIOS.usuario_rol", "rolId", "SCH_USUARIOS.roles");
            DropForeignKey("SCH_USUARIOS.usuario_rol", "usuarioId", "SCH_USUARIOS.usuarios");
            DropForeignKey("SCH_USUARIOS.permisos", "RolId", "SCH_USUARIOS.roles");
            DropForeignKey("SCH_USUARIOS.permisos", "AccionId", "SCH_USUARIOS.acciones");
            DropForeignKey("SCH_USUARIOS.usuario_modulo", "moduloId", "SCH_USUARIOS.modulos");
            DropForeignKey("SCH_USUARIOS.usuario_modulo", "usuarioId", "SCH_USUARIOS.usuarios");
            DropForeignKey("SCH_USUARIOS.acciones", "FuncionalidadId", "SCH_USUARIOS.funcionalidades");
            DropIndex("SCH_USUARIOS.usuario_rol", new[] { "rolId" });
            DropIndex("SCH_USUARIOS.usuario_rol", new[] { "usuarioId" });
            DropIndex("SCH_USUARIOS.usuario_modulo", new[] { "moduloId" });
            DropIndex("SCH_USUARIOS.usuario_modulo", new[] { "usuarioId" });
            DropIndex("SCH_USUARIOS.views", new[] { "Name" });
            DropIndex("SCH_USUARIOS.parametro_opciones", new[] { "ParametroId" });
            DropIndex("SCH_USUARIOS.parametros", "UX_PAR_SIS_ID_PAR_CODIGO");
            DropIndex("SCH_USUARIOS.menuitems", new[] { "PadreId" });
            DropIndex("SCH_USUARIOS.menuitems", "UX_MIT_MEN_ID_MIT_CODIGO");
            DropIndex("SCH_USUARIOS.menus", "UX_MEN_SIS_ID_MEN_CODIGO");
            DropIndex("SCH_USUARIOS.menus", new[] { "ModuloId" });
            DropIndex("SCH_USUARIOS.permisos", new[] { "AccionId" });
            DropIndex("SCH_USUARIOS.permisos", new[] { "RolId" });
            DropIndex("SCH_USUARIOS.roles", new[] { "Codigo" });
            DropIndex("SCH_USUARIOS.usuarios", new[] { "Cuenta" });
            DropIndex("SCH_USUARIOS.modulos", new[] { "Codigo" });
            DropIndex("SCH_USUARIOS.acciones", "UX_ACC_FUN_ID_ACC_CODIGO");
            DropIndex("SCH_USUARIOS.funcionalidades", new[] { "ModuloId" });
            DropIndex("SCH_USUARIOS.funcionalidades", "UX_FUN_SIS_ID_FUN_CODIGO");
            DropTable("SCH_USUARIOS.usuario_rol");
            DropTable("SCH_USUARIOS.usuario_modulo");
            DropTable("SCH_USUARIOS.auditorias");
            DropTable("SCH_USUARIOS.views");
            DropTable("SCH_USUARIOS.sessiones");
            DropTable("SCH_USUARIOS.parametro_opciones");
            DropTable("SCH_USUARIOS.parametros");
            DropTable("SCH_USUARIOS.menuitems");
            DropTable("SCH_USUARIOS.menus");
            DropTable("SCH_USUARIOS.permisos");
            DropTable("SCH_USUARIOS.roles");
            DropTable("SCH_USUARIOS.usuarios",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Usuario_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("SCH_USUARIOS.modulos");
            DropTable("SCH_USUARIOS.acciones");
            DropTable("SCH_USUARIOS.funcionalidades");
        }
    }
}
