using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Core.Repositories
{
    public interface IOperationHistoryRepository : IDBTable
    {
        IEnumerable<OperationHistoryEntity> LoadOperationsWithClientByID(int ID);
        OperationHistoryEntity LoadOperationByID(int ID);
        void Add(OperationHistoryEntity operation);
    }
}
