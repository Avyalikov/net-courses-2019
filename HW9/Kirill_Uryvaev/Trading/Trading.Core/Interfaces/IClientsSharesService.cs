using System.Collections.Generic;
using System.Linq;
using Trading.Core;
using Trading.Core.DataTransferObjects;

namespace Trading.Core
{
    public interface IClientsSharesService
    {
        void UpdateShares(ClientsSharesEntity clientsSharesInfo);
        void AddShares(ClientsSharesInfo clientsSharesInfo);
        void RemoveShares(ClientsSharesEntity clientsSharesInfo);
        IQueryable<ClientsSharesEntity> GetAllClientsShares();
    }
}