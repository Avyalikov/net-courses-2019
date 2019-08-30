using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Core.Repositories
{
    public interface IClientRepository: IDBTable
    {
        IEnumerable<ClientEntity> LoadAllClients();
        ClientEntity LoadClientByID(int ID);
        void Add(ClientEntity client);
        void Update(ClientEntity client);
        void Remove(int ID);
    }
}
