using Abp.Domain.Entities;
using Abp.Threading;
using Bogus;
using com.cpp.calypso.web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace com.cpp.calypso.tests
{
    public class SecuenciaDbContext : DbContext
    {
        public DbSet<SecuenciaSinDependencia> Secuencias { get; set; }

        public DbSet<DestinoSecuencia> DestinoSecuencia { get; set; }

        public SecuenciaDbContext():base("SecuenciaDbContext")
        {

        }

  
    }

    public class DestinoSecuencia : Entity
    {
        public DateTime Fecha { get; set; }
        public string Foo { get; set; }
        public long Secuencia { get; set; }
        public string SecuenciaUtilizada { get; internal set; }

        public override string ToString()
        {
            return string.Format("Secuencia Utilizada: {0}. Foo: {1}. Fecha {2}. Secuencia Valor: {3}. Id: {4}",
                SecuenciaUtilizada,Foo, Fecha,Secuencia, Id);
        }
    }

    public class SecuenciaSinDependencia : Entity
    {
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public long Ultimo { get; set; }
        public int Incremento { get; set; }

        public override string ToString()
        {
            return string.Format("Codigo {0}. Ultimo {1}. Incremento {2} ",Codigo,Ultimo, Incremento);
        }
    }


    /// <summary>
    /// Secuencia sin integraciones con ABP
    /// </summary>
    [TestClass]
    public class GeneracionSecuenciaSinDependenciaTest
    {
        private static readonly object _syncObj = new object();


        private TestContext m_testContext;
        public TestContext TestContext
        {
            get { return m_testContext; }
            set { m_testContext = value; }
        }

        [TestInitialize()]
        public void Initialize()
        {
            System.Data.Entity.Database.SetInitializer(new CreateDatabaseIfNotExists<SecuenciaDbContext>());
        }


        [TestMethod]
        public void Debe_Generar_Secuencia_Sin_Saltos()
        {

            var fakerSecuencia = new Faker<SecuenciaSinDependencia>()
              .RuleFor(u => u.Nombre, (f, u) => f.Random.String(20))
              .RuleFor(u => u.Codigo, (f, u) => f.Phone.PhoneNumber())
              .RuleFor(u => u.Incremento, (f, u) => f.Random.Int(1,4))
              ;

            
            var secuencia = fakerSecuencia.Generate();

            TestContext.WriteLine(string.Format("Secuencia Inicial {0}", secuencia));

            using (var context = new SecuenciaDbContext())
            {
                context.Secuencias.Add(secuencia);

                context.SaveChanges();
            }

            //Ejecutar tareas en paralelo...
            Task task1 = Task.Factory.StartNew(() => EjecutarBloque(secuencia,"Bloque1"));
            Task task2 = Task.Factory.StartNew(() => EjecutarBloque(secuencia,"Bloque2"));
            Task task3 = Task.Factory.StartNew(() => EjecutarBloque(secuencia,"Bloque3"));
            Task.WaitAll(task1, task2, task3);

            TestContext.WriteLine("All threads complete");

            List<DestinoSecuencia> destinoList = null;
            SecuenciaSinDependencia secuenciaFinal = null;
            //REcuperar secuencia
            using (var context = new SecuenciaDbContext())
            {
                secuenciaFinal = context.Secuencias.Where(s => s.Codigo == secuencia.Codigo).SingleOrDefault();

                destinoList = context.DestinoSecuencia.Where(d => d.SecuenciaUtilizada == secuencia.Codigo).ToList();
            }

            TestContext.WriteLine(string.Format("Secuencia Final {0}", secuenciaFinal));

            foreach (var item in destinoList)
            {
                TestContext.WriteLine(item.ToString());
            }

        }


        private void EjecutarBloque(SecuenciaSinDependencia secuencia, string strName)
        {
            for (int i = 1; i <= 3; i++)
            {

                var fakerModel = new Faker<DestinoSecuencia>()
                  .RuleFor(u => u.Foo, (f, u) => f.Name.FullName()) 
                  ;

                var model = fakerModel.Generate();
                TestContext.WriteLine(string.Format("Item Ejecutado: {0}-{1}",strName,i.ToString()));

                try
                {
                    GuardarSecuenciaDestino(secuencia, model);
                    TestContext.WriteLine(string.Format("Correctamente. {0}-{1}. Secuencia establecida {2}", strName, i.ToString(),model.Secuencia));
                }
                catch (Exception ex)
                {

                    TestContext.WriteLine(string.Format("Exception  {0}-{1}. {2}", strName, i.ToString(), ex.Message));
                }
                
                TestContext.WriteLine(string.Format("Resultado Item {0}-{1}. Destino: {2}", strName, i.ToString(), model.ToString()));

                Thread.Yield();
            }
        }

        private void GuardarSecuenciaDestino(SecuenciaSinDependencia secuencia, DestinoSecuencia model)
        {
            //lock (_syncObj)
            //{
            //    AsyncHelper.RunSync(async () =>
            //    {
                    long ValorSecuencia = 0;
                    //TransactionScopeOption.RequiresNew
                    using (var scope = new TransactionScope())
                    {
                        using (var context = new SecuenciaDbContext( ))
                        {

                            //Generar la siguiente secuencia
                            var secuenciaRecuperar = context.Secuencias.AsQueryable()
                                    .Where(s => s.Codigo == secuencia.Codigo).SingleOrDefault();

                            //Aumentar secuencia
                            secuenciaRecuperar.Ultimo = secuenciaRecuperar.Ultimo+ secuenciaRecuperar.Incremento;
                            model.Secuencia = secuenciaRecuperar.Ultimo;
                            model.Fecha = DateTime.Now;

                            //Establecer Valor
                            ValorSecuencia = secuenciaRecuperar.Ultimo;
                            model.Secuencia = secuenciaRecuperar.Ultimo;
                            model.SecuenciaUtilizada = secuencia.Codigo;


                            context.DestinoSecuencia.Add(model);

                            //Actualizar secuencia
                            context.Entry(secuenciaRecuperar).State = EntityState.Modified;

                            //await context.SaveChangesAsync(); //Save change to this context
                            context.SaveChanges();
                        }

                        //Simular un error
                        Random rand = new Random();
                        if (rand.Next(0, 2) == 0) {
                            throw new Exception(string.Format("Error en la secuencia {0}", ValorSecuencia));
                        }
                        
                        scope.Complete();
                    }
            //    });
            //}

        }

        [TestMethod]
        public void Ejecutar_Paralelo()
        {
            Task task1 = Task.Factory.StartNew(() => doStuff("Task1"));
            Task task2 = Task.Factory.StartNew(() => doStuff("Task2"));
            Task task3 = Task.Factory.StartNew(() => doStuff("Task3"));
            Task.WaitAll(task1, task2, task3);

            TestContext.WriteLine("All threads complete");
        }

        private void doStuff(string strName)
        {
            for (int i = 1; i <= 3; i++)
            {
                TestContext.WriteLine(strName + " " + i.ToString());
                Thread.Yield();
            }
        }
    }
}
