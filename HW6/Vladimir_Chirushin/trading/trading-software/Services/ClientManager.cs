namespace trading_software
{
    using System.Linq;
    public class ClientManager : IClientManager
    {
        private readonly IOutputDevice outputDevice;
        private readonly IInputDevice inputDevice;
        private readonly ITableDrawer tableDrawer;
        public ClientManager(IInputDevice inputDevice, IOutputDevice outputDevice, ITableDrawer tableDrawer)
        {
            this.inputDevice = inputDevice;
            this.outputDevice = outputDevice;
            this.tableDrawer = tableDrawer;
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
                var client = new Client
                {
                    Name = name,
                    PhoneNumber = phoneNumber,
                    Balance = balance
                };
                db.Clients.Add(client);
                db.SaveChanges();
            }
        }


        public void ReadAllClients()
        {
            using (var db = new TradingContext())
            {
                IQueryable<Client> query = db.Clients.AsQueryable<Client>();
                tableDrawer.Show(query);
            }
            /*
            using (var db = new TradingContext())
            {
                var query = from b in db.Clients
                            orderby b.Name
                            select new { b.Name, b.PhoneNumber, b.Balance };
                int i = 0;
                foreach (var iteam in query)
                {
                    i++;
                    outputDevice.WriteLine($"{i}).================================================");
                    outputDevice.WriteLine($"We have client with name: {iteam.Name}.");
                    outputDevice.WriteLine($"We can call him by number: {iteam.PhoneNumber}.");
                    outputDevice.WriteLine($"He has enormous balance size of: ${iteam.Balance}");
                }
            }*/
        }
    }
}
