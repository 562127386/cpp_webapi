namespace com.cpp.calypso.comun.dominio
{

    ///<summary>
    /// Define una interfaz para los objetos de negocio que requieren control de concurrencia
    ///</summary>
    public interface IVersionable
    {
        ///TODO: JSA Se puede Establecer la version desde afuera del objeto, no deberia permitir realizarlo. Se puede utilizar interna
        ///<summary>
        /// Número consecutivo para el control de concurrencia
        ///</summary>
        int VersionRegistro { get; set; }
    }
}
