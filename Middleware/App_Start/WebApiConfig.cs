using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Middleware
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Remove the JSON formatter remove if needed this allow to return XMl even without Accept: application/xml
            config.Formatters.Remove(config.Formatters.JsonFormatter);
            // Set the default response type to XML
            config.Formatters.XmlFormatter.UseXmlSerializer = true;
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
