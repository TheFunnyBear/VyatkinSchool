using System.Web.Optimization;

namespace VyatkinSchool
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/fonts.css",
                      "~/Content/default.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/Mobilejs").Include(
                "~/Scripts/jquery.mobile-1.*",
                "~/Scripts/jquery-1.*"));

            bundles.Add(new StyleBundle("~/Content/Mobilecss").Include(
                "~/Content/jquery.mobile.structure-1.4.5.min.css",
                "~/Content/jquery.mobile-1.4.5.css",
                "~/Content/site.css", 
                "~/Content/PagedList.css"));
        }
    }
}
