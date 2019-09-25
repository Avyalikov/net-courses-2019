using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TradingSimulator.Core.Models;
using TradingSimulatorConsoleApp;
using TradingSimulatorConsoleApp.Data;


namespace TradingSimulatorWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        TradingSimulatorDbContext db = new TradingSimulatorDbContext();

        [HttpPost] // https://localhost:44397/api/transaction
        public IActionResult Post()
        {
            try
            {
                TradingSimulation tradingSimulation = new TradingSimulation(db);
                tradingSimulation.RunTradingSimulation();
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}