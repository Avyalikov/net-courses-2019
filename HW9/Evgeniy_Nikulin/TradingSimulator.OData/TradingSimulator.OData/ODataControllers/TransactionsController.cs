namespace TradingSimulator.OData.Controllers
{
    using Microsoft.AspNet.OData;
    using System.Linq;
    using TradingSimulator.DataBase;
    using TradingSimulator.DataBase.Model;

    public class TransactionsController : ODataController
    {
        TradingDbContext db = new TradingDbContext();

        [EnableQuery]
        public IQueryable<TransactionEntity> Get()
        {
            return db.Transactions;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}