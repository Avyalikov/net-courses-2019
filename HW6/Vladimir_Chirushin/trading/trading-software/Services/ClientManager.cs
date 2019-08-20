namespace trading_software
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public class ClientManager : IClientManager
    {
        private readonly IOutputDevice outputDevice;
        private readonly IInputDevice inputDevice;
        private readonly ITableDrawer tableDrawer;
        private readonly IClientRepository clientRepository;

        private Random random = new Random();

        public ClientManager(
            IInputDevice inputDevice,
            IOutputDevice outputDevice,
            ITableDrawer tableDrawer,
            IClientRepository clientRepository)
        {
            this.inputDevice = inputDevice;
            this.outputDevice = outputDevice;
            this.tableDrawer = tableDrawer;
            this.clientRepository = clientRepository;
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

        public int SelectRandomID()
        {
            int numberOfClients = clientRepository.GetNumberOfClients();
            int clientId = random.Next(1, numberOfClients);
            return clientId;
        }

        public void AddClient(Client client)
        {
            if (!clientRepository.IsClientExist(client.Name))
            {
                clientRepository.Add(client);
            }
            else
            {
                outputDevice.WriteLine("Client name collision!");
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
            outputDevice.WriteLine("Clients in 'Black' zone:");
            IEnumerable<Client> query = clientRepository.GetBlackClients();
            tableDrawer.Show(query);
        }

        public void ShowOrangeZone()
        {
            outputDevice.WriteLine("Clients in 'Orange' zone:");
            IEnumerable<Client> query = clientRepository.GetOrangeClients();
            tableDrawer.Show(query);
        }

        public void ReduceAssetsRandomClient()
        {
            const decimal inflationDrop = -10000;
            int clientId = SelectRandomID();
            ChangeBalance(clientId, inflationDrop);
        }

        public void ReadAllClients()
        {
            IEnumerable<Client> query = clientRepository.GetAllClients().AsEnumerable<Client>();
            tableDrawer.Show(query);
        }

        public void ChangeBalance(int ClientID, decimal accountGain)
        {

            if (clientRepository.IsClientExist(ClientID))
            {
                clientRepository.ChangeBalance(ClientID, accountGain);
            }
            else
            {
                outputDevice.WriteLine("There is no such client!");
            }
        }

        public void BankruptRandomClient()
        {
            int clientId = SelectRandomID();
            clientRepository.BankruptClient(clientId);
        }
    }
}