using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace WeatherForecast
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/weatherapp").Include(
                "~/WebUI/app/shared/scripts/angular.min.js",
                "~/WebUI/app/app.js",
                "~/WebUI/app/services/forecastService.js",
                "~/WebUI/app/weatherMain/weatherMain.js",
                "~/WebUI/app/directives/dailyForecast/dailyForecast.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                 "~/Content/bootstrap.css",
                 "~/Content/styles.css"));
        }
    }
}
