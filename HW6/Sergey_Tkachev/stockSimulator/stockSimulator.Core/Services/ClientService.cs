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

            return entityToAdd.ID;
        }

        public ClientEntity GetClient(int clientId)
        {
            if (!this.clientTableRepository.ContainsById(clientId))
            {
                throw new ArgumentException("Can't get client by this ID. May be it has not been registered.");
            }

            return this.clientTableRepository.Get(clientId);
        }

        public IEnumerable<ClientEntity> GetClientsWithPositiveBalance()
        {
            var clients = clientTableRepository.GetClients();
            var result = clients.Where(c => c.AccountBalance > 0).ToList();
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
