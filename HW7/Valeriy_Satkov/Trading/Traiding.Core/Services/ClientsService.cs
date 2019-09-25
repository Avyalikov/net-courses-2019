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
            Validation(args);

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

        public void UpdateClientData(int clientId, ClientRegistrationInfo args)
        {
            Validation(args);

            var client = GetClient(clientId);

            client.LastName = args.LastName;
            client.FirstName = args.FirstName;
            client.PhoneNumber = args.PhoneNumber;            

            this.tableRepository.Update(client);

            this.tableRepository.SaveChanges();
        }

        public ClientEntity GetClient(int clientId)
        {
            ContainsById(clientId);

            return this.tableRepository.Get(clientId);
        }

        public void RemoveClient(int clientId)
        {
            ContainsById(clientId);

            this.tableRepository.Deactivate(clientId);
            this.tableRepository.SaveChanges();
        }

        public void Validation(ClientRegistrationInfo args)
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
        }

        public void ContainsById(int clientId)
        {
            if (!this.tableRepository.ContainsById(clientId))
            {
                throw new ArgumentException("Can't find client by this Id. May it has not been registered.");
            }
        }
    }
}
