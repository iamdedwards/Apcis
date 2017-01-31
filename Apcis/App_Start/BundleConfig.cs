using System.Web;
using System.Web.Optimization;

namespace Apcis
{
    public class BundleConfig
    {
        // Pour plus d’informations sur le regroupement, rendez-vous sur http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts")
            .IncludeDirectory("~/Scripts", "*.js", true));

            bundles.Add(new StyleBundle("~/Content/style")
                .IncludeDirectory("~/Content/Fonts", "*.css", true).Include("~/Content/css/main.css"));
            bundles.Add(new StyleBundle("~/Content/css/bootstrap")
               .IncludeDirectory("~/Content/Css/bootstrap", "*.css", true));

          



        }
    }
}
