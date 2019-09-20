namespace TradingSimulator.OData
{
    using DataBase.Model;
    using Microsoft.AspNet.OData.Builder;
    using Microsoft.AspNet.OData.Extensions;
    using System.Linq;
    using System.Web.Http;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            ODataModelBuilder builder = new ODataConventionModelBuilder();
            config.Count().Filter().OrderBy().Expand().Select().MaxTop(10);

            builder.EntitySet<TraderEntity>("Traders");
            builder.EntitySet<ShareEntity>("Shares");
            builder.EntitySet<TransactionEntity>("Transactions");
            builder.EntitySet<CardEntity>("Cards");

            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: "odata",
                model: builder.GetEdmModel());
        }
    }
}
