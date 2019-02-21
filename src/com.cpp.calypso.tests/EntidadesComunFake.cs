using Bogus;
using com.cpp.calypso.comun.dominio;
using System.Collections.Generic;
using System.Linq;

namespace com.cpp.calypso.tests
{
    public static class EntidadesComunFake
    {
        public static List<Usuario> FakeAgregarUsuarios(Modulo modulo, int cantidadUsuarios)
        {
            for (int i = 0; i < cantidadUsuarios; i++)
            {
                var fakerUsuario = new Faker<Usuario>()
               .RuleFor(u => u.Nombres, (f, u) => f.Name.FirstName())
               .RuleFor(u => u.Apellidos, (f, u) => f.Name.LastName())
               .RuleFor(u => u.Correo, (f, u) => f.Internet.Email())
               .RuleFor(u => u.Password, (f, u) => f.Internet.Password())
               .RuleFor(u => u.Cuenta, (f, u) => f.Person.UserName)
               .RuleFor(u => u.Identificacion, (f, u) => f.Random.String2(11))
               ;

                modulo.Usuarios.Add(fakerUsuario.Generate());
            }

            return modulo.Usuarios.ToList();
        }


        public static List<Modulo> FakeModuloConUsuarios(int cantidadModulos,int cantidadUsuarios) {

            var list = new List<Modulo>();

            for (int i = 0; i < cantidadModulos; i++)
            {
                var fakerModulo = new Faker<Modulo>()
                  .RuleFor(u => u.Nombre, (f, u) => f.Name.FullName())
                  .RuleFor(u => u.Codigo, (f, u) => f.Random.String(10))
                  ;
                var modulo = fakerModulo.Generate();
                FakeAgregarUsuarios(modulo, cantidadUsuarios);

                list.Add(modulo);
            }
            return list;
        }

        public static List<Funcionalidad> FakeFuncionalidades(int cantidad)
        {
            var fakerModulo = new Faker<Modulo>()
                .RuleFor(u => u.Nombre, (f, u) => f.Name.FullName())
                .RuleFor(u => u.Codigo, (f, u) => f.Random.String(10))
                ;

            var modulo = fakerModulo.Generate();


            var result = new List<Funcionalidad>();

            var faker = new Faker<Funcionalidad>()
                .RuleFor(u => u.Nombre, (f, u) => f.Name.FullName())
                .RuleFor(u => u.Codigo, (f, u) => f.Random.String(10))
                .RuleFor(u => u.Controlador, (f, u) => f.Internet.Url())
                .RuleFor(u => u.Estado, (f) => EstadoFuncionalidad.Activa)
                ;


            var fakerAccion = new Faker<Accion>()
                 .RuleFor(u => u.Nombre, (f, u) => f.Name.FullName())
                 .RuleFor(u => u.Codigo, (f, u) => f.Random.String(5))
                 ;

            for (int i = 0; i < cantidad; i++)
            {

                var funcionalidad = faker.Generate();

                //Acciones
                funcionalidad.AddAccion(fakerAccion.Generate());
                funcionalidad.AddAccion(fakerAccion.Generate());

                modulo.AddFuncionalidad(funcionalidad);

                result.Add(funcionalidad);
            }


            return result;
        }
    }
}
