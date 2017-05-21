using System.Web;
using System.Web.Optimization;

namespace GoodVideoSystem
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Resources/js").Include(
                        "~/Resources/js/common/*.js",
                        "~/Resources/js/custom/*.js",
                        "~/Resources/Plugin/bootstrap/js/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Resources/css").Include("~/Resources/*.css",
                "~/Resources/Plugin/bootstrap/css/bootstrap.min.css"));
        }
    }
}