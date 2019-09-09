namespace TradingApiClient.Services.CommandStrategy
{
    using System.Net.Http;
    using System.Text;
    using Newtonsoft.Json;
    using TradingApiClient.Devices;
    using TradingSoftware.Core.Models;

    public class AddClientsStrategy : ICommandStrategy
    {
        private readonly IOutputDevice outputDevice;
        private readonly IHttpRequestManager httpRequestManager;
        private readonly IInputDevice inputDevice;

        public AddClientsStrategy(
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
            if (command == Command.clientsAdd)
            {
                return true;
            }

            return false;
        }

        public void Execute()
        {
            string url = "http://localhost/clients/add";

            this.outputDevice.WriteLine("Write name:");
            string name = this.inputDevice.ReadLine();

            this.outputDevice.WriteLine("Write PhoneNumber:");
            string phoneNumber = this.inputDevice.ReadLine();

            this.outputDevice.WriteLine("Write Balance:");
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