using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TradingSimulator.Core.Models;
using TradingSimulatorConsoleApp.Data;


namespace TradingSimulatorWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        TradingSimulatorDbContext db = new TradingSimulatorDbContext();

        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Start";
        }
    }
}
