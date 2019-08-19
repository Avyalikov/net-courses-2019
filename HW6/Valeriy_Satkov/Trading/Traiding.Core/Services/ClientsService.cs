using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traiding.Core.Models;
using Traiding.Core.Repositories;

namespace Traiding.Core.Services
{
    public class ClientsService
    {
        private readonly IClientTableRepository clientTableRepository;

        public ClientsService(IClientTableRepository clientTableRepository)
        {
            this.clientTableRepository = clientTableRepository;
        }

        public int RegisterNewClient(ClientRegistrationInfo args)
        {
            var entityToAdd = new ClientEntity()
            {
                CreatedAt = DateTime.Now,
                LastName = args.LastName,
                FirstName = args.FirstName,
                PhoneNumber = args.PhoneNumber,
                Status = args.Status
            };

            if (this.clientTableRepository.Contains(entityToAdd))
            {
                throw new ArgumentException("This client has been registered. Can't continue.");
            }

            this.clientTableRepository.Add(entityToAdd);

            this.clientTableRepository.SaveChanges();

            return entityToAdd.Id;
        }

        public ClientEntity GetClient(int clientId)
        {
            if (!this.clientTableRepository.ContainsById(clientId))
            {
                throw new ArgumentException("Can't get client by this Id. May it has not been registered.");
            }

            return this.clientTableRepository.Get(clientId);
        }
    }
}
