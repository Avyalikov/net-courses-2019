using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using TradingSimulator.Core.Interfaces;


namespace WebApiServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
        private readonly ITraderService tradersService;

        public BalanceController(ITraderService tradersService)
        {
            this.tradersService = tradersService;
        }

        // GET /balance?clientId=_
        [HttpGet]
        public ActionResult Get(int clientId)
        {
            decimal traderBalance;
            try
            {
                traderBalance = tradersService.GetBalanceById(clientId);
            }
            catch(ArgumentException)
            {
                return StatusCode(400, $"Can`t find this id {clientId}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            string zone = traderBalance == 0 ? "Orange zone" : traderBalance > 0 ? "green zone" : "black zone";
            return Ok(string.Concat("Client balance = " + traderBalance.ToString() + " zone = " + zone));
        }
    }
}