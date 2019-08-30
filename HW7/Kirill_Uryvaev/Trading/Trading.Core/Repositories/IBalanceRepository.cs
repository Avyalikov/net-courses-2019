using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Core.Repositories
{
    public interface IBalanceRepository: IDBTable
    {
        BalanceEntity LoadClientByID(int ID);
        void Add(BalanceEntity balance);
        void Update(BalanceEntity balance);
    }
}
