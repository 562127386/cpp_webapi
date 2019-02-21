namespace com.cpp.calypso.tests.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<com.cpp.calypso.tests.ComunAbpIntegratedDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "com.cpp.calypso.tests.ComunAbpIntegratedDbContext";
        }

        protected override void Seed(com.cpp.calypso.tests.ComunAbpIntegratedDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
