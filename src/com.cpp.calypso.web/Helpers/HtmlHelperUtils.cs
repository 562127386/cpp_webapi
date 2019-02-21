using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;


using System.IO;
using com.cpp.calypso.framework;
using CommonServiceLocator;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;
using MvcReportViewer;

namespace com.cpp.calypso.web
{
    /// <summary>
    /// TODO: analizar una mejor distribuccion de estas utilidades. 
    /// </summary>
    public static class HtmlHelperUtils
    {
        static readonly ILogger log =
ServiceLocator.Current.GetInstance<ILoggerFactory>().Create(typeof(HtmlHelperUtils));


      

        /// <summary>
        /// Determinar si existe la vista
        /// </summary>
        /// <param name="html"></param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public static bool ExistsView(this HtmlHelper html,string viewName)
        {
            var controllerContext = html.ViewContext.Controller.ControllerContext;
            var viewResult = ViewEngines.Engines.FindView(controllerContext, viewName, null);
 
            return (viewResult.View != null) ? true: false;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="imagen"></param>
        /// <returns></returns>
        public static MvcHtmlString RenderImagen(this HtmlHelper htmlHelper,string imagen, object htmlAttributes = null)
        {
 
            string urlImagen = htmlHelper.UrlImagen(imagen);

            var mainAttrs = new Dictionary<string, object>();
            mainAttrs.Add("src", urlImagen);
             
            var tagBuilder = new FluentTagBuilder("img")
                .AddAttributes(htmlAttributes)
                .AddAttributes(mainAttrs);

            return tagBuilder.ToMvcHtmlString(TagRenderMode.Normal);
        }

 

        /// <summary>
        /// Obtener la URL de la Imagen
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="imagen"></param>
        /// <returns></returns>
        public static string UrlImagen(this HtmlHelper htmlHelper, string imagen)
        {
            IParametroService _parametroService = ServiceLocator.Current.GetInstance<IParametroService>();
            var pathImagen = _parametroService.GetValor<string>(CodigosParametros.PARAMETRO_IMAGENES_URL_BASE);

            return Path.Combine(pathImagen, imagen);

        }


        /// <summary>
        /// Renderizar el nombre de la aplicacion
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static MvcHtmlString RenderNombreAplicacion(this HtmlHelper htmlHelper)
        {
            IParametroService _parametroService = ServiceLocator.Current.GetInstance<IParametroService>();
            string app = _parametroService.GetValor<string>(CodigosParametros.PARAMETRO_NOMBRE_APLICACION);
            return new MvcHtmlString(app);
        }

        
        public static MvcHtmlString RenderNombreUsuario(this HtmlHelper htmlHelper)
        {
            var application = ServiceLocator.Current.GetInstance<IApplication>();

            var usuario = application.GetCurrentUser();
            if (usuario == null)
                return new MvcHtmlString(string.Empty);

            var msg = string.Format("{0} {1}", usuario.Nombres, usuario.Apellidos);

            return new MvcHtmlString(msg);
        }


        public static MvcHtmlString RenderNombreModulo(this HtmlHelper htmlHelper)
        {
            var application = ServiceLocator.Current.GetInstance<IApplication>();

            var modulo = application.GetCurrentModule();

            var msg = string.Format("{0}", modulo.Nombre);

            return new MvcHtmlString(msg);
        }



        /// <summary>
        /// Renderizar el logo de la aplicacion
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static IHtmlString RenderLogoAplicacion(this HtmlHelper htmlHelper)
        {
            IParametroService _parametroService = ServiceLocator.Current.GetInstance<IParametroService>();
            string logo = _parametroService.GetValor<string>(CodigosParametros.PARAMETRO_APLICACION_LOGO);

            if (string.IsNullOrWhiteSpace(logo))
                return new HtmlString(string.Empty);


            string urlLogo = htmlHelper.UrlImagen(logo);

            var mainAttrs = new Dictionary<string, object>();
            mainAttrs.Add("src", urlLogo);
            mainAttrs.Add("class", "logo");


            var img = new FluentTagBuilder("img")
                .AddAttributes(mainAttrs);

            return img;
        }





        /// <summary>
        /// Verificar si es necesario recolectar informacion
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static bool RecolectarInformacionAnalisis(this HtmlHelper htmlHelper)
        {
            IParametroService _parametroService = ServiceLocator.Current.GetInstance<IParametroService>();
            bool result = _parametroService.GetValor<bool>(CodigosParametros.PARAMETRO_RECOLECTAR_INFO_ANALISIS);
            return result;
        }



        #region Pager

        public static IHtmlString Pager(
            this HtmlHelper html,
            int recordsCount, Func<int, string> getUrl,
            int pageSize = 0,
            int pageIndex = 0)
        {
            if (pageSize <= 0)
            {
                return null;
            }
            int pages = recordsCount / pageSize + 1;
            if (recordsCount % pageSize == 0)
            {
                pages--;
            }
            if (pages <= 1)
            {
                return null;
            }

            const int lBorder = 0;
            int uBorder = pages - 1;

            var prev = pageIndex > lBorder;
            var next = pageIndex < uBorder;

            var ul = new TagBuilder("ul");
            ul.AddCssClass("pagination");

            var sb = new StringBuilder();

            var tag = new TagBuilder(!prev ? "span" : "a") { InnerHtml = "&laquo;" };
            if (prev)
            {
                tag.Attributes["href"] = getUrl(pageIndex - 1);
            }
            var li = new TagBuilder("li") { InnerHtml = tag.ToString() };
            if (!prev)
            {
                li.AddCssClass("disabled");
            }
            sb.Append(li);

            const int radio = 4;
            int lr = 0;
            int rr = 0;
            int inicio = pageIndex - radio;
            if (inicio < lBorder)
            {
                rr = -inicio;
                inicio = lBorder;
            }
            int final = pageIndex + radio;
            if (final > uBorder)
            {
                lr = final - uBorder;
                final = uBorder;
            }
            inicio = Math.Max(lBorder, inicio - lr);
            final = Math.Min(uBorder, final + rr);
            for (int i = inicio; i <= final; i++)
            {
                var current = i == pageIndex;
                tag = new TagBuilder(current ? "span" : "a");
                tag.SetInnerText(Convert.ToString(i + 1));
                if (!current)
                {
                    tag.Attributes["href"] = getUrl(i);
                }
                li = new TagBuilder("li") { InnerHtml = tag.ToString() };
                if (current)
                {
                    li.AddCssClass("active");
                }
                sb.Append(li);
            }

            tag = new TagBuilder(!next ? "span" : "a") { InnerHtml = "&raquo;" };
            if (next)
            {
                tag.Attributes["href"] = getUrl(pageIndex + 1);
            }

            li = new TagBuilder("li") { InnerHtml = tag.ToString() };
            if (!next)
            {
                li.AddCssClass("disabled");
            }
            sb.Append(li);

            ul.InnerHtml = sb.ToString();
            return new HtmlString(ul.ToString());
        }

        #endregion

        #region BsValidationSummary

        public static IHtmlString BsValidationSummary(this HtmlHelper html)
        {
            return html.ValidationSummary(null, new { @class = CssClass.Form.ValidationSummary });
        }

        #endregion

        #region BsButtons

        public static IHtmlString BsLink(this HtmlHelper htmlHelper, object innerText, string buttonClass, string iconClass, string linkHref, object htmlAttributes = null, AjaxOptions ajaxOptions = null)
        {
            var mainAttrs = new Dictionary<string, object>();
            if (!String.IsNullOrWhiteSpace(linkHref)) mainAttrs.Add("href", linkHref);
            if (!String.IsNullOrWhiteSpace(buttonClass)) mainAttrs.Add("class", buttonClass);

            var link = new FluentTagBuilder("a")
                .AddAttributes(mainAttrs)
                .AddAttributes(htmlAttributes)
                .AddContent(htmlHelper.Icon(iconClass, innerText));

            if (ajaxOptions != null)
            {
                link.MergeAttributes(ajaxOptions.ToUnobtrusiveHtmlAttributes());
            }

            return link;
        }

        public static IHtmlString BsButton(this HtmlHelper htmlHelper, object innerText, string buttonClass, string iconClass, object htmlAttributes)
        {
            return BsButton(htmlHelper, innerText, buttonClass, iconClass, "button", HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static IHtmlString BsButton(this HtmlHelper htmlHelper, object innerText, string buttonClass, string iconClass, string buttonType, object htmlAttributes)
        {
            return BsButton(htmlHelper, innerText, buttonClass, iconClass, buttonType, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static IHtmlString BsButton(this HtmlHelper htmlHelper, object innerText, string buttonClass, string iconClass, string buttonType = "button", IDictionary<string, object> htmlAttributes = null)
        {
            var mainAttrs = new Dictionary<string, object>();
            if (!String.IsNullOrWhiteSpace(buttonType)) mainAttrs.Add("type", buttonType);
            if (!String.IsNullOrWhiteSpace(buttonClass)) mainAttrs.Add("class", buttonClass);

            return new FluentTagBuilder("button")
                .AddAttributes(mainAttrs)
                .AddAttributes(htmlAttributes)
                .AddContent(htmlHelper.Icon(iconClass, innerText));
        }

        #endregion

        #region Icon

        public static IHtmlString Icon(this HtmlHelper helper, string icon, object text, object htmlAttributes = null)
        {
            string encodedText = helper.Encode(text);
            return String.IsNullOrEmpty(encodedText) ? Icon(helper, icon, false, htmlAttributes) : new HtmlString(Icon(helper, icon, true, htmlAttributes) + encodedText);
        }

        public static IHtmlString Icon(this HtmlHelper helper, string icon, bool appendNonBreakSpace = false, object htmlAttributes = null)
        {
            if (String.IsNullOrEmpty(icon)) return null;
            if (!icon.StartsWith("fa")) icon = "fa fa-" + icon;
            FluentTagBuilder tagBuilder = new FluentTagBuilder("span").WithCssClass(icon).AddAttributes(htmlAttributes);
            if (!appendNonBreakSpace) return tagBuilder;
            return new HtmlString(tagBuilder + helper.Encode(Char.ConvertFromUtf32(160)));
        }

        #endregion

        #region ReportViewer

        public static IHtmlString ReportViewer(
            this HtmlHelper html,
            string reportName,
            object parameters = null,
            ControlSettings settings = null,
            string id = "iframe1",
            string width = "100%",
            string height = "300px",
            string marginheight = "0",
            string frameborder = "0")
        {
            if (reportName == null) throw new ArgumentNullException("reportName");
            if (reportName == String.Empty) throw new ArgumentException("reportName");

            string reportsFolder = ConfigurationManager.AppSettings.Get("MvcReportViewer.ReportsFolder");
            if (String.IsNullOrEmpty(reportsFolder))
                throw new ConfigurationErrorsException(String.Format(MensajesWeb.ConfigurationErrors_MissingApplicationSetting_ErrorMessage, "MvcReportViewer.ReportsFolder"));

            reportsFolder = VirtualPathUtility.AppendTrailingSlash(reportsFolder);
            string reportPath = VirtualPathUtility.Combine(reportsFolder, reportName);

            var htmlAttributes = new { id, width, height, marginheight, frameborder, @class = "report-viewer" };

            settings = settings ?? new ControlSettings
            {
                PromptAreaCollapsed = true
            };

            var parametros = Newtonsoft.Json.JsonConvert.SerializeObject(parameters);

            log.DebugFormat("Se envia a ejecutar el reporte {0} con los siguientes parametros: {1}", reportName, parametros);

            var viewr = html.MvcReportViewerFluent(reportPath)
                .ReportParameters(parameters)
                .Attributes(htmlAttributes)
                .ControlSettings(settings);
            return viewr;
        }

        #endregion



    }
}