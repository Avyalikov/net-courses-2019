using System.Collections.Generic;
using Trading.Core.DataTransferObjects;

namespace Trading.Core
{
    public interface IClientService
    {
        void ChangeMoney(int id, decimal amount);
        IEnumerable<ClientEntity> GetAllClients();
        IEnumerable<ClientEntity> GetClientsFromBlackZone();
        IEnumerable<ClientEntity> GetClientsFromOrangeZone();
        int RegisterClient(ClientRegistrationInfo clientInfo);
    }
}