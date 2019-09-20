namespace WebApi.ODataControllers
{
    using Microsoft.AspNet.OData;
    using Microsoft.AspNetCore.Mvc;
    using TradingApp.Shared;

    [Route("odata/Companies")]
    public class OCompaniesController : ODataController
    {
        private TradingAppDbContext dbContext;
        public OCompaniesController(TradingAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {

            return Ok(dbContext.Companies);
        }
    }
}
