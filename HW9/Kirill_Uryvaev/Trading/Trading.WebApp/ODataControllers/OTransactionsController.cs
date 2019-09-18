using Microsoft.AspNet.OData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core;
using Trading.Core.Services;

namespace Trading.WebApp.ODataControllers
{
    public class OTransactionsController : ODataController
    {
        private readonly TransactionHistoryService transactionService;
        public OTransactionsController(TransactionHistoryService transactionService)
        {
            this.transactionService = transactionService;
        }

        [EnableQuery]
        public IQueryable<TransactionHistoryEntity> Get()
        {
            return transactionService.GetAllOperations();
        }
    }
}
