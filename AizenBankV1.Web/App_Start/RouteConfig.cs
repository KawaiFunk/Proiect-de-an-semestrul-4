using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AizenBankV1.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }

    public static class BundleConfig
    {
        public static void RegisterBundle(BundleCollection bundles)
        {
            // Bootstrap setup
            bundles.Add(new StyleBundle("~/bundles/template/bootstrap/css").Include(
                "~/Conent/themes/bootstrap.min.css",
                "~/Conent/themes/bootstrap-grid.css",
                "~/Conent/themes/bootstrap-grid.min.css",
                "~/Conent/themes/bootstrap-grid.rtl.css",
                "~/Conent/themes/bootstrap-grid.rtl.min.css",
                "~/Conent/themes/bootstrap-reboot.css",
                "~/Conent/themes/bootstrap-reboot.min.css",
                "~/Conent/themes/bootstrap-reboot.rtl.css",
                "~/Conent/themes/bootstrap-reboot.rtl.min.css",
                "~/Conent/themes/bootstrap-utilities.css",
                "~/Conent/themes/bootstrap-utilities.min.css",
                "~/Conent/themes/bootstrap-utilities.rtl.css",
                "~/Conent/themes/bootstrap-utilities.rtl.min.css",
                "~/Conent/themes/bootstrap.css",
                "~/Conent/themes/bootstrap.rtl.css",
                "~/Conent/themes/bootstrap.rtl.min.css",
                "~/Conent/themes/font-awesome.css",
                "~/Conent/themes/font-awesome.min.css",
                "~/Conent/themes/Site.css"));

            bundles.Add(new StyleBundle("~/bundles/style/css").Include("~/Template/css/sb-admin-2.css",
                "~/Template/css/sb-admin-2.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/style/js").Include("~/Template/js/sb-admin-2.js",
                "~/Template/js/sb-admin-2.min.js",
                "~/Template/js/demo/chart-area-demo.js",
                "~/Template/js/demo/chart-bar-demo.js",
                "~/Template/js/demo/chart-pie-demo.js",
                "~/Template/js/demo/datatables-demo.js",
                "~/Template/vendor/bootstrap/js/bootstrap.bundle.js",
                "~/Template/vendor/bootstrap/js/bootstrap.bundle.min.js",
                "~/Template/vendor/bootstrap/js/bootstrap.js",
                "~/Template/vendor/bootstrap/js/bootstrap.min.js",
                "~/Template/vendor/chart.js/Chart.bundle.js",
                "~/Template/vendor/chart.js/Chart.bundle.min.js",
                "~/Template/vendor/chart.js/Chart.js",
                "~/Template/vendor/chart.js/Chart.min.js",
                "~/Template/vendor/datatables/dataTables.bootstrap4.js",
                "~/Template/vendor/datatables/dataTables.bootstrap4.min.js",
                "~/Template/vendor/datatables/jquery.dataTables.js",
                "~/Template/vendor/datatables/jquery.dataTables.min.js"));

            bundles.Add(new StyleBundle("~/bundles/font-awesome/css").Include("~/Template/vendor/fontawesome-free/css/all.css",
                "~/Template/vendor/fontawesome-free/css/all.min.css",
                "~/Template/vendor/fontawesome-free/css/brands.css",
                "~/Template/vendor/fontawesome-free/css/brands.min.css",
                "~/Template/vendor/fontawesome-free/css/fontawesome.css",
                "~/Template/vendor/fontawesome-free/css/fontawesome.min.css",
                "~/Template/vendor/fontawesome-free/css/regular.css",
                "~/Template/vendor/fontawesome-free/css/regular.min.css",
                "~/Template/vendor/fontawesome-free/css/solid.css",
                "~/Template/vendor/fontawesome-free/css/solid.min.css",
                "~/Template/vendor/fontawesome-free/css/svg-with-js.css",
                "~/Template/vendor/fontawesome-free/css/svg-with-js.min.css",
                "~/Template/vendor/fontawesome-free/css/v4-shims.css",
                "~/Template/vendor/fontawesome-free/css/v4-shims.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/fontawesome/js").Include("~/Template/vendor/fontawesome-free/js/all.js",
                "~/Template/vendor/fontawesome-free/js/all.min.js",
                "~/Template/vendor/fontawesome-free/js/brands.js",
                "~/Template/vendor/fontawesome-free/js/brands.min.js",
                "~/Template/vendor/fontawesome-free/js/conflict-detection.js",
                "~/Template/vendor/fontawesome-free/js/conflict-detection.min.js",
                "~/Template/vendor/fontawesome-free/js/fontawesome.js",
                "~/Template/vendor/fontawesome-free/js/fontawesome.min.js",
                "~/Template/vendor/fontawesome-free/js/regular.js",
                "~/Template/vendor/fontawesome-free/js/regular.min.js",
                "~/Template/vendor/fontawesome-free/js/solid.js",
                "~/Template/vendor/fontawesome-free/js/solid.min.js",
                "~/Template/vendor/fontawesome-free/js/v4-shims.js",
                "~/Template/vendor/fontawesome-free/js/v4-shims.min.js",
                "~/Template/vendor/fontawesome-free/attribution.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery/js").Include("~/Template/vendor/jquery/jquery.js",
                "~/Template/vendor/jquery/jquery.min.js",
                "~/Template/vendor/jquery/jquery.slim.js",
                "~/Template/vendor/jquery/jquery.slim.min.js",
                "~/Template/vendor/jquery-easing/jquery.easing.compatibility.js",
                "~/Template/vendor/jquery-easing/jquery.easing.js",
                "~/Template/vendor/jquery-easing/jquery.easing.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/gulpfile/js").Include("~/Template/gulpfile.js"));
        }
    }
}
