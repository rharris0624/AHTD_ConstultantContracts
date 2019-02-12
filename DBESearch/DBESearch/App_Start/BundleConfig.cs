using System.Web;
using System.Web.Optimization;

namespace DBESearch
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/Scripts/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            //bundles.Add(new ScriptBundle("~/scripts/site").Include(
            bundles.Add(new ScriptBundle("~/Scripts/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/Scripts/jqueryui").Include(
               "~/Scripts/jquery-ui-{version}.js",
               "~/Scripts/jquery-ui.helpers.js"));
            bundles.Add(new ScriptBundle("~/Scripts/jqueryui-selector").Include(
               "~/Scripts/select2.js",
               "~/Scripts/select2.helpers.js"));

            bundles.Add(new ScriptBundle("~/Scripts/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
          
            bundles.Add(new ScriptBundle("~/Scripts/bootstrap").Include(
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/bootstrap-multiselect.js"));

            bundles.Add(new ScriptBundle("~/Scripts/jquery-dropdown").Include(
               "~/Scripts/dropdown.js"));
            bundles.Add(new ScriptBundle("~/Scripts/site").Include("~/Scripts/site-shared.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/bootstrap-multipleselect.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/metro/jquery-dropdown").Include("~/Content/themes/metro/dropdown.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/jqueryui").Include("~/Content/themes/base/jquery-ui.css","~/Content/themes/base/jquery.ui.all.css","~/Content/themes/base/jquery.ui.autocomplete"));

            bundles.Add(new StyleBundle("~/Content/themes/metro/jqueryui-selector").Include("~/Content/themes/metro/select2.css"));

            bundles.Add(new StyleBundle("~/Styles/CommonCSS").Include("~/Content/Common.css", new CssRewriteUrlTransform()));
        }
    }
}
