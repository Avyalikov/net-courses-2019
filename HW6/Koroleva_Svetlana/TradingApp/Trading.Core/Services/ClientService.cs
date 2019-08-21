﻿// <copyright file="Client.cs" company="SKorol">
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
       // private readonly ITableRepository clientTableRepository;
        private readonly IOnePKTableRepository onePKTableRepository;
        public ClientService( IOnePKTableRepository onePKTableRepository)
        {
            //this.clientTableRepository = clientTableRepository;
            this.onePKTableRepository = onePKTableRepository;
        }


        public void AddClient(ClientInfo args)
        {
            var clientToAdd = new Client()
            {
                FirstName = args.FirstName,
                LastName = args.LastName,
                Phone = args.Phone,
                Balance = args.Balance,
                RegistrationDateTime = DateTime.Now
            };

            // if (this.onePKTableRepository.ContainsByID()) ;
            this.onePKTableRepository.Add(clientToAdd);
            this.onePKTableRepository.SaveChanges();
        }


        public Client GetEntityByID(int clientId)
        {
           if (!this.onePKTableRepository.ContainsByID(clientId))
            {
                throw new ArgumentException("Client  doesn't exist");
            }
            return (Client)onePKTableRepository.GetEntityByID(clientId);
        }



        public void EditClientBalance(int clientId, decimal sumToAdd)
        {
            var client = this.GetEntityByID(clientId);
            client.Balance += sumToAdd;
            onePKTableRepository.SaveChanges();
        }

        public IEnumerable<Client> FindClientsByRequest()
        {
            return (IEnumerable<Client>)onePKTableRepository.FindEntitiesByRequest("");
        }

       /* public Client GetRandomClient()
        {
            Random random = new Random();
            Client client = null;
            var clients = db.Clients.Select(o => o).ToList();
            if (clients != null)
            {
                int number = random.Next(clients.Count() - 1);
                client = clients[number];
            }
            return client;
        }*/
    }
}