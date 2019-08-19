using stockSimulator.Core.Models;
using stockSimulator.Core.Repositories;
using System;
using System.Collections.Generic;
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

        public int RegisterNewClient(ClientReservationInfo args)
        {
            var entityToAdd = new ClientEntity()
            {
                CreateAt = DateTime.Now,
                Name = args.Name,
                Surname = args.Surname,
                PhoneNumber = args.PhoneNumber,
                AccountBalance = args.AccountBalance,
                ZoneID = 0
            };

            clientTableRepository.Add(entityToAdd);

            clientTableRepository.SaveChanges();

            return entityToAdd.ID;
        }
    }
}
