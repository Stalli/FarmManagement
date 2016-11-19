using System.Web;
using System.Web.Optimization;

namespace FarmManagement
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryvalidate").Include(
                        "~/Scripts/jquery.validate.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.unobtrusive-ajax*"));

            bundles.Add(new ScriptBundle("~/Kendojs").Include(
                       "~/Scripts/KendoJs/kendo.all.js",
                       "~/Scripts/FarmCustom/jquery-typing.js",
                       "~/Scripts/FarmCustom/jquery.highlight.js"));

            bundles.Add(new ScriptBundle("~/Scripts").Include(
                       "~/Content/assets/js/libs/breakpoints.js",
                       "~/Scripts/FarmCustom/bootstrap.js",
                       "~/Content/assets/js/slimscroll/jquery.slimscroll.js",
                       "~/Content/assets/js/slimscroll/jquery.slimscroll.horizontal.js",
                       "~/Content/assets/js/jquery.sparkline.js",
                       "~/Scripts/FarmCustom/jquery.blockUI.js",
                       "~/Scripts/FarmCustom/jquery.noty.js",
                       "~/Scripts/FarmCustom/top.js",
                       "~/Scripts/FarmCustom/default.js",
                       "~/Content/assets/js/app.js",
                       "~/Content/assets/js/plugins.js",
                       "~/Content/assets/js/plugins.form-components.js",
                       "~/Content/assets/js/custom.js",
                       "~/Scripts/FarmCustom/Farm.js",
                       "~/Scripts/FarmCustom/BindDropdown.js"));


            bundles.Add(new StyleBundle("~/Style").Include(
                        "~/Content/assets/css/main.css",
                        "~/Content/assets/css/plugins.css",
                        "~/Content/assets/css/responsive.css",
                        "~/Content/assets/css/icons.css",
                        "~/Content/assets/css/fontawesome/font-awesome.css"));

            bundles.Add(new StyleBundle("~/kendocss").Include(
                        "~/Content/kendo/kendo.common.css",
                        "~/Content/kendo/kendo.bootstrap.css"));

            bundles.Add(new StyleBundle("~/bootstrap").Include("~/Content/bootstrap.css"));

            BundleTable.EnableOptimizations = false;
        }
    }
}