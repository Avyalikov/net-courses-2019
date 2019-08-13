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
            using (var db = new TradingContext())
            {
                var client = new Client
                {
                    Name = name,
                    PhoneNumber = phoneNumber,
                    Balance = balance
                };
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
        private bool IsExist(Client client)
        {
            using (var db = new TradingContext())
            {
                return db.Clients.Where(c => c.Name == client.Name).FirstOrDefault() != null;
            }
        }
        public Client SelectRandom()
        {
            using (var db = new TradingContext())
            {
                int numberOfClients = db.Clients.Count();
                int clientId = random.Next(1, numberOfClients);
                var randomClient = db.Clients
                               .FirstOrDefault<Client>(c => c.ClientID == clientId);
                return randomClient;
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
        public void AddNewClient()
        {
            using (var db = new TradingContext())
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
        }


        public void ReadAllClients()
        {
            using (var db = new TradingContext())
            {
                IQueryable<Client> query = db.Clients.OrderBy(c=>c.Name).AsQueryable<Client>();
                tableDrawer.Show(query);
            }
        }
    }
}
