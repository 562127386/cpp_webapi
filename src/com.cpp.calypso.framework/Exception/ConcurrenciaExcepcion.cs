using System;


namespace com.cpp.calypso.framework
{
    public class ConcurrenciaExcepcion : Exception
    {
        public ConcurrenciaExcepcion(string mensaje)
            :base(mensaje)
        {
        }
    }
    
}