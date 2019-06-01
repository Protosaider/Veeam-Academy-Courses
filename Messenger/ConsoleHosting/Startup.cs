using MessengerService;
using Other;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using SLogger = Other.SLogger;

namespace ConsoleHosting
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(Owin.IAppBuilder appBuilder)
        {
            // Configure Web API for self-host.
            SLogger.GetLogger().LogInfo("Configure Web API");
            HttpConfiguration config = new HttpConfiguration();

            //LogRequestAndResponseHandler
            config.MessageHandlers.Add(new LogRequestAndResponseHandler());

            SLogger.GetLogger().LogInfo("Register WebApi Middleware");
            //! WebApiMiddleware
            MessengerService.WebApiConfig.Register(config);
            MessengerService.SwaggerConfig.Register();

            appBuilder.UseWebApi(config);
        }
    }

}
