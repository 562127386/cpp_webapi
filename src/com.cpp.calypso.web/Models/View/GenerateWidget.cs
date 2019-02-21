using Abp.Domain.Entities;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;
using CommonServiceLocator;
using System.Collections.Generic;
using System.Reflection;

namespace com.cpp.calypso.web
{
    public  class GenerateWidget: IGenerateWidget
    {
        private static readonly ILogger Log =
ServiceLocator.Current.GetInstance<ILoggerFactory>().Create(typeof(GenerateWidget));

        /// <summary>
        /// Auto generar el widget segun el tipo
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public  string AutoGenerate(PropertyInfo property) {

            //TODO: Buscar una forma de mejorar, para agregar hander de una 
            //forma mas flexible...
            //Option 1. Obtener un listado de hander... (inyectar en el constructor)
            //y agregar el succesor segun el orden en la lista... 
            var listBoxListHandler = new ListBoxHandlerGenerateWidget();
            var dropDownListHandler = new DropDownListHandlerGenerateWidget();
            var enumHandler = new EnumHandlerGenerateWidget();

            //enumHandler.SetSuccessor(listBoxListHandler);
            listBoxListHandler.SetSuccessor(dropDownListHandler);
            dropDownListHandler.SetSuccessor(enumHandler);

            var widget = listBoxListHandler.Generate(property);


            if (!string.IsNullOrEmpty(widget))
                return widget;

            //Generico
            return Widget.TextBox.ToString();
        }
    }

  

    /// <summary>
    /// Manegador de generacion de Widget. (Cadena de Responsabilidad)
    /// </summary>
    public abstract class HandlerGenerateWidget
    {
        /// <summary>
        /// El sucesor manegador
        /// </summary>
        protected HandlerGenerateWidget successor;

        public void SetSuccessor(HandlerGenerateWidget successor)
        {
            this.successor = successor;
        }


        /// <summary>
        /// Si se puede manegar la propiedad
        /// </summary>
        /// <param name="propert"></param>
        /// <returns></returns>
        public abstract bool CanHandle(PropertyInfo propert);


        /// <summary>
        /// Generar del widget
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public abstract string Generate(PropertyInfo property);
    }

    public class ListBoxHandlerGenerateWidget : HandlerGenerateWidget
    {
        public override string Generate(PropertyInfo propert)
        {
            if (CanHandle(propert))
            {
                return Widget.ListBox.ToString();
            }
            else if (successor != null)
            {
                return successor.Generate(propert);
            }
            return string.Empty;
        }

        public override bool CanHandle(PropertyInfo property)
        {
            if (property.PropertyType.IsGenericType
        && (property.PropertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>)
        || property.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>)
        || property.PropertyType.GetGenericTypeDefinition().Implements(typeof(IEnumerable<>))
        || property.PropertyType.GetGenericTypeDefinition().Implements(typeof(ICollection<>))))
            {
                return true;
            }

            //Array
            if (property.PropertyType.IsArray) {
                return true;
            }

            return false;
        }
    }

    public class DropDownListHandlerGenerateWidget : HandlerGenerateWidget
    {
        public override string Generate(PropertyInfo propert)
        {
            if (CanHandle(propert))
            {
                return Widget.DropDownList.ToString();
            }
            else if (successor != null)
            {
                return successor.Generate(propert);
            }

            return string.Empty;
        }

        public override bool CanHandle(PropertyInfo propert)
        {
            if (propert.PropertyType.Implements<IEntity>() && !propert.PropertyType.IsEnum)
            {
                return true;

            }
            return false;
        }
    }

    public class EnumHandlerGenerateWidget : HandlerGenerateWidget
    {
        public override string Generate(PropertyInfo propert)
        {
            if (CanHandle(propert))
            {
                return Widget.Enum.ToString();
            }
            else if (successor != null)
            {
                return successor.Generate(propert);
            }

            return string.Empty;
        }

        public override bool CanHandle(PropertyInfo propert)
        {
            if (propert.PropertyType.IsEnum)
            {
                return true;

            }
            return false;
        }
    }


    public enum Widget {
        TextBox = 1,
        ListBox = 2,
        DropDownList = 3,
        Enum = 4
    }
}