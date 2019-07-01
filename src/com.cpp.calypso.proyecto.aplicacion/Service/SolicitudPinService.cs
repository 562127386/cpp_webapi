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
    public class SolicitudPinAsyncBaseCrudAppService : AsyncBaseCrudAppService<SolicitudPin, SolicitudPinDto, PagedAndFilteredResultRequestDto>, ISolicitudPinAsyncBaseCrudAppService
    {
        private readonly IBaseRepository<Usuario> _usuarioRepository;

        public SolicitudPinAsyncBaseCrudAppService(
            IBaseRepository<SolicitudPin> repository
            ) : base(repository)
        {
        }


        public void SolicitarPin(string correoElectronico)
        {
            var solicitudPin = new SolicitudPin()
            {
                CorreoElectronico = correoElectronico,
                FechaSolicitud = DateTime.Now,
                Estado = "SOL"
            };

            Repository.Insert(solicitudPin);

        }
    }
}
