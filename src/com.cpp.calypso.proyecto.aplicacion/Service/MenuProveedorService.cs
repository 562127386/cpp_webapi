using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.proyecto.aplicacion.Dto;
using com.cpp.calypso.proyecto.aplicacion.Interfaces;
using com.cpp.calypso.proyecto.dominio.Entidades;

namespace com.cpp.calypso.proyecto.aplicacion.Service
{
    public class MenuProveedorAsyncBaseCrudAppService
        : AsyncBaseCrudAppService<MenuProveedor, MenuProveedorDto, PagedAndFilteredResultRequestDto>,
        IMenuProveedorAsyncBaseCrudAppService
    {
        public MenuProveedorAsyncBaseCrudAppService(IBaseRepository<MenuProveedor> repository) : base(repository)
        {
        }
    }
}
