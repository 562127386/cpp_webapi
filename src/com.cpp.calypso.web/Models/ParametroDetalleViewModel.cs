using com.cpp.calypso.comun.dominio;
using System.ComponentModel;

namespace com.cpp.calypso.web
{
    public class ParametroDetalleViewModel : AuditableEntityViewModel
    {
        public int Id { get; set; }

        [DisplayNameAttribute("Código")]
        public string Codigo { get; set; }

      
        public string Nombre { get; set; }

       
        [DisplayNameAttribute("Descripción")]
        public string Descripcion { get; set; }

        /// <summary>
        /// Categoria del parametro. 
        /// </summary>
        [DisplayNameAttribute("Categoría")]
        public CategoriaParametro Categoria { get; set; }

        /// <summary>
        /// Tipo del Parametro. (Valor Simple, Lista de Valores, Json)
        /// </summary>
        public TipoParametro Tipo { get; set; }


        /// <summary>
        /// Valor del parametro
        /// </summary>
        //[StringLength(255)]
        public string Valor { get; set; }

        /// <summary>
        /// Si el parametro es editable por el usuario (UI)
        /// </summary>
        [DisplayNameAttribute("Es editable?")]
        public bool EsEditable
        {
            get;
            set;
        }


         
    }
}