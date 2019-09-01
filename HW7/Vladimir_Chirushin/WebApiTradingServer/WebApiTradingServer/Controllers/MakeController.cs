namespace WebApiTradingServer.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TradingSoftware.Core.Dto;
    using TradingSoftware.Core.Services;

    [Route("deal/[controller]")]
    [ApiController]
    public class MakeController : ControllerBase
    {
        private readonly ITransactionManager transactionManager;

        public MakeController(ITransactionManager transactionManager)
        {
            this.transactionManager = transactionManager;
        }

        // POST api/make?sellerID=...&buyerID=...&shareID=...&shareAmount=...
        [HttpPost]
        public ActionResult<string> Post([FromBody] TransactionsMakeData transaction)
        {
            if (this.transactionManager.Make(transaction))
            {
                return Ok("Transaction completed");
            }
            return BadRequest("Transactions failed");
        }
    }
}