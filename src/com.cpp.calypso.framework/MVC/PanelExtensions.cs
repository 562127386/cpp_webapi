using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace com.cpp.calypso.framework
{
    /// <summary>
    /// Presenta un elemento html para realizar paneles
    /// </summary>
    public class HtmlPanel : IDisposable
    {

        private readonly ViewContext _viewContext;
        private bool _disposed;

        public HtmlPanel(ViewContext viewContext)
        {
            // TODO: Complete member initialization
            if (viewContext == null)
            {
                throw new ArgumentNullException("viewContext");
            }

            _viewContext = viewContext;

            // push the new FormContext
            //_viewContext.FormContext = new FormContext();
        }

        public void Dispose()
        {
            Dispose(true /* disposing */);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
                PanelExtensions.EndPanel(_viewContext);
            }
        }

        //public void EndForm()
        //{
        //    Dispose(true);
        //}

    }

    /// <summary>
    /// Representan una cabecera de panel
    /// </summary>
    public class HtmlPanelHeader : IDisposable
    {

        private readonly ViewContext _viewContext;
        private bool _disposed;

        public HtmlPanelHeader(ViewContext viewContext)
        {
            // TODO: Complete member initialization
            if (viewContext == null)
            {
                throw new ArgumentNullException("viewContext");
            }

            _viewContext = viewContext;

            // push the new FormContext
            //_viewContext.FormContext = new FormContext();
        }

        public void Dispose()
        {
            Dispose(true /* disposing */);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
                PanelExtensions.EndPanelHeader(_viewContext);
            }
        }

        //public void EndForm()
        //{
        //    Dispose(true);
        //}

    }

    public class HtmlPanelBody : IDisposable
    {

        private readonly ViewContext _viewContext;
        private bool _disposed;

        public HtmlPanelBody(ViewContext viewContext)
        {
            // TODO: Complete member initialization
            if (viewContext == null)
            {
                throw new ArgumentNullException("viewContext");
            }

            _viewContext = viewContext;

            // push the new FormContext
            //_viewContext.FormContext = new FormContext();
        }

        public void Dispose()
        {
            Dispose(true /* disposing */);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
                PanelExtensions.EndPanelBody(_viewContext);
            }
        }

        //public void EndForm()
        //{
        //    Dispose(true);
        //}

    }

    public class HtmlPanelFooter : IDisposable
    {

        private readonly ViewContext _viewContext;
        private bool _disposed;

        public HtmlPanelFooter(ViewContext viewContext)
        {
            // TODO: Complete member initialization
            if (viewContext == null)
            {
                throw new ArgumentNullException("viewContext");
            }

            _viewContext = viewContext;

            // push the new FormContext
            //_viewContext.FormContext = new FormContext();
        }

        public void Dispose()
        {
            Dispose(true /* disposing */);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
                PanelExtensions.EndPanelFooter(_viewContext);
            }
        }

        //public void EndForm()
        //{
        //    Dispose(true);
        //}

    }


    /// <summary>
    /// Metodos de ayuda para crear paneles
    /// </summary>
    public static class PanelExtensions
    {
        /// <summary>
        /// Contruir un panel 
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static HtmlPanel BeginPanel(this HtmlHelper htmlHelper)
        {
            return PanelHelper(htmlHelper, new RouteValueDictionary());
        }


        /// <summary>
        /// Contruir un panel
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="htmlAttributes">Atributos adicionales que se colocan en tag del panel</param>
        /// <returns></returns>
        public static HtmlPanel BeginPanel(this HtmlHelper htmlHelper, object htmlAttributes)
        {
            return PanelHelper(htmlHelper, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }


        public static void EndPanel(this HtmlHelper htmlHelper)
        {
            EndPanel(htmlHelper.ViewContext);
        }

        /// <summary>
        /// Contruir una cabecera para un panel, como un contenedor. 
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="titulo">Titulo de la cabecera</param>
        /// <returns></returns>
        public static HtmlPanelHeader BeginPanelHeader(this HtmlHelper htmlHelper, string titulo)
        {
            return PanelHeaderHelper(htmlHelper, titulo, new RouteValueDictionary());
        }

        /// <summary>
        /// Contruir una cabecera para un panel, como un contenedor. 
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="titulo">Titulo de la cabecera</param>
        /// <param name="htmlAttributes">Atributos adicionales que se colocan en tag del panel</param>
        /// <returns></returns>
        public static HtmlPanelHeader BeginPanelHeader(this HtmlHelper htmlHelper, string titulo, object htmlAttributes)
        {
            return PanelHeaderHelper(htmlHelper, titulo, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static HtmlPanelBody BeginPanelBody(this HtmlHelper htmlHelper)
        {
            return PanelBodyHelper(htmlHelper, new RouteValueDictionary());
        }

        public static HtmlPanelBody BeginPanelBody(this HtmlHelper htmlHelper, object htmlAttributes)
        {
            return PanelBodyHelper(htmlHelper, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        private static HtmlPanel PanelHelper(this HtmlHelper htmlHelper, IDictionary<string, object> htmlAttributes) //, string formAction, FormMethod method, IDictionary<string, object> htmlAttributes)
        {

            TagBuilder tagBuilder = new TagBuilder("div");
            EnsureHtmlAttribute(htmlAttributes, "class", "card");


            tagBuilder.MergeAttributes(htmlAttributes);

            htmlHelper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));
 
            HtmlPanel thePanel = new HtmlPanel(htmlHelper.ViewContext);
            
            return thePanel;
        }

        public static MvcHtmlString PanelHeader(this HtmlHelper htmlHelper, string titulo)
        {
            return PanelHeader(htmlHelper, titulo, new RouteValueDictionary());
        }

        public static MvcHtmlString PanelHeader(this HtmlHelper htmlHelper, string titulo, IDictionary<string, object> htmlAttributes)
        {

            TagBuilder tagBuilder = new TagBuilder("div");

            EnsureHtmlAttribute(htmlAttributes, "class", "card-header");

            tagBuilder.MergeAttributes(htmlAttributes);

            TagBuilder h3 = new TagBuilder("h3");
            h3.InnerHtml = titulo;

            tagBuilder.InnerHtml += h3;


            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal));
        }

        /// <summary>
        /// Contruir un bloque de pie para un panel
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static HtmlPanelFooter BeginPanelFooter(this HtmlHelper htmlHelper)
        {
            return PanelFooterHelper(htmlHelper, new RouteValueDictionary());
        }


        public static HtmlPanelFooter BeginPanelFooter(this HtmlHelper htmlHelper, object htmlAttributes)
        {
            return PanelFooterHelper(htmlHelper, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }


        private static HtmlPanelHeader PanelHeaderHelper(this HtmlHelper htmlHelper, string titulo, IDictionary<string, object> htmlAttributes)
        {

            TagBuilder tagBuilder = new TagBuilder("div");

            EnsureHtmlAttribute(htmlAttributes, "class", "card-header");

            tagBuilder.MergeAttributes(htmlAttributes);

            htmlHelper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));

            //Bloque para columnas
            htmlHelper.ViewContext.Writer.Write(string.Format("<div class='{0}'>", "row"));

            //Columna Derecha y el titulo
            htmlHelper.ViewContext.Writer.Write(string.Format("<div class='{0}'><h3>{1}</h3></div>", "col-sm-8", titulo));

            //Columna Izquiero
            htmlHelper.ViewContext.Writer.Write(string.Format("<div class='{0}'>", "col-sm-4 text-right"));

            HtmlPanelHeader thePanelHeader = new HtmlPanelHeader(htmlHelper.ViewContext);

            return thePanelHeader;
        }


        private static HtmlPanelBody PanelBodyHelper(this HtmlHelper htmlHelper, IDictionary<string, object> htmlAttributes)
        {

            TagBuilder tagBuilder = new TagBuilder("div");


            EnsureHtmlAttribute(htmlAttributes, "class", "card-body");


            tagBuilder.MergeAttributes(htmlAttributes);

            htmlHelper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));

            HtmlPanelBody thePanelBody = new HtmlPanelBody(htmlHelper.ViewContext);

            return thePanelBody;

        }



        private static HtmlPanelFooter PanelFooterHelper(this HtmlHelper htmlHelper, IDictionary<string, object> htmlAttributes)
        {

            TagBuilder tagBuilder = new TagBuilder("div");

            var attClass = new RouteValueDictionary();

            EnsureHtmlAttribute(htmlAttributes, "class", "card-footer");


            tagBuilder.MergeAttributes(htmlAttributes);

            htmlHelper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));

            HtmlPanelFooter theElement = new HtmlPanelFooter(htmlHelper.ViewContext);

            return theElement;

        }


        internal static void EndPanel(ViewContext viewContext)
        {
            viewContext.Writer.Write("</div>");
        }

        internal static void EndPanelHeader(ViewContext viewContext)
        {
            viewContext.Writer.Write("</div>"); //Cerrar bloque izquierdo
            viewContext.Writer.Write("</div>"); //Cerrar Row
            viewContext.Writer.Write("</div>"); //Cerrar Header
        }

        internal static void EndPanelBody(ViewContext viewContext)
        {
            viewContext.Writer.Write("</div>");

        }


        internal static void EndPanelFooter(ViewContext viewContext)
        {
            viewContext.Writer.Write("</div>");

        }

        internal static void EnsureHtmlAttribute(IDictionary<string, object> attributes, string key, string value)
        {
            if (attributes == null)
            {
                attributes = new RouteValueDictionary();
            }

            if (attributes.ContainsKey(key))
            {
                attributes[key] += " " + value;
            }
            else
            {
                attributes.Add(key, value);
            }
        }



    }
}
