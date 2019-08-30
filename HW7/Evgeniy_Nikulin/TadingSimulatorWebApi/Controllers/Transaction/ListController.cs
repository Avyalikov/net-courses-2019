namespace TadingSimulatorWebApi.Controllers.Transaction
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using TradingSimulator.Core.Interfaces;

    [ApiController]
    [Route("transaction/[controller]")]
    public class ListController : ControllerBase
    {
        private readonly ITransactionService transactionService;
        public ListController(ITransactionService transactionService) =>
            this.transactionService = transactionService;

        // GET: /transaction/list
        [HttpGet]
        public string Get()
        {
            return JsonConvert.SerializeObject(transactionService.GetTransactions());
        }
    }
}