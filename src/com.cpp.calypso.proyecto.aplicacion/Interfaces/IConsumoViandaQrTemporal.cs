using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.proyecto.aplicacion.Dto;
using com.cpp.calypso.proyecto.dominio.Entidades;
using Newtonsoft.Json.Linq;

namespace com.cpp.calypso.proyecto.aplicacion.Interfaces
{
    public interface IConsumoViandaQrTemporalAsyncBaseCrudAppService : IAsyncBaseCrudAppService<ConsumoViandaQrTemporal, ConsumoViandaQrTemporalDto, PagedAndFilteredResultRequestDto>
    {
        JArray Sync(int version, JArray registrosJson, List<int> usuariosId);
    }
}
