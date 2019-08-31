using System;
using System.Collections.Generic;
using System.Linq;
using Trading.Core.Dto;
using Trading.Core.Models;
using Trading.Core.Repositories;

namespace Trading.Core.Services
{
    public class ClientsService : IClientsService
    {
        private readonly IClientTableRepository clientTableRepository;

        public ClientsService(IClientTableRepository clientTableRepository)
        {
            this.clientTableRepository = clientTableRepository;
        }
        public int RegisterNewClient(ClientRegistrationInfo args)
        {
            var entityToAdd = (new ClientEntity()
            {
                RegistationDateTime = DateTime.Now,
                Name = args.Name,
                Phone = args.Phone,
                Balance = 0M
            });
            if (clientTableRepository.Contains(entityToAdd))
            {
                throw new ArgumentException("This client has been already registered");
            }
            clientTableRepository.Add(entityToAdd);
            clientTableRepository.SaveChanges();
            return entityToAdd.Id;
        }

        public void PutMoneyToBalance(ArgumentsForPutMoneyToBalance args)
        {
            if (!clientTableRepository.ContainsById(args.ClientId))
            {
                throw new ArgumentException($"Client with Id {args.ClientId} doesn't exist");
            }

            ClientEntity clientToChangeBalance = clientTableRepository.GetById(args.ClientId);
            clientToChangeBalance.Balance += args.AmountToPut;
            clientTableRepository.Change(clientToChangeBalance);
            clientTableRepository.SaveChanges();
        }

        public ICollection<ClientEntity> GetClientsInOrangeZone()
        {
            return clientTableRepository.GetClientsInOrangeZone();
        }

        public ICollection<ClientEntity> GetClientsInBlackZone()
        {
            return clientTableRepository.GetClientsInBlackZone();
        }
    }
}
