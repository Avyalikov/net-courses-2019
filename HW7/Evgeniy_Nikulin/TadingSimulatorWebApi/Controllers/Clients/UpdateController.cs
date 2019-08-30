namespace TadingSimulatorWebApi.Controllers.Clients
{
    using Microsoft.AspNetCore.Mvc;
    using TradingSimulator.Core.Interfaces;

    [ApiController]
    [Route("clients/[controller]")]
    public class UpdateController : ControllerBase
    {
        private readonly ITraderService traderService;
        public UpdateController(ITraderService traderService) =>
            this.traderService = traderService;

        // POST: /clients/update?clientId=_&newName=_&newSurname=_&newPhone=_&newMoney=_
        [HttpPost]
        public string Post(int clientId, string newName, string newSurname, string newPhone, string newMoney)
        {
            return traderService.ChangeTrader(clientId, newName, newSurname, newPhone, newMoney);
        }
    }
}