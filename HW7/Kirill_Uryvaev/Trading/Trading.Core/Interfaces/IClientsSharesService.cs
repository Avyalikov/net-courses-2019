using System.Collections.Generic;
using Trading.Core;
using Trading.Core.DataTransferObjects;

namespace Trading.Core
{
    public interface IClientsSharesService
    {
        void UpdateShares(ClientsSharesEntity clientsSharesInfo);
        void AddShares(ClientsSharesInfo clientsSharesInfo);
        void RemoveShares(ClientsSharesEntity clientsSharesInfo);
        IEnumerable<ClientsSharesEntity> GetAllClientsShares();
    }
}