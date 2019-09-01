using System.Collections.Generic;
using Trading.Core.Dto;
using Trading.Core.Models;

namespace Trading.Core.Services
{
    public interface IClientsService
    {
        ICollection<ClientEntity> GetAllInBlackZone();
        ICollection<ClientEntity> GetAllInOrangeZone();
        void PutMoneyToBalance(ArgumentsForPutMoneyToBalance args);
        int RegisterNew(ClientRegistrationInfo args);
        void UpdateInfo(int clientId, ClientRegistrationInfo infoToUpdate);
        ICollection<ClientEntity> GetTop(int top, int page);
        void RemoveById(int clientId);
        string GetBalance(int clientId);
        IDictionary<SharesEntity, int> GetClientSharesById(int clientId);
    }
}