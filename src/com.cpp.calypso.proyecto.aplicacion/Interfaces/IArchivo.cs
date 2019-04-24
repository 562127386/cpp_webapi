using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.proyecto.aplicacion.Dto;
using com.cpp.calypso.proyecto.dominio.Entidades;
using System;

namespace com.cpp.calypso.proyecto.aplicacion.Interfaces
{
    public interface IArchivoAsyncBaseCrudAppService : IAsyncBaseCrudAppService<Archivo, ArchivoDto, PagedAndFilteredResultRequestDto>
    {
    }
}
