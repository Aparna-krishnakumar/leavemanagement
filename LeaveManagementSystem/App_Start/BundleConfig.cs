using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;


namespace LeaveManagementSystem
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-3.*"));
            bundles.Add(new ScriptBundle("~/Scripts/bootstrap").Include( "~/Scripts/umd/popper.js", "~/Scripts/bootstrap.js", 
                "~/Scripts/jquery.validate.js", "~/Scripts/jquery.validate.unobtrusive.js"));
            
            bundles.Add(new StyleBundle("~/Styles/bootstrap").Include("~/Content/bootstrap.css"));
            bundles.Add(new StyleBundle("~/Styles/site").Include("~/Content/Styles.css"));
            BundleTable.EnableOptimizations = true;
        }
    }
}