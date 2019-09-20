namespace WebApi.ODataControllers
{
    using Microsoft.AspNet.OData;
    using Microsoft.AspNetCore.Mvc;
    using TradingApp.Shared;

    [Route("odata/Traders")]
    public class OTradersController : ODataController
    {
        private TradingAppDbContext dbContext;
        public OTradersController(TradingAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {

            return Ok(dbContext.Traders);
        }
    }
}
