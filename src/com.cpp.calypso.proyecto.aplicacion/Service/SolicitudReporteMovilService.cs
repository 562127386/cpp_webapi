using System;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.proyecto.aplicacion.Dto;
using com.cpp.calypso.proyecto.dominio.Entidades;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.proyecto.aplicacion.Interfaces;

namespace com.cpp.calypso.proyecto.aplicacion.Service
{
    public class SolicitudReporteMovilAsyncBaseCrudAppService : AsyncBaseCrudAppService<SolicitudReporteMovil, SolicitudReporteMovilDto, PagedAndFilteredResultRequestDto>, ISolicitudReporteMovilAsyncBaseCrudAppService
    {
        private readonly IBaseRepository<SolicitudReporteMovil> _repository;
        private readonly IReporteMovilAsyncBaseCrudAppService _reporteMovilService;

        public SolicitudReporteMovilAsyncBaseCrudAppService(
            IBaseRepository<SolicitudReporteMovil> repository
            ) : base(repository)
        {
            _repository = repository;
        }


        
    }
}
