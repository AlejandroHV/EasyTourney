using System.Web;
using System.Web.Optimization;

namespace EasyTourney
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
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/BaseTemplate/css").Include(
                      "~/Content/BaseTemplate/css/*.css"
                      , "~/Content/BaseTemplate/css/bootstrap.min.css"
                      , "~/Content/BaseTemplate/css/font-awesome.min.css"
                      , "~/Content/BaseTemplate/css/animate.min.css"));

            bundles.Add(new ScriptBundle("~/Content/BaseTemplate/js").Include(
                      "~/Content/BaseTemplate/js/*.js"

                      , "~/Content/BaseTemplate/js/bootstrap.min.js"
                      , "~/Content/BaseTemplate/js/jquery.isotope.min.js"
                      , "~/Content/BaseTemplate/js/respond.min.js"
                      , "~/Content/BaseTemplate/js/wow.min.js"));
        }
    }
}
