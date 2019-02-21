using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Linq.Expressions;
 
using com.cpp.calypso.framework;
using com.cpp.calypso.comun.dominio;

namespace com.cpp.calypso.comun.aplicacion
{
    /// <summary>
    /// 
    /// </summary>
    public class AppSettingParametroService : IParametroService
    {
         
        
        ICacheManager _cacheManager;
        IApplication _application;

        

        public AppSettingParametroService(IApplication application,  ICacheManager cacheManager)
        {
            _application = application;
            _cacheManager = cacheManager;
        }


        /// <summary>
        /// Recuperar el valor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetValor<T>(string codigoParametro)
        {
                    var parametros = GetParametros();

                    var parametro = parametros.Where(c => c.Codigo == codigoParametro).SingleOrDefault();
                    string error = string.Empty;
                    if (parametro == null){
                         error = string.Format("El parametro con el codigo  [{0}] no existe",codigoParametro);
                        throw new GenericException(error, error);
                    }

                    var valor = parametro.Valor as object;

                    //Verificar si el tipo pasado es el tipo especificado del valor del parametro
                    //si no son iguales lanzar exception...
                    Type tipoObjeto = Type.GetType(typeof(T).FullName);


                    if (tipoObjeto.Equals(typeof(string))) {
                        if (parametro.Tipo != TipoParametro.Cadena) { 
                            error = string.Format("El tipo de valor configurado es [{0}], sin embargo se trata de obtener con otro tipo [{1}]", parametro.Tipo.ToString(), TipoParametro.Cadena.ToString());
                            throw new GenericException(error, error);
                        }
                        return (T)valor;
                    }

                    if (tipoObjeto.Equals(typeof(int))) {
                        if (parametro.Tipo != TipoParametro.Numero)
                        {
                            error = string.Format("El tipo de valor configurado es [{0}], sin embargo se trata de obtener con otro tipo [{1}]", parametro.Tipo.ToString(), TipoParametro.Numero.ToString());
                            throw new GenericException(error, error);
                        }

                            try
                            {
                                return (T)(object)Convert.ToInt32(valor);
                            }
                            catch (FormatException)
                            {
                                 error = string.Format("El valor  [{0}] del parametro [{1}], no es Numero", valor, parametro.Codigo);
                                throw new GenericException(error, error);
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
                            error = string.Format("El tipo de valor configurado es [{0}], sin embargo se trata de obtener con otro tipo [{1}]", parametro.Tipo.ToString(), TipoParametro.Booleano.ToString());
                            throw new GenericException(error, error);
                        }
                       

                try
                {
                    return (T)(object)Convert.ToBoolean(valor);
                }
                catch (FormatException)
                {
                    error = string.Format("El valor  [{0}] del parametro [{1}], no es Boolean", valor, parametro.Codigo);
                    throw new GenericException(error, error);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

                    if (tipoObjeto.Equals(typeof(DateTime)))
                    {
                        if (parametro.Tipo != TipoParametro.Fecha)
                        {
                            error = string.Format("El tipo de valor configurado es [{0}], sin embargo se trata de obtener con otro tipo [{1}]", TipoParametro.Fecha.ToString(), tipoObjeto.Name);
                            throw new GenericException(error, error);
                        }
                        return (T)(object)Convert.ToDateTime(valor);
                    }


                    if (parametro.Tipo == TipoParametro.Listado)
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
                //1. Recuperar todos los parametros del sistema asociada.

                //AppSettings. La clave es formada por 
                
                //CodigoSistema:TipoParametro:NombreParametro

                //TipoParametro: Numero = 1,  Cadena = 2, Booleano = 3, Listado = 4, Fecha = 5 
                //<add key = "CodigoSistema:1:SizeGrid" value = "20" />
                //<add key = "CodigoSistema:2:NombreAplicacion" value = "NombreAplicacion" />
                //<add key = "CodigoSistema:3:VisualizarMenu" value = "true" />
                //<add key = "CodigoSistema:4:Opciones" value = "Valor1,Valor2,Valor3,Valor4" />
                //<add key = "CodigoSistema:5:FechaInicio" value = "20/01/2016" />


                 
                string[] appSettingsSystem = ConfigurationManager.AppSettings.AllKeys
                             .Where(key => key.StartsWith(string.Format("Sistema:")))
                             .Select(key => key)
                             .ToArray();

                char[] delimiterChars = { ':'};

              

                var listParam = new List<ParametroSistema>();
                var random = new Random();
                foreach (var key in appSettingsSystem) {
                    var param = new ParametroSistema();

                    string[] parts = key.Split(delimiterChars);

                    var tipoParametro = TipoParametro.Cadena;

                    if (!(parts.Count() == 2 || parts.Count() == 3)) {
                        string error = string.Format("La calve [{0}], no tiene el formato correcto CodigoSistema:TipoParametro:NombreParametro", key);
                        throw new GenericException(error, error);
                    }

                    if (parts.Count() == 3) {
                        int tipoParametroInt = int.Parse(parts[1]);
                        tipoParametro  = (TipoParametro)tipoParametroInt;
                    }

                 
                    param.Codigo = parts[parts.Length-1];
                    param.Valor = ConfigurationManager.AppSettings[key];
                    
                    param.CreatorUserId = 1;
                    param.CreationTime = DateTime.Now;
                    param.LastModificationTime = DateTime.Now;
                    param.Descripcion = string.Empty;
                    param.Categoria = CategoriaParametro.General;
                    param.EsEditable = false;
                    param.Id = random.Next();
                    param.Nombre = key;
                    param.TieneOpciones = false;
                    param.Tipo = tipoParametro;

                    listParam.Add(param);
                }

                parametros = listParam.ToList();
                _cacheManager.Add(ConstantesCache.CACHE_PARAMETROS_SISTEMA, parametros);
            }
            return parametros;
        }




        public IEnumerable<ParametroSistema> GetList()
        {
            var parametros = GetParametros();
            return parametros;
        }


        public ParametroSistema Get(int id)
        {
            var parametros = GetParametros();

            return parametros.Where(p => p.Id == id).SingleOrDefault();
 

            //TODO: No utilizar cache, ya que si este parametro se utiliza para modificar, se cambia el objeto del cache tambien

            //var parametros = GetParametros();

            //var parametro = parametros.Where(c => c.Id == id).SingleOrDefault();
            //string error = string.Empty;
            //if (parametro == null)
            //{
            //    error = string.Format("El parametro con el identificador  [{0}] no existe", id);
            //    throw new GenericException(error, error);
            //}

            //return parametro;

        }


        public ParametroSistema SaveOrUpdate(ParametroSistema parametro)
        {
            throw new NotImplementedException("Utilizar el archivo de configuracion web.config o app.config");
        }

        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ParametroSistema> GetList(IList<FilterEntity> filters)
        {
            throw new NotImplementedException();
        }

        public IPagedListMetaData<ParametroSistema> GetList(IList<FilterEntity> filters, int Skip, int Take)
        {
            throw new NotImplementedException();
        }

        public IPagedListMetaData<ParametroSistema> GetList(int Skip, int Take)
        {
            throw new NotImplementedException();
        }

        public IList<ParametroSistema> SaveOrUpdate(IList<ParametroSistema> listEntity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ParametroSistema> GetList(Expression<Func<ParametroSistema, bool>> criteria)
        {
            throw new NotImplementedException();
        }

        public IPagedListMetaData<ParametroSistema> GetList(Expression<Func<ParametroSistema, bool>> criteria, int skip, int take)
        {
            throw new NotImplementedException();
        }

        public long LongCount()
        {
            return GetParametros().LongCount();
        }

        public long LongCount(Expression<Func<ParametroSistema, bool>> predicate)
        {
            return GetParametros().AsQueryable().Where(predicate).LongCount();
        }

        public void Eliminar(ParametroSistema entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ParametroSistema> GetList(IList<FilterEntity> filters, string orderBy)
        {
            throw new NotImplementedException();
        }

        public IPagedListMetaData<ParametroSistema> GetList(IList<FilterEntity> filters, int Skip, int Take, string orderBy)
        {
            throw new NotImplementedException();
        }
    }
}
