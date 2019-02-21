using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.proyecto.dominio.Servicios_Proveedores;

namespace com.cpp.calypso.proyecto.aplicacion
{
    public interface ICatalogoAsyncBaseCrudAppService : IAsyncBaseCrudAppService<Catalogo, CatalogoDto, PagedAndFilteredResultRequestDto>
    {
    }
}
