using System.Collections.Generic;
using Trading.Core.Dto;
using Trading.Core.Models;

namespace Trading.Core.Services
{
    public interface IClientsService
    {
        ICollection<ClientEntity> GetClientsInBlackZone();
        ICollection<ClientEntity> GetClientsInOrangeZone();
        void PutMoneyToBalance(ArgumentsForPutMoneyToBalance args);
        int RegisterNewClient(ClientRegistrationInfo args);
        void UpdateClientInfo(int clientId, ClientRegistrationInfo infoToUpdate);
        ICollection<ClientEntity> GetTop(int top, int page);
        void RemoveClientById(int clientId);
        string GetBalance(int clientId);
        IDictionary<SharesEntity, int> GetClientSharesById(int clientId);
    }
}