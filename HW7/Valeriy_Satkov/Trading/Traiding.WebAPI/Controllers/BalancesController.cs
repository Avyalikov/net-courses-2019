namespace Traiding.WebAPI.Controllers
{
    using StructureMap;
    using System.Web.Http;
    using Traiding.Core.Services;

    public class BalancesController : ApiController
    {
        private readonly SalesService salesService;

        public BalancesController()
        {
            this.salesService = new Container(new Models.DependencyInjection.TraidingRegistry()).GetInstance<SalesService>();
        }

        // GET /balances?clientId=...  returns client status (orange, bloack, green)
        public string Get([FromUri]int clientId)
        {
            var balanceAmount = this.salesService.SearchBalanceByClientId(clientId).Amount;

            if (balanceAmount > 0)
            {
                return "green";
            }
            else if (balanceAmount == 0)
            {
                return "orange";
            }
            else
            {
                return "black";
            }
        }
    }
}
