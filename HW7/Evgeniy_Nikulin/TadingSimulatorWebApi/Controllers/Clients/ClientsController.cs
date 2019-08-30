namespace TadingSimulatorWebApi.Controllers.Clients
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using TradingSimulator.Core.Interfaces;

    [ApiController]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly ITraderService traderService;
        public ClientsController(ITraderService traderService) =>
            this.traderService = traderService;

        // GET: clients?top=_&page=_
        [HttpGet]
        public string Get(int top, int page)
        {
            return JsonConvert.SerializeObject(traderService.GetTradersPerPage(top, page));
        }
    }
}