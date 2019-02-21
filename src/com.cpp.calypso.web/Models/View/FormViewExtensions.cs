using Abp.Domain.Entities;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace com.cpp.calypso.web
{
    public static class FormViewExtensions
    {

        /// <summary>
        /// Obtener el valor del campo, desde el modelo. (Segun la definicion del FieldForm)
        /// </summary>
        /// <param name="fieldForm"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static object GetValue(this FieldForm fieldForm, object model)
        {
            PropertyInfo property = model.GetType().GetProperty(fieldForm.Name);
            if (property == null)
            {
                var msg = string.Format("La propiedad [{0}] no existe en el modelo de tipo [{1}]",
                    fieldForm.Name, model.GetType());
                throw new ArgumentException(msg);
            }

            var value = property.GetValue(model, null);

            return value;
        }

        /// <summary>
        /// Obtener el valor de la propeidad [propertyName], desde el modelo. 
        /// </summary>
        /// <param name="fieldForm"></param>
        /// <param name="model"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static object GetValue(this FieldForm fieldForm, object model, string propertyName)
        {
            PropertyInfo property = model.GetType().GetProperty(propertyName);
            if (property == null)
            {
                var msg = string.Format("La propiedad [{0}] no existe en el modelo de tipo [{1}]",
                    propertyName, model.GetType());

                throw new ArgumentException(msg);
            }

            var value = property.GetValue(model, null);

            return value;
        }

        /// <summary>
        /// Obtiene el identicador,del campo del modelo
        /// El valor de este campo debe ser una Entity
        /// </summary>
        /// <param name="fieldForm">Campo a procesar</param>
        /// <param name="model">Modelo</param>
        /// <returns></returns>
        public static int? GetId(this FieldForm fieldForm, object model) {

            PropertyInfo property = model.GetType().GetProperty(fieldForm.Name);

            if (property == null)
            {
                var msg = string.Format("La propiedad [{0}] no existe en el modelo de tipo [{1}]",
                    fieldForm.Name, model.GetType());
                throw new ArgumentException(msg);
            }

            var value = property.GetValue(model, null);

            if (value == null)
                return null;

            if (!value.GetType().Implements(typeof(IEntity)))
            {
                var msg = string.Format("La propiedad [{0}] del tipo [{1}], no es IEntity",
                  fieldForm.Name, model.GetType());

                throw new InvalidCastException(msg);
            }

            var entity = value as IEntity;


            return entity.Id;

        }

        /// <summary>
        /// Obtener listado de identificadores, del campo del modelo. 
        /// El valor de este campo debe ser una collection de objetos
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int[] GetIds(this FieldForm fieldForm, object model) {

            var list = fieldForm.GetValueListProperty(model);
            if (list == null)
                return new int[] { };

            //Recuperar collection from Model. SelectedValue
            var selectedValue = list.Select(r => r.Id).ToArray();
            return selectedValue;
        }

        /// <summary>
        /// Obtener listado entidades del modelo, segun la definicion del campo
        /// El valor debe ser una collection de Entity
        /// </summary>
        /// <param name="fieldForm"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static IEnumerable<IEntity> GetValueListProperty(this FieldForm fieldForm, object model) // IEntity model)
        {
           
            PropertyInfo listProperty = model.GetType().GetProperty(fieldForm.Name);

            if (listProperty == null) {
                var msg = string.Format("La propiedad [{0}] no existe en el modelo de tipo [{1}]",
                    fieldForm.Name,model.GetType());
                throw new ArgumentException(msg);
            }

            var value = listProperty.GetValue(model, null);

            if (value == null)
                return null;

            if (value.GetType().IsPrimitive) {

                var msg = string.Format("La propiedad [{0}] del tipo [{1}], no es IEnumerable",
                 fieldForm.Name, model.GetType());

                throw new InvalidCastException(msg);
            }


            //No es Generico
            if (!value.GetType().IsGenericType)
            {
                var msg = string.Format("La propiedad [{0}] del tipo [{1}], no es un tipo Generico",
                  fieldForm.Name, model.GetType());

                throw new InvalidCastException(msg);
            }

            //Es Generico, y no es del  Tipo Enumarable o Collection
            if (value.GetType().IsGenericType
    && !(value.GetType().GetGenericTypeDefinition() == typeof(IEnumerable<>)
    || value.GetType().GetGenericTypeDefinition() == typeof(ICollection<>)
    || value.GetType().GetGenericTypeDefinition().Implements(typeof(IEnumerable<>))
    || value.GetType().GetGenericTypeDefinition().Implements(typeof(ICollection<>))))
            {
                var msg = string.Format("La propiedad [{0}] del tipo [{1}], no es IEnumerable",
                  fieldForm.Name, model.GetType());

                throw new InvalidCastException(msg);
            }
  
            var enumerable = value as IEnumerable<IEntity>;
     
            return enumerable;
        }



        /// <summary>
        /// Separate fields in columns depending on the configuration of the group
        /// </summary>
        /// <param name="groupForm"></param>
        /// <returns></returns>
        public static List<FieldForm>[] SeparateFieldsColumns(this Group groupForm) {

            //Separar campos en columnas
            List<FieldForm>[] Cols = new List<FieldForm>[groupForm.Col];

            var countField = 0;
            foreach (var item in groupForm.Fields)
            {
                if (Cols[countField] == null)
                {
                    Cols[countField] = new List<FieldForm>();
                }

                Cols[countField].Add(item);
                countField++;
                if (countField >= groupForm.Col)
                {
                    countField = 0;
                }
            }

            return Cols;
        }

        /// <summary>
        /// Obtener la etiqueta de un campo.
        /// Si el campo posee el atributo "DescriptionAttribute", obtiene el valor de este.
        /// Si no posee el atributo "DescriptionAttribute", se genera la etiqueta desde el nombre del campo
        /// </summary>
        /// <param name="html"></param>
        /// <param name="field"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string GetLabelField(this HtmlHelper html,string field,object model) {

            //Model
            PropertyInfo
                propertiesModel = model.GetType().GetEnumerableType().GetProperties().Where(f => f.Name == field).FirstOrDefault();

            if (propertiesModel == null)
                return string.Empty;


            var descriptionAttribute = propertiesModel.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
            var header = string.Empty;
            if (descriptionAttribute != null)
            {
                header = descriptionAttribute.Description;
            }
            else
            {
                var displayNameAttribute =
                    propertiesModel.GetCustomAttributes(typeof(DisplayNameAttribute), false).FirstOrDefault() as
                    DisplayNameAttribute;
                if (displayNameAttribute != null)
                {
                    header = displayNameAttribute.DisplayName;
                }
                else
                {
                    header = propertiesModel.Name.Replace('_', ' ');
                }
            }

            return header;
        }
    }
}