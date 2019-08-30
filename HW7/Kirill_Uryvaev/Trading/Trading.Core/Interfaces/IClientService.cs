using System.Collections.Generic;
using Trading.Core.DataTransferObjects;

namespace Trading.Core
{
    public interface IClientService
    {
        IEnumerable<ClientEntity> GetAllClients();
        IEnumerable<ClientEntity> GetClientsFromBlackZone();
        IEnumerable<ClientEntity> GetClientsFromOrangeZone();
        IEnumerable<ClientEntity> GetClientsFromGreenZone();
        int AddClient(ClientRegistrationInfo clientInfo);
        void UpdateClient(ClientEntity client);
        void RemoveClient(int ID);
    }
}