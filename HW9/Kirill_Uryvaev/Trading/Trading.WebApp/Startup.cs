using Owin;
using System.Web.Http;
using Trading.Core;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.OData.Edm;

namespace Trading.WebApp
{
    public class Startup
    {
        // This code configures Web API contained in the class Startup, which is additionally specified as the type parameter in WebApplication.Start
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();

            var model = GetODataModel();
            config.Count().Filter().OrderBy().Expand().Select().MaxTop(null);
            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: "odata",
                model: model);

            config.Routes.MapHttpRoute(
            name: "ControllersAndActions",
            routeTemplate: "{controller}/{action}",
            defaults: new { id = RouteParameter.Optional });
            config.Routes.MapHttpRoute(
            name: "ControllerOnly",
            routeTemplate: "{controller}");

            SwaggerConfig.Register(config);

            StructureMapDependencyResolver container = new StructureMapDependencyResolver();
            config.DependencyResolver = container;
            config.Formatters.JsonFormatter.SerializerSettings
            .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            config.Formatters.Remove(config.Formatters.XmlFormatter);



            appBuilder.Use<Logger>();
            appBuilder.UseWebApi(config);
        }

        private IEdmModel GetODataModel()
        {
            ODataModelBuilder builder = new ODataConventionModelBuilder();

            var clients = builder.EntitySet<ClientEntity>("OClients");
            clients.EntityType.HasKey(entity => entity.ClientID);
            var shares = builder.EntitySet<ClientsSharesEntity>("OShares");
            shares.EntityType.HasKey(entity => new { entity.ClientID, entity.ShareID });
            var balances = builder.EntitySet<BalanceEntity>("OBalances");
            balances.EntityType.HasKey(entity => entity.ClientID);
            var transactions = builder.EntitySet<TransactionHistoryEntity>("OTransactions");
            transactions.EntityType.HasKey(entity => entity.TransactionID);
            return builder.GetEdmModel();
        }
    }
}
