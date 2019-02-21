namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.IO;

    public partial class DatosIniciales : DbMigration
    {
        //Agregar llamada a script
        public override void Up()
        {
            var sqlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Migrations\Script_Datos_Iniciales.sql");
            Sql(File.ReadAllText(sqlFile));
        }

        public override void Down()
        {
        }
    }
}
