namespace com.cpp.calypso.comun.dominio
{
    /// <summary>
    /// Define una interface para autenticar a un usuario
    /// </summary>
    public interface IAuthentication
    {
        ///<summary>
        /// Verificar si el usuario se puede autentificar
        ///</summary>
        ///<param name="username"></param>
        ///<param name="password"></param>
        ///<returns></returns>
        bool Authenticate(string username, string password);


        ///<summary>
        /// Si se realiza una verificación del nombre usuario sin hacer diferencia entre mayusculas y minusculas
        ///</summary>
        ///<returns></returns>
        bool IsCompareStrictUserName();
    }
}
