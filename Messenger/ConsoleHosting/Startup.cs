using Owin;
using System.Web.Http;
using ConsoleHosting.Other;

namespace ConsoleHosting
{
	internal class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(Owin.IAppBuilder appBuilder)
        {
            // Configure Web API for self-host.
            SLogger.GetLogger().LogInfo("Configure Web API");
            HttpConfiguration config = new HttpConfiguration();

            //LogRequestAndResponseHandler
            config.MessageHandlers.Add(new CLogRequestAndResponseHandler());

            SLogger.GetLogger().LogInfo("Register WebApi Middleware");
            //! WebApiMiddleware
            MessengerService.WebApiConfig.Register(config);
            MessengerService.SwaggerConfig.Register();

            appBuilder.UseWebApi(config);
        }
    }

}
