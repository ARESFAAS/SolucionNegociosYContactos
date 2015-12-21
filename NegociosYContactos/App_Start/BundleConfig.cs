using System.Web;
using System.Web.Optimization;

namespace NegociosYContactos
{
    public class BundleConfig
    {
        // Para obtener más información sobre Bundles, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/contact").Include(
                        "~/Scripts/contact.js"));
            bundles.Add(new ScriptBundle("~/bundles/colorpicker").Include(
                       "~/Scripts/bootstrap-colorpicker.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryupload").Include(
                     "~/Scripts/jQuery.FileUpload/vendor/jquery.ui.widget.js",
                     "~/Scripts/JavaScript-Templates/tmpl.min.js",
                     "~/Scripts/JavaScript-Canvas-to-Blob/canvas-to-blob.min.js",
                     "~/Scripts/JavaScript-Load-Image/load-image.all.min.js",
                     "~/Scripts/jQuery.FileUpload/jquery.iframe-transport.js",
                     "~/Scripts/jQuery.FileUpload/jquery.fileupload.js",
                     "~/Scripts/jQuery.FileUpload/jquery.fileupload-process.js",
                     "~/Scripts/jQuery.FileUpload/jquery.fileupload-image.js",
                     "~/Scripts/jQuery.FileUpload/jquery.fileupload-audio.js",
                     "~/Scripts/jQuery.FileUpload/jquery.fileupload-video.js",
                     "~/Scripts/jQuery.FileUpload/jquery.fileupload-validate.js",
                     "~/Scripts/jQuery.FileUpload/jquery.fileupload-ui.js"
                     ));
            bundles.Add(new ScriptBundle("~/bundles/login").Include(
                     "~/Scripts/login.js"));
            bundles.Add(new ScriptBundle("~/bundles/account").Include(
                     "~/Scripts/account.js"));
            bundles.Add(new ScriptBundle("~/bundles/admin").Include(
                     "~/Scripts/admin.js"));
            bundles.Add(new ScriptBundle("~/bundles/search").Include(
                      "~/Scripts/search.js"));
            bundles.Add(new ScriptBundle("~/bundles/index").Include(
                       "~/Scripts/index.js"));
            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                       "~/Scripts/site.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"//,
                                                       //"~/Scripts/jquery-ui.min.js",
                                                       //"~/Scripts/jquery.cycle.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        //"~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui.min.js",
                        "~/Scripts/jquery.cycle.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-social.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/bootstrap-colorpicker.min.css",
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/jquery-ui.min.css",
                      "~/Content/jquery-ui.structure.min.css", "~/Content/jquery-ui.theme.min.css"));
        }
    }
}
