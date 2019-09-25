namespace TradingApp.Core.Services
{
    using Interfaces;
    using DTO;
    using Models;
    using Repos;
    using System;
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

        public void UpdateClient(Client client)
        {
            this.clientRepository.Update(client);
        }

        public void RemoveClient(int clientID)
        {
            this.clientRepository.Remove(clientID);
        }

        public decimal GetClientBalance(int clientID)
        {
            if (this.clientRepository.DoesClientExists(clientID))
            {
                return this.clientRepository.GetClientBalance(clientID);
            }

            throw new Exception($"There is no clients with such id {clientID}");
        } 

        public ClientBalanceStatus GetClientBalanceStatus(int clientID)
        {
            ClientBalanceStatus status = new ClientBalanceStatus();

            status.Name = this.GetClientName(clientID);
            status.Balance = this.GetClientBalance(clientID);
            status.Status = "Green";

            if (status.Balance < 0)
            {
                status.Status = "Black";
            }

            if (status.Balance == 0)
            {
                status.Status = "Orange";
            }

            return status;
        }
    }
}