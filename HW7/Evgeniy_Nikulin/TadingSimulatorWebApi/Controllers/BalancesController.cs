namespace TadingSimulatorWebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TradingSimulator.Core.Interfaces;

    [ApiController]
    [Route("[controller]")]
    public class BalancesController : ControllerBase
    {
        private readonly ITraderService traderService;

        public BalancesController(ITraderService traderService) =>
            this.traderService = traderService;

        // GET: /balances?clientId=
        [HttpGet]
        public string Get(int clientId)
        {
            return traderService.GetTraderStatus(clientId);
        }
    }
}