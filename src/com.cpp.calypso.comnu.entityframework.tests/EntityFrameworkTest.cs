using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using Abp.Domain.Entities;
using Bogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.cpp.calypso.comun.entityframework.tests
{
    public class EntityFrameworkTestDbContext : DbContext
    {
        public DbSet<EntityEFTest> EntidadTest { get; set; }

        public EntityFrameworkTestDbContext()
        {

        }
    }

    public class EntityEFTest : Entity {

        public string Nombre { get; set; }
        public string Codigo { get; set; }
    }

    [TestClass]
    public class EntityFrameworkTest
    {
        private TestContext m_testContext;
        public TestContext TestContext
        {
            get { return m_testContext; }
            set { m_testContext = value; }
        }

        [TestInitialize()]
        public void Initialize()
        {
            System.Data.Entity.Database.SetInitializer(new CreateDatabaseIfNotExists<EntityFrameworkTestDbContext>());
        }

        [TestMethod]
        public void Debe_Ejecutar_SQL()
        {
            //1. Insertar Datos
            var fakerEntidad = new Faker<EntityEFTest>()
               .RuleFor(u => u.Nombre, (f, u) => f.Name.FullName())
               .RuleFor(u => u.Codigo, (f, u) => f.Random.String(10))
               ;

            var entidad = fakerEntidad.Generate();

            using (var db = new EntityFrameworkTestDbContext())
            {
                db.EntidadTest.Add(entidad);
                db.SaveChanges();
            }
            TestContext.WriteLine(string.Format("Entidad Id: {0}, Codigo: {1}", entidad.Id, entidad.Codigo));


            using (var ctx = new EntityFrameworkTestDbContext())
            {
                var entidadSql = ctx.EntidadTest
                                .SqlQuery("Select * from [dbo].[entityeftest]  where id=@id", new SqlParameter("@id", entidad.Id))
                                .FirstOrDefault();

                TestContext.WriteLine(string.Format("Entidad Recuperado Id: {0}, Codigo: {1}", entidadSql.Id, entidadSql.Codigo));
            }

        }
    }


}
