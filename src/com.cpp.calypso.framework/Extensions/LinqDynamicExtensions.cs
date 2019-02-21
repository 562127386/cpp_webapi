using com.cpp.calypso.comun.dominio;
using CommonServiceLocator;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
 
namespace com.cpp.calypso.framework
{
    public static class LinqDynamicExtensions
    {
        static readonly ILogger log =
   ServiceLocator.Current.GetInstance<ILoggerFactory>().Create(typeof(LinqDynamicExtensions));


        /// <summary>
        /// Operadores soportados.
        /// TODO: SE PUEDE utilizar para validar las view, para evitar inconvientes al momento de construir la consulta
        /// </summary>
        public static List<string> Operators = new List<string>() {
            "==",
            "<",
            ">",
            "<=",
            ">=",
            "!=",
            "Contains",
            "StartsWith"
        };


        public static string Format(this FilterEntity filterEntity, int numberValue) {

            //TODO:
            //si filterEntity.Operator es nulo, realizar alguna accion por defecto
            //ejemplo un operador por defecto


            //TODO: Mejorar, sin valores quemados
            if (filterEntity.Operator.ToUpper() == "CONTAINS" ||
                filterEntity.Operator.ToUpper() == "STARTSWITH")
            {
                return string.Format("{0}.{1}(@{2})", filterEntity.Field, filterEntity.Operator, numberValue);

            }
            else {
                return string.Format("{0} {1} @{2}", filterEntity.Field, filterEntity.Operator, numberValue);
            }
        } 


        /// <summary>
        /// Generar linq, segun los filtros enviados
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="source"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public static IQueryable<TElement> Where<TElement>(this IQueryable<TElement> source, IList<FilterEntity> filters) {

            //TODO: filters puede ser nulo???

            if (filters.Count == 0)
                return source;

            var domainsString = new List<string>();
            var valueObject = new List<object>();

            for (int i = 0; i < filters.Count; i++)
            {
                
                domainsString.Add(filters[i].Format(i));

                valueObject.Add(filters[i].Value);
            }

            var where = string.Join(" AND ", domainsString);

            log.DebugFormat("where : {0}", where);

            return source.Where(where, valueObject.ToArray());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public static IQueryable Where(this IQueryable source, IList<FilterEntity> filters) {
 
            var domainsString = new List<string>();
            var valueObject = new List<object>();

            for (int i = 0; i < filters.Count; i++)
            {
                domainsString.Add(string.Format("{0} {1} @{2}", filters[i].Field, filters[i].Operator, i));

                valueObject.Add(filters[i].Value);
            }

            var where =  string.Join(" AND ", domainsString);

            return source.Where(where, valueObject.ToArray()); 
        }

        
         
    }
}
