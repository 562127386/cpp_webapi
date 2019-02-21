using System;
using System.Collections.Generic;


namespace com.cpp.calypso.comun.dominio
{

    /// <summary>
    /// Representa un Menu
    /// </summary>
    [Serializable]
    public class Menu : EntityNamed
    {
        public Menu()
        {
            this.Items = new List<MenuItem>();
        }


        //public int Id { get; set; }

        //[Obligado]
        //[LongitudMayor(15)]
        //public virtual string Codigo { get; set; }

        //[Obligado]
        //[LongitudMayor(80)]
        //public virtual string Nombre { get; set; }


        [LongitudMayor(255)]
        public virtual string Descripcion { get; set; }

        //[Required(ErrorMessageResourceType = typeof (Mensajes), ErrorMessageResourceName = "RequiredAttribute_ValidationError")]
        //public virtual int EstadoId { get; set; }

    


        public virtual ICollection<MenuItem> Items { get; set; }

        [Obligado]
        public virtual int ModuloId { get; set; }

        public virtual Modulo Modulo { get; set; }
    }
}
