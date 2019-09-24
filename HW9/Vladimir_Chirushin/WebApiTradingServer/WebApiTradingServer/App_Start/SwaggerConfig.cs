using System.Web.Http;
using WebActivatorEx;
using WebApiTradingServer;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace WebApiTradingServer
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "WebApiTradingServer");
                })
            .EnableSwaggerUi(c =>
            {
            });
        }
    }
}
