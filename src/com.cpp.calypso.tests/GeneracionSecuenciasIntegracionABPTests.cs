using Abp.Domain.Entities;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Threading;
using Bogus;
using Castle.MicroKernel.Registration;
using com.cpp.calypso.comun.dominio;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Xunit;
using Xunit.Abstractions;

namespace com.cpp.calypso.tests
{

    /// <summary>
    /// Entidad, para generar secuencias.
    /// </summary>
    public class Secuencia : Entity
    {

        public Secuencia()
        {
            Valor = 0;
        }


        /// <summary>
        /// Nombre de la Secuencia
        /// </summary>
        [Obligado]
        public string Nombre { get; set; }

        /// <summary>
        /// Codigo de la Secuencia
        /// </summary>
        [Obligado]
        public string Codigo { get; set; }

        /// <summary>
        /// Valor de la Secuencia.
        /// </summary>
        public long Valor { get; set; }

        /// <summary>
        /// Valor de incremento de la secuencia
        /// </summary>
        public int Incremento { get; set; }



    }

    /// <summary>
    /// Gestion de Secuencias
    /// </summary>
    public class SecuenciaManager : DomainService, ISecuenciaManager
    {

        private readonly IBaseRepository<Secuencia> secuenciaRepository;
        private readonly ISqlExecuter sqlExecuter;

        public SecuenciaManager(
            IBaseRepository<Secuencia> secuenciaRepository,
            ISqlExecuter sqlExecuter)
        {
            this.secuenciaRepository = secuenciaRepository;
            this.sqlExecuter = sqlExecuter;
        }

        /// <summary>
        /// Obtener siguiente secuencia
        /// </summary>
        /// <param name="codeSequence"></param>
        /// <returns></returns>
        public long GetNextSequence(string codeSequence)
        {
          
            //Este proceso, debe ser sincrono. 
            var secuencia = secuenciaRepository.GetAll().Where(s => s.Codigo == codeSequence).AsNoTracking().SingleOrDefault();
            if (secuencia == null)
            {
                throw new ArgumentException("No existe secuencia con el codigo {0}", codeSequence);
            }

            //Generar nuevo Valor de la secuencia
            secuencia.Valor = secuencia.Valor + secuencia.Incremento;

            //Guardar valor de la secuencia
            var task = secuenciaRepository.UpdateAsync(secuencia);
            task.Wait();

            return task.Result.Valor;
        }

        public async Task<long> GetNextSequenceAsync(string codeSequence)
        {
            //Este proceso, debe ser sincrono. 
            var secuencia = await secuenciaRepository.GetAll().Where(s => s.Codigo == codeSequence).SingleOrDefaultAsync();
            if (secuencia == null)
            {
                throw new ArgumentException("No existe secuencia con el codigo {0}", codeSequence);
            }

            //Generar nuevo Valor de la secuencia
            secuencia.Valor = secuencia.Valor + secuencia.Incremento;

            //Guardar valor de la secuencia
            secuencia = await secuenciaRepository.UpdateAsync(secuencia);

            return secuencia.Valor;
        }

        public async Task<long> GetNextSequenceSQL(string codeSequence)
        {

            //1. Obtener valor actual
            var valor = await sqlExecuter.SqlQuery<long>("SELECT Valor FROM dbo.Secuencias where codigo = @codigo", new SqlParameter("@codigo", codeSequence));

            var incremento = await sqlExecuter.SqlQuery<int>("SELECT Incremento FROM dbo.Secuencias where codigo = @codigo", new SqlParameter("@codigo", codeSequence));

            //Generar nuevo Valor
            valor = valor + incremento;

            //2. Actualizar valor
            sqlExecuter.Execute("UPDATE dbo.Secuencias SET VALOR = @valor where codigo = @codigo", new SqlParameter("@valor", valor), new SqlParameter("@codigo", codeSequence));

            return valor;

        }

        
         public async Task<long> GetNextSequenceSQLWithLock(string codeSequence)
        {

            //1. Obtener valor actual
            var valor = await sqlExecuter.SqlQuery<long>("SELECT Valor FROM dbo.Secuencias  with (SERIALIZABLE, ROWLOCK) where codigo = @codigo", new SqlParameter("@codigo", codeSequence));

            var incremento = await sqlExecuter.SqlQuery<int>("SELECT Incremento FROM dbo.Secuencias  with  (SERIALIZABLE, ROWLOCK) where codigo = @codigo", new SqlParameter("@codigo", codeSequence));

            //Generar nuevo Valor
            valor = valor + incremento;

            //2. Actualizar valor
            sqlExecuter.Execute("UPDATE dbo.Secuencias SET VALOR = @valor where codigo = @codigo", new SqlParameter("@valor", valor), new SqlParameter("@codigo", codeSequence));

            return valor;

        }
    }

    /// <summary>
    /// Gestor para utilizar la secuencia
    /// </summary>
    public class DestinoSecuenciaManager : DomainService, IDestinoSecuenciaManager
    {
        private static readonly object _syncObj = new object();

        private readonly ISecuenciaManager secuenciaManager;
        private readonly IUnitOfWorkManager unitOfWorkManager;

        public IBaseRepository<DestinoSecuencia> DestinoRepository { get; }

        public DestinoSecuenciaManager(
            IBaseRepository<DestinoSecuencia> destinoRepository,
            ISecuenciaManager secuenciaManager,
            IUnitOfWorkManager unitOfWorkManager)
        {
            DestinoRepository = destinoRepository;
            this.secuenciaManager = secuenciaManager;
            this.unitOfWorkManager = unitOfWorkManager;
        }


        public virtual DestinoSecuencia CrearDestinoSecuencia(DestinoSecuencia destinoSecuencia, string codigSecuencia)
        {

            DestinoSecuencia entidadGuardada = null;           
            using (var unitOfWork = unitOfWorkManager.Begin())
            {
                //El bloque debe ser ejecuta forma sincrona
                AsyncHelper.RunSync(async () =>
                {
                        //1. Obtener la secuencia
                        //var valor = secuenciaManager.GetNextSequence(codigSecuencia);
                        var valor = await secuenciaManager.GetNextSequenceAsync(codigSecuencia);
                        
                        //2. Utilizar secuencia
                        destinoSecuencia.SecuenciaUtilizada = codigSecuencia;
                        destinoSecuencia.Secuencia = valor;

                        //Simular un error
                        Random rand = new Random();
                        ///if (rand.Next(0, 2) == 0)
                        if (rand.Next(0, 3) == 0)
                        {
                            throw new SimulacionException(string.Format("Error en la secuencia {0}", valor));
                        }

                        //3. Guardar 
                        entidadGuardada = await DestinoRepository.InsertAsync(destinoSecuencia);
                });
                unitOfWork.Complete();
            }
            //}

            return entidadGuardada;
        }

        public virtual DestinoSecuencia CrearDestinoSecuenciaLock(DestinoSecuencia destinoSecuencia, string codigSecuencia)
        {

            DestinoSecuencia entidadGuardada = null;
            lock (_syncObj)
            {
                using (var unitOfWork = unitOfWorkManager.Begin())
                {
                    //El bloque debe ser ejecuta forma sincrona
                    AsyncHelper.RunSync(async () =>
                    {
                        //1. Obtener la secuencia
                        //var valor = secuenciaManager.GetNextSequence(codigSecuencia);
                        var valor = await secuenciaManager.GetNextSequenceAsync(codigSecuencia);

                        //2. Utilizar secuencia
                        destinoSecuencia.SecuenciaUtilizada = codigSecuencia;
                        destinoSecuencia.Secuencia = valor;

                        //Simular un error
                        Random rand = new Random();
                        ///if (rand.Next(0, 2) == 0)
                        if (rand.Next(0, 3) == 0)
                        {
                            throw new SimulacionException(string.Format("Error en la secuencia {0}", valor));
                        }

                        //3. Guardar 
                        entidadGuardada = await DestinoRepository.InsertAsync(destinoSecuencia);
                    });
                    unitOfWork.Complete();
                }
            }
            return entidadGuardada;
        }

        public virtual DestinoSecuencia CrearDestinoSecuenciaSQL(DestinoSecuencia destinoSecuencia, string codigSecuencia)
        {

            DestinoSecuencia entidadGuardada = null;
       
            using (var unitOfWork = unitOfWorkManager.Begin())
            {
                //El bloque debe ser ejecuta forma sincrona
                AsyncHelper.RunSync(async () =>
                {
                    //1. Obtener la secuencia
                    var valor = await secuenciaManager.GetNextSequenceSQL(codigSecuencia);

                    //2. Utilizar secuencia
                    destinoSecuencia.SecuenciaUtilizada = codigSecuencia;
                    destinoSecuencia.Secuencia = valor;

                    //Simular un error
                    Random rand = new Random();
                    if (rand.Next(0, 3) == 0)
                    {
                        throw new SimulacionException(string.Format("Error en la secuencia {0}", valor));
                    }

                    //3. Guardar 
                    entidadGuardada = await DestinoRepository.InsertAsync(destinoSecuencia);
                });
                unitOfWork.Complete();
            }
            return entidadGuardada;
        }

        public virtual DestinoSecuencia CrearDestinoSecuenciaSQLWithIsoSerializable(DestinoSecuencia destinoSecuencia, string codigSecuencia)
        {
            //Establecer el asilamiento a Serializable
            var unitOfWorkOptions = new UnitOfWorkOptions();
            unitOfWorkOptions.IsolationLevel = IsolationLevel.Serializable;
            //transactionOptions.Timeout = TransactionManager.MaximumTimeout;

            DestinoSecuencia entidadGuardada = null;

            using (var unitOfWork = unitOfWorkManager.Begin(unitOfWorkOptions))
            {
                //El bloque debe ser ejecuta forma sincrona
                AsyncHelper.RunSync(async () =>
                {
                    //1. Obtener la secuencia
                    var valor = await secuenciaManager.GetNextSequenceSQL(codigSecuencia);

                    //2. Utilizar secuencia
                    destinoSecuencia.SecuenciaUtilizada = codigSecuencia;
                    destinoSecuencia.Secuencia = valor;

                    //Simular un error
                    Random rand = new Random();
                    if (rand.Next(0, 3) == 0)
                    {
                        throw new SimulacionException(string.Format("Error en la secuencia {0}", valor));
                    }

                    //3. Guardar 
                    entidadGuardada = await DestinoRepository.InsertAsync(destinoSecuencia);
                });
                unitOfWork.Complete();
            }
            return entidadGuardada;
        }

        public virtual DestinoSecuencia CrearDestinoSecuenciaSQLAndLockSQL(DestinoSecuencia destinoSecuencia, string codigSecuencia)
        {
          
            DestinoSecuencia entidadGuardada = null;

            using (var unitOfWork = unitOfWorkManager.Begin())
            {
                //El bloque debe ser ejecuta forma sincrona
                AsyncHelper.RunSync(async () =>
                {
                    //1. Obtener la secuencia
                    var valor = await secuenciaManager.GetNextSequenceSQLWithLock(codigSecuencia);

                    //2. Utilizar secuencia
                    destinoSecuencia.SecuenciaUtilizada = codigSecuencia;
                    destinoSecuencia.Secuencia = valor;

                    //Simular un error
                    Random rand = new Random();
                    if (rand.Next(0, 3) == 0)
                    {
                        throw new SimulacionException(string.Format("Error en la secuencia {0}", valor));
                    }

                    //3. Guardar 
                    entidadGuardada = await DestinoRepository.InsertAsync(destinoSecuencia);
                });
                unitOfWork.Complete();
            }
            return entidadGuardada;
        }


        public virtual DestinoSecuencia CrearDestinoSecuenciaSQLAndLock(DestinoSecuencia destinoSecuencia, string codigSecuencia)
        {

            DestinoSecuencia entidadGuardada = null;
            lock (_syncObj)
            {
                using (var unitOfWork = unitOfWorkManager.Begin())
                {
                    //El bloque debe ser ejecuta forma sincrona
                    AsyncHelper.RunSync(async () =>
                    {
                        //1. Obtener la secuencia
                        var valor = await secuenciaManager.GetNextSequenceSQL(codigSecuencia);

                        //2. Utilizar secuencia
                        destinoSecuencia.SecuenciaUtilizada = codigSecuencia;
                        destinoSecuencia.Secuencia = valor;

                        //Simular un error
                        Random rand = new Random();
                        /////if (rand.Next(0, 2) == 0)
                        if (rand.Next(0, 3) == 0)
                        {
                            throw new SimulacionException(string.Format("Error en la secuencia {0}", valor));
                        }

                        //3. Guardar 
                        entidadGuardada = await DestinoRepository.InsertAsync(destinoSecuencia);
                    });
                    unitOfWork.Complete();
                }
            }
            return entidadGuardada;
        }
    }

    public class SimulacionException : Exception
    {
        public SimulacionException(string message) : base(message)
        {
        }
    }

    public class GeneracionSecuenciasLockIntegracionABPTests : ComunAbpIntegratedTestBase
    {
        private readonly IDestinoSecuenciaManager destinoSecuenciaManager;

        private IBaseRepository<DestinoSecuencia> destionoRepository;

        private IBaseRepository<Secuencia> secuenciaRepository;

        private IUnitOfWorkManager unitOfWorkManager;

        private readonly ITestOutputHelper output;



        protected override void PreInitialize()
        {
            base.PreInitialize();

            LocalIocManager.IocContainer.Register(
               Component.For<ISecuenciaManager>()
                   .ImplementedBy<SecuenciaManager>()
                   .LifestyleTransient()
               );

            LocalIocManager.IocContainer.Register(
               Component.For<IDestinoSecuenciaManager>()
                   .ImplementedBy<DestinoSecuenciaManager>()
                   .LifestyleTransient()
               );
        }
        public GeneracionSecuenciasLockIntegracionABPTests(ITestOutputHelper output)
        {
            this.output = output;

            destinoSecuenciaManager = LocalIocManager.Resolve<IDestinoSecuenciaManager>();
            destionoRepository = LocalIocManager.Resolve<IBaseRepository<DestinoSecuencia>>();

            secuenciaRepository = LocalIocManager.Resolve<IBaseRepository<Secuencia>>();

            unitOfWorkManager = LocalIocManager.Resolve<IUnitOfWorkManager>();

            //CREATE DATABASE statement not allowed within multi-statement transaction
            //Crear Manualmente la base de datos
            //System.Data.Entity.Database.SetInitializer(new CreateDatabaseIfNotExists<ComunAbpIntegratedDbContext>());


        }

        [Fact]
        public void Debe_Generar_Secuencia_Sin_Saltos_Incluso_Con_Procesos_Con_Excepciones()
        {
            //1. Crear una secuencia y guardar
            var secuencia = CrearSecuencia();
            output.WriteLine("Secuencia Inicial Codigo: {0}. Id {1}. Incremento {2}", secuencia.Codigo, secuencia.Id, secuencia.Incremento);


            //2. Ejecutar tareas en paralelo, para utilizar la secuencia
            Task task1 = Task.Factory.StartNew(() => EjecutarBloque("Bloque1", secuencia.Codigo));
            Task task2 = Task.Factory.StartNew(() => EjecutarBloque("Bloque2", secuencia.Codigo));
            Task task3 = Task.Factory.StartNew(() => EjecutarBloque("Bloque3", secuencia.Codigo));
            Task.WaitAll(task1, task2, task3);

            output.WriteLine("All threads complete");

            //3. Obtener destinos de secuencias
            output.WriteLine("Resultado de secuencias generadas, y asignadas al destino. Utilizando secuencia {0}", secuencia.Codigo);

            var list = new List<DestinoSecuencia>();
            using (var unitOfWork = unitOfWorkManager.Begin())
            {
                list = destionoRepository.GetAll().Where(d => d.SecuenciaUtilizada == secuencia.Codigo).ToList();
                unitOfWork.Complete();
            }

            foreach (var item in list)
            {
                output.WriteLine(item.ToString());
            }
        }

        [Fact]
        public void Debe_Generar_Secuencia_Mecanismo_Lock_Sin_Saltos_Incluso_Con_Procesos_Con_Excepciones()
        {
            //1. Crear una secuencia y guardar
            var secuencia = CrearSecuencia();
            output.WriteLine("Secuencia Inicial Codigo: {0}. Id {1}. Incremento {2}", secuencia.Codigo, secuencia.Id, secuencia.Incremento);


            //2. Ejecutar tareas en paralelo, para utilizar la secuencia
            Task task1 = Task.Factory.StartNew(() => EjecutarBloqueLock("Bloque1", secuencia.Codigo));
            Task task2 = Task.Factory.StartNew(() => EjecutarBloqueLock("Bloque2", secuencia.Codigo));
            Task task3 = Task.Factory.StartNew(() => EjecutarBloqueLock("Bloque3", secuencia.Codigo));
            Task.WaitAll(task1, task2, task3);

            output.WriteLine("All threads complete");

            //3. Obtener destinos de secuencias
            output.WriteLine("Resultado de secuencias generadas, y asignadas al destino. Utilizando secuencia {0}", secuencia.Codigo);

            var list = new List<DestinoSecuencia>();
            using (var unitOfWork = unitOfWorkManager.Begin())
            {
                list = destionoRepository.GetAll().Where(d => d.SecuenciaUtilizada == secuencia.Codigo).ToList();
                unitOfWork.Complete();
            }

            foreach (var item in list)
            {
                output.WriteLine(item.ToString());
            }
        }

        public Secuencia CrearSecuencia()
        {

            //1 Crear una secuencia
            var fakerSecuencia = new Faker<Secuencia>()
             .RuleFor(u => u.Nombre, (f, u) => f.Random.String(20))
             .RuleFor(u => u.Codigo, (f, u) => f.Random.String2(10))
             .RuleFor(u => u.Incremento, (f, u) => f.Random.Int(1, 4))
             ;

            var secuencia = fakerSecuencia.Generate();
            using (var unitOfWork = unitOfWorkManager.Begin())
            {
                secuencia = secuenciaRepository.Insert(secuencia);
                unitOfWork.Complete();
            }

            return secuencia;
        }

        private void EjecutarBloque(string strName, string codigoSecuencia)
        {
            for (int i = 1; i <= 5; i++)
            {

                var fakerModel = new Faker<DestinoSecuencia>()
                  .RuleFor(u => u.Foo, (f, u) => f.Name.FullName())
                  .RuleFor(u => u.Fecha, (f, u) => DateTime.Now)
                  ;

                var model = fakerModel.Generate();
                output.WriteLine(string.Format("Item Ejecutado: {0}-{1}", strName, i.ToString()));

                try
                {
                    model = destinoSecuenciaManager.CrearDestinoSecuencia(model, codigoSecuencia);
                    output.WriteLine(string.Format("Correctamente. {0}-{1}. Secuencia establecida {2}", strName, i.ToString(), model.Secuencia));
                }
                catch (SimulacionException ex)
                {
                    output.WriteLine(string.Format("Exception  {0}-{1}. {2}", strName, i.ToString(), ex.Message));
                }
                catch (Exception ex)
                {
                    output.WriteLine(string.Format("Exception  {0}-{1}. {2}", strName, i.ToString(), ex.ToString()));
                }

                output.WriteLine(string.Format("Resultado Item {0}-{1}. Destino: {2}", strName, i.ToString(), model.ToString()));

                Thread.Yield();
            }
        }

        private void EjecutarBloqueLock(string strName, string codigoSecuencia)
        {
            for (int i = 1; i <= 5; i++)
            {

                var fakerModel = new Faker<DestinoSecuencia>()
                  .RuleFor(u => u.Foo, (f, u) => f.Name.FullName())
                  .RuleFor(u => u.Fecha, (f, u) => DateTime.Now)
                  ;

                var model = fakerModel.Generate();
                output.WriteLine(string.Format("Item Ejecutado: {0}-{1}", strName, i.ToString()));

                try
                {
                    model = destinoSecuenciaManager.CrearDestinoSecuenciaLock(model, codigoSecuencia);
                    output.WriteLine(string.Format("Correctamente. {0}-{1}. Secuencia establecida {2}", strName, i.ToString(), model.Secuencia));
                }
                catch (SimulacionException ex)
                {
                    output.WriteLine(string.Format("Exception  {0}-{1}. {2}", strName, i.ToString(), ex.Message));
                }
                catch (Exception ex)
                {
                    output.WriteLine(string.Format("Exception  {0}-{1}. {2}", strName, i.ToString(), ex.ToString()));
                }

                output.WriteLine(string.Format("Resultado Item {0}-{1}. Destino: {2}", strName, i.ToString(), model.ToString()));

                Thread.Yield();
            }
        }


    }


    public class GeneracionSecuenciasSQLIntegracionABPTests : ComunAbpIntegratedTestBase
    {
        private readonly IDestinoSecuenciaManager destinoSecuenciaManager;

        private IBaseRepository<DestinoSecuencia> destionoRepository;

        private IBaseRepository<Secuencia> secuenciaRepository;

        private IUnitOfWorkManager unitOfWorkManager;

        private readonly ITestOutputHelper output;



        protected override void PreInitialize()
        {
            //DbContext real
            LocalIocManager.IocContainer.Register(
                Component.For<ComunAbpIntegratedDbContext>()
                    .ImplementedBy<ComunAbpIntegratedDbContext>()
                    .LifestyleTransient()
                );

            LocalIocManager.IocContainer.Register(
               Component.For<ISecuenciaManager>()
                   .ImplementedBy<SecuenciaManager>()
                   .LifestyleTransient()
               );

            LocalIocManager.IocContainer.Register(
               Component.For<IDestinoSecuenciaManager>()
                   .ImplementedBy<DestinoSecuenciaManager>()
                   .LifestyleTransient()
               );
        }
        public GeneracionSecuenciasSQLIntegracionABPTests(ITestOutputHelper output)
        {
            this.output = output;

            destinoSecuenciaManager = LocalIocManager.Resolve<IDestinoSecuenciaManager>();
            destionoRepository = LocalIocManager.Resolve<IBaseRepository<DestinoSecuencia>>();

            secuenciaRepository = LocalIocManager.Resolve<IBaseRepository<Secuencia>>();

            unitOfWorkManager = LocalIocManager.Resolve<IUnitOfWorkManager>();

            //CREATE DATABASE statement not allowed within multi-statement transaction
            //Crear Manualmente la base de datos
            //System.Data.Entity.Database.SetInitializer(new CreateDatabaseIfNotExists<ComunAbpIntegratedDbContext>());


        }

        [Fact]
        public void Debe_Generar_Secuencia_SQL_Sin_Saltos_Incluso_Con_Procesos_Con_Excepciones()
        {
            //1. Crear una secuencia y guardar
            var secuencia = CrearSecuencia();
            output.WriteLine("Secuencia Inicial Codigo: {0}. Id {1}. Incremento {2}", secuencia.Codigo, secuencia.Id, secuencia.Incremento);


            //2. Ejecutar tareas en paralelo, para utilizar la secuencia
            Task task1 = Task.Factory.StartNew(() => EjecutarBloque("Bloque1", secuencia.Codigo));
            Task task2 = Task.Factory.StartNew(() => EjecutarBloque("Bloque2", secuencia.Codigo));
            Task task3 = Task.Factory.StartNew(() => EjecutarBloque("Bloque3", secuencia.Codigo));
            Task.WaitAll(task1, task2, task3);

            output.WriteLine("All threads complete");

            //3. Obtener destinos de secuencias
            output.WriteLine("Resultado de secuencias generadas, y asignadas al destino. Utilizando secuencia {0}", secuencia.Codigo);

            var list = new List<DestinoSecuencia>();
            using (var unitOfWork = unitOfWorkManager.Begin())
            {
                list = destionoRepository.GetAll().Where(d => d.SecuenciaUtilizada == secuencia.Codigo).ToList();
                unitOfWork.Complete();
            }

            foreach (var item in list)
            {
                output.WriteLine(item.ToString());
            }
        }

        [Fact]
        public void Debe_Generar_Secuencia_SQL_WithIsoSerializable_Sin_Saltos_Incluso_Con_Procesos_Con_Excepciones()
        {
            //1. Crear una secuencia y guardar
            var secuencia = CrearSecuencia();
            output.WriteLine("Secuencia Inicial Codigo: {0}. Id {1}. Incremento {2}", secuencia.Codigo, secuencia.Id, secuencia.Incremento);


            //2. Ejecutar tareas en paralelo, para utilizar la secuencia
            Task task1 = Task.Factory.StartNew(() => EjecutarBloqueSQLWithIsoSerializable("Bloque1", secuencia.Codigo));
            Task task2 = Task.Factory.StartNew(() => EjecutarBloqueSQLWithIsoSerializable("Bloque2", secuencia.Codigo));
            Task task3 = Task.Factory.StartNew(() => EjecutarBloqueSQLWithIsoSerializable("Bloque3", secuencia.Codigo));
            Task.WaitAll(task1, task2, task3);

            output.WriteLine("All threads complete");

            //3. Obtener destinos de secuencias
            output.WriteLine("Resultado de secuencias generadas, y asignadas al destino. Utilizando secuencia {0}", secuencia.Codigo);

            var list = new List<DestinoSecuencia>();
            using (var unitOfWork = unitOfWorkManager.Begin())
            {
                list = destionoRepository.GetAll().Where(d => d.SecuenciaUtilizada == secuencia.Codigo).ToList();
                unitOfWork.Complete();
            }

            foreach (var item in list)
            {
                output.WriteLine(item.ToString());
            }
        }


        [Fact]
        public void Debe_Generar_Secuencia_SQL_And_Lock_SQL_Sin_Saltos_Incluso_Con_Procesos_Con_Excepciones()
        {
            //1. Crear una secuencia y guardar
            var secuencia = CrearSecuencia();
            output.WriteLine("Secuencia Inicial Codigo: {0}. Id {1}. Incremento {2}", secuencia.Codigo, secuencia.Id, secuencia.Incremento);


            //2. Ejecutar tareas en paralelo, para utilizar la secuencia
            Task task1 = Task.Factory.StartNew(() => EjecutarBloqueSQLAndLockSQL("Bloque1", secuencia.Codigo));
            Task task2 = Task.Factory.StartNew(() => EjecutarBloqueSQLAndLockSQL("Bloque2", secuencia.Codigo));
            Task task3 = Task.Factory.StartNew(() => EjecutarBloqueSQLAndLockSQL("Bloque3", secuencia.Codigo));
            Task.WaitAll(task1, task2, task3);

            output.WriteLine("All threads complete");

            //3. Obtener destinos de secuencias
            output.WriteLine("Resultado de secuencias generadas, y asignadas al destino. Utilizando secuencia {0}", secuencia.Codigo);

            var list = new List<DestinoSecuencia>();
            using (var unitOfWork = unitOfWorkManager.Begin())
            {
                list = destionoRepository.GetAll().Where(d => d.SecuenciaUtilizada == secuencia.Codigo).ToList();
                unitOfWork.Complete();
            }

            foreach (var item in list)
            {
                output.WriteLine(item.ToString());
            }
        }


        [Fact]
        public void Debe_Generar_Secuencia_SQL_And_Lock_Sin_Saltos_Incluso_Con_Procesos_Con_Excepciones()
        {
            //1. Crear una secuencia y guardar
            var secuencia = CrearSecuencia();
            output.WriteLine("Secuencia Inicial Codigo: {0}. Id {1}. Incremento {2}", secuencia.Codigo, secuencia.Id, secuencia.Incremento);


            //2. Ejecutar tareas en paralelo, para utilizar la secuencia
            Task task1 = Task.Factory.StartNew(() => EjecutarBloqueSQLAndLock("Bloque1", secuencia.Codigo));
            Task task2 = Task.Factory.StartNew(() => EjecutarBloqueSQLAndLock("Bloque2", secuencia.Codigo));
            Task task3 = Task.Factory.StartNew(() => EjecutarBloqueSQLAndLock("Bloque3", secuencia.Codigo));
            Task.WaitAll(task1, task2, task3);

            output.WriteLine("All threads complete");

            //3. Obtener destinos de secuencias
            output.WriteLine("Resultado de secuencias generadas, y asignadas al destino. Utilizando secuencia {0}", secuencia.Codigo);

            var list = new List<DestinoSecuencia>();
            using (var unitOfWork = unitOfWorkManager.Begin())
            {
                list = destionoRepository.GetAll().Where(d => d.SecuenciaUtilizada == secuencia.Codigo).ToList();
                unitOfWork.Complete();
            }

            foreach (var item in list)
            {
                output.WriteLine(item.ToString());
            }
        }


        public Secuencia CrearSecuencia()
        {

            //1 Crear una secuencia
            var fakerSecuencia = new Faker<Secuencia>()
             .RuleFor(u => u.Nombre, (f, u) => f.Random.String(20))
             .RuleFor(u => u.Codigo, (f, u) => f.Random.String2(10))
             .RuleFor(u => u.Incremento, (f, u) => f.Random.Int(1, 4))
             ;

            var secuencia = fakerSecuencia.Generate();
            using (var unitOfWork = unitOfWorkManager.Begin())
            {
                secuencia = secuenciaRepository.Insert(secuencia);
                unitOfWork.Complete();
            }

            return secuencia;
        }

        private void EjecutarBloque(string strName, string codigoSecuencia)
        {
            for (int i = 1; i <= 5; i++)
            {

                var fakerModel = new Faker<DestinoSecuencia>()
                  .RuleFor(u => u.Foo, (f, u) => f.Name.FullName())
                  .RuleFor(u => u.Fecha, (f, u) => DateTime.Now)
                  ;

                var model = fakerModel.Generate();
                output.WriteLine(string.Format("Item Ejecutado: {0}-{1}", strName, i.ToString()));

                try
                {
                    model = destinoSecuenciaManager.CrearDestinoSecuenciaSQL(model, codigoSecuencia);
                    output.WriteLine(string.Format("Correctamente. {0}-{1}. Secuencia establecida {2}", strName, i.ToString(), model.Secuencia));
                }
                catch (SimulacionException ex)
                {
                    output.WriteLine(string.Format("Exception  {0}-{1}. {2}", strName, i.ToString(), ex.Message));
                }
                catch (Exception ex)
                {
                    output.WriteLine(string.Format("Exception  {0}-{1}. {2}", strName, i.ToString(), ex.ToString()));
                }

                output.WriteLine(string.Format("Resultado Item {0}-{1}. Destino: {2}", strName, i.ToString(), model.ToString()));

                Thread.Yield();
            }
        }


        private void EjecutarBloqueSQLWithIsoSerializable(string strName, string codigoSecuencia)
        {
            for (int i = 1; i <= 5; i++)
            {

                var fakerModel = new Faker<DestinoSecuencia>()
                  .RuleFor(u => u.Foo, (f, u) => f.Name.FullName())
                  .RuleFor(u => u.Fecha, (f, u) => DateTime.Now)
                  ;

                var model = fakerModel.Generate();
                output.WriteLine(string.Format("Item Ejecutado: {0}-{1}", strName, i.ToString()));

                try
                {

                    model = destinoSecuenciaManager.CrearDestinoSecuenciaSQLWithIsoSerializable(model, codigoSecuencia);
                    output.WriteLine(string.Format("Correctamente. {0}-{1}. Secuencia establecida {2}", strName, i.ToString(), model.Secuencia));
                }
                catch (SimulacionException ex)
                {
                    output.WriteLine(string.Format("SimulacionException  {0}-{1}. {2}", strName, i.ToString(), ex.Message));
                }
                catch (Exception ex)
                {
                    output.WriteLine(string.Format("Exception    {0}-{1}. {2}", strName, i.ToString(), ex.Message));
                }

                output.WriteLine(string.Format("Resultado Item {0}-{1}. Destino: {2}", strName, i.ToString(), model.ToString()));

                Thread.Yield();
            }
        }


        private void EjecutarBloqueSQLAndLockSQL(string strName, string codigoSecuencia)
        {
            for (int i = 1; i <= 5; i++)
            {

                var fakerModel = new Faker<DestinoSecuencia>()
                  .RuleFor(u => u.Foo, (f, u) => f.Name.FullName())
                  .RuleFor(u => u.Fecha, (f, u) => DateTime.Now)
                  ;

                var model = fakerModel.Generate();
                output.WriteLine(string.Format("Item Ejecutado: {0}-{1}", strName, i.ToString()));

                try
                {

                    model = destinoSecuenciaManager.CrearDestinoSecuenciaSQLAndLockSQL(model, codigoSecuencia);
                    output.WriteLine(string.Format("Correctamente. {0}-{1}. Secuencia establecida {2}", strName, i.ToString(), model.Secuencia));
                }
                catch (SimulacionException ex)
                {
                    output.WriteLine(string.Format("SimulacionException  {0}-{1}. {2}", strName, i.ToString(), ex.Message));
                }
                catch (Exception ex)
                {
                    output.WriteLine(string.Format("Exception  {0}-{1}. {2}", strName, i.ToString(), ex.Message));
                }

                output.WriteLine(string.Format("Resultado Item {0}-{1}. Destino: {2}", strName, i.ToString(), model.ToString()));

                Thread.Yield();
            }
        }

        private void EjecutarBloqueSQLAndLock(string strName, string codigoSecuencia)
        {
            for (int i = 1; i <= 5; i++)
            {

                var fakerModel = new Faker<DestinoSecuencia>()
                  .RuleFor(u => u.Foo, (f, u) => f.Name.FullName())
                  .RuleFor(u => u.Fecha, (f, u) => DateTime.Now)
                  ;

                var model = fakerModel.Generate();
                output.WriteLine(string.Format("Item Ejecutado: {0}-{1}", strName, i.ToString()));

                try
                {
                    
                    model = destinoSecuenciaManager.CrearDestinoSecuenciaSQLAndLock(model, codigoSecuencia);
                    output.WriteLine(string.Format("Correctamente. {0}-{1}. Secuencia establecida {2}", strName, i.ToString(), model.Secuencia));
                }
                catch (SimulacionException ex)
                {
                    output.WriteLine(string.Format("Message  {0}-{1}. {2}", strName, i.ToString(), ex.Message));
                }
                catch (Exception ex)
                {
                    output.WriteLine(string.Format("Exception  {0}-{1}. {2}", strName, i.ToString(), ex.Message));
                }

                output.WriteLine(string.Format("Resultado Item {0}-{1}. Destino: {2}", strName, i.ToString(), model.ToString()));

                Thread.Yield();
            }
        }



    }


}
