namespace com.cpp.calypso.tests
{
    public interface IDestinoSecuenciaManager
    {
        DestinoSecuencia CrearDestinoSecuencia(DestinoSecuencia destinoSecuencia, string codigSecuencia);

        DestinoSecuencia CrearDestinoSecuenciaLock(DestinoSecuencia destinoSecuencia, string codigSecuencia);

        DestinoSecuencia CrearDestinoSecuenciaSQL(DestinoSecuencia destinoSecuencia, string codigSecuencia);

        DestinoSecuencia CrearDestinoSecuenciaSQLWithIsoSerializable(DestinoSecuencia destinoSecuencia, string codigSecuencia);
       
        DestinoSecuencia CrearDestinoSecuenciaSQLAndLockSQL(DestinoSecuencia destinoSecuencia, string codigSecuencia);

        DestinoSecuencia CrearDestinoSecuenciaSQLAndLock(DestinoSecuencia destinoSecuencia, string codigSecuencia);

        
    }
}