using System.Web.Optimization;

namespace BeautyCare.Configuration.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Areas/AZ/Styles/jqueryui/css").Include(
                "~/Areas/AZ/Styles/jquery/jquery.ui.core.css",
                "~/Areas/AZ/Styles/jquery/jquery.ui.resizable.css",
                "~/Areas/AZ/Styles/jquery/jquery.ui.selectable.css",
                "~/Areas/AZ/Styles/jquery/jquery.ui.accordion.css",
                "~/Areas/AZ/Styles/jquery/jquery.ui.autocomplete.css",
                "~/Areas/AZ/Styles/jquery/jquery.ui.button.css",
                "~/Areas/AZ/Styles/jquery/jquery.ui.dialog.css",
                "~/Areas/AZ/Styles/jquery/jquery.ui.slider.css",
                "~/Areas/AZ/Styles/jquery/jquery.ui.tabs.css",
                "~/Areas/AZ/Styles/jquery/jquery.ui.datepicker.css",
                "~/Areas/AZ/Styles/jquery/jquery-ui.timepicker.css",
                "~/Areas/AZ/Styles/jquery/jquery.ui.progressbar.css",
                "~/Areas/AZ/Styles/jquery/jquery.ui.theme.css"));

            bundles.Add(new StyleBundle("~/Areas/AZ/Styles/bootstrap/css/css").Include(
                "~/Areas/AZ/Styles/bootstrap/css/bootstrap.min.css",
                "~/Areas/AZ/Styles/bootstrap/css/bootstrap-datetimepicker.min.css",
                "~/Areas/AZ/Styles/bootstrap/css/bootstrap-multiselect.css"));

            bundles.Add(new StyleBundle("~/Areas/AZ/Styles/css").Include(
                "~/Areas/AZ/Styles/Reset.css",
                "~/Areas/AZ/Styles/Form.css",
                "~/Areas/AZ/Styles/Grid.css",
                "~/Areas/AZ/Styles/Fonts.css",
                "~/Areas/AZ/Styles/SiteAdmin.css"));

            bundles.Add(new ScriptBundle("~/Areas/AZ/Scripts/jquery").Include(
                "~/Areas/AZ/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/Areas/AZ/Scripts/jqueryui").Include(
                "~/Areas/AZ/Scripts/jquery-ui-{version}.js",
                "~/Areas/AZ/Scripts/jquery-ui-timepicker-addon.js",
                "~/Areas/AZ/Scripts/jquery-ui-datepicker-ru.js",
                "~/Areas/AZ/Scripts/jquery-ui-timepicker-ru.js",
                "~/Areas/AZ/Scripts/globalize.js",
                "~/Areas/AZ/Scripts/globalize.culture.ru-RU.js"));

            bundles.Add(new ScriptBundle("~/Areas/AZ/Scripts/jqueryval").Include(
                "~/Areas/AZ/Scripts/jquery.unobtrusive*",
                "~/Areas/AZ/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/Areas/AZ/Scripts/bootstrap").Include(
                "~/Areas/AZ/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/Areas/AZ/Scripts/AZ").Include(
                "~/Areas/AZ/Scripts/AZ.js",
                "~/Areas/AZ/Scripts/cufon-yui-1.09i.js",
                "~/Areas/AZ/Scripts/CorporateACyr_400.font.js",
                "~/Areas/AZ/Scripts/jquery.tablednd_0_5.js",
                "~/Areas/AZ/Scripts/jquery.form.js",
                "~/Areas/AZ/Scripts/jquery.blockUI.js",
                "~/Areas/AZ/Scripts/noty/jquery.noty.js",
                "~/Areas/AZ/Scripts/noty/themes/default.js",
                "~/Areas/AZ/Scripts/noty/layouts/top.js",
                "~/Areas/AZ/Scripts/intravision.grid.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));
        }
    }
}
