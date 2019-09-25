namespace WebApi.ODataControllers
{
    using Microsoft.AspNet.OData;
    using Microsoft.AspNetCore.Mvc;
    using TradingApp.Shared;

    [Route("odata/Transactions")]
    public class OTransactionsController : ODataController
    {
        private TradingAppDbContext dbContext;
        public OTransactionsController(TradingAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {

            return Ok(dbContext.Transactions);
        }
    }
}
