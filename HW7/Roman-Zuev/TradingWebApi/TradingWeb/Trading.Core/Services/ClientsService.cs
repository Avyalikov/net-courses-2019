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
        private readonly ISharesTableRepository sharesTableRepository;

        public ClientsService(IClientTableRepository clientTableRepository, ISharesTableRepository sharesTableRepository)
        {
            this.clientTableRepository = clientTableRepository;
            this.sharesTableRepository = sharesTableRepository;
        }
        public int RegisterNew(ClientRegistrationInfo args)
        {
            if (args.Name.Length < 2)
            {
                throw new ArgumentException("Wrong Data");
            }

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

        public ICollection<ClientEntity> GetAllInOrangeZone()
        {
            return clientTableRepository.GetAllInOrangeZone();
        }

        public ICollection<ClientEntity> GetAllInBlackZone()
        {
            return clientTableRepository.GetAllInBlackZone();
        }

        public void UpdateInfo(int clientId, ClientRegistrationInfo infoToUpdate)
        {
            if (infoToUpdate.Name.Length < 2)
            {
                throw new ArgumentException("Wrong data");
            }
            if (!clientTableRepository.ContainsById(clientId))
            {
                throw new ArgumentException($"Client with Id {clientId} doesn't exist");
            }

            ClientEntity clientToChangeBalance = clientTableRepository.GetById(clientId);
            clientToChangeBalance.Name = infoToUpdate.Name;
            clientToChangeBalance.Phone = infoToUpdate.Phone;
            clientTableRepository.Change(clientToChangeBalance);
            clientTableRepository.SaveChanges();
        }

        public ICollection<ClientEntity> GetTop(int top, int page)
        {
            if (clientTableRepository.Count < (page - 1) * top)
            {
                throw new ArgumentException("Empty page");
            }
            var clients = clientTableRepository.GetTop(page * top).ToList();
            clients.RemoveRange(0, (page - 1) * top);
            return clients;
        }

        public void RemoveById(int clientId)
        {
            if (!clientTableRepository.ContainsById(clientId))
            {
                throw new ArgumentException($"Client with Id {clientId} doesn't exist");
            }
            ClientEntity client = clientTableRepository.GetById(clientId);
            clientTableRepository.Remove(client);
            clientTableRepository.SaveChanges();
        }

        public string GetBalance(int clientId)
        {
            if (!clientTableRepository.ContainsById(clientId))
            {
                throw new ArgumentException($"Client with Id {clientId} doesn't exist");
            }
            ClientEntity client = clientTableRepository.GetById(clientId);
            return string.Format($"Balance: {client.Balance}, Zone: " + (client.Balance > 0 ? "Green" : client.Balance < 0 ? "Black" : "Orange"));
        }

        public IDictionary<SharesEntity, int> GetClientSharesById(int clientId)
        {
            if (!clientTableRepository.ContainsById(clientId))
            {
                throw new ArgumentException($"Client with Id {clientId} doesn't exist");
            }

            ClientEntity client = clientTableRepository.GetById(clientId);
            if (client.Portfolio.Count < 1)
            {
                throw new ArgumentException("Client have no shares left");
            }

            Dictionary<SharesEntity, int> sharesWithQuantity = new Dictionary<SharesEntity, int>();
            foreach (var item in client.Portfolio)
            {
                sharesWithQuantity.Add(sharesTableRepository.GetById(item.Shares.Id), item.Quantity);
            }

            return sharesWithQuantity;
        }
    }
}
