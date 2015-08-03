using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Lunz.Web.EDT.Management
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Login",
                url: "Login/{action}/{id}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Basic",
                url: "basic/{controller}/{action}/{id}",
                defaults: new { controller = "District", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Vehicle",
                url: "vehicle/{controller}/{action}/{id}",
                defaults: new { controller = "Affiliation", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "CompanyApply",
                url: "Company/{action}/{id}",
                defaults: new { controller = "Company", action = "Index", id = UrlParameter.Optional }
            );
			
			routes.MapRoute(
                name: "Product",
                url: "product/{controller}/{action}/{id}",
                defaults: new { controller = "Personal", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "Products",
               url: "products/{controller}/{action}/{id}",
               defaults: new { controller = "products", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Settings",
                url: "Settings/{controller}/{action}/{id}",
                defaults: new { controller = "DataDictionary", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Business",
                url: "Business/{controller}/{action}/{id}",
                defaults: new { controller = "Apply", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
