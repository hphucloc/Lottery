using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebAppLottery
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("LotteryOpenApi", "api/v1/lottery/openapi",
                new { controller = "LotteryApi", action = "OpenApi" });
            routes.MapRoute("LotteryAll", "api/v1/lottery/all",
                new { controller = "LotteryApi", action = "All" });
            routes.MapRoute("LotteryDraws", "api/v1/lottery/{game}/draws",
                new { controller = "LotteryApi", action = "Draws" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
