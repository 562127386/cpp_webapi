using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace com.cpp.calypso.web
{
    public static class BsGroupExtensions
    {
        private const string DefaultTagName = "div";
        private const string DefaultWrapperClassName = CssClass.Form.ControlGroup;
        private const string DefaultErrorClassName = "has-error";

        public static IHtmlString BeginBsGroupFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> modelProperty, string tagName = DefaultTagName, object htmlAttributes = null, bool appendControlGroupClass = true)
        {
            return BeginBsGroupFor(html, modelProperty, tagName, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), appendControlGroupClass);
        }

        public static IHtmlString BeginBsGroupFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> modelProperty, string tagName = DefaultTagName, IDictionary<string, object> htmlAttributes = null, bool appendControlGroupClass = true)
        {
            string propertyName = ExpressionHelper.GetExpressionText(modelProperty);
            return BeginBsGroup(html, propertyName, tagName, htmlAttributes, appendControlGroupClass);
        }

        public static IHtmlString BeginBsGroup(this HtmlHelper html, string propertyName, string tagName = DefaultTagName, object htmlAttributes = null, bool appendControlGroupClass = true)
        {
            return BeginBsGroup(html, propertyName, tagName, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), appendControlGroupClass);
        }

        public static IHtmlString BeginBsGroup(this HtmlHelper html, string propertyName, string tagName = DefaultTagName, IDictionary<string, object> htmlAttributes = null, bool appendControlGroupClass = true)
        {
            var bsGroupWrapper = new TagBuilder(tagName);
            bsGroupWrapper.MergeAttributes(htmlAttributes);
            if (appendControlGroupClass)
                bsGroupWrapper.AddCssClass(DefaultWrapperClassName);
            string partialFieldName = propertyName;
            string fullHtmlFieldName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(partialFieldName);
            if (!html.ViewData.ModelState.IsValidField(fullHtmlFieldName))
            {
                bsGroupWrapper.AddCssClass(DefaultErrorClassName);
            }
            string openingTag = bsGroupWrapper.ToString(TagRenderMode.StartTag);
            return html.Raw(openingTag);
        }

        public static IHtmlString EndBsGroup(this HtmlHelper html, string tagName = DefaultTagName)
        {
            return html.Raw(new TagBuilder(tagName).ToString(TagRenderMode.EndTag));
        }

        public static BsGroup BsGroupFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> modelProperty, object htmlAttributes, string tagName = DefaultTagName, bool appendControlGroupClass = true)
        {
            return BsGroupFor(html, modelProperty, tagName, htmlAttributes, appendControlGroupClass);
        }

        public static BsGroup BsGroupFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> modelProperty, string tagName = DefaultTagName, object htmlAttributes = null, bool appendControlGroupClass = true)
        {
            string propertyName = ExpressionHelper.GetExpressionText(modelProperty);
            return BsGroup(html, propertyName, tagName, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), appendControlGroupClass);
        }

        public static BsGroup BsGroup(this HtmlHelper html, string propertyName, string tagName, object htmlAttributes, bool appendControlGroupClass = true)
        {
            return BsGroup(html, propertyName, tagName, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), appendControlGroupClass);
        }

        public static BsGroup BsGroup(this HtmlHelper html, string propertyName = null, string tagName = DefaultTagName, IDictionary<string, object> htmlAttributes = null, bool appendControlGroupClass = true)
        {
            html.ViewContext.Writer.Write(BeginBsGroup(html, propertyName, tagName, htmlAttributes, appendControlGroupClass));
            return new BsGroup(html, tagName);
        }
    }

    public class BsGroup : IDisposable
    {
        private readonly HtmlHelper _html;
        private readonly string _tagName;

        public BsGroup(HtmlHelper html, string tagName)
        {
            _html = html;
            _tagName = tagName;
        }

        public void Dispose()
        {
            _html.ViewContext.Writer.Write(_html.EndBsGroup(_tagName));
        }
    }
}