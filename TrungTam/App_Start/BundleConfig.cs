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
            bundles.Add(new ScriptBundle("~/bundles/jscore").Include(      
                        "~/Asset/admin/vendor/jquery/jquery.min.js",
                        "~/Asset/admin/vendor/bootstrap/js/bootstrap.bundle.min.js",
                        "~/Asset/admin/vendor/jquery-easing/jquery.easing.min.js",
                        "~/Asset/admin/js/sb-admin-2.min.js",
                        "~/Asset/admin/vendor/chart.js/Chart.min.js",
                        "~/Asset/admin/js/demo/chart-area-demo.js",
                        "~/Asset/admin/js/demo/chart-pie-demo.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"
                        ));

            
            var fontCDNPath = "https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i";
            bundles.Add(new StyleBundle("~/bundles/fonts", fontCDNPath).Include(
                               
                        ));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                                  "~/Content/bootstrap.min.css",
                                  "~/Content/Site.css",
                                  "~/Content/PagedList.css"
                ));
            bundles.Add(new StyleBundle("~/bundles/core").Include(

                      "~/Asset/admin/vendor/fontawesome-free/css/all.min.css",
                      "~/Asset/admin/vendor/fontawesome-free/css/fontawesome.min.css",
                       "~/Asset/admin/vendor/fontawesome-free/css/regular.min.css",
                      "~/Asset/admin/css/sb-admin-2.min.css"));
            BundleTable.EnableOptimizations = true;
        }
    }
}
