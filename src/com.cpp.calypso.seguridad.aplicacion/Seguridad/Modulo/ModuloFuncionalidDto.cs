using com.cpp.calypso.comun.aplicacion;
using System.Collections.Generic;

namespace com.cpp.calypso.seguridad.aplicacion
{
    public class ModuloFuncionalidDto : ModuloDto
    {

        public virtual ICollection<FuncionalidadDto> Funcionalidades { get; set; }

    }
}