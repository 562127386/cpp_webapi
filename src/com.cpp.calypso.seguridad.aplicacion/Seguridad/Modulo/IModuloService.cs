using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;
using System.Threading.Tasks;

namespace com.cpp.calypso.seguridad.aplicacion
{
    public interface  IModuloService :
        IAsyncBaseCrudAppService<Modulo, ModuloDto, PagedAndFilteredResultRequestDto>
    {

        ModuloDto Get(string codigo);

        Task<ModuloFuncionalidDto> GetModuleAndFunctionality(int id);
    }
}
