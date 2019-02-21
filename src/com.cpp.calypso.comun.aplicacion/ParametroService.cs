using System;
using System.Collections.Generic;
using System.Linq;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;

namespace com.cpp.calypso.comun.aplicacion
{
    public class ParametroService : GenericService<ParametroSistema>, IParametroService
    {
 
        
        ICacheManager _cacheManager;
        IApplication _application;

        public ParametroService(IApplication application, IBaseRepository<ParametroSistema> repository, ICacheManager cacheManager)
            : base(repository)
        {
            _application = application;
            _repository = repository;
            _cacheManager = cacheManager;
        }


        /// <summary>
        /// Recuperar el valor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetValor<T>(string codigoParametro)
        {
            Guard.AgainstNullOrEmptyString(codigoParametro, "codigoParametro");

            var parametros = GetParametros();

            var parametro = parametros.Where(c => c.Codigo == codigoParametro).SingleOrDefault();
            string error = string.Empty;
            if (parametro == null)
            {
                error = string.Format("El parametro con el codigo  [{0}] no existe", codigoParametro);
                throw new GenericException(error, error);
            }

            var valor = parametro.Valor as object;

            //Verificar si el tipo pasado es el tipo especificado del valor del parametro
            //si no son iguales lanzar exception...
            Type tipoObjeto = Type.GetType(typeof(T).FullName);


            if (tipoObjeto.Equals(typeof(string)))
            {
                if (parametro.Tipo != TipoParametro.Cadena && parametro.Tipo != TipoParametro.Imagen)
                {
                    error = string.Format("El tipo de valor configurado es [{0}], sin embargo se trata de obtener con otro tipo [{1}]", parametro.Tipo, tipoObjeto.Name);
                    throw new GenericException(error, error);
                }
                return (T)valor;
            }

            if (tipoObjeto.Equals(typeof(int)))
            {
                if (parametro.Tipo != TipoParametro.Numero)
                {
                    error = string.Format("El tipo de valor configurado es [{0}], sin embargo se trata de obtener con otro tipo [{1}]", parametro.Tipo, tipoObjeto.Name);
                    throw new GenericException(error, error);
                }

                try
                {
                    return (T)(object)Convert.ToInt32(valor);
                }
                catch (FormatException)
                {
                    error = string.Format("El valor [{0}] del parametro [{1}], no tiene formato correcto para convertir al Tipo [{2}] configurado",
                                        valor, parametro.Codigo, parametro.Tipo);

                    throw new FormatException(error);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            if (tipoObjeto.Equals(typeof(bool)))
            {
                if (parametro.Tipo != TipoParametro.Booleano)
                {
                    error = string.Format("El tipo de valor configurado es [{0}], sin embargo se trata de obtener con otro tipo [{1}]", parametro.Tipo, tipoObjeto.Name);
                    throw new GenericException(error, error);
                }
                try
                {
                    return (T)(object)Convert.ToBoolean(valor);
                }
                catch (FormatException)
                {
                    error = string.Format("El valor [{0}] del parametro [{1}], no tiene formato correcto para convertir al Tipo [{2}] configurado",
                                        valor, parametro.Codigo, parametro.Tipo);

                    throw new FormatException(error);
                }
                catch (Exception)
                {

                    throw;
                }

            }

            if (tipoObjeto.Equals(typeof(DateTime)))
            {
                if (parametro.Tipo != TipoParametro.Fecha)
                {
                    error = string.Format("El tipo de valor configurado es [{0}], sin embargo se trata de obtener con otro tipo [{1}]", parametro.Tipo, tipoObjeto.Name);
                    throw new GenericException(error, error);
                }
                try
                {
                    return (T)(object)Convert.ToDateTime(valor);
                }
                catch (FormatException)
                {
                    error = string.Format("El valor [{0}] del parametro [{1}], no tiene formato correcto para convertir al Tipo [{2}] configurado",
                                        valor, parametro.Codigo, parametro.Tipo);

                    throw new FormatException(error);
                }
                catch (Exception)
                {

                    throw;
                }

            }
             
            if (parametro.Tipo != TipoParametro.Listado)
            {
                error = string.Format("El tipo de valor configurado  [{0}], no es soportado", TipoParametro.Listado);
                throw new GenericException(error, error);
            }



            error = string.Format("El  tipo de valor [{0}] no es soportado", tipoObjeto.Name);
            throw new GenericException(error, error);
        }



        private IEnumerable<ParametroSistema> GetParametros()
        {
            //Cache
            var parametros = _cacheManager.GetData(ConstantesCache.CACHE_PARAMETROS_SISTEMA) as IEnumerable<ParametroSistema>;
            if (parametros == null)
            {
                //1. Recuperar todos los parametros
                parametros = _repository.GetAllIncluding(include => include.Opciones).ToList();

                _cacheManager.Add(ConstantesCache.CACHE_PARAMETROS_SISTEMA, parametros);
            }
            return parametros;
        }




        public override IEnumerable<ParametroSistema> GetList()
        {
            var parametros = GetParametros();
            return parametros;
        }


        //public ParametroSistema Get(int id)
        //{

        //    return _repository.Get(id);

        //    //TODO: No utilizar cache, ya que si este parametro se utiliza para modificar, se cambia el objeto del cache tambien

        //    //var parametros = GetParametros();

        //    //var parametro = parametros.Where(c => c.Id == id).SingleOrDefault();
        //    //string error = string.Empty;
        //    //if (parametro == null)
        //    //{
        //    //    error = string.Format("El parametro con el identificador  [{0}] no existe", id);
        //    //    throw new GenericException(error, error);
        //    //}

        //    //return parametro;

        //}


        public override ParametroSistema SaveOrUpdate(ParametroSistema parametro)
        {

            var parametroActualizado = _repository.InsertOrUpdate(parametro);

            //Reset cache
            _cacheManager.Remove(ConstantesCache.CACHE_PARAMETROS_SISTEMA);

            return parametroActualizado;

        }

        public override void Eliminar(int id)
        {
            throw new NotImplementedException();
        }
    }
}
