using System.Web.Optimization;

namespace JqueryDapper
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

            bundles.Add(new StyleBundle("~/Content/LayoutCSS").Include(
                "~/Content/Style/menu.css",
                "~/Content/Style/style.css",
                "~/Content/App/AutoStyle.css",
                "~/Content/Style/owlcarousel.css",
                "~/Content/Style/owltheme.css",
                "~/Content/Style/owltransitions.css",
                "~/Content/Style/fontawesome.min.css", 
                "~/Content/Style/zerogrid.css"));

            bundles.Add(new ScriptBundle("~/bundles/LayoutJS").Include(
                "~/Scripts/Style/html5.js",
                "~/Scripts/Style/mediaqueries.js",
                "~/Scripts/Style/owlcarousel.js",
                "~/Scripts/Style/script.js",
                "~/Scripts/js2form.js",
                "~/Scripts/form2js.js"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
