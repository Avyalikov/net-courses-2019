// <copyright file="Client.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace TradingApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TradingApp.DAL;
    using TradingApp.Interfaces;
    using TradingApp.Model;

    /// <summary>
    /// Client description
    /// </summary>
    public class ClientModifier : IClientModifier
    {
        ExchangeContext db;
        public ClientModifier(ExchangeContext db)
        {
            this.db = db;
        }
        public void AddClient(string lastName, string firstName, string phone, decimal balance)
        {
            var client = new Client { LastName = lastName, FirstName = firstName, Phone = phone, Balance = balance };
            db.Clients.Add(client);
            db.SaveChanges();

        }
        public Client GetClientByID(int id)
        {
            return db.Clients.Where(i => i.ClientID == id).Select(c => c).Single();
        }
        public Client GetRandomClient()
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
        }
        public void EditClientBalanse(Order custOrder, Order salerOrder, decimal price)
        {
            db.Clients.Where(c => c.ClientID == salerOrder.ClientID).Single().Balance += (decimal)(price * salerOrder.Quantity);
            db.Clients.Where(c => c.ClientID == custOrder.ClientID).Single().Balance -= (decimal)(price * custOrder.Quantity);
            db.SaveChanges();
        }

        public IEnumerable<Client> GetClientsFromOrangeZone()
        {
            return db.Clients.Where(c => c.Balance == 0).Select(c => c);
        }

        public IEnumerable<Client> GetClientsFromBlackZone()
        {
            return db.Clients.Where(c => c.Balance < 0).Select(c => c);
        }
    }
}