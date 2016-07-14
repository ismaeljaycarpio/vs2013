using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace AMS
{
    //bundles for .css
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //js
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                "~/Scripts/moment.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-datepicker").Include(
                "~/Scripts/bootstrap-datepicker.js"));

            //css
            bundles.Add(new StyleBundle("~/Content/site").Include(
                "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                "~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-theme").Include(
                "~/Content/bootstrap-theme.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-datepicker").Include(
                "~/Content/bootstrap-datepicker.css"));

            BundleTable.EnableOptimizations = true;
        }
    }
}