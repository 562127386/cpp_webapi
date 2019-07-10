using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.proyecto.aplicacion.Dto;
using com.cpp.calypso.proyecto.dominio.Entidades;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace com.cpp.calypso.proyecto.aplicacion.Interfaces
{
    public interface IRespaldoMovilAsyncBaseCrudAppService : IAsyncBaseCrudAppService<RespaldoMovil, RespaldoMovilDto, PagedAndFilteredResultRequestDto>
    {
        void Create(string json, int userId);
    }
}
