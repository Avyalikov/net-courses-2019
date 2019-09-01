namespace TradingSoftware.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TradingSoftware.Core.Dto;
    using TradingSoftware.Core.Models;
    using TradingSoftware.Core.Repositories;

    public class ClientManager : IClientManager
    {
        private readonly IClientRepository clientRepository;

        public ClientManager(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        public void AddClient(string name, string phoneNumber, decimal balance)
        {
            var client = new Client
            {
                Name = name,
                PhoneNumber = phoneNumber,
                Balance = balance
            };
            this.AddClient(client);
        }

        public void AddClient(Client client)
        {
            if (!this.clientRepository.IsClientExist(client.Name))
            {
                this.clientRepository.Insert(client);
            }
            else
            {
                throw new Exception($"User {client.Name} already exist in DB");
            }
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

        public string GetClientName(int clientID)
        {
            if (this.clientRepository.IsClientExist(clientID))
            {
                return this.clientRepository.GetClientName(clientID);
            }

            throw new Exception($"There is no clients with id {clientID}");
        }

        public int GetClientID(string clientName)
        {
            if (this.clientRepository.IsClientExist(clientName))
            {
                return this.clientRepository.GetClientID(clientName);
            }

            throw new Exception($"There is no clients with name {clientName}");
        }

        public bool IsClientExist(int clientID)
        {
            return this.clientRepository.IsClientExist(clientID);
        }

        public bool IsClientExist(string clientName)
        {
            return this.clientRepository.IsClientExist(clientName);
        }

        public IEnumerable<Client> GetAllClients()
        {
            return this.clientRepository.GetAllClients().AsEnumerable<Client>();
        }

        public int GetNumberOfClients()
        {
            return this.clientRepository.GetNumberOfClients();
        }

        public decimal GetClientBalance(int clientID)
        {
            if (this.clientRepository.IsClientExist(clientID))
            {
                return this.clientRepository.GetClientBalance(clientID);
            }

            throw new Exception($"There is no clients with id {clientID}");
        }

        public void ChangeBalance(int clientID, decimal accountGain)
        {
            if (this.clientRepository.IsClientExist(clientID))
            {
                this.clientRepository.ChangeBalance(clientID, accountGain);
            }
            else
            {
                throw new Exception($"There is no clients with id {clientID}");
            }
        }

        public void ClientUpdate(Client client)
        {
            this.clientRepository.ClientUpdate(client);
        }

        public void DeleteClient(Client client)
        {
            if (this.IsClientExist(client.Name))
            {
                this.clientRepository.Remove(client);
            }
        }
    }
}