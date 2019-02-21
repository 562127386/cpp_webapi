using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using Bogus;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;
using com.cpp.calypso.web;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.cpp.calypso.tests
{

    public class ModeloTestDbContext : PrincipalDbContext
    {

        public ModeloTestDbContext()
        {
            
        }
    }

    [TestClass]
    public class ModeloDominioTest
    {
        [TestInitialize()]
        public void Initialize()
        {
            System.Data.Entity.Database.SetInitializer(new CreateDatabaseIfNotExists<ModeloTestDbContext>());
        }

        private TestContext m_testContext;
        public TestContext TestContext
        {
            get { return m_testContext; }
            set { m_testContext = value; }
        }

        

        [TestMethod]
        public void Debe_Recuperar_Datos()
        {

            using (var db = new ModeloTestDbContext())
            {
                db.Database.Log = l => TestContext.WriteLine(l);

                var items = db.Sesiones.ToList();

                TestContext.WriteLine("Datos Persistidos. Total {0}", items.Count);
                foreach (var item in items)
                {
                    TestContext.WriteLine(string.Format("item: {0}", item));


                }

            }
        }



        [TestMethod]
        public void Debe_Recuparar_Datos_Roles_Usuarios()
        {

        }


        [TestMethod]
        public void Debe_Recuparar_Datos_Acciones_Funcionalidades()
        {
            try
            {

                var data = EntidadesComunFake.FakeFuncionalidades(5);

                TestContext.WriteLine("Datos Generados...");
                foreach (var item in data)
                {
                    TestContext.WriteLine(string.Format("Funcionalidad: {0}", item.Nombre));

                    foreach (var accion in item.Acciones)
                    {
                        TestContext.WriteLine(string.Format("F: {0}. Accion: {1}", item.Nombre, accion.Nombre));
                    }
                }


                //Save
                using (var db = new ModeloTestDbContext())
                {

                    foreach (var item in data)
                    {
                        db.Funcionalidades.Add(item);
                    }
                    db.SaveChanges();

                }


                //Get
                using (var db = new ModeloTestDbContext())
                {

                    var funcionalidades = db.Funcionalidades.Include(f => f.Acciones);

                    TestContext.WriteLine("Datos Persistidos...");
                    foreach (var item in funcionalidades)
                    {
                        TestContext.WriteLine(string.Format("Funcionalidad: {0}", item.Nombre));

                        foreach (var accion in item.Acciones)
                        {
                            TestContext.WriteLine(string.Format("F: {0}. Accion: {1}", item.Nombre, accion.Nombre));
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DbEntityValidationException))
                {
                    var dbEntityValidationException = ex as DbEntityValidationException;

                    var fullErrorMessage = dbEntityValidationException.DbEntityValidationExceptionToString();

                    TestContext.WriteLine(fullErrorMessage);

                }
                else
                {
                    TestContext.WriteLine(ex.ToString());
                }


                throw;
            }



        }


    }


}
