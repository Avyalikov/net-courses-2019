using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Core.Repositories
{
    public interface ITransactionHistoryRepository : IDBTable
    {
        IQueryable<TransactionHistoryEntity> LoadOperationsWithClientByID(int ID);
        TransactionHistoryEntity LoadOperationByID(int ID);
        void Add(TransactionHistoryEntity operation);
        IQueryable<TransactionHistoryEntity> LoadAllOperations();
    }
}
