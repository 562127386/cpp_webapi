//using Abp.Domain.Entities;
//using com.cpp.calypso.comun.dominio;
//using System;
//using System.Collections.Generic;

//namespace com.cpp.calypso.proyecto.dominio
//{
//    [Serializable]
//    public class Foo : Entity //, IEntityNamed
//    {

//        public string Codigo { get; set; }

//        [Obligado]
//        public string Nombre { get; set; }

//        public DateTime FechaInicio { get; set; }

//        public DateTime FechaFinal { get; set; }

//        public Estado Estado { get; set; }

//        public ICollection<Lista> Items { get; set; }
//    }

//    public class Lista : Entity
//    {

//        public string Codigo { get; set; }

//        [Obligado]
//        public string Nombre { get; set; }

//        public int FooId { get; set; }

//        public virtual ICollection<Foo> Foo { get; set; }

//    }



//    public enum Estado
//    {
//        Activo,
//        Finalizado,
//        Obsoleto,
//        Aprobado
//    }
//}
