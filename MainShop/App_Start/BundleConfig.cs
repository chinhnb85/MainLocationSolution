﻿using System.Web;
using System.Web.Optimization;

namespace MainShop
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/respond").Include(
                      //"~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/css/bootstrap-rtl").Include(
                "~/assets/css/bootstrap-rtl.min.css"));

            bundles.Add(new StyleBundle("~/css/beyond").Include(
                "~/assets/css/beyond.min.css",
                "~/assets/css/demo.min.css",
                "~/assets/css/font-awesome.min.css",
                "~/assets/css/typicons.min.css",
                "~/assets/css/weather-icons.min.css",
                "~/assets/css/animate.min.css"
                ));

            bundles.Add(new StyleBundle("~/css/beyond-rtl").Include(
                "~/assets/css/beyond-rtl.min.css",
                "~/assets/css/demo.min.css",
                "~/assets/css/font-awesome.min.css",
                "~/assets/css/typicons.min.css",
                "~/assets/css/weather-icons.min.css",
                "~/assets/css/animate.min.css"
                ));

            bundles.Add(new StyleBundle("~/css/datatable").Include(
                "~/assets/css/dataTables.bootstrap.css"));

            bundles.Add(new ScriptBundle("~/bundles/skin").Include(
                "~/assets/js/skins.min.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/assets/js/bootstrap.min.js",
                "~/assets/js/slimscroll/jquery.slimscroll.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/beyond").Include(
                "~/assets/js/beyond.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/assets/js/jqueryval/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
                "~/assets/js/datatable/jquery.dataTables.min.js",
                "~/assets/js/datatable/dataTables.tableTools.min.js",
                "~/assets/js/datatable/dataTables.bootstrap.min.js",
                "~/assets/js/datatable/datatables-init.js"));

            bundles.Add(new StyleBundle("~/css/chosen").Include(
                "~/assets/modules/plugins/chosen/css/chosen.css",
                "~/assets/modules/plugins/jScrollPane/css/jquery.jscrollpane.css",
                "~/assets/modules/plugins/tipsy/css/tipsy.css"));

            bundles.Add(new ScriptBundle("~/bundles/chosen").Include(                
                "~/assets/modules/plugins/chosen/js/chosen.jquery.min.js",
                "~/assets/modules/plugins/jScrollPane/js/jquery.jscrollpane.min.js",
                "~/assets/modules/plugins/tipsy/js/jquery.tipsy.js"));

            bundles.Add(new ScriptBundle("~/bundles/editer").Include(
               "~/assets/js/editors/summernote/summernote.js"));
        }
    }
}
