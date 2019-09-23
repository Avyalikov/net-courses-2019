using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HW7.Core;
using HW7.Core.Models;
using HW7.Core.Services;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HW7.Server.ODataControllers
{
    
    public class TransactionsController : ODataController
    {
        private readonly IContextProvider contextProvider;
        private readonly TransactionsService transactionsService;

        public TransactionsController(TransactionsService transactionsService, IContextProvider contextProvider)
        {
            this.contextProvider = contextProvider;
            this.transactionsService = transactionsService;
        }

        //Get all transactions
        [EnableQuery]
        [Route("odata/transactions")]
        public IEnumerable<Transaction> Get()
        {
            return contextProvider.Transactions;
        }
    }
}