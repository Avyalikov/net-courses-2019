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

        public ClientsSharesService(IClientsSharesRepository clientsSharesRepository)
        {
            this.clientsSharesRepository = clientsSharesRepository;
        }

        public int ChangeClientsSharesAmount(ClientsSharesInfo clientsSharesInfo)
        {
            var clientSharesToChange = clientsSharesRepository.LoadClientsSharesByID(clientsSharesInfo);
            if (clientSharesToChange !=null)
            {
                clientSharesToChange.Amount += clientsSharesInfo.Amount;
            }
            else
            {
                clientSharesToChange = new ClientsSharesEntity()
                {
                    ShareID = clientsSharesInfo.ShareID,
                    ClientID = clientsSharesInfo.ClientID,
                    Amount = clientsSharesInfo.Amount,
                };
                clientsSharesRepository.Add(clientSharesToChange);
            }

            clientsSharesRepository.SaveChanges();
            return (int)clientSharesToChange.Amount;
        }
    }
}
