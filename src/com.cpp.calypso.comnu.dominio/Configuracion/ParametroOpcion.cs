using System;
using Abp.Domain.Entities;


namespace com.cpp.calypso.comun.dominio
{
    /// <summary>
    /// Opcion de parametro.
    /// </summary>
    [Serializable]
    public class ParametroOpcion : Entity
    {
        //public int Id { get; set; }

        [Obligado]
        [LongitudMayor(30)]
        public string Valor { get; set; }

        [Obligado]
        [LongitudMayor(255)]
        public string Texto { get; set; }


        /// <summary>
        /// Identificador del Parametro
        /// </summary>
        [Obligado]
        public int ParametroId { get; set; }

        public virtual ParametroSistema Parametro { get; set; }
 
    }
}
