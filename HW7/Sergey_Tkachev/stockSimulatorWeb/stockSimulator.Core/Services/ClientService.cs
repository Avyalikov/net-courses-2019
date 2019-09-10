using stockSimulator.Core.DTO;
using stockSimulator.Core.Models;
using stockSimulator.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace stockSimulator.Core.Services
{
    public class ClientService
    {
        private readonly IClientTableRepository clientTableRepository;

        public ClientService(IClientTableRepository clientTableRepository)
        {
            this.clientTableRepository = clientTableRepository;
        }

        public int RegisterNewClient(ClientRegistrationInfo args)
        {
            var entityToAdd = new ClientEntity()
            {
                CreateAt = DateTime.Now,
                Name = args.Name,
                Surname = args.Surname,
                PhoneNumber = args.PhoneNumber,
                AccountBalance = args.AccountBalance
            };

            if (this.clientTableRepository.Contains(entityToAdd))
            {
                throw new ArgumentException("This client has been registered already. Can't continue");
            }

            this.clientTableRepository.Add(entityToAdd);

            this.clientTableRepository.SaveChanges();

            entityToAdd.ID = this.clientTableRepository.GetClientId(entityToAdd);

            return entityToAdd.ID;
        }

        public string GetStateOfClient(int clientId)
        {
            string result = string.Empty;
            if (clientTableRepository.ContainsById(clientId))
            {
                decimal clientBalance = clientTableRepository.GetBalance(clientId);
                if (clientBalance > 0)
                {
                    result = $"This client belongs to Green zone.";
                }
                else if (clientBalance == 0)
                {
                    result = $"This client belongs to Orange zone.";
                }
                else if (clientBalance < 0)
                {
                    result = $"This client belongs to Black zone.";
                }
                return result;
            }
            result = "Such client doesn't exist in DataBase.";
            return result;
        }

        public IEnumerable<ClientEntity> GetClients(int amount, int page)
        {
            return clientTableRepository.GetClients(amount * page - amount, amount);
        }

        public ClientEntity GetClient(int clientId)
        {
            if (!this.clientTableRepository.ContainsById(clientId))
            {
                throw new ArgumentException($"Can't get client by {clientId} ID. May be it has not been registered.");
            }

            return this.clientTableRepository.Get(clientId);
        }

        public string UpdateClient(UpdateClientInfo updateInfo)
        {
            string result = clientTableRepository.Update(updateInfo);
            return result;
        }

        public IEnumerable<ClientEntity> GetClientsWithPositiveBalance()
        {
            var clients = clientTableRepository.GetClients();
            var result = clients.Where(c => c.AccountBalance > 0).ToList();
            return result;
        }

        public string RemoveClient(int clientId)
        {
            string result = clientTableRepository.Remove(clientId);
            return result;
        }

        public IEnumerable<ClientEntity> GetClientsWithZeroBalance()
        {
            var clients = clientTableRepository.GetClients();
            var result = clients.Where(c => c.AccountBalance == 0).ToList();
            return result;
        }

        public IEnumerable<ClientEntity> GetClientsWithNegativeBalance()
        {
            var clients = clientTableRepository.GetClients();
            var result = clients.Where(c => c.AccountBalance < 0).ToList();
            return result;
        }
    }
}
