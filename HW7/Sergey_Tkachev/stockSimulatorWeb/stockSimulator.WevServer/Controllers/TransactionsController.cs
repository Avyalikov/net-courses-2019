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
    public class TransactionsController : ControllerBase
    {
        private readonly TransactionHistoryService transactionHistoryService;

        public TransactionsController(TransactionHistoryService transactionHistoryService)
        {
            this.transactionHistoryService = transactionHistoryService;
        }

        [HttpGet]
        [Route("")]
        // transactions
        public ActionResult<IEnumerable<HistoryEntity>> GetClientsTransactions(int clientId, int top)
        {
            try
            {
                var transactionOfClient = this.transactionHistoryService.GetClientsTransactions(clientId, top);
                return Ok(transactionOfClient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
