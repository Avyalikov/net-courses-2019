namespace TradingAppWebAPI.Controllers
{
    using TradingApp.Core.Interfaces;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class BalanceController : ApiController
    {
        private readonly IClientService clientService;

        public BalanceController(IClientService clientService)
        {
            this.clientService = clientService;
        }

        [HttpGet]
        public HttpResponseMessage Get(int clientID)
        {
            this.clientService.GetClientBalanceStatus(clientID);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
