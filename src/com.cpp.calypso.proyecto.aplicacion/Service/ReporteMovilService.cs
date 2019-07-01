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
    public class ReporteMovilAsyncBaseCrudAppService : AsyncBaseCrudAppService<ReporteMovil, ReporteMovilDto, PagedAndFilteredResultRequestDto>, IReporteMovilAsyncBaseCrudAppService
    {
        private readonly IBaseRepository<ReporteMovil> _repository;

        public ReporteMovilAsyncBaseCrudAppService(
            IBaseRepository<ReporteMovil> repository
            ) : base(repository)
        {
            _repository = repository;
        }


        public List<ReporteMovil> GetReportesMovilPorRolServicio(string rol, int servicioId)
        {
            var reportesMovil = _repository.GetAll()
                .Where(o => o.Rol == rol)
                .Where(o => o.CatalogoServicioId == servicioId)
                .ToList();

            return reportesMovil;
        }


    }
}
