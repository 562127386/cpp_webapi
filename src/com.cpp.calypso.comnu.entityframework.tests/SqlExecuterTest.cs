using Abp.Domain.Entities;
using Abp.EntityFramework;
using Bogus;
using com.cpp.calypso.comun.dominio;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;
using Xunit;
using Xunit.Abstractions;

namespace com.cpp.calypso.comun.entityframework.tests
{
    public class SqlExecuterTestDbContext : PrincipalBaseDbContext
    {
        public DbSet<SqlExecuterEntityTest> EntidadTest { get; set; }

        public SqlExecuterTestDbContext():base("ComunEntityFrameworkDbContextTest")
        {

        }

        public SqlExecuterTestDbContext(DbConnection existingConnection)
        :base(existingConnection){

        }
    }

    public class SqlExecuterEntityTest : Entity
    {
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public DateTime Fecha { get; set; }
    }

    public class SqlExecuterTest 
    {
        private readonly ITestOutputHelper output;

        public SqlExecuterTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public async Task Debe_Recuperar_Valor_Consulta_SQL()
        {
            var totalRegistros = 3;
            //Preparar datos
            using (var context = new SqlExecuterTestDbContext())
            {
                var simpleDbContextProvider = new SimpleDbContextProvider<SqlExecuterTestDbContext>(context);
                var sqlExecuter = new SqlExecuter(simpleDbContextProvider);
                
                //1. Eliminar todos los datos existentes
                var items = await context.Parametros.ToListAsync();
                context.Parametros.RemoveRange(items);
              
                //2. Agregar Datos. 
                var list = FakeParametros(totalRegistros);
                foreach (var item in list)
                {
                    context.Parametros.Add(item);
                }
                context.SaveChanges();
            }

           
            //Ejecutar Test
            using (var context = new SqlExecuterTestDbContext())
            {
                var simpleDbContextProvider = new SimpleDbContextProvider<SqlExecuterTestDbContext>(context);
                var sqlExecuter = new SqlExecuter(simpleDbContextProvider);
 
                //2. Consultar datos
                var verificarTotalRegistros = await sqlExecuter.SqlQuery<int>("SELECT count(*) FROM ParametroSistemas");

                //3. Verificar
                //Los valores recuperados con consulta SQL debe ser iguales a los total de registros insertados
                verificarTotalRegistros.ShouldBe(totalRegistros); 
            }
        }

        [Fact]
        public async Task Debe_Ejecutar_Consulta_SQL()
        {

            var totalRegistros = 5;
            //Preparar datos
            using (var context = new SqlExecuterTestDbContext())
            {
                var simpleDbContextProvider = new SimpleDbContextProvider<SqlExecuterTestDbContext>(context);
                var sqlExecuter = new SqlExecuter(simpleDbContextProvider);

                //1. Eliminar todos los datos existentes
                var items = await context.Parametros.ToListAsync();
                context.Parametros.RemoveRange(items);

                //2. Agregar Datos. 
                var list = FakeParametros(totalRegistros);
                foreach (var item in list)
                {
                    context.Parametros.Add(item);
                }
                context.SaveChanges();
            }

            //Ejecutar Test
            using (var context = new SqlExecuterTestDbContext())
            {
                var simpleDbContextProvider = new SimpleDbContextProvider<SqlExecuterTestDbContext>(context);
                var sqlExecuter = new SqlExecuter(simpleDbContextProvider);

        
                //3. Consultar datos
                var valorCambiar = "new.value.foo";
                var registrosModificados =  sqlExecuter.Execute("UPDATE ParametroSistemas SET valor = @value", new SqlParameter("@value", valorCambiar));
                registrosModificados.ShouldBe(totalRegistros);


                //4. Verificar
                foreach (var parametro in context.Parametros)
                {
                    parametro.Valor.ShouldBe(valorCambiar);

                }
            }
        }

        [Fact]
        public async Task Debe_Ejecutar_Consulta_SQL_Con_Bloque_Transaccion()
        {
            var totalRegistros = 5;
            //Preparar datos
            using (var context = new SqlExecuterTestDbContext())
            {
                var simpleDbContextProvider = new SimpleDbContextProvider<SqlExecuterTestDbContext>(context);
                var sqlExecuter = new SqlExecuter(simpleDbContextProvider);

                //1. Eliminar todos los datos existentes
                var items = await context.Parametros.ToListAsync();
                context.Parametros.RemoveRange(items);

                //2. Agregar Datos. 
                var list = FakeParametros(totalRegistros);
                foreach (var item in list)
                {
                    context.Parametros.Add(item);
                }
                context.SaveChanges();
            }

            var valorTransaccionComplementa = "new.value.bar";
            var valorTransaccionRollback = "foo.rollback";

            var valorCambiar = valorTransaccionComplementa;

            using (var scope = new TransactionScope())
            {
                using (var context = new SqlExecuterTestDbContext())
                {
                    var simpleDbContextProvider = new SimpleDbContextProvider<SqlExecuterTestDbContext>(context);
                    var sqlExecuter = new SqlExecuter(simpleDbContextProvider);

                     var registrosModificados = sqlExecuter.Execute("UPDATE ParametroSistemas SET valor = @value", new SqlParameter("@value", valorCambiar));
                    registrosModificados.ShouldBe(totalRegistros);
                }

                //transctional code…
                scope.Complete();
            }

            //Realizar otra actualizacion. Rollback
            valorCambiar = valorTransaccionRollback;
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var context = new SqlExecuterTestDbContext())
                    {
                        var simpleDbContextProvider = new SimpleDbContextProvider<SqlExecuterTestDbContext>(context);
                        var sqlExecuter = new SqlExecuter(simpleDbContextProvider);

                        var registrosModificados = sqlExecuter.Execute("UPDATE ParametroSistemas SET valor = @value", new SqlParameter("@value", valorCambiar));
                        registrosModificados.ShouldBe(totalRegistros);
                    }

                    //transctional rollback…
                    throw new Exception("RollBack..");
                }
            }
            catch (Exception ex)
            {
                output.WriteLine(ex.Message);
            }

            using (var context = new SqlExecuterTestDbContext())
            {
                var simpleDbContextProvider = new SimpleDbContextProvider<SqlExecuterTestDbContext>(context);
                var sqlExecuter = new SqlExecuter(simpleDbContextProvider);
  
                //4. Verificar
                foreach (var parametro in context.Parametros)
                {
                    parametro.Valor.ShouldBe(valorTransaccionComplementa);
                }
            }

        }


        public static List<ParametroSistema> FakeParametros(int cantidad)
        {
            
            var fakerModel = new Faker<ParametroSistema>()
            .RuleFor(u => u.Codigo, (f, u) => f.Random.String2(6))
            .RuleFor(u => u.Valor, (f, u) => f.Random.String2(20))
            .RuleFor(u => u.Nombre, (f, u) => f.Company.CompanyName())
            .RuleFor(u => u.Categoria, (f, u) => CategoriaParametro.General)
            .RuleFor(u => u.Tipo, (f, u) => TipoParametro.Cadena)
            ;

            return fakerModel.Generate(cantidad);
        
        }
    }
}
