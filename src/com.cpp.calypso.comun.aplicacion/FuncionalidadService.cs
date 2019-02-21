using Abp.Domain.Repositories;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;
using System.Collections.Generic;
using System.Linq;

namespace com.cpp.calypso.comun.aplicacion
{


    public class FuncionalidadService : IFuncionalidadService
    {
        
        IRepository<Funcionalidad> _repository;
        ICacheManager _cacheManager;
        IApplication _application;

        public FuncionalidadService(IApplication application, 
            IRepository<Funcionalidad> repository, ICacheManager cacheManager)
        {
            _application = application;
            _repository = repository;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Obtener listado de funcionalidades del Sistema
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Funcionalidad> GetFuncionalidades()
        {
            var funcionalidades = _cacheManager.GetData(ConstantesCache.CACHE_FUNCIONALIDADES_SISTEMA) as IEnumerable<Funcionalidad>;
            if (funcionalidades == null)
            {
                //1. Recuperar las funcionalidaes
                funcionalidades = _repository.GetAllIncluding(incluir => incluir.Acciones).ToList();

                //TODO: JSA, Revisar si es mas conveniente establecer con una variable estatica a nivel de la aplicacion, y no utilizar el servicio de cache 
                //en ambos casos se tiene una copia del listado de todas funcionalidades del sistema.
                _cacheManager.Add(ConstantesCache.CACHE_FUNCIONALIDADES_SISTEMA, funcionalidades);
            }

            return funcionalidades;

        }
    }
}
