// <copyright file="Client.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace Trading.Core.Services

{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Trading.Core.Model;
    using Trading.Core.Repositories;
    using Trading.Core.DTO;

    /// <summary>
    /// Client description
    /// </summary>
    public class ClientService
    {
       
        private  ITableRepository<Client> tableRepository;

        public ClientService( ITableRepository<Client> tableRepository)
        {
            this.tableRepository = tableRepository;
        }


        public void AddClientToDB(ClientInfo args)
        {
            var clientToAdd = new Client()
            {
                FirstName = args.FirstName,
                LastName = args.LastName,
                Phone = args.Phone,
                Balance = args.Balance,
                RegistrationDateTime = DateTime.Now
            };

           if (this.tableRepository.ContainsDTO(clientToAdd)) {
                throw new ArgumentException("This client exists. Can't continue");
            };
            this.tableRepository.Add(clientToAdd);
            this.tableRepository.SaveChanges();
        }


      public Client GetEntityByID(int clientId)
        {

           if (!tableRepository.ContainsByPK(clientId))
            {
                throw new ArgumentException("Client  doesn't exist");
            }
            return (Client)tableRepository.FindByPK(clientId);
        }



       public void EditClientBalance(int clientId, decimal sumToAdd)
        {
            var client = this.GetEntityByID(clientId);
            client.Balance += sumToAdd;
            tableRepository.SaveChanges();
        }


        public IEnumerable<Client> GetClientsFromOrangeZone()
        {

            return this.tableRepository.Get(o => o.Balance == 0);
        }

        public IEnumerable<Client> GetClientsFromBlackZone()
        {

            return this.tableRepository.Get(o => o.Balance <= 0);
        }
    }
}