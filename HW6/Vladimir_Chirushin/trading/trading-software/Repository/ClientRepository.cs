namespace trading_software
{
    using System.Collections.Generic;
    using System.Linq;

    public class ClientRepository : IClientRepository
    {
        public bool Add(Client client)
        {
            using (var db = new TradingContext())
            {
                db.Clients.Add(client);
                db.SaveChanges();
                return true;
            }
        }

        public void BankruptClient(int ClientID)
        {
            using (var db = new TradingContext())
            {
                Client client = db.Clients.Where(c => c.ClientID == ClientID).FirstOrDefault();
                client.Balance = 0;
                db.SaveChanges();
            }
        }

        public int GetNumberOfClients()
        {
            using (var db = new TradingContext())
            {
                return db.Clients.Count();
            }
        }

        public bool ChangeBalance(int ClientID, decimal accountGain)
        {
            using (var db = new TradingContext())
            {
                Client client = db.Clients.Where(c => c.ClientID == ClientID).FirstOrDefault();
                client.Balance += accountGain;
                db.SaveChanges();
                return true;
            }
        }

        public IEnumerable<Client> GetAllClients()
        {
            using (var db = new TradingContext())
            {
                return db.Clients.OrderBy(c => c.Name).AsEnumerable<Client>().ToList(); ;
            }
        }

        public IEnumerable<Client> GetBlackClients()
        {
            using (var db = new TradingContext())
            {
                IEnumerable<Client> query = db.Clients.Where(c => c.Balance < 0)
                    .OrderBy(c => c.Name).AsEnumerable<Client>().ToList(); ;
                return query;
            }
        }

        public decimal GetClientBalance(int ClientID)
        {
            using (var db = new TradingContext())
            {
                return db.Clients.Where(c => c.ClientID == ClientID).FirstOrDefault().Balance;
            }
        }

        public int GetClientID(string ClientName)
        {
            using (var db = new TradingContext())
            {
                return db.Clients.Where(c => c.Name == ClientName).FirstOrDefault().ClientID;
            }
        }

        public string GetClientName(int ClientID)
        {
            using (var db = new TradingContext())
            {
                return db.Clients.Where(c => c.ClientID == ClientID).FirstOrDefault().Name;
            }
        }

        public IEnumerable<Client> GetOrangeClients()
        {
            using (var db = new TradingContext())
            {
                IEnumerable<Client> query = db.Clients.Where(c => c.Balance == 0)
                    .OrderBy(c => c.Name).AsEnumerable<Client>().ToList(); ;
                return query;
            }
        }

        public bool IsClientExist(int ClientID)
        {
            using (var db = new TradingContext())
            {
                return db.Clients.Where(c => c.ClientID == ClientID).FirstOrDefault() != null;
            }
        }

        public bool IsClientExist(string ClientName)
        {
            using (var db = new TradingContext())
            {
                return db.Clients.Where(c => c.Name == ClientName).FirstOrDefault() != null;
            }
        }
    }
}