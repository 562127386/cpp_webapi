using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.proyecto.aplicacion.Dto;
using com.cpp.calypso.proyecto.aplicacion.Interfaces;
using com.cpp.calypso.proyecto.dominio.Entidades;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace com.cpp.calypso.proyecto.aplicacion.Service
{
    public class SincronizacionLogAsyncBaseCrudAppService : AsyncBaseCrudAppService<SincronizacionLog, SincronizacionLogDto, PagedAndFilteredResultRequestDto>, ISincronizacionLogAsyncBaseCrudAppService
    {
        public SincronizacionLogAsyncBaseCrudAppService(
            IBaseRepository<SincronizacionLog> repository
            ) : base(repository)
        {
        }
    }
}
