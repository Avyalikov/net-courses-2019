using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HW7.Core;
using HW7.Core.Models;
using HW7.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HW7.Server.Controllers
{
    
    public class TransactionsController : ControllerBase
    {
        private readonly TransactionsService transactionsService;

        public TransactionsController (TransactionsService transactionsService)
        {
            this.transactionsService = transactionsService;
        }

        //Returns first N transactions involving a trader
        [Route("transactions")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> Get(int clientId, int top)
        {
            return await transactionsService.GetNumberTransactionsForTrader(clientId, top).ToListAsync();
        }

        [Route("transactions/sharequantity")]
        [HttpGet]
        public async Task<ActionResult<int>> GetShareQuantityFromPortfoio(int clientId, int shareId)
        {
            return transactionsService.GetShareQuantityFromPortfoio(clientId, shareId);
        }
    }
}