using Abp.Domain.Entities;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;
using CommonServiceLocator;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace com.cpp.calypso.web
{
    /// <summary>
    /// Generar metadatos, para cada campo en la vista. 
    /// 
    /// Los metadatos, puede ser datos adicionales. Ejemplo, si el modelo posee una propiedad de tipo Entity, entonces recuperar el listado de esa Entity.
    /// </summary>
    public static class GenerateMetadataView
    {
        private static readonly ILogger Log =
      ServiceLocator.Current.GetInstance<ILoggerFactory>().Create(typeof(GenerateMetadataView));


        /// <summary>
        /// Generar Metadatos de campos definidos en  Form View
        /// </summary>
        /// <param name="viewForm"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Dictionary<string, object> Generate(Form viewForm, Type model)
        {

            var listMetadata = new Dictionary<string, object>();

            foreach (var item in viewForm.Fields)
            {
                var result = GenerateMetadataField(item);
                if (result != null)
                {
                    listMetadata.Add(item.Name, result);
                }
            }

            foreach (var group in viewForm.Group)
            {
                foreach (var item in group.Fields)
                {
                    var result = GenerateMetadataField(item);
                    if (result != null)
                    {
                        listMetadata.Add(item.Name, result);
                    }
                }
            }

            return listMetadata;
        }


        /// <summary>
        /// Generar metadatos, segun el tipo de campo. (Lista, Entidad, Enum).
        /// Lista de Entidades. (Obtener la lista de objetos)
        /// Entidad. 
        /// Enum. (Pendiente)
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        private static object GenerateMetadataField(FieldForm field)
        {



            //TODO: Buscar una forma de mejorar, para agregar hander de una 
            //forma mas flexible...
            //Option 1. Obtener un listado de hander... (inyectar en el constructor)
            //y agregar el succesor segun el orden en la lista... 


            var enumerableEntityHandlerGenerateMetadata = new EnumerableEntiyHandlerGenerateMetadata();
            var entityHandlerGenerateMetadata = new EntityHandlerGenerateMetadata();
            var enumEntiyHandlerGenerateMetadata = new EnumEntiyHandlerGenerateMetadata();


            enumerableEntityHandlerGenerateMetadata.SetSuccessor(entityHandlerGenerateMetadata);
            entityHandlerGenerateMetadata.SetSuccessor(enumEntiyHandlerGenerateMetadata);

            return enumerableEntityHandlerGenerateMetadata.Generate(field);


        }

    }

    /// <summary>
    /// Manegador de generacion de Metadatos. (Cadena de Responsabilidad)
    /// </summary>
    public abstract class HandlerGenerateMetadata
    {
        /// <summary>
        /// El sucesor manegador
        /// </summary>
        protected HandlerGenerateMetadata successor;

        public void SetSuccessor(HandlerGenerateMetadata successor)
        {
            this.successor = successor;
        }

        /// <summary>
        /// Si puede manejar el FieldForm
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public abstract bool CanHandle(FieldForm field);

        /// <summary>
        /// Generar del widget
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public abstract object Generate(FieldForm field);
    }

    public class EnumerableEntiyHandlerGenerateMetadata : HandlerGenerateMetadata
    {

        private static readonly ILogger Log =
ServiceLocator.Current.GetInstance<ILoggerFactory>().Create(typeof(EnumerableEntiyHandlerGenerateMetadata));



        public override bool CanHandle(FieldForm field)
        {

            Type fieldType = field.FieldType;
            Type entityType = null;

            //1. List Entity. 
            //Si es Lista de entidades, obtener lista de objetos, con IEntityService, y metodo GetList
            if (fieldType.IsGenericType
                    && (fieldType.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                    || fieldType.GetGenericTypeDefinition() == typeof(ICollection<>)
                    || fieldType.GetGenericTypeDefinition().Implements(typeof(IEnumerable<>))
                    || fieldType.GetGenericTypeDefinition().Implements(typeof(ICollection<>))))
            {
                return true;
            }

            return false;
        }

        public override object Generate(FieldForm field)
        {
            Type fieldType = field.FieldType;
            Type entityType = null;


            if (CanHandle(field))
            {

                entityType = fieldType.GetGenericArguments()[0];

                if (entityType.Implements<IEntity>())
                {

                    //1)  recuperar alguna implementacion IEntityService<entityType>
                    var typeGeneric = typeof(IEntityService<>);
                    Type[] typeArgs = { entityType };
                    var entityServiceType = typeGeneric.MakeGenericType(typeArgs);

                    object instance = null;
                    try
                    {
                        instance = ServiceLocator.Current.GetInstance(entityServiceType);
                    }
                    catch (Exception)
                    {
                        //var msg = string.Format("No se encuentra la implementacion de servicio: IEntityService<{0}>. Revisar las configuraciones del contenedor de Inyección de Dependencias ", entityType);
                        //throw new ArgumentException(msg);

                        //No existe, una implementacion IEntityService<entityType>
                        Log.WarnFormat("No existe una implementacion IEntityService<{0}> explicita.", typeArgs);
                    }

                    if (instance == null)
                    {

                        //2) si existe algun error o es nulo, intenter recuperar clase generica de servicios
                        //GenericService<Entity> : IEntityService<Entity>
                        typeGeneric = typeof(GenericService<>);
                        entityServiceType = typeGeneric.MakeGenericType(typeArgs);

                        instance = ServiceLocator.Current.GetInstance(entityServiceType);
                    }


                    //3)
                    //TODO: Si no existe un servicio ?? que hacer??
                    //Opcion 1. Retornar Null
                    //Opcion 2. Lanzar excepcion
                    if (instance == null)
                    {
                        return null;
                    }



                    //TODO: Mejorar:
                    MethodInfo method = entityServiceType.GetMethod("GetList", new Type[] { });
                    var result = method.Invoke(instance, null);

                    var list = result as IEnumerable<IEntity>;

                    return list;
                }

                return null;

            }
            else if (successor != null)
            {
                return successor.Generate(field);
            }

            return null;




        }
    }


    public class EnumEntiyHandlerGenerateMetadata : HandlerGenerateMetadata
    {

        private static readonly ILogger Log =
ServiceLocator.Current.GetInstance<ILoggerFactory>().Create(typeof(EnumerableEntiyHandlerGenerateMetadata));

 

        public override bool CanHandle(FieldForm field)
        {

            Type fieldType = field.FieldType;
          
            if (fieldType.IsEnum)
            {
                return true;

            }
            return false;
       
        }

        public override object Generate(FieldForm field)
        {
            Type fieldType = field.FieldType;
          
            if (CanHandle(field))
            {
                var list =   Enum.GetValues(fieldType);
                return list;
 
            }
            else if (successor != null)
            {
                return successor.Generate(field);
            }

            return null;

          
        }
    }

    public class EntityHandlerGenerateMetadata : HandlerGenerateMetadata
    {
        private static readonly ILogger Log =
ServiceLocator.Current.GetInstance<ILoggerFactory>().Create(typeof(EntityHandlerGenerateMetadata));


        public override bool CanHandle(FieldForm field)
        {
            Type fieldType = field.FieldType;
            Type entityType = null;

            //2. Entity
            if (fieldType.Implements<IEntity>())
            {

                return true;
            }

            return false;
        }

        public override object Generate(FieldForm field)
        {
            Type fieldType = field.FieldType;
            Type entityType = null;

            if (CanHandle(field)) {

                entityType = fieldType;

                //Primer recuperar alguna implementacion IEntityService<entityType>
                var typeGeneric = typeof(IEntityService<>);
                Type[] typeArgs = { entityType };
                var entityServiceType = typeGeneric.MakeGenericType(typeArgs);

                object instance = null;
                try
                {
                    instance = ServiceLocator.Current.GetInstance(entityServiceType);
                }
                catch (Exception)
                {
                    //var msg = string.Format("No se encuentra la implementacion de servicio: IEntityService<{0}>. Revisar las configuraciones del contenedor de Inyección de Dependencias ", entityType);
                    //throw new ArgumentException(msg);

                    //No existe, una implementacion IEntityService<entityType>
                    Log.WarnFormat("No existe una implementacion IEntityService<{0}> explicita.", typeArgs);
                }

                if (instance == null)
                {

                    //2) si existe algun error o es nulo, intenter recuperar clase generica de servicios
                    //GenericService<Entity> : IEntityService<Entity>
                    typeGeneric = typeof(GenericService<>);
                    entityServiceType = typeGeneric.MakeGenericType(typeArgs);

                    instance = ServiceLocator.Current.GetInstance(entityServiceType);
                }


                //3)
                //TODO: Si no existe un servicio ?? que hacer??
                //Opcion 1. Retornar Null
                //Opcion 2. Lanzar excepcion
                if (instance == null)
                {
                    return null;
                }

                //TODO: PENDIENTE APLICAR Domain, AL LISTADO. 


                //TODO: Mejorar:
                MethodInfo method = entityServiceType.GetMethod("GetList", new Type[] { });
                var result = method.Invoke(instance, null);

                var list = result as IEnumerable<IEntity>;

                return list;
            }
            else if (successor != null)
            {
                return successor.Generate(field);
            }

            return null;

        }
    }
 
 

}