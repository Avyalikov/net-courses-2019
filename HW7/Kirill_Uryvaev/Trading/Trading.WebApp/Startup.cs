using Owin;
using System.Web.Http;
using StructureMap;

namespace Trading.WebApp
{
    public class Startup
    {
        // This code configures Web API contained in the class Startup, which is additionally specified as the type parameter in WebApplication.Start
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "ControllersAndActions",
                routeTemplate: "{controller}/{action}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ControllerOnly",
                routeTemplate: "{controller}");

            StructureMapDependencyResolver container = new StructureMapDependencyResolver();

            config.DependencyResolver = container;
            config.Formatters.JsonFormatter.SerializerSettings
            .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            appBuilder.Use<Logger>();
            appBuilder.UseWebApi(config);
        }
    }
}
