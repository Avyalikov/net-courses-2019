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
        public string Get(int clientId)
        {
            decimal traderBalance;
            try
            {
                traderBalance = tradersService.GetBalanceById(clientId);
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
            string zone = traderBalance == 0 ? "Orange zone" : traderBalance > 0 ? "green zone" : "black zone";
            return string.Concat("Client balance = " + traderBalance.ToString() + " zone = " + zone);
        }
    }
}