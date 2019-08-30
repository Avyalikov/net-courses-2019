namespace TadingSimulatorWebApi.Controllers.Clients
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using TradingSimulator.Core.Interfaces;

    [ApiController]
    [Route("clients/[controller]")]
    public class OrangeListController : ControllerBase
    {
        private readonly ITraderService traderService;
        public OrangeListController(ITraderService traderService) =>
            this.traderService = traderService;

        // GET: /clients/orangelist
        [HttpGet]
        public string Get()
        {
            return JsonConvert.SerializeObject(traderService.OrangeList);
        }
    }
}