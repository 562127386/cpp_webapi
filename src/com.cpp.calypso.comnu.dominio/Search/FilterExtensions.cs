using System;
using System.Collections.Generic;
using System.Reflection;


namespace com.cpp.calypso.comun.dominio
{
    /// <summary>
    /// Extensiones para filtros en busquedas
    /// </summary>
    public static class FilterExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static FilterEntity ProcessFilter(this FilterEntity filter, Type type)
        {

            BindingFlags memberFlags = BindingFlags.Public | BindingFlags.Instance;
            PropertyInfo[] propertyInfos = type.GetProperties(memberFlags);


            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (filter.Field == propertyInfo.Name)
                {
                    return ConvertTypeFieldEntity(propertyInfo.PropertyType, filter);
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public static List<FilterEntity> ProcessFilter(this List<FilterEntity> filters,Type type)
        {
            if (filters.Count == 0)
                return filters;


            if (type is null)
                return filters;

            //TODO: Mejorar. Para no estar procesando frecuentente las propiedades del tipo del modelo asociado en la vista...
            BindingFlags memberFlags = BindingFlags.Public | BindingFlags.Instance;
            PropertyInfo[] propertyInfos = type.GetProperties(memberFlags);

            List<FilterEntity> filtersProcess = new List<FilterEntity>();

            
            foreach (var filter in filters)
            {
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    if (filter.Field == propertyInfo.Name)
                    {
                        filtersProcess.Add(ConvertTypeFieldEntity(propertyInfo.PropertyType, filter));
                    }
                }
            }

            return filtersProcess;
        }

        /// <summary>
        /// Convierte el valor del filtro, al correspondiente tipo typeEntity  establecido en el fitro
        /// </summary>
        /// <param name="typeEntity"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static FilterEntity ConvertTypeFieldEntity(Type typeEntity, FilterEntity filter)
        {

            //TODO: Mejorar los mensajes de error lanzados... 

            var valor = filter.Value as object;

            string error = string.Empty;

            if (typeEntity.Equals(typeof(string)))
            {
                filter.Value = filter.Value.ToString();
                return filter;
            }

            if (typeEntity.Equals(typeof(int)) && filter.Value.GetType() != typeof(int))
            {

                try
                {
                    filter.Value = Convert.ToInt32(filter.Value);
                    return filter;
                }
                catch (FormatException ex)
                {
                    error = string.Format("El valor [{0}], no tiene formato correcto para convertir al Tipo numerico configurado",
                                        valor);

                    throw new FormatException(error,ex);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            if (typeEntity.Equals(typeof(bool)))
            {

                try
                {

                    filter.Value = Convert.ToBoolean(filter.Value);
                    return filter;
                }
                catch (FormatException ex)
                {
                    error = string.Format("El valor [{0}], no tiene formato correcto para convertir al Tipo booleano configurado",
                                       valor);

                    throw new FormatException(error, ex);
                }
                catch (Exception)
                {

                    throw;
                }

            }

            if (typeEntity.Equals(typeof(DateTime)))
            {

                try
                {

                    filter.Value = Convert.ToDateTime(filter.Value);
                    return filter;
                }
                catch (FormatException ex)
                {
                    error = string.Format("El valor [{0}], no tiene formato correcto para convertir al Tipo fecha configurado",
                                       valor);

                    throw new FormatException(error, ex);
                }
                catch (Exception)
                {

                    throw;
                }

            }

            //Enum
            if (typeEntity.IsEnum)
            {
                
                try
                {
                    var result = Enum.Parse(typeEntity, filter.Value.ToString());
                    filter.Value = result;
                    return filter;
                }
                catch (Exception)
                {

                    throw;
                }
            }

            if (typeEntity.Equals(typeof(int?))) // && filter.Value.GetType() != typeof(int?))
            {
                try
                {
                    if (filter.Value == null)
                        return filter;

                    int? ValueNullable = Convert.ToInt32(filter.Value);
                    filter.Value = ValueNullable;
                    return filter;
                }
                catch (FormatException ex)
                {
                    error = string.Format("El valor [{0}], no tiene formato correcto para convertir al Tipo numerico configurado",
                                         valor);

                    throw new FormatException(error, ex);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            if (typeEntity.Equals(typeof(bool?))) // && filter.Value.GetType() != typeof(bool?))
            {

                try
                {
                    if (filter.Value == null)
                        return filter;

                    bool? ValueNullable = Convert.ToBoolean(filter.Value);
                    filter.Value = ValueNullable;
                    return filter;
                }
                catch (FormatException)
                {
                    //error = string.Format("El valor [{0}] del parametro [{1}], no tiene formato correcto para convertir al Tipo [{2}] configurado",
                    //                    valor, parametro.Codigo, parametro.Tipo);

                    throw new FormatException(error);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            if (typeEntity.Equals(typeof(DateTime?)) && filter.Value.GetType() != typeof(DateTime?))
            {

                try
                {

                    DateTime? ValueNullable = Convert.ToDateTime(filter.Value);
                    filter.Value = ValueNullable;
                    return filter;
                }
                catch (FormatException ex)
                {
                    error = string.Format("El valor [{0}], no tiene formato correcto para convertir al Tipo fecha configurado",
                                      valor);

                    throw new FormatException(error, ex);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }


            if (typeEntity.IsNullableEnum())
            {
                try
                {
                    if (filter.Value == null)
                        return filter;

                    Type enumType = Nullable.GetUnderlyingType(typeEntity);

                    var result = Enum.Parse(enumType, filter.Value.ToString());
                    filter.Value = result;
                    return filter;
                }
                catch (Exception)
                {
                    //
                    throw;
                }
            }


            error = string.Format("El  tipo de valor [{0}] no es soportado", typeEntity.Name);
            throw new GenericException(error, error);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsNullableEnum(this Type t)
        {
            Type u = Nullable.GetUnderlyingType(t);
            return (u != null) && u.IsEnum;
        }
    }

   
}
