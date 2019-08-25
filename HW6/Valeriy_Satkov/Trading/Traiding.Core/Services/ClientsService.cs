namespace Traiding.Core.Services
{
    using System;
    using Traiding.Core.Dto;
    using Traiding.Core.Models;
    using Traiding.Core.Repositories;

    public class ClientsService
    {
        private readonly IClientTableRepository tableRepository;

        public ClientsService(IClientTableRepository clientTableRepository)
        {
            this.tableRepository = clientTableRepository;
        }

        public int RegisterNewClient(ClientRegistrationInfo args)
        {
            if (args.LastName.Length < 2 
                || args.LastName.Length > 20
                || args.FirstName.Length < 2
                || args.FirstName.Length > 20
                || args.PhoneNumber.Length < 2
                || args.PhoneNumber.Length > 20)
            {
                throw new ArgumentException("Invalid ClientRegistrationInfo. Can't continue.");
            }

            var entityToAdd = new ClientEntity()
            {
                CreatedAt = DateTime.Now,
                LastName = args.LastName,
                FirstName = args.FirstName,
                PhoneNumber = args.PhoneNumber,
                Status = true
            };

            if (this.tableRepository.Contains(entityToAdd))
            {
                throw new ArgumentException("This client has been registered. Can't continue.");
            }

            this.tableRepository.Add(entityToAdd);

            this.tableRepository.SaveChanges();

            return entityToAdd.Id;
        }

        public ClientEntity GetClient(int clientId)
        {
            if (!this.tableRepository.ContainsById(clientId))
            {
                throw new ArgumentException("Can't get client by this Id. May it has not been registered.");
            }

            return this.tableRepository.Get(clientId);
        }
    }
}
