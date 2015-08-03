using Lunz.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Lunz.Web.EDT.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            CorsConfig.Register(config);
            //WebApiUnityActionFilterProvider.RegisterFilterProviders(config);

            //config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            //// Web API configuration and services
            //var jsonp = new JsonpMediaTypeFormatter();
            //jsonp.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //config.Formatters.Insert(0, jsonp);

            //GlobalConfiguration.Configuration.Formatters
            //    .JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            //config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "BasicApi",
                routeTemplate: "api/basic/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ProductApi",
                routeTemplate: "api/product/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "SettingsApi",
                routeTemplate: "api/settings/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
               name: "productsApi",
               routeTemplate: "api/products/{controller}/{action}/{id}",
               defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
