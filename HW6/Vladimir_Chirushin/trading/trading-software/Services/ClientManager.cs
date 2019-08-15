namespace trading_software
{
    using System;
    using System.Linq;
    public class ClientManager : IClientManager
    {
        private readonly IOutputDevice outputDevice;
        private readonly IInputDevice inputDevice;
        private readonly ITableDrawer tableDrawer;
        Random random = new Random();

        public ClientManager(IInputDevice inputDevice, IOutputDevice outputDevice, ITableDrawer tableDrawer)
        {
            this.inputDevice = inputDevice;
            this.outputDevice = outputDevice;
            this.tableDrawer = tableDrawer;
        }
        public void AddClient(string name, string phoneNumber, decimal balance)
        {
                var client = new Client
                {
                    Name = name,
                    PhoneNumber = phoneNumber,
                    Balance = balance
                };
                AddClient(client);
        }

        private bool IsExist(Client client)
        {
            using (var db = new TradingContext())
            {
                return db.Clients.Where(c => c.Name == client.Name).FirstOrDefault() != null;
            }
        }
        public int SelectRandomID()
        {
            using (var db = new TradingContext())
            {
                int numberOfClients = db.Clients.Count();
                int clientId = random.Next(1, numberOfClients);
                return clientId;
            }
        }

        public void AddClient(Client client)
        {
            using (var db = new TradingContext())
            {
                if (!IsExist(client))
                {
                    db.Clients.Add(client);
                    db.SaveChanges();
                }
                else
                {
                    outputDevice.WriteLine("Client name collision!");
                }
            }
        }
        public void ManualAddClient()
        {
                outputDevice.WriteLine("Write name:");
                string name = inputDevice.ReadLine();

                outputDevice.WriteLine("Write PhoneNumber:");
                string phoneNumber = inputDevice.ReadLine();

                outputDevice.WriteLine("Write Balance:");
                decimal balance = 0;
                while (true)
                {
                    if (decimal.TryParse(inputDevice.ReadLine(), out balance))
                        break;
                    else
                        outputDevice.WriteLine("Please enter valid balance");
                }
                AddClient(name, phoneNumber, balance);
        }

        public void ShowBlackClients()
        {
            using (var db = new TradingContext())
            {
                outputDevice.WriteLine("Clients in 'Black' zone: ");
                IQueryable<Client> query = db.Clients.Where(c=>c.Balance<0)
                    .OrderBy(c => c.Name).AsQueryable<Client>();
                tableDrawer.Show(query);
            }
        }
        public void ShowOrangeZone()
        {
            using (var db = new TradingContext())
            {
                outputDevice.WriteLine("Clients in 'Orange' zone: ");
                IQueryable<Client> query = db.Clients.Where(c => c.Balance == 0)
                    .OrderBy(c => c.Name).AsQueryable<Client>();
                tableDrawer.Show(query);
            }
        }

        public void ReduceAssetsRandomClient()
        {
            using (var db = new TradingContext())
            {
                int numberOfClients = db.Clients.Count();
                int clientId = random.Next(1, numberOfClients);
                Client client = db.Clients.Where(c => c.ClientID == clientId).FirstOrDefault();
                client.Balance -= 100000;
                db.SaveChanges();
            }
        }

        public void ReadAllClients()
        {
            using (var db = new TradingContext())
            {
                IQueryable<Client> query = db.Clients.OrderBy(c=>c.Name).AsQueryable<Client>();
                tableDrawer.Show(query);
            }
        }
        
        public void ChangeBalance(int ClientID, decimal accountGain)
        {
            using (var db = new TradingContext())
            {
                Client client = db.Clients.Where(c => c.ClientID == ClientID).FirstOrDefault();
                if(client != null)
                {
                    client.Balance += accountGain;
                    db.SaveChanges();
                }
                else
                {
                    outputDevice.WriteLine("There is no such client!");
                }
            }
        }

        public void BankruptRandomClient()
        {
            using (var db = new TradingContext())
            {
                int numberOfClients = db.Clients.Count();
                int clientId = random.Next(1, numberOfClients);
                Client client = db.Clients.Where(c=>c.ClientID == clientId).FirstOrDefault();
                client.Balance = 0;
                db.SaveChanges();
            }
        }
    }
}
