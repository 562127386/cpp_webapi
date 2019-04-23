using System.Collections.Generic;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.proyecto.dominio.Entidades;
using com.cpp.calypso.proyecto.aplicacion.Dto;
using Newtonsoft.Json.Linq;

namespace com.cpp.calypso.proyecto.aplicacion.Interfaces
{
    public interface ITipoCatalogoAsyncBaseCrudAppService : IAsyncBaseCrudAppService<TipoCatalogo, TipoCatalogoDto, PagedAndFilteredResultRequestDto>
    {
        JArray Sync(int version, JArray registrosJson, List<int> usuariosId);
    }
}
