namespace TadingSimulatorWebApi.Controllers.Clients
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using TradingSimulator.Core.Interfaces;

    [ApiController]
    [Route("clients/[controller]")]
    public class ListController : ControllerBase
    {
        private readonly ITraderService traderService;
        public ListController(ITraderService traderService) =>
            this.traderService = traderService;

        // GET: /clients/list
        [HttpGet]
        public string Get()
        {
            return JsonConvert.SerializeObject(traderService.TradersList);
        }
    }
}