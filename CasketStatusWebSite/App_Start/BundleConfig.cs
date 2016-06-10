using System.Web;
using System.Web.Optimization;

namespace CasketStatusWebSite
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/bootstrap-theme.min.css",
                      "~/Content/bootstrap-datetimepicker.min.css",
                      "~/Content/site.css"));

            /*
            // Bootstrap пикер даты (http://tarruda.github.io/bootstrap-datetimepicker/)
            bundles.Add(new ScriptBundle("~/bundles/datetimepicker").Include(
                    "~/Scripts/bootstrap-datetimepicker.min.js"
                ));
            bundles.Add(new StyleBundle("~/Content/css/datetimepicker").Include(
                    "~/Content/bootstrap-datetimepicker.min.css"
                ));
            */

            // moment.js - для работы с датами
            bundles.Add(new ScriptBundle("~/bundles/datetimepicker").Include(
                    "~/Scripts/moment.min.js",
                    "~/Scripts/moment-with-locales.min.js",
                    "~/Scripts/bootstrap-datetimepicker.min.js"
                ));
        }
    }
}
