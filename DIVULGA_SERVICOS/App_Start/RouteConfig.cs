using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DIVULGA_SERVICOS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "SitePrestador",
                url: "{NomePrestador}/ItemMenu",
                defaults: new
                {
                    controller = "SitePrestador",
                    action = "ItemMenu",
                }
            );

            routes.MapRoute(
                name: "SitePrestador1",
                url: "{NomePrestador}",
                defaults: new
                {
                    controller = "SitePrestador",
                    action = "SiteHomePrestador",
                }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
