using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core;
using Trading.Core.Repositories;

namespace Trading.WebApp.Repositories
{
    class TransactionRepository : DBTable, ITransactionHistoryRepository
    {
        private readonly TradingDBContext dbContext;
        public TransactionRepository(TradingDBContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(TransactionHistoryEntity operation)
        {
            dbContext.Transactions.Add(operation);
        }

        public TransactionHistoryEntity LoadOperationByID(int ID)
        {
            return dbContext.Transactions.Where(x => x.TransactionID == ID).FirstOrDefault();
        }

        public IEnumerable<TransactionHistoryEntity> LoadOperationsWithClientByID(int ID)
        {
            return dbContext.Transactions.Where(x => x.BuyerClientID == ID || x.SellerClientID == ID);
        }
    }
}
