namespace TadingSimulatorWebApi.Controllers.Clients
{
    using Microsoft.AspNetCore.Mvc;
    using TradingSimulator.Core.Interfaces;

    [ApiController]
    [Route("clients/[controller]")]
    public class CountController : ControllerBase
    {
        private readonly ITraderService traderService;
        public CountController(ITraderService traderService) =>
            this.traderService = traderService;

        // GET: /clients/count
        [HttpGet]
        public string Get()
        {
            return traderService.GetTraderCount().ToString();
        }
    }
}