using Abp.Application.Services;

namespace com.cpp.calypso.comun.aplicacion
{
    public interface IBaseApplicationService: IApplicationService
    {

    }

    /// <summary>
    /// Interfaz para marcar que servicios seran generandos automaticamente las API
    /// </summary>
    public interface IDynamicApiApplicationService : IBaseApplicationService {

    }
}
