namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class UsuariosMoviles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SCH_USUARIOS.usuarios_movil",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        usuario_id = c.Int(nullable: false),
                        username = c.String(),
                        apellidos_nombre = c.String(),
                        pin = c.String(),
                        fecha_creacion = c.DateTime(nullable: false),
                        ultimo_acceso = c.DateTime(),
                        rol = c.String(),
                        activo_movil = c.Boolean(nullable: false),
                        m_version = c.Int(nullable: false),
                        m_ref = c.String(),
                        vigente = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UsuarioMovil_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("SCH_USUARIOS.usuarios_movil",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UsuarioMovil_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
