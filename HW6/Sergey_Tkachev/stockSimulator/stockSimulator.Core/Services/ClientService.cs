namespace stockSimulator.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using stockSimulator.Core.DTO;
    using stockSimulator.Core.Models;
    using stockSimulator.Core.Repositories;

    public class ClientService
    {
        private readonly IClientTableRepository clientTableRepository;

        /// <summary>
        /// Creates an Instance of ClientService class.
        /// </summary>
        /// <param name="clientTableRepository">Inctance of implementing IClientTableRepository interface.</param>
        public ClientService(IClientTableRepository clientTableRepository)
        {
            this.clientTableRepository = clientTableRepository;
        }

        /// <summary>
        /// Adds new Client into Database.
        /// </summary>
        /// <param name="args">New client data.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns client instance by its ID.
        /// </summary>
        /// <param name="clientId">Clients's id.</param>
        /// <returns></returns>
        public ClientEntity GetClient(int clientId)
        {
            if (!this.clientTableRepository.ContainsById(clientId))
            {
                throw new ArgumentException($"Can't get client by {clientId} ID. May be it has not been registered.");
            }

            return this.clientTableRepository.Get(clientId);
        }

        /// <summary>
        /// Returns all clients from Database.
        /// </summary>
        /// <returns></returns>
        public IQueryable<ClientEntity> GetClients()
        {
            return this.clientTableRepository.GetClients();
        }

        /// <summary>
        /// Return all clients from database with positive balance.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ClientEntity> GetClientsWithPositiveBalance()
        {
            var clients = this.clientTableRepository.GetClients();
            var result = clients.Where(c => c.AccountBalance > 0).ToList();
            return result;
        }

        /// <summary>
        /// Return all clients from database with zero balance.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ClientEntity> GetClientsWithZeroBalance()
        {
            var clients = this.clientTableRepository.GetClients();
            var result = clients.Where(c => c.AccountBalance == 0).ToList();
            return result;
        }

        /// <summary>
        /// Return all clients from database with negative balance.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ClientEntity> GetClientsWithNegativeBalance()
        {
            var clients = this.clientTableRepository.GetClients();
            var result = clients.Where(c => c.AccountBalance < 0).ToList();
            return result;
        }
    }
}
