using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Web;
using System.Web.Hosting;
using System.Web.Optimization;


namespace ConsultantContractsInternal
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = false;

            #region Scripts
            // Scripts that all sites can reference
            bundles.Add(new ScriptBundle("~/scripts/global", "/ahtdnet/scripts/global").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.cookie.js",
                "~/Scripts/moment.js",
                "~/Scripts/json2.js",
                "~/Scripts/global-shared.js",
                "~/Scripts/global-topbar.js",
                "~/Scripts/global-loader.js",
                "~/Scripts/accounting.js",
                "~/Scripts/numeral/numeral.min.js"));

            bundles.Add(new ScriptBundle("~/scripts/modernizr", "/ahtdnet/scripts/modernizr").Include(
                "~/Scripts/modernizr-*"));

            // Scripts specifically for this site
            bundles.Add(new ScriptBundle("~/scripts/site").Include(
                "~/Scripts/site-shared.js"));

            // Optionally, views/sites can reference individual stuff as needed
            bundles.Add(new ScriptBundle("~/scripts/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/json2.js"));

            bundles.Add(new ScriptBundle("~/scripts/jquery-dropdown").Include(
                "~/Scripts/dropdown.js"));

            bundles.Add(new ScriptBundle("~/scripts/jqueryui").Include(
                "~/Scripts/jquery-ui-{version}.js",
                "~/Scripts/jquery-ui.helpers.js"));

            bundles.Add(new ScriptBundle("~/scripts/jqueryui-selector").Include(
                "~/Scripts/select2.js",
                "~/Scripts/select2.helpers.js"));

            bundles.Add(new ScriptBundle("~/scripts/knockout").Include(
                "~/Scripts/knockout-3.3.0.debug.js",
                "~/Scripts/knockout.custom-bindings.js",
                "~/Scripts/knockout.mapping-latest.debug.js",
                "~/Scripts/knockout.validation.js",
                "~/Scripts/knockout.custom-validation-rules.js",
                "~/Scripts/Shared/koMonetaryFormatting.js"));

            bundles.Add(new ScriptBundle("~/scripts/jqueryval").Include(
                "~/Scripts/jquery.validate*",
                "~/Scripts/jquery.unobtrusive*"));
            // All-inclusive super bundles
            bundles.Add(new ScriptBundle("~/scripts/jtable").Include(
                "~/Scripts/jquery-ui-{version}.js",
                "~/Scripts/jquery-ui.helpers.js",
                "~/Scripts/jquery.validate*",
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.jtable.js",
                "~/Scripts/jquery.jtable.helpers.js",
                "~/Scripts/select2.js",
                "~/Scripts/select2.helpers.js"));

            bundles.Add(new ScriptBundle("~/scripts/inputmask").Include(
                "~/Scripts/jquery.maskedinput.min.js"));

            bundles.Add(new ScriptBundle("~/scripts/vms").IncludeDirectory("~/Scripts/ViewModels", "*.js"));
            
            #endregion

            #region Styles
            // Styles that all sites can reference
            bundles.Add(new StyleBundle("~/Content/global").Include("~/Content/global-shared.css", new CssRewriteUrlTransform())
            .Include("~/Content/global-topbar.css", new CssRewriteUrlTransform()));

            // Styles specifically for this site
            bundles.Add(new StyleBundle("~/Content/site").Include("~/Content/site-shared.css", new CssRewriteUrlTransform())
                                                        .Include("~/Content/Site.css", new CssRewriteUrlTransform()));

            // Optionally, views/sites can reference individual stuff as needed
            bundles.Add(new StyleBundle("~/Content/themes/metro/jquery-dropdown").Include("~/Content/themes/metro/dropdown.css"));

            bundles.Add(new StyleBundle("~/Content/themes/metro/jqueryui").Include("~/Content/themes/metro/jquery-ui.css"));

            bundles.Add(new StyleBundle("~/Content/themes/metro/jqueryui-selector").Include("~/Content/themes/metro/select2.css"));


            bundles.Add(new StyleBundle("~/Content/themes/metro/images/jtable").Include("~/Content/themes/metro/jquery-ui.css")
                .Include("~/Content/themes/metro/jtable-metro.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/Content/themes/metro/jtable-new").Include("~/Content/themes/metro/jquery-ui.css")
                .Include("~/Content/themes/jtable/jtable-metro.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/Styles/CommonCSS").Include("~/Styles/Common.css", new CssRewriteUrlTransform()));               

            #endregion
        }
    }
}