namespace WebApi.ODataControllers
{
    using Microsoft.AspNet.OData;
    using Microsoft.AspNetCore.Mvc;
    using TradingApp.Shared;

    [Route("odata/Shares")]
    public class OSharesController : ODataController
    {
        private TradingAppDbContext dbContext;
        public OSharesController(TradingAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {

            return Ok(dbContext.Shares);
        }
    }
}
