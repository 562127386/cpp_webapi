using System;
using Abp.Domain.Entities;


namespace com.cpp.calypso.comun.dominio
{
    /// <summary>
    /// Representa una entidad de gestion de auditoria. (Visualizacion)
    /// </summary>
    [Serializable]
    public class Auditoria : Entity
    {
        
        //public int Id { get; set; }
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime Fecha  { get; set; }

        public string Identificacion  { get; set; }

        public string Usuario  { get; set; }

        public string Funcionalidad  { get; set; }

        public string Accion  { get; set; }
         

        public string Mensaje  { get; set; }

        /// <summary>
        /// Direccion IP del usuario 
        /// </summary>
        public string IpAddress { get; set; }
 

    }
}
