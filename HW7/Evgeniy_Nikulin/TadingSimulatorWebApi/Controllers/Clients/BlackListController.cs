namespace TadingSimulatorWebApi.Controllers.Clients
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using TradingSimulator.Core.Interfaces;

    [ApiController]
    [Route("clients/[controller]")]
    public class BlackListController : ControllerBase
    {
        private readonly ITraderService traderService;
        public BlackListController(ITraderService traderService) =>
            this.traderService = traderService;

        // GET: /clients/blacklist
        [HttpGet]
        public string Get()
        {
            return JsonConvert.SerializeObject(traderService.BlackList);
        }
    }
}