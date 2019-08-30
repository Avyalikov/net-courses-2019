namespace TadingSimulatorWebApi.Controllers.Clients
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using TradingSimulator.Core.Interfaces;

    [ApiController]
    [Route("clients/[controller]")]
    public class GreenListController : ControllerBase
    {
        private readonly ITraderService traderService;
        public GreenListController(ITraderService traderService) =>
            this.traderService = traderService;

        // GET: /clients/greenlist
        [HttpGet]
        public string Get()
        {
            return JsonConvert.SerializeObject(traderService.GreenList);
        }
    }
}