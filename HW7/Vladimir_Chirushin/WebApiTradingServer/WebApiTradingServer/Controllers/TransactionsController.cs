using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TradingSoftware.Core.Dto;
using TradingSoftware.Core.Services;
using System.Linq;

namespace WebApiTradingServer.Controllers
{
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
            return Ok(transactionManager.GetTransactionWithClient(clientID).Take(top));
        }
    }
}
