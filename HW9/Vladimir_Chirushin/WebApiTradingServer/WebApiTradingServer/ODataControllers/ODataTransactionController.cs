using Microsoft.AspNet.OData;
using System.Linq;
using TradingSoftware.Core.Dto;
using TradingSoftware.Core.Services;

namespace WebApiTradingServer.ODataControllers
{
    public class ODataTransactionController : ODataController
    {
        private readonly ITransactionManager transactionManager;
        public ODataTransactionController(ITransactionManager transactionManager)
        {
            this.transactionManager = transactionManager;
        }
        
        [EnableQuery]
        public IQueryable<TransactionsFullData> Get()
        {
            return transactionManager.GetAllTransactions().AsQueryable();
        }

        [EnableQuery]
        public SingleResult<TransactionsFullData> Get([FromODataUri] int key)
        {
            IQueryable<TransactionsFullData> result = transactionManager.GetAllTransactions().Where(t => t.TransactionID == key).AsQueryable();
            return SingleResult.Create<TransactionsFullData>(result);
        }
    }
}