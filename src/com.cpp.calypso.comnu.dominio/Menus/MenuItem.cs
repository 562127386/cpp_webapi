using System;


namespace com.cpp.calypso.comun.dominio
{
    /// <summary>
    /// Item de Menu
    /// </summary>
    [Serializable]
    public class MenuItem : EntityNamed
    {
        //public int Id { get; set; }

        //[Obligado]
        //[LongitudMayor(15)]
        //public  string Codigo { get; set; }

        //[Obligado]
        //[LongitudMayor(80)]
        //public  string Nombre { get; set; }


        [LongitudMayor(255)]
        public  string Descripcion { get; set; }

        [LongitudMayor(255)]
        public  string Url { get; set; }


        [Obligado]
        public EstadoItemMenu Estado { get; set; }

        /// <summary>
        /// Tipo del Item de Menu (Contenedor / Item Menu)
        /// </summary>
        [Obligado]
        public TipoItemMenu TipoId { get; set; }

        /// <summary>
        /// Orden del item menu
        /// </summary>
         [Obligado]
        public int Orden { get; set; }

         /// <summary>
         /// Icono del item menu
         /// </summary>
         public string Icono { get; set; }

        public  int MenuId { get; set; }

        public virtual Menu Menu { get; set; }

        public  int? PadreId { get; set; }

        public virtual MenuItem Padre { get; set; }

        public int? FuncionalidadId { get; set; }


        ///// <summary>
        ///// Funcionalidad asociada al menu
        ///// </summary>
        //public virtual Funcionalidad Funcionalidad { get; set; }

        
        ////public  int VersionRegistro
        ////{
        ////    get;
        ////    set;
        ////}
    }


    public enum EstadoItemMenu
    {
        Desactivo = 0,
        Activo = 1
    }


    public enum TipoItemMenu
    {
        Contenedor = 1,
        ItemMenu = 2
    }  
}
