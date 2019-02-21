using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace com.cpp.calypso.framework
{
    public class FluentTagBuilder : TagBuilder, IHtmlString
    {
        private const TagRenderMode DefaultRenderMode = TagRenderMode.Normal;

        private FluentTagBuilder Parent { get; set; }
        private TagRenderMode RenderMode { get; set; }

        public FluentTagBuilder(string tagName, TagRenderMode renderMode = DefaultRenderMode)
            : base(tagName)
        {
            RenderMode = renderMode;
        }

        private FluentTagBuilder(string tagName, TagRenderMode renderMode, FluentTagBuilder parent)
            : this(tagName, renderMode)
        {
            Parent = parent;
        }

        public FluentTagBuilder StartTag(string tagName, TagRenderMode renderMode = DefaultRenderMode)
        {
            return new FluentTagBuilder(tagName, renderMode, this);
        }

        public FluentTagBuilder EndTag()
        {
            Parent.AddHtmlContent(ToString(RenderMode));
            return Parent;
        }

        public FluentTagBuilder AddContent(object content)
        {
            return AddHtmlContent(HttpUtility.HtmlEncode(content));
        }

        public FluentTagBuilder AddHtmlContent(string content)
        {
            InnerHtml = String.Concat(InnerHtml, content);
            return this;
        }

        public FluentTagBuilder AddAttributes<TKey, TValue>(IDictionary<TKey, TValue> htmlAttributes, bool replaceExisting = false)
        {
            MergeAttributes(htmlAttributes, replaceExisting);
            return this;
        }

        public FluentTagBuilder WithCssClass(string value)
        {
            if (String.IsNullOrWhiteSpace(value)) return this;

            AddCssClass(value);
            return this;
        }

        public FluentTagBuilder AddAttributes(object htmlAttributes, bool replaceExisting = false)
        {
            return AddAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), replaceExisting);
        }

        public IHtmlString ToHtmlString()
        {
            return new HtmlString(ToString(RenderMode));
        }

        public IHtmlString ToHtmlString(TagRenderMode renderMode)
        {
            return new HtmlString(ToString(renderMode));
        }

        string IHtmlString.ToHtmlString()
        {
            return ToString(RenderMode);
        }

        public override string ToString()
        {
            return ToString(RenderMode);
        }
    }
}