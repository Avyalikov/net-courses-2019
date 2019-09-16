using System.Collections.Generic;
using System.Linq;
using Trading.Core.DataTransferObjects;

namespace Trading.Core
{
    public interface IClientService
    {
        IQueryable<ClientEntity> GetAllClients();
        int AddClient(ClientRegistrationInfo clientInfo);
        void UpdateClient(ClientEntity client);
        void RemoveClient(int ID);
    }
}