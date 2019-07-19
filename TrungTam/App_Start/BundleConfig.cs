using System.Web;
using System.Web.Optimization;

namespace TrungTam
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;
            BundleTable.EnableOptimizations = true;
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //  "~/Scripts/jquery-{version}.js",
            //          "~/Scripts/jquery-ui-{version}.js",
            //          "~/Scripts/jquery.unobtrusive-ajax.js"
            //          ));
            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //                   "~/Content/bootstrap.css",
            //                   "~/Content/Site.css",
            //                   "~/Content/PagedList.css"
            // ));
            //bundles.Add(new ScriptBundle("~/bundles/jscore").Include(                        
            //            "~/Asset/admin/vendor/chart.js/Chart.js"
            //            ));        
            //bundles.Add(new ScriptBundle("~/bundles/js").Include(
            //            "~/Asset/admin/js/sb-admin-2.js",
            //             "~/Asset/admin/js/demo/chart-area-demo.js",
            //            "~/Asset/admin/js/demo/chart-pie-demo.js"
            //            ));         
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js",
            //            "~/Scripts/jquery-ui.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/sb-admin-2.css",
                      "~/Content/PagedList.css",                    
                      "~/Content/jquery-ui.css"));
            var fontCDNPath = "https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i";
            bundles.Add(new StyleBundle("~/bundles/fonts", fontCDNPath).Include(
                               
                        ));
            bundles.Add(new ScriptBundle("~/bundles/bootbox").Include(
                         "~/Scripts/sb-admin-2.js",
                      "~/Scripts/boot/bootbox.js",
                      "~/Scripts/boot/bootbox.locales.js"));
            //bundles.Add(new StyleBundle("~/bundles/core").Include(
            //          "~/Asset/admin/vendor/fontawesome-free/css/all.css",
            //          "~/Asset/admin/vendor/fontawesome-free/css/fontawesome.css",
            //           "~/Asset/admin/vendor/fontawesome-free/css/regular.css"
            //          ));

        }
    }
}
