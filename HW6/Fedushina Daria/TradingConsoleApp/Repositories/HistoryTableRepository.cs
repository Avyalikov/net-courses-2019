using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingApp.Core.Models;
using TradingApp.Core.Repositories;

namespace TradingConsoleApp.Repositories
{
    class HistoryTableRepository : IHistoryTableRepository
    {
        private readonly TradingAppDbContext dbContext;

        public HistoryTableRepository(TradingAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public int Add(TransactionHistoryEntity entity)
        {
            this.dbContext.Transactions.Add(entity);
            return entity.TransactionID;
        }

        public bool Contains(int transactionId)
        {
            var entity = this.dbContext.Transactions.Find(transactionId);
            return this.dbContext.Transactions.Contains(entity);
        }

        public TransactionHistoryEntity Get(int transactionId)
        {
            return this.dbContext.Transactions.Find(transactionId);
        }

        public List<TransactionHistoryEntity> Get(DateTime dateTime)
        {
            List<TransactionHistoryEntity> arrTrans = new List<TransactionHistoryEntity>();
            foreach (TransactionHistoryEntity transaction in this.dbContext.Transactions)
            {
                if (transaction.TimeOfTransaction == dateTime) arrTrans.Add(transaction);
            }
            return arrTrans;
        }

        public List<TransactionHistoryEntity> GetAll(int userId)
        {
            List<TransactionHistoryEntity> arrTrans = new List<TransactionHistoryEntity>();
            List<BalanceEntity> arr = new List<BalanceEntity>();
            foreach (BalanceEntity entity in this.dbContext.Balances)
            {
                if (entity.UserID == userId)
                {
                    foreach (TransactionHistoryEntity transaction in this.dbContext.Transactions)
                    {
                        if (transaction.BuyerBalanceID == entity.BalanceID || transaction.SellerBalanceID == entity.BalanceID) arrTrans.Add(transaction);
                    }
                } ;
            }           
            
            return arrTrans;
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }
    }
}
