namespace com.cpp.calypso.comun.dominio
{
    /// <summary>
    /// A domain  tuple of (field_name, operator, value)
    /// Ejemplo: Tuple (Codigo, ==, 'FOO')
    /// </summary>
    public class FilterEntity
    {
        public FilterEntity()
        {
            Operator = "==";
        }

        /// <summary>
        /// Nombre del Campo
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Operador aplicado.
        /// 
        /// "==", "<", ">", "<=", ">=", "!=",  "Contains", "StartsWith"
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        ///  Valor del Filtro
        /// </summary>
        public object Value { get; set; }
    }


    //public class FilterSimplified
    //{
    //    public string F { get; set; }
    //    public string O { get; set; }
    //    public string V { get; set; }
    //}
}
