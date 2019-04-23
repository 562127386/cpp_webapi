﻿using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.proyecto.aplicacion.Dto;
using com.cpp.calypso.proyecto.aplicacion.Interfaces;
using com.cpp.calypso.proyecto.dominio.Entidades;

namespace com.cpp.calypso.proyecto.aplicacion.Service
{
    public class ArchivoAsyncBaseCrudAppService
        : AsyncBaseCrudAppService<Archivo, ArchivoDto, PagedAndFilteredResultRequestDto>,
        IArchivoAsyncBaseCrudAppService
    {
        public ArchivoAsyncBaseCrudAppService(IBaseRepository<Archivo> repository) : base(repository)
        {
        }
    }
}
