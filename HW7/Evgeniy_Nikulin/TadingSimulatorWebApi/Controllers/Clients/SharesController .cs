namespace TadingSimulatorWebApi.Controllers.Clients
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using TradingSimulator.Core.Interfaces;

    [ApiController]
    [Route("clients/[controller]")]
    public class SharesController : ControllerBase
    {
        private readonly ITraderService traderService;
        public SharesController(ITraderService traderService) =>
            this.traderService = traderService;

        // GET: /clients/shares?clientId=
        [HttpGet]
        public string Get(int clientId)
        {
            return JsonConvert.SerializeObject(traderService.GetShareList(clientId));
        }
    }
}