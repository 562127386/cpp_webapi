//using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Infrastructure.Annotations;
//using System.Data.Entity.ModelConfiguration;
//using com.cpp.calypso.proyecto.dominio;

//namespace com.cpp.calypso.proyecto.entityframework
//{
//    public class FooMap :
//        EntityTypeConfiguration<Foo>
//    {
//         public FooMap()
//        {
//            ToTable("Foo", "SCH_ESQUEMA");


//            Property(d => d.Codigo)
//               .HasColumnAnnotation("IX_CODIGO",
//                new IndexAnnotation(new IndexAttribute() { IsUnique = true }));


//            HasKey(d => d.Id);
//            Property(d => d.Id)
//                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
 
//        }
//    }

//    public class ListaMap :
//        EntityTypeConfiguration<Lista>
//    {
//        public ListaMap()
//        {
//            ToTable("lista", "SCH_ESQUEMA");

//            HasKey(d => d.Id);
//            Property(d => d.Id)
//                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);



//            HasRequired(a => a.Foo)
//              .WithMany()
//              .HasForeignKey(u => u.FooId);

//        }
//    }
//}
