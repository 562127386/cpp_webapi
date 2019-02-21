namespace com.cpp.calypso.comun.dominio
{
    /// <summary>
    /// Gestor de parametros / configuraciones
    /// </summary>
    public interface IParametroManager
    {
        /// <summary>
        /// Obtener el valor del parametro 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="codigoParametro"></param>
        /// <returns></returns>
        T GetValor<T>(string codigoParametro);
    }
}
