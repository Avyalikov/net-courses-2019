namespace WebApiTradingServer.Controllers
{
    using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<string> Post(int sellerID, int buyerID, int shareID, int shareAmount)
        {
            if(transactionManager.Make(sellerID, buyerID, shareID, shareAmount))
            {
                return new ActionResult<string>("Success");
            }
            return new ActionResult<string>("Fail");
        }
    }
}