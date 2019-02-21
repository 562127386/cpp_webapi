using System;

namespace com.cpp.calypso.comun.dominio
{

    /// <summary>
    /// Bloque de usuarios
    /// </summary>
    public interface IUserLockout
    {
        bool BloqueoHabilitado { get; set; }
        int CantidadAccesoFallido { get; set; }
        DateTime? FechaFinalizacionBloqueUtc { get; set; }
    }
}