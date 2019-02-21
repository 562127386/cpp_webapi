using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace com.cpp.calypso.web
{
    public static class EnumHelper2_Verificar
    {


        public static MvcHtmlString EnumDropDownList2(this HtmlHelper htmlHelper, string expression,Type typeEnum)
        {
            ModelMetadata metadata = ModelMetadata.FromStringExpression(expression, htmlHelper.ViewData);

            //metadata.Model;
            //metadata.PropertyName
            //Enums are actually numeric. GetNames returns the field names.GetValues returns the numeric values.

            var values = Enum.GetValues(typeEnum);
            var names = Enum.GetNames(typeEnum);


            return null;
            
            //IEnumerable<SelectListItem> items =
            //    from value in values
            //    select new SelectListItem
            //    {
            //        Text = value.ToString(),
            //        Value = value.ToString(),
            //        Selected = (value.Equals(selectedValue))
            //    };

            //return htmlHelper.DropDownList(
            //    name,
            //    items
            //    );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="name"></param>
        /// <param name="selectedValue"></param>
        /// <returns></returns>
        //public static MvcHtmlString EnumDropDownList<TEnum>(this HtmlHelper htmlHelper,string expression, string name, TEnum selectedValue)
        //{
        //    ModelMetadata metadata = ModelMetadata.FromStringExpression(expression, htmlHelper.ViewData);

        //    //metadata.Model;
        //    //metadata.PropertyName

        //    IEnumerable<TEnum> values = Enum.GetValues(typeof(TEnum))
        //        .Cast<TEnum>();

        //    IEnumerable<SelectListItem> items =
        //        from value in values
        //        select new SelectListItem
        //        {
        //            Text = value.ToString(),
        //            Value = value.ToString(),
        //            Selected = (value.Equals(selectedValue))
        //        };

        //    return htmlHelper.DropDownList(
        //        name,
        //        items
        //        );
        //}

        //public static MvcHtmlString EnumDropDownList(this HtmlHelper htmlHelper, Type typeEnum)
        //{
        //    var values = Enum.GetValues(typeEnum);

        //    IEnumerable<SelectListItem> items =
        //        from value in values.
        //        select new SelectListItem
        //        {
        //            Text = value.ToString(),
        //            Value = value.ToString(),
        //            Selected = (value.Equals(selectedValue))
        //        };

        //    return htmlHelper.DropDownList(
        //        name,
        //        items
        //        );
        //}
    }
}