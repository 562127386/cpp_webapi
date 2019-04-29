using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.proyecto.aplicacion.Dto;
using com.cpp.calypso.proyecto.dominio.Entidades;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace com.cpp.calypso.proyecto.aplicacion.Interfaces
{
    public interface IContratoProveedorAsyncBaseCrudAppService : IAsyncBaseCrudAppService<ContratoProveedor, ContratoProveedorDto, PagedAndFilteredResultRequestDto>
    {
        JArray Sync(int version, JArray registrosJson, List<int> usuariosId);
    }
}
