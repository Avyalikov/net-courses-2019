namespace TradingAppWebAPI
{
    using Owin;
    using System.Web.Http;

    public class StartUp
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "ControllersAndActions",
                routeTemplate: "{controller}/{action}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ControllerOnly",
                routeTemplate: "{controller}");

            DependencyResolver container = new DependencyResolver();

            config.DependencyResolver = container;
            config.Formatters.JsonFormatter.SerializerSettings
            .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            appBuilder.Use<Logger>();
            appBuilder.UseWebApi(config);
        }
    }
}