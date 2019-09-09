namespace TradingApp.Core.Services
{
    using Interfaces;
    using DTO;
    using Models;
    using Repos;
    using System.Collections.Generic;
    using System.Linq;

    public class ClientService : IClientService
    {
        private readonly IClientRepository clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        public void RegisterClient(ClientRegistrationData clientData)
        {
            var newClient = new Client()
            {
                Name = clientData.ClientName,
                PhoneNumber = clientData.ClientPhone,
                Balance = clientData.Balance
            };

            clientRepository.Insert(newClient);
            clientRepository.SaveChanges();
        }

        public void ChangeBalance(int clientID, decimal cost)
        {
            var client = clientRepository.GetClientByID(clientID);

            if (client is null)
            {
                return;
            }

            client.Balance += cost;
            clientRepository.SaveChanges();
        }

        public string GetClientName(int clientID)
        {
            return clientRepository.GetAllClients().Where(x => x.ClientID == clientID).FirstOrDefault().Name;
        }

        public int GetClientIDByName(string name)
        {
            return clientRepository.GetAllClients().Where(x => x.Name == name).FirstOrDefault().ClientID;
        }

        public IEnumerable<Client> GetAllClients()
        {
            return clientRepository.GetAllClients();
        }
    }
}