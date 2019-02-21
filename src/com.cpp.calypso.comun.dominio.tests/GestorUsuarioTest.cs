using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp;
using Bogus;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.cpp.calypso.comun.dominio.tests
{
    [TestClass]
    public class GestorUsuarioTest
    {
        private TestContext m_testContext;
        public TestContext TestContext
        {
            get { return m_testContext; }
            set { m_testContext = value; }
        }

        [TestMethod]
        public void Debe_Generar_SecurityStamp()
        {
            var securityStamp = SequentialGuidGenerator.Instance.Create().ToString();
            TestContext.WriteLine(securityStamp);
        }

        [TestMethod]
        public async Task Debe_Crear_Usuario_Simple_Clave() {

            var fakerUsuarioSimpleClave = new Faker<UsuarioSimpleClave>()
               .RuleFor(u => u.UserName, (f, u) => f.Person.UserName)
               .RuleFor(u => u.Id, (f, u) => f.Random.Int(0))
               ;

            var usuario = fakerUsuarioSimpleClave.Generate();

            //AD0dRB615MIDJy01SdDRD5UNPlwKoAmGQBaVPKkm2xJayzmA3OYUA9oxLXnDRjH1QA==
            //usuario.Password = "Clave.Texto.Plano";

            //AMZ5r53e7lcOyxYaD3KIlSvC3/jZUc1G3I/Ze397gNzGwOVOAwV34vPxigrlI4tsEA==
            usuario.Password = "clave";

            //Generar Hash
            usuario.Password = new PasswordHasher().HashPassword(usuario.Password);

            System.Data.Entity.Database.SetInitializer(new CreateDatabaseIfNotExists<FooDbContext>());


            //1. Guardar usuario
            using (var db = new FooDbContext())
            {
                var userStore = new SimpleClaveUserStore(db);
                await userStore.CreateAsync(usuario);

            }

            UsuarioSimpleClave usuarioRecuperado = null;
            //2. recuperar Usuario
            using (var db = new FooDbContext())
            {
                var userStore = new SimpleClaveUserStore(db);

                usuarioRecuperado = await userStore.FindByNameAsync(usuario.UserName);
            }

            TestContext.WriteLine(usuarioRecuperado.ToString());

        }

        [TestMethod]
        public async Task Debe_Crear_Usuario_Simple()
        {
            var fakerUsuarioSimple = new Faker<UsuarioSimple>()
                .RuleFor(u => u.UserName, (f, u) => f.Person.UserName)
                .RuleFor(u => u.Id, (f, u) => f.Random.Int(0))
                ;

            var usuario = fakerUsuarioSimple.Generate();

     
            System.Data.Entity.Database.SetInitializer(new CreateDatabaseIfNotExists<FooDbContext>());


            //1. Guardar usuario
            using (var db = new FooDbContext())
            {
                var userStore = new SimpleUserStore(db);
                await userStore.CreateAsync(usuario);

            }

            UsuarioSimple usuarioRecuperado = null;
            //2. recuperar Usuario
            using (var db = new FooDbContext())
            {
                var userStore = new SimpleUserStore(db);

                usuarioRecuperado = await userStore.FindByNameAsync(usuario.UserName);
            }

            TestContext.WriteLine(usuarioRecuperado.ToString());


        }
    }

    public class FooDbContext : DbContext
    {
        public FooDbContext():base("fooDbContext")
        {
            
        }

        public virtual IDbSet<UsuarioSimple> UsuarioSimple { get; set; }
        public virtual IDbSet<UsuarioSimpleClave> UsuarioSimpleClave { get; set; }

    }

    /// <summary>
    /// Usuario Simple
    /// </summary>
    public class UsuarioSimple : IUser<int>
    {
        public int Id { set; get; }

        public string UserName { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0} UserName: {1}", Id, UserName);
        }
    }

    public class UsuarioSimpleClave : IUser<int>
    {
        public int Id { set; get; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0} UserName: {1} Password {2} ", Id, UserName, Password);
        }
    }

    public class SimpleClaveUserStore : IUserStore<UsuarioSimpleClave, int>,
        IUserPasswordStore<UsuarioSimpleClave, int>
    {
        private readonly FooDbContext dbContext;

        public SimpleClaveUserStore(FooDbContext dbContext)
        {
            this.dbContext = dbContext;
        }



        public Task CreateAsync(UsuarioSimpleClave user)
        {
            dbContext.UsuarioSimpleClave.Add(user);
            return dbContext.SaveChangesAsync();
        }

        public Task DeleteAsync(UsuarioSimpleClave user)
        {
            dbContext.UsuarioSimpleClave.Remove(user);
            dbContext.Configuration.ValidateOnSaveEnabled = false;
            return dbContext.SaveChangesAsync();
        }


        public Task<UsuarioSimpleClave> FindByIdAsync(int userId)
        {
            return dbContext.UsuarioSimpleClave.Where(u => u.Id == userId).FirstOrDefaultAsync();
        }

        public Task<UsuarioSimpleClave> FindByNameAsync(string userName)
        {
            return dbContext.UsuarioSimpleClave.Where(u => u.UserName.ToLower() == userName.ToLower()).FirstOrDefaultAsync();

        }

        public Task UpdateAsync(UsuarioSimpleClave user)
        {
            dbContext.UsuarioSimpleClave.Attach(user);
            dbContext.Entry(user).State = EntityState.Modified;
            dbContext.Configuration.ValidateOnSaveEnabled = false;
            return dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {

        }

        #region IUserPasswordStore

        public virtual Task SetPasswordHashAsync(UsuarioSimpleClave user, string passwordHash)
        {
            user.Password = passwordHash;
            return Task.FromResult(0);
        }

        public virtual Task<string> GetPasswordHashAsync(UsuarioSimpleClave user)
        {
            return Task.FromResult(user.Password);
        }

        public virtual Task<bool> HasPasswordAsync(UsuarioSimpleClave user)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.Password));
        }

        #endregion
         
    }

    public class SimpleUserStore  : IUserStore<UsuarioSimple, int>
    {
        private readonly FooDbContext dbContext;
        
        public SimpleUserStore(FooDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

      

        public Task CreateAsync(UsuarioSimple user)
        {
            dbContext.UsuarioSimple.Add(user);
            return dbContext.SaveChangesAsync();
        }

        public Task DeleteAsync(UsuarioSimple user)
        {
            dbContext.UsuarioSimple.Remove(user);
            dbContext.Configuration.ValidateOnSaveEnabled = false;
            return dbContext.SaveChangesAsync();
        }


        public Task<UsuarioSimple> FindByIdAsync(int userId)
        {
            return dbContext.UsuarioSimple.Where(u => u.Id == userId).FirstOrDefaultAsync();
        }

        public Task<UsuarioSimple> FindByNameAsync(string userName)
        {
            return dbContext.UsuarioSimple.Where(u => u.UserName.ToLower() == userName.ToLower()).FirstOrDefaultAsync();

        }

        public Task UpdateAsync(UsuarioSimple user)
        {
            dbContext.UsuarioSimple.Attach(user);
            dbContext.Entry(user).State = EntityState.Modified;
            dbContext.Configuration.ValidateOnSaveEnabled = false;
            return dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
             
        }

        
    }

}
