namespace TadingSimulatorWebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TradingSimulator.Core.Interfaces;

    [ApiController]
    [Route("deal/[controller]")]
    public class MakeController : ControllerBase
    {
        private readonly ITransactionService transactionService;
        public MakeController(ITransactionService transactionService) =>
            this.transactionService = transactionService;

        // POST: /deal/make?sellerId=_&buyerId=_&shareName=_&quantity=_
        [HttpPost]
        public void Post(int sellerId, int buyerId, string shareName, int quantity)
        {
            var transaction = transactionService.MakeDeal(sellerId, buyerId, shareName, quantity);
            transactionService.Save(transaction);
        }
    }
}