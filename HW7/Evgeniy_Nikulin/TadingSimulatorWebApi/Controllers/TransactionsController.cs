namespace TadingSimulatorWebApi.Controllers.Transaction
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using TradingSimulator.Core.Interfaces;

    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService transactionService;
        public TransactionsController(ITransactionService transactionService) =>
            this.transactionService = transactionService;

        // GET: transactions?clientId=_&top=_
        [HttpGet]
        public string Get(int clientId, int top)
        {
            return JsonConvert.SerializeObject(transactionService.GetTransactions(clientId, top));
        }

        // GET: /transaction/list
        [HttpGet("list")]
        public string Get()
        {
            return JsonConvert.SerializeObject(transactionService.GetTransactions());
        }
    }
}