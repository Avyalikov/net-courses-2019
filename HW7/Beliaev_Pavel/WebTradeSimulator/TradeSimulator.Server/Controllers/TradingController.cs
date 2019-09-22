using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TradeSimulator.Core.Dto;
using TradeSimulator.Core.Services;

namespace TradeSimulator.Server.Controllers
{
    [Route("deal")]
    [ApiController]
    public class TradingController
    {
        private readonly TradingService tradingService;
        public TradingController(TradingService tradingService)
        {
            this.tradingService = tradingService;
        }

        [Route("make")]
        [HttpPost]
        public ActionResult<HttpResponseMessage> MakeATrade([FromBody] TransactionInfo transactionInfo) 
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                this.tradingService.MakeATrade(transactionInfo);
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return response;
            }
            catch (Exception)
            {
                response.StatusCode = System.Net.HttpStatusCode.Conflict;
                return response;
            }
        }
    }
}
