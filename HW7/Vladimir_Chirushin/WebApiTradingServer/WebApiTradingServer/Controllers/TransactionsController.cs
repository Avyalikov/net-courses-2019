namespace WebApiTradingServer.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using TradingSoftware.Core.Dto;
    using TradingSoftware.Core.Services;

    [Route("[controller]")]
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
        public ActionResult<IEnumerable<TransactionsFullData>> Get(int clientID, int top)
        { 
            return Ok(this.transactionManager.GetTransactionWithClient(clientID).Take(top));
        }
    }
}
