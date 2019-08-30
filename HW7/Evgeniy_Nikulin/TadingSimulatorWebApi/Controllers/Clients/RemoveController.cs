namespace TadingSimulatorWebApi.Controllers.Clients
{
    using Microsoft.AspNetCore.Mvc;
    using TradingSimulator.Core.Interfaces;

    [ApiController]
    [Route("clients/[controller]")]
    public class RemoveController : ControllerBase
    {
        private readonly ITraderService traderService;
        public RemoveController(ITraderService traderService) =>
            this.traderService = traderService;

        // POST: /clients/remove?clientId=_
        [HttpPost]
        public void Post(int clientId)
        {
            traderService.Remove(clientId);
        }
    }
}