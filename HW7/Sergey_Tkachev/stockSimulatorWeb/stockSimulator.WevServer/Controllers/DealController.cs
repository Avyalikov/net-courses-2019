using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using stockSimulator.Core.DTO;
using stockSimulator.Core.Models;
using stockSimulator.Core.Services;

namespace stockSimulator.WevServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DealController : ControllerBase
    {
        private readonly TransactionService transactionService;

        public DealController(TransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        [HttpPost]
        [Route("make")]
        public ActionResult<string> MakeDeal([FromBody]TradeInfo tradeInfo)
        {
            try
            {
                this.transactionService.Trade(tradeInfo);
                return Ok("Transaction done.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
