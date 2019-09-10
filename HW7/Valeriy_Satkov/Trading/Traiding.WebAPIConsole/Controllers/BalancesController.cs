namespace Traiding.WebAPIConsole.Controllers
{
    using StructureMap;
    using System.Web.Http;
    using Traiding.Core.Services;

    public class BalancesController : ApiController
    {
        private readonly SalesService salesService;

        public BalancesController(SalesService salesService)
        {
            this.salesService = salesService;
        }

        //public BalancesController()
        //{
        //    this.salesService = new Container(new Models.DependencyInjection.TraidingRegistry()).GetInstance<SalesService>();
        //}

        // GET /balances?clientId=...  returns client status (orange, bloack, green)
        public string Get([FromUri]int clientId)
        {
            var balanceAmount = this.salesService.SearchBalanceByClientId(clientId).Amount;
            string color;

            if (balanceAmount > 0)
            {
                color = "green";
                return color;
            }
            else if (balanceAmount == 0)
            {
                color = "orange";
                return color;
            }
            else
            {
                color = "black";
                return color;
            }
        }
    }
}
