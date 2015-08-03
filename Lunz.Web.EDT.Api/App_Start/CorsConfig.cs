using Lunz.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Lunz.Web.EDT.Api
{
    public static class CorsConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.SuppressHostPrincipal();

            WebApiUnityActionFilterProvider.RegisterFilterProviders(config);

            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            // Web API configuration and services
            var jsonp = new JsonpMediaTypeFormatter();
            jsonp.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            config.Formatters.Insert(0, jsonp);

            GlobalConfiguration.Configuration.Formatters
                .JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
