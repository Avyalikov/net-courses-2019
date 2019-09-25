using System.Web.Http;
using WebActivatorEx;
using Trading.WebApp;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Trading.WebApp
{
    public class SwaggerConfig
    {

        public static void Register(HttpConfiguration config)
        {
            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "Trading.WebApp");
            })
            .EnableSwaggerUi(c =>
            {
            });
        }
    }
}
