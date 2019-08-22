using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Core.Repositories
{
    public interface IClientsSharesRepository: IDBTable
    {
        void Add(ClientsSharesEntity clientsShares);
        bool IsExists(out ClientsSharesEntity clientsShares);
    }
}
