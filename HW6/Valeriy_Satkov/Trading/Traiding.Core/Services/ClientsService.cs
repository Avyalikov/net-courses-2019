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
                Status = true
            };

            clientTableRepository.Add(entityToAdd);

            clientTableRepository.SaveChanges();

            return entityToAdd.Id;
        }
    }
}
