namespace com.cpp.calypso.tests.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DestinoSecuencias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        Foo = c.String(),
                        Secuencia = c.Long(nullable: false),
                        SecuenciaUtilizada = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Funcionalidads",
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
                .ForeignKey("dbo.Moduloes", t => t.ModuloId, cascadeDelete: true)
                .Index(t => t.ModuloId);
            
            CreateTable(
                "dbo.Accions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codigo = c.String(nullable: false, maxLength: 60),
                        Nombre = c.String(nullable: false, maxLength: 80),
                        FuncionalidadId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Funcionalidads", t => t.FuncionalidadId, cascadeDelete: true)
                .Index(t => t.FuncionalidadId);
            
            CreateTable(
                "dbo.Moduloes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codigo = c.String(nullable: false, maxLength: 60),
                        Nombre = c.String(nullable: false, maxLength: 80),
                        Descripcion = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Usuarios",
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rols",
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Permisoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RolId = c.Int(nullable: false),
                        AccionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accions", t => t.AccionId, cascadeDelete: true)
                .ForeignKey("dbo.Rols", t => t.RolId, cascadeDelete: true)
                .Index(t => t.RolId)
                .Index(t => t.AccionId);
            
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(maxLength: 255),
                        Codigo = c.String(nullable: false, maxLength: 15),
                        Nombre = c.String(nullable: false, maxLength: 80),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MenuItems",
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
                .ForeignKey("dbo.Menus", t => t.MenuId, cascadeDelete: true)
                .ForeignKey("dbo.MenuItems", t => t.PadreId)
                .Index(t => t.MenuId)
                .Index(t => t.PadreId);
            
            CreateTable(
                "dbo.ParametroSistemas",
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ParametroOpcions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Valor = c.String(nullable: false, maxLength: 30),
                        Texto = c.String(nullable: false, maxLength: 255),
                        ParametroId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ParametroSistemas", t => t.ParametroId, cascadeDelete: true)
                .Index(t => t.ParametroId);
            
            CreateTable(
                "dbo.Secuencias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Codigo = c.String(nullable: false),
                        Valor = c.Long(nullable: false),
                        Incremento = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sesions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UsuarioId = c.Int(),
                        Cuenta = c.String(maxLength: 50),
                        CreationTime = c.DateTime(nullable: false),
                        ClientIpAddress = c.String(maxLength: 64),
                        ClientName = c.String(maxLength: 128),
                        BrowserInfo = c.String(maxLength: 512),
                        ModuloId = c.Int(nullable: false),
                        Result = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Views",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 80),
                        Model = c.String(nullable: false, maxLength: 80),
                        Arch = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UsuarioModuloes",
                c => new
                    {
                        Usuario_Id = c.Int(nullable: false),
                        Modulo_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Usuario_Id, t.Modulo_Id })
                .ForeignKey("dbo.Usuarios", t => t.Usuario_Id, cascadeDelete: true)
                .ForeignKey("dbo.Moduloes", t => t.Modulo_Id, cascadeDelete: true)
                .Index(t => t.Usuario_Id)
                .Index(t => t.Modulo_Id);
            
            CreateTable(
                "dbo.RolUsuarios",
                c => new
                    {
                        Rol_Id = c.Int(nullable: false),
                        Usuario_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Rol_Id, t.Usuario_Id })
                .ForeignKey("dbo.Rols", t => t.Rol_Id, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.Usuario_Id, cascadeDelete: true)
                .Index(t => t.Rol_Id)
                .Index(t => t.Usuario_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ParametroOpcions", "ParametroId", "dbo.ParametroSistemas");
            DropForeignKey("dbo.MenuItems", "PadreId", "dbo.MenuItems");
            DropForeignKey("dbo.MenuItems", "MenuId", "dbo.Menus");
            DropForeignKey("dbo.RolUsuarios", "Usuario_Id", "dbo.Usuarios");
            DropForeignKey("dbo.RolUsuarios", "Rol_Id", "dbo.Rols");
            DropForeignKey("dbo.Permisoes", "RolId", "dbo.Rols");
            DropForeignKey("dbo.Permisoes", "AccionId", "dbo.Accions");
            DropForeignKey("dbo.UsuarioModuloes", "Modulo_Id", "dbo.Moduloes");
            DropForeignKey("dbo.UsuarioModuloes", "Usuario_Id", "dbo.Usuarios");
            DropForeignKey("dbo.Funcionalidads", "ModuloId", "dbo.Moduloes");
            DropForeignKey("dbo.Accions", "FuncionalidadId", "dbo.Funcionalidads");
            DropIndex("dbo.RolUsuarios", new[] { "Usuario_Id" });
            DropIndex("dbo.RolUsuarios", new[] { "Rol_Id" });
            DropIndex("dbo.UsuarioModuloes", new[] { "Modulo_Id" });
            DropIndex("dbo.UsuarioModuloes", new[] { "Usuario_Id" });
            DropIndex("dbo.ParametroOpcions", new[] { "ParametroId" });
            DropIndex("dbo.MenuItems", new[] { "PadreId" });
            DropIndex("dbo.MenuItems", new[] { "MenuId" });
            DropIndex("dbo.Permisoes", new[] { "AccionId" });
            DropIndex("dbo.Permisoes", new[] { "RolId" });
            DropIndex("dbo.Accions", new[] { "FuncionalidadId" });
            DropIndex("dbo.Funcionalidads", new[] { "ModuloId" });
            DropTable("dbo.RolUsuarios");
            DropTable("dbo.UsuarioModuloes");
            DropTable("dbo.Views");
            DropTable("dbo.Sesions");
            DropTable("dbo.Secuencias");
            DropTable("dbo.ParametroOpcions");
            DropTable("dbo.ParametroSistemas");
            DropTable("dbo.MenuItems");
            DropTable("dbo.Menus");
            DropTable("dbo.Permisoes");
            DropTable("dbo.Rols");
            DropTable("dbo.Usuarios",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Usuario_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Moduloes");
            DropTable("dbo.Accions");
            DropTable("dbo.Funcionalidads");
            DropTable("dbo.DestinoSecuencias");
        }
    }
}
