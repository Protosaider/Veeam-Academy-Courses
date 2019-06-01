using log4net;
using MessengerService.Other;
using Other;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Configuration;
using System.Web.Http;

namespace MessengerService
{
    public static class WebApiConfig
    {
        public static ObservableDirectRouteProvider GlobalObservableDirectRouteProvider = new ObservableDirectRouteProvider();

        public static void Register(HttpConfiguration config)
        {
            //! Web API configuration and services

            // Use camel case for JSON data.
            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();

            SLogger.GetLogger().LogInfo("Test Messenger Service!");

            //! Web API routes
            // включает маршрутизацию с помощью атрибутов
            config.MapHttpAttributeRoutes(GlobalObservableDirectRouteProvider);

            //convention-based routing
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional }
            );

            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
            //config.Formatters.Add(new System.Net.Http.Formatting.JsonMediaTypeFormatter());
            //config.Formatters.JsonFormatter.SerializerSettings.DateParseHandling = Newtonsoft.Json.DateParseHandling.DateTimeOffset;

            config.EnsureInitialized();

            foreach (var item in GlobalObservableDirectRouteProvider.DirectRoutes)
            {
                Console.WriteLine(item.Route.RouteTemplate);
            }

            var str = ConfigurationManager.AppSettings["Secret"];
            Console.WriteLine();

        }
    }

    public class AppSettings
    {
        public string Secret { get; set; }
    }
}
