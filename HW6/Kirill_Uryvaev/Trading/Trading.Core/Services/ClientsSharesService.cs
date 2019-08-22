using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core.Repositories;
using Trading.Core.DataTransferObjects;

namespace Trading.Core.Services
{
    public class ClientsSharesService
    {
        private readonly IClientsSharesRepository clientsSharesRepository;

        public ClientsSharesService(IClientsSharesRepository clientsSharesRepository)
        {
            this.clientsSharesRepository = clientsSharesRepository;
        }

        public int ChangeClientsSharesAmount(ClientsSharesInfo clientsSharesInfo)
        {
            var clientSharesToChange = new ClientsSharesEntity()
            {
                ShareID = clientsSharesInfo.ShareID,
                ClientID = clientsSharesInfo.ClientID,
                Amount = clientsSharesInfo.Amount,
            };

            if (clientsSharesRepository.IsExists(out clientSharesToChange))
            {
                clientSharesToChange.Amount += clientsSharesInfo.Amount;
            }
            else
            {
                clientsSharesRepository.Add(clientSharesToChange);
            }

            clientsSharesRepository.SaveChanges();
            return (int)clientSharesToChange.Amount;
        }
    }
}
