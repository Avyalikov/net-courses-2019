

namespace WebApiTradingServer.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TradingSoftware.Core.Models;
    using TradingSoftware.Core.Repositories;

    public class ClientRepository : IClientRepository
    {
        public bool Insert(Client client)
        {
            using (var db = new TradingContext())
            {
                db.Clients.Add(client);
                db.SaveChanges();
                return true;
            }
        }

        public string GetClientName(int clientID)
        {
            using (var db = new TradingContext())
            {
                return db.Clients.Where(c => c.ClientID == clientID).FirstOrDefault().Name;
            }
        }

        public int GetClientID(string clientName)
        {
            using (var db = new TradingContext())
            {
                return db.Clients.Where(c => c.Name == clientName).FirstOrDefault().ClientID;
            }
        }

        public int GetNumberOfClients()
        {
            using (var db = new TradingContext())
            {
                return db.Clients.Count();
            }
        }

        public decimal GetClientBalance(int clientID)
        {
            using (var db = new TradingContext())
            {
                return db.Clients.Where(c => c.ClientID == clientID).FirstOrDefault().Balance;
            }
        }

        public IEnumerable<Client> GetAllClients()
        {
            using (var db = new TradingContext())
            {
                return db.Clients.OrderBy(c => c.Name).AsEnumerable<Client>().ToList();
            }
        }

        public bool IsClientExist(int clientID)
        {
            using (var db = new TradingContext())
            {
                return db.Clients.Where(c => c.ClientID == clientID).FirstOrDefault() != null;
            }
        }

        public bool IsClientExist(string clientName)
        {
            using (var db = new TradingContext())
            {
                return db.Clients.Where(c => c.Name == clientName).FirstOrDefault() != null;
            }
        }

        public bool ChangeBalance(int clientID, decimal accountGain)
        {
            using (var db = new TradingContext())
            {
                Client client = db.Clients.Where(c => c.ClientID == clientID).FirstOrDefault();
                client.Balance += accountGain;
                db.SaveChanges();
                return true;
            }
        }

        public void Remove(Client client)
        {
            using (var db = new TradingContext())
            {
                var clientToDelete = db.Clients.Where(c => c.Name == client.Name).FirstOrDefault();
                db.Clients.Remove(clientToDelete);
                db.SaveChanges();
            }
        }

        public void ClientUpdate(Client clientData)
        {
            using (var db = new TradingContext())
            {
                var clientToUpdate = db.Clients.Where(c => c.ClientID == clientData.ClientID).FirstOrDefault();
                clientToUpdate.Name = clientData.Name;
                clientToUpdate.PhoneNumber = clientData.PhoneNumber;
                clientToUpdate.Balance = clientData.Balance;
                db.SaveChanges();
            }
        }
    }
}
