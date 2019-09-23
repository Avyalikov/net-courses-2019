using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HW7.Core;
using HW7.Core.Dto;
using HW7.Core.Models;
using HW7.Core.Services;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HW7.Server.ODataControllers
{
    public class DealController : ODataController
    {
        private readonly TransactionsService transactionsService;

        public DealController(TransactionsService transactionsService)
        {
            this.transactionsService = transactionsService;
        }

        //Adds transaction
        [Route("odata/deal/make")]
        public async Task<ActionResult<string>> Post([FromBody]TransactionToAdd transaction)
        {
            var newTransaction = transactionsService.PerformNewDeal(transaction);

            if (newTransaction == null)
            {
                return new ActionResult<string>("Cannot complete deal");
            }

            return new ActionResult<string>("New deal added");
        }
    }
}