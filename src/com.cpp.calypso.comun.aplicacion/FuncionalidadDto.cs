using Abp.Application.Services.Dto;
using com.cpp.calypso.comun.dominio;
using System.ComponentModel;

namespace com.cpp.calypso.comun.aplicacion
{
    public class FuncionalidadDto : EntityDto
    {
        [Obligado]
        [LongitudMayor(15)]
        public string Codigo { get; set; }

        [Obligado]
        [LongitudMayor(80)]
        public string Nombre { get; set; }

        [LongitudMayor(255)]
        [DisplayNameAttribute("Descripción")]
        public string Descripcion { get; set; }


        /// <summary>
        /// Nombre del controlador que gestiona la funcionalidad. (MVC. Nombre del Controller)
        /// </summary>
        [Obligado]
        public string Controlador { get; set; }


        [Obligado]
        public EstadoFuncionalidad Estado { get; set; }

        [Obligado]
        public virtual int ModuloId { get; set; }
    }
}
