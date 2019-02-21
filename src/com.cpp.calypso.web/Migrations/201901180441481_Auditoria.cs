namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.IO;

    public partial class Auditoria : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SCH_USUARIOS.AuditoriaEntidad",
                c => new
                    {
                        AuditEntryID = c.Int(nullable: false, identity: true),
                        EntitySetName = c.String(maxLength: 255),
                        EntityTypeName = c.String(maxLength: 255),
                        State = c.Int(nullable: false),
                        StateName = c.String(maxLength: 255),
                        CreatedBy = c.String(maxLength: 255),
                        CreatedDate = c.DateTime(nullable: false),
                        Id = c.Int(),
                        ObjectType = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.AuditEntryID);
            
            CreateTable(
                "SCH_USUARIOS.AuditoriaPropiedad",
                c => new
                    {
                        AuditEntryPropertyID = c.Int(nullable: false, identity: true),
                        AuditEntryID = c.Int(nullable: false),
                        RelationName = c.String(maxLength: 255),
                        PropertyName = c.String(maxLength: 255),
                        OldValue = c.String(),
                        NewValue = c.String(),
                        Id = c.Int(),
                        ObjectType = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.AuditEntryPropertyID)
                .ForeignKey("SCH_USUARIOS.AuditoriaEntidad", t => t.AuditEntryID, cascadeDelete: true)
                .Index(t => t.AuditEntryID);

            var sqlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Migrations\Script_Datos_Auditoria_Datos.sql");
            Sql(File.ReadAllText(sqlFile));

        }
        
        public override void Down()
        {
            DropForeignKey("SCH_USUARIOS.AuditoriaPropiedad", "AuditEntryID", "SCH_USUARIOS.AuditoriaEntidad");
            DropIndex("SCH_USUARIOS.AuditoriaPropiedad", new[] { "AuditEntryID" });
            DropTable("SCH_USUARIOS.AuditoriaPropiedad");
            DropTable("SCH_USUARIOS.AuditoriaEntidad");
        }
    }
}
