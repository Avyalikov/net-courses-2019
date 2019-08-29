using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TradingSoftware.Core.Dto;
using TradingSoftware.Core.Services;

namespace WebApiTradingServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionManager transactionManager;

        public TransactionsController(ITransactionManager transactionManager)
        {
            this.transactionManager = transactionManager;
        }

        // GET api/transactions?clientID=...
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get(int clientID)
        { 
            IEnumerable<TransactionsFullData> transactions = transactionManager.GetTransactionWithClient(clientID);
            List<string> answer = new List<string>();
            foreach (var transaction in transactions)
            {
                answer.Add($"Seller:{transaction.SellerName} Buyer: {transaction.BuyerName} ShareID: {transaction.ShareType} SharePrice: {transaction.SharePrice}$ Amount: {transaction.ShareAmount}");
            }
            return answer.ToArray();
        }

        // POST api/values
        [HttpPost]
        public ActionResult<string> Post([FromBody] ValuesRequestData value)
        {
            return new ActionResult<string>("testResponse");
        }
    }
}
