namespace TradingApiClient.Services.CommandStrategy
{
    using System.Net.Http;
    using System.Text;
    using Newtonsoft.Json;
    using TradingApiClient.Devices;
    using TradingSoftware.Core.Models;

    public class UpdateClientsStrategy : ICommandStrategy
    {
        private readonly IOutputDevice outputDevice;
        private readonly IHttpRequestManager httpRequestManager;
        private readonly IInputDevice inputDevice;

        public UpdateClientsStrategy(
            IOutputDevice outputDevice,
            IHttpRequestManager httpRequestManager,
            IInputDevice inputDevice)
        {
            this.outputDevice = outputDevice;
            this.httpRequestManager = httpRequestManager;
            this.inputDevice = inputDevice;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.clientsUpdate)
            {
                return true;
            }

            return false;
        }

        public void Execute()
        {
            string url = "http://localhost/clients/update";

            this.outputDevice.WriteLine("Write client ID you want to update:");
            int clientID = 0;
            while (true)
            {
                if (int.TryParse(this.inputDevice.ReadLine(), out clientID))
                {
                    break;
                }
                else
                {
                    this.outputDevice.WriteLine("Please enter valid client ID");
                }
            }

            this.outputDevice.WriteLine("Write new name:");
            string name = this.inputDevice.ReadLine();

            this.outputDevice.WriteLine("Write new PhoneNumber:");
            string phoneNumber = this.inputDevice.ReadLine();

            this.outputDevice.WriteLine("Write new Balance:");
            decimal balance = 0;
            while (true)
            {
                if (decimal.TryParse(this.inputDevice.ReadLine(), out balance))
                {
                    break;
                }
                else
                {
                    this.outputDevice.WriteLine("Please enter valid balance");
                }
            }

            var client = new Client
            {
                ClientID = clientID,
                Name = name,
                PhoneNumber = phoneNumber,
                Balance = balance
            };

            HttpContent contentString = new StringContent(JsonConvert.SerializeObject(client), Encoding.UTF8, "application/json");
            var result = this.httpRequestManager.PostAsync(url, contentString);
            this.outputDevice.WriteLine(result.ToString());
        }
    }
}
