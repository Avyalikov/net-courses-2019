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
        private readonly IBalanceRepository balanceRepository;
        private readonly IValidator validator;

        public ClientService(IClientRepository clientsRepository, IBalanceRepository balanceRepository, IValidator validator)
        {
            this.clientsRepository = clientsRepository;
            this.balanceRepository = balanceRepository;
            this.validator = validator;
        }

        public int AddClient(ClientRegistrationInfo clientInfo)
        {
            if (validator.ValidateClientInfo(clientInfo))
            {
                var clientToAdd = new ClientEntity()
                {
                    ClientFirstName = clientInfo.FirstName,
                    ClientLastName = clientInfo.LastName,
                    PhoneNumber = clientInfo.PhoneNumber
                };

                clientsRepository.Add(clientToAdd);
                clientsRepository.SaveChanges();

                var balance = new BalanceEntity()
                {
                    ClientID = clientToAdd.ClientID,
                    ClientBalance = 0
                };

                balanceRepository.Add(balance);
                balanceRepository.SaveChanges();
                return clientToAdd.ClientID;
            }
            return -1;
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

        public IQueryable<ClientEntity> GetAllClients()
        {
            return clientsRepository.LoadAllClients();
        }
    }
}
