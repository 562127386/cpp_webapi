using System.Web;
using System.Web.Optimization;

namespace com.cpp.calypso.web
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                     "~/Scripts/jquery.blockUI*",
                     "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                    "~/Scripts/jquery.unobtrusive*",
                    "~/Scripts/jquery.validate*"));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap-datepicker.js",
                      "~/Scripts/locales/bootstrap-datepicker.es.min.js",
                      "~/Scripts/respond.js"));


            //Script Vendor 
            bundles.Add(new ScriptBundle("~/bundles/vendor").Include(
                    "~/Scripts/locales/moment.min.js",
                    "~/Scripts/toastr.js",

                    "~/Scripts/sweetalert/dist/sweetalert.min.js",
                    //abp
                    "~/Scripts/Abp/Framework/scripts/abp.js",
                    "~/Scripts/Abp/Framework/scripts/libs/abp.jquery.js",
                    "~/Scripts/Abp/Framework/scripts/libs/abp.toastr.js",
                    "~/Scripts/Abp/Framework/scripts/libs/abp.blockUI.js",
                    "~/Scripts/Abp/Framework/scripts/libs/abp.sweet-alert.js"
                    ));



            //Agregar javascript App de la plataforma
            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                            "~/Scripts/app/app.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.css",
                      "~/Content/simple-line-icons.min.css",
                      "~/Content/bootstrap-datepicker3.css",
                      "~/Content/toastr.min.css",
                      "~/Content/site.css"));
 
        }
    }
}
