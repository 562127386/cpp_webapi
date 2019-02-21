using System.Web.Mvc;
using com.cpp.calypso.framework;

namespace com.cpp.calypso.web
{
    public static class ViewExtensions
    {

        /// <summary>
        /// Get Title of view tree
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string Title(this HtmlHelper htmlHelper, TreeModelView model)
        {
            
            if (model.Model != null)
                return model.Model.GetType().GetEnumerableType().GetDescription();

            if (model.ModelDto != null)
                return model.ModelDto.GetType().GetEnumerableType().GetDescription();


            return string.Empty;
        }

        public static string Title(this HtmlHelper htmlHelper, TreeModelChildView model)
        {
            //TODO: Mejorar, colocar la descripcion en arquitectura de la vista. (XML)
            //como un atributo.. 
            return model.ChildModel.GetType().GetEnumerableType().GetDescription();

        }

        /// <summary>
        /// Get Title of view form
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string Title(this HtmlHelper htmlHelper, FormModelView model)
        {
            if (model.Model != null && model.Model.GetType().GetEnumerableType()!=null)
                return model.Model.GetType().GetEnumerableType().GetDescription();

            if (model.ModelDto != null && model.ModelDto.GetType().GetEnumerableType() !=null)
                return model.ModelDto.GetType().GetEnumerableType().GetDescription();

            if (model.Model != null)
                return model.Model.GetType().GetDescription();

            if (model.ModelDto != null)
                return model.ModelDto.GetType().GetDescription();

            return string.Empty;
        }

        /// <summary>
        ///  Get Title of view form (Child)
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string Title(this HtmlHelper htmlHelper, FormModelChildView model)
        {
            return model.Model.GetType().GetDescription();

        }
    }
}