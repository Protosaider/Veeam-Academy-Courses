using System.Web.Http;
using Owin;

namespace ConsoleHostingMaintenance
{
	internal class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(Owin.IAppBuilder appBuilder)
        {
            // Configure Web API for self-host.
            HttpConfiguration config = new HttpConfiguration();

            MaintenanceService.WebApiConfig.Register(config);

            appBuilder.UseWebApi(config);
        }
    }
}
