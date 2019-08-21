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
       
        private  ITableRepository tableRepository;

        public ClientService( ITableRepository tableRepository)
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

           if (this.tableRepository.Contains(clientToAdd)) {
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
            return (Client)tableRepository.Find(clientId);
        }



       public void EditClientBalance(int clientId, decimal sumToAdd)
        {
            var client = this.GetEntityByID(clientId);
            client.Balance += sumToAdd;
            tableRepository.SaveChanges();
        }

      
       public Client GetRandomClient()
        {
            Random random = new Random();
            int clientsAmount = this.tableRepository.Count();
           if (clientsAmount == 0)
            {
                throw new NullReferenceException("There are no clients to select from");
            }
                int number = random.Next(1,clientsAmount);
             Client   client = (Client)tableRepository.GetElementAt(number);
           
            return client;
        }
    }
}