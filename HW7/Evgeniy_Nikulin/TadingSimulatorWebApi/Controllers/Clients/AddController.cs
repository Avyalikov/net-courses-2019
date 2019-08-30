namespace TadingSimulatorWebApi.Controllers.Clients
{
    using Microsoft.AspNetCore.Mvc;
    using TradingSimulator.Core.Interfaces;

    [ApiController]
    [Route("clients/[controller]")]
    public class AddController : ControllerBase
    {
        private readonly ITraderService traderService;
        public AddController(ITraderService traderService) =>
            this.traderService = traderService;

        // POST: /clients/add?name=_&surname=_&phone=_&money=_
        [HttpPost]
        public string Post(string name, string surname, string phone, string money)
        {
            return traderService.AddTrader(name, surname, phone, money);
        }
    }
}