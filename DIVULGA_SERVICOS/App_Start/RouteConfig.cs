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
                "SitePrestador", // Route name
                "{NomePrestador}/{ItemMenu}", // URL with parameters
                new { controller = "SitePrestador", action = "ItemMenu"},
                new {ItemMenu = "(Servico|Cliente)"}
                );


            routes.MapRoute(
                "SitePrestador1",
                "{NomePrestador}",
                new
                {
                    controller = "SitePrestador",
                    action = "SiteHomePrestador",
                }
            );

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
