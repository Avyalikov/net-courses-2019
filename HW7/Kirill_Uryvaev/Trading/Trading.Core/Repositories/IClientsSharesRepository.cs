using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core.DataTransferObjects;

namespace Trading.Core.Repositories
{
    public interface IClientsSharesRepository: IDBTable
    {
        void Add(ClientsSharesEntity clientsShares);
        ClientsSharesEntity LoadClientsSharesByID(ClientsSharesInfo clientsShares);
    }
}
