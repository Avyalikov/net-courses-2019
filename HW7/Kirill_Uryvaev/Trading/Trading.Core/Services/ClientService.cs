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

        public int AddClient(ClientRegistrationInfo clientInfo)
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

        public void UpdateClient(ClientEntity client)
        {
            clientsRepository.Update(client);
            clientsRepository.SaveChanges();
        }

        public void RemoveClient(int ID)
        {
            var client = clientsRepository.LoadClientByID(ID);
            if (client ==null)
            {
                return;
            }
            clientsRepository.Remove(ID);
            clientsRepository.SaveChanges();
        }

        public IEnumerable<ClientEntity> GetAllClients()
        {
            return clientsRepository.LoadAllClients();
        }

        public IEnumerable<ClientEntity> GetClientsFromGreenZone()
        {
            return clientsRepository.LoadAllClients().Where(x => x.ClientBalance.ClientBalance > 0);
        }

        public IEnumerable<ClientEntity> GetClientsFromOrangeZone()
        {
            return clientsRepository.LoadAllClients().Where(x=>x.ClientBalance.ClientBalance==0);
        }

        public IEnumerable<ClientEntity> GetClientsFromBlackZone()
        {
            return clientsRepository.LoadAllClients().Where(x => x.ClientBalance.ClientBalance < 0);
        }
    }
}
