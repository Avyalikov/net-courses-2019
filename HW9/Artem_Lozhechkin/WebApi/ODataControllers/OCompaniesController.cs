using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingApp.Shared;

namespace WebApi.ODataControllers
{
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
