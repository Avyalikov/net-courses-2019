using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core.Repositories;
using Trading.Core.DataTransferObjects;

namespace Trading.Core.Services
{
    public class ClientsSharesService : IClientsSharesService
    {
        private readonly IClientsSharesRepository clientsSharesRepository;
        private readonly IValidator validator;

        public ClientsSharesService(IClientsSharesRepository clientsSharesRepository, IValidator validator)
        {
            this.clientsSharesRepository = clientsSharesRepository;
            this.validator = validator;
        }

        public void RemoveShares(ClientsSharesEntity clientsSharesInfo)
        {
            var clientSharesToRemove = clientsSharesRepository.LoadClientsSharesByID(clientsSharesInfo);
            if (clientSharesToRemove == null)
            {
                return;
            }
            clientsSharesRepository.Remove(clientSharesToRemove);
            clientsSharesRepository.SaveChanges();
        }

        public void AddShares(ClientsSharesInfo clientsSharesInfo)
        {
            if (validator.ValidateShareToClient(clientsSharesInfo))
            {
                var clientsShares = new ClientsSharesEntity()
                {
                    ClientID = clientsSharesInfo.ClientID,
                    ShareID = clientsSharesInfo.ShareID,
                    Amount = clientsSharesInfo.Amount,
                    CostOfOneShare = clientsSharesInfo.CostOfOneShare
                };
                var clientSharesToAdd = clientsSharesRepository.LoadClientsSharesByID(clientsShares);
                if (clientSharesToAdd != null)
                {
                    return;
                }
                clientsSharesRepository.Add(clientsShares);
                clientsSharesRepository.SaveChanges();
            }
        }

        public void UpdateShares(ClientsSharesEntity clientsSharesInfo)
        {
            clientsSharesRepository.Update(clientsSharesInfo);

            clientsSharesRepository.SaveChanges();
        }

        public IEnumerable<ClientsSharesEntity> GetAllClientsShares()
        {
            return clientsSharesRepository.GetAllShares();
        }
    }
}
