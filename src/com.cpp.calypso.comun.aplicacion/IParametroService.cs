using Abp.Application.Services;
using com.cpp.calypso.comun.dominio;

namespace com.cpp.calypso.comun.aplicacion
{
    /// <summary>
    /// Servicio para parametros del Sistema
    /// </summary>
    public interface IParametroService : IParametroManager, 
        IEntityService<ParametroSistema>, IApplicationService
    {
       
         
    }
}
