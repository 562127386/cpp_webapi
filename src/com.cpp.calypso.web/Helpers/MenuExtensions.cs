using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;
using CommonServiceLocator;
using com.cpp.calypso.comun.aplicacion;

namespace com.cpp.calypso.web
{
    public static class MenuExtensions
    {
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


        /// <summary>
        /// Renderiza menu de acciones sobre un registro (Editar, Eliminar, Detalles)
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static MvcHtmlString RenderMenuAction(this HtmlHelper htmlHelper,params ActionRegistro[] acciones)
        {
            return RenderMenuAction(htmlHelper, new RouteValueDictionary());
        }

        public static MvcHtmlString RenderMenuAction(this HtmlHelper htmlHelper, IDictionary<string, object> htmlAttributes)
        {
            return null;
        }


        public static MvcHtmlString RenderMenu(this HtmlHelper htmlHelper)
        {
         
            return RenderMenu(htmlHelper,new RouteValueDictionary());
        }

       

        /// <summary>
        /// Contruir un panel 
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static MvcHtmlString RenderMenu(this HtmlHelper htmlHelper, IDictionary<string, object> htmlAttributes)
        {
            //TODO: Mejorar generacion de Menu
            //1. Aplicar Seguridad
            //2. Estilos
            //3. Cache o guardar Session
            //4. Menus Jerarquicos
 
            var app = ServiceLocator.Current.GetInstance<IApplication>();

            if (!app.IsAuthenticated())
                return null; 

            var cache = ServiceLocator.Current.GetInstance<ICacheManager>();

            var user = app.GetCurrentUser();
            if (user == null)
                return null;

            var codigoCache = "Web.Cache.Menu." + app.GetCurrentModule().Codigo + "." + user.Cuenta;
  
            var menuCache = cache.GetData(codigoCache) as MvcHtmlString;

            if (menuCache == null)
            {
                var menuService = ServiceLocator.Current.GetInstance<IMenuService>();

                var itemMenus = menuService.GetItemMenuAutorizados();
                
                menuCache = BuildMenuBootstrap4(app.GetCurrentModule().Nombre, itemMenus, htmlAttributes);

                cache.Add(codigoCache, menuCache);
            }
 
            return menuCache;

        }

         

        private static string GenerateMenuItemBootstrap4(MenuItem item)
        {
            TagBuilder li = new TagBuilder("li");
            li.AddCssClass("nav-item");

            var url = VirtualPathUtility.ToAbsolute("~/" + item.Url);

            TagBuilder a = new TagBuilder("a");
            a.AddCssClass("nav-link");
            a.MergeAttribute("href", url);

            var textMenu = string.Format(item.Nombre);
            if (!string.IsNullOrWhiteSpace(item.Icono))
            {
                textMenu = string.Format("<i class=\"{0}\"></i> {1}", item.Icono, item.Nombre);
            }
            a.InnerHtml += textMenu;

            li.InnerHtml += a;

            return li.ToString();
        }

        private static MvcHtmlString BuildMenuBootstrap4(string modulo, ICollection<MenuItem> itemMenus, IDictionary<string, object> htmlAttributes)
        {
            TagBuilder tagBuilder = new TagBuilder("ul");

            EnsureHtmlAttribute(htmlAttributes, "class", "nav");

            tagBuilder.MergeAttributes(htmlAttributes);

           
            TagBuilder liModulo = new TagBuilder("li");
            liModulo.AddCssClass("nav-title");
            liModulo.InnerHtml += modulo;
            tagBuilder.InnerHtml += liModulo.ToString();

            foreach (var item in itemMenus.OrderBy(a => a.Orden))
            {
                if (item.Estado == EstadoItemMenu.Desactivo)
                    continue;

                if (item.PadreId == null)
                {
                    //Obtener todos los hijos
                    var hijos = itemMenus.Where(mnuItem => mnuItem.PadreId == item.Id).OrderBy(mnuItem => mnuItem.Orden).ToList();

                    if (hijos.Count == 0)
                    {
                        //Crear unicamente si el item no es un contenedor, ya que un contenedor si no tiene hijos no debe crearse
                        if (item.TipoId != TipoItemMenu.Contenedor)
                            tagBuilder.InnerHtml += GenerateMenuItemBootstrap4(item);
                    }
                    else
                    {
                        TagBuilder li = new TagBuilder("li");
                        li.AddCssClass("nav-item nav-dropdown");

                        var url = "#";

                        TagBuilder a = new TagBuilder("a");
                        a.AddCssClass("nav-link nav-dropdown-toggle");
                        a.MergeAttribute("href", url);
                        

                        var textMenu = string.Format(item.Nombre);
                        if (!string.IsNullOrWhiteSpace(item.Icono))
                        {
                            textMenu = string.Format("<i class=\"{0}\"></i> {1}", item.Icono, item.Nombre);
                        }
                        a.InnerHtml += textMenu;

                        li.InnerHtml += a;

                        TagBuilder ul = new TagBuilder("ul");
                        ul.AddCssClass("dropdown-menu");
                      
                        foreach (var hijo in hijos)
                        {
                            if (hijo.Estado == EstadoItemMenu.Desactivo)
                                continue;

                            ul.InnerHtml += GenerateMenuItemBootstrap4(hijo);

                        }

                        li.InnerHtml += ul;
                        tagBuilder.InnerHtml += li;

                    }
                }
            }

            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal));
        }


        private static MvcHtmlString BuildMenu(ICollection<MenuItem> itemMenus, IDictionary<string, object> htmlAttributes)
        {
            TagBuilder tagBuilder = new TagBuilder("ul");

            EnsureHtmlAttribute(htmlAttributes, "class", "nav navbar-nav");

            tagBuilder.MergeAttributes(htmlAttributes);

            foreach (var item in itemMenus.OrderBy(a => a.Orden))
            {
                if (item.Estado == EstadoItemMenu.Desactivo)
                    continue;

                if (item.PadreId == null)
                {
                    //Obtener todos los hijos
                    var hijos = itemMenus.Where(mnuItem => mnuItem.PadreId == item.Id).OrderBy(mnuItem => mnuItem.Orden).ToList();

                    if (hijos.Count == 0)
                    {
                        //Crear unicamente si el item no es un contenedor, ya que un contenedor si no tiene hijos no debe crearse
                        if (item.TipoId != TipoItemMenu.Contenedor)
                            tagBuilder.InnerHtml += GenerateMenuItem(item);
                    }
                    else
                    {
                        TagBuilder li = new TagBuilder("li");
                        li.AddCssClass("dropdown");

                        var url = "#";

                        TagBuilder a = new TagBuilder("a");
                        a.AddCssClass("dropdown-toggle");
                        a.Attributes.Add("data-toggle", "dropdown");
                        a.MergeAttribute("href", url);
                        a.InnerHtml += item.Nombre;

                        li.InnerHtml += a;

                        TagBuilder ul = new TagBuilder("ul");
                        ul.AddCssClass("dropdown-menu");
                        ul.Attributes.Add("role", "menu");

                        foreach (var hijo in hijos)
                        {
                            if (hijo.Estado == EstadoItemMenu.Desactivo)
                                continue;

                            ul.InnerHtml += GenerateMenuItem(hijo);

                        }

                        li.InnerHtml += ul;
                        tagBuilder.InnerHtml += li;

                    }
                }
            }

            return  MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal));
        }

        private static string GenerateMenuItem(MenuItem item) {
            TagBuilder li = new TagBuilder("li");


            //TODO:JSA, REVISAR EL TEMA DE MENUS, LA URL. QUE PASA SI EL SITIO ESTA COMO APLICACION.
            var url = VirtualPathUtility.ToAbsolute("~/" + item.Url);

            TagBuilder a = new TagBuilder("a");
            a.MergeAttribute("href", url);

            var textMenu = string.Format(item.Nombre);
            if (!string.IsNullOrWhiteSpace(item.Icono)) {
                textMenu = string.Format("<i class=\"{0}\"></i> {1}",item.Icono,item.Nombre);
            }
            a.InnerHtml += textMenu;

            li.InnerHtml += a;

            return li.ToString();
        }
    }
}