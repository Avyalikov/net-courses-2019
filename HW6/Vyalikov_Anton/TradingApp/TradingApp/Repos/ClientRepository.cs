namespace TradingApp.Repos
{
    using TradingApp.Core.Models;
    using TradingApp.Core.Repos;
    using Services;
    using System.Collections.Generic;
    using System.Linq;

    class ClientRepository : DBComm, IClientRepository
    {
        private readonly DBContext dBContext;

        public ClientRepository(DBContext dBContext) : base(dBContext)
        {
            this.dBContext = dBContext;
        }

        public void Insert(Client client)
        {
            dBContext.Clients.Add(client);
        }

        public Client GetClientByID(int clientID)
        {
            return dBContext.Clients.Where(x => x.ClientID == clientID).FirstOrDefault();
        }

        public int GetClientID(string name)
        {
            return dBContext.Clients.Where(x => x.Name == name).FirstOrDefault().ClientID;
        }

        public string GetClientName(int clientID)
        {
            return dBContext.Clients.Where(x => x.ClientID == clientID).FirstOrDefault().Name;
        }

        public bool DoesClientExists(int clientID)
        {
            return dBContext.Clients.Where(x => x.ClientID == clientID).FirstOrDefault() != null;
        }

        public bool DoesClientExists(string name)
        {
            return dBContext.Clients.Where(x => x.Name == name).FirstOrDefault() != null;
        }

        public decimal GetClientBalance(int clientID)
        {
            return dBContext.Clients.Where(x => x.ClientID == clientID).FirstOrDefault().Balance;
        }

        public void ChangeBalance(int clientID, decimal money)
        {
            dBContext.Clients.Where(x => x.ClientID == clientID).FirstOrDefault().Balance += money;
            dBContext.SaveChanges();
        }

        public IEnumerable<Client> GetAllClients()
        {
            return dBContext.Clients;
        }
    }
}