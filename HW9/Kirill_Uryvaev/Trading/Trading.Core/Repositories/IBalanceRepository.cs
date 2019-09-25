using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Core.Repositories
{
    public interface IBalanceRepository: IDBTable
    {
        IQueryable<BalanceEntity> LoadAllBalances();
        BalanceEntity LoadBalanceByID(int ID);
        void Add(BalanceEntity balance);
        void Update(BalanceEntity balance);
    }
}
