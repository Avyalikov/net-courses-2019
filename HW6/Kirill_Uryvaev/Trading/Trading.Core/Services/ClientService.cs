using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core.DataTransferObjects;
using Trading.Core.Repositories;

namespace Trading.Core.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository clientsRepository;

        public ClientService(IClientRepository clientsRepository)
        {
            this.clientsRepository = clientsRepository;
        }

        public int RegisterClient(ClientRegistrationInfo clientInfo)
        {
            var clientToAdd = new ClientEntity()
            {
                ClientFirstName = clientInfo.FirstName,
                ClientLastName = clientInfo.LastName,
                PhoneNumber = clientInfo.PhoneNumber
            };

            clientsRepository.Add(clientToAdd);
            clientsRepository.SaveChanges();

            return clientToAdd.ClientID;
        }

        public void ChangeMoney(int id, decimal amount)
        {
            var client = clientsRepository.LoadClientByID(id);
            if (client==null)
            {
                return;
            }
            client.ClientBalance += amount;
            clientsRepository.SaveChanges();
        }

        public IEnumerable<ClientEntity> GetAllClients()
        {
            return clientsRepository.LoadAllClients();
        }

        public IEnumerable<ClientEntity> GetClientsFromOrangeZone()
        {
            return clientsRepository.LoadAllClients().Where(x=>x.ClientBalance==0);
        }

        public IEnumerable<ClientEntity> GetClientsFromBlackZone()
        {
            return clientsRepository.LoadAllClients().Where(x => x.ClientBalance < 0);
        }
    }
}
