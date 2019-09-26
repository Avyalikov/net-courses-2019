namespace WebApiTradingServer.ODataControllers
{
    using System.Linq;
    using Microsoft.AspNet.OData;
    using TradingSoftware.Core.Dto;
    using TradingSoftware.Core.Services;

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
            return this.transactionManager.GetAllTransactions().AsQueryable();
        }

        [EnableQuery]
        public SingleResult<TransactionsFullData> Get([FromODataUri] int key)
        {
            IQueryable<TransactionsFullData> result = this.transactionManager.GetAllTransactions().Where(t => t.TransactionID == key).AsQueryable();
            return SingleResult.Create<TransactionsFullData>(result);
        }
    }
}