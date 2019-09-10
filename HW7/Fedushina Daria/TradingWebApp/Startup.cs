using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using System.Web.Http;
using Owin;

[assembly: OwinStartup(typeof(TradingWebApp.Startup))]

namespace TradingWebApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            //config.Routes.MapHttpRoute(
            //    name: "ControllerOnly",
            //    routeTemplate: "{controller}");

            StructureMapDependencyResolver container = new StructureMapDependencyResolver();

            config.DependencyResolver = container;
            config.Formatters.JsonFormatter.SerializerSettings
            .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //appBuilder.Use<Logger>();
            app.UseWebApi(config);
        }
       /* public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            var config = new HttpConfiguration();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            app.UseWebApi(config);
        }*/
    }
}
