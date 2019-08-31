using System.Collections.Generic;
using Trading.Core.DataTransferObjects;

namespace Trading.Core
{
    public interface IClientService
    {
        IEnumerable<ClientEntity> GetAllClients();
        int AddClient(ClientRegistrationInfo clientInfo);
        void UpdateClient(ClientEntity client);
        void RemoveClient(int ID);
    }
}