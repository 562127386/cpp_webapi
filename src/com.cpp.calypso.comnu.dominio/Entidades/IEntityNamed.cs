using Abp.Domain.Entities;

namespace com.cpp.calypso.comun.dominio
{

    /// <summary>
    /// Intefaz de una entidad nombrada.  "Entidad Nombrada: Posee las propiedad codigo y nombre la entidad"
    /// </summary>
    public interface IEntityNamed : IEntity<int>
    {

        /// <summary>
        /// Codigo de la entidad
        /// </summary>
        string Codigo { get; set; }

        /// <summary>
        /// Nombre de la Entidad
        /// </summary>
        string Nombre { get; set; }
    }
}
