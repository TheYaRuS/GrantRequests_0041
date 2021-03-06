﻿using System.Web;
using System.Web.Optimization;

namespace GrantRequests.WEB
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
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/mvcfoolproof").Include(
            "~/Scripts/MicrosoftAjax.js",
            "~/Scripts/MicrosoftMvcAjax.js",
            "~/Scripts/MicrosoftMvcValidation.js",
            "~/Scripts/MvcFoolproofJQueryValidation.min.js",
            "~/Scripts/MvcFoolproofValidation.min.js",
            "~/Scripts/mvcfoolproof.unobtrusive.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                "~/Scripts/Custom/custom-*"));
        }
    }
}
