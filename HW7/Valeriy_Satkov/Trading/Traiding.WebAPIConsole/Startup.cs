namespace Traiding.WebAPIConsole
{
    using Owin;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Http;

    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "ActionsAPI",
                routeTemplate: "{controller}/{action}"
            );
            config.Routes.MapHttpRoute(
                name: "ReportsAPI",
                routeTemplate: "{controller}"
            );

            config.DependencyResolver = new StructureMapDependencyResolver();

            appBuilder.UseWebApi(config);
        }
    }
}
