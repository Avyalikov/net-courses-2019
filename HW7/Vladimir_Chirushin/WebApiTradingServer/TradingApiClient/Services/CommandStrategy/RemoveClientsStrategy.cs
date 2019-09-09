namespace TradingApiClient.Services.CommandStrategy
{
    using System.Net.Http;
    using System.Text;
    using Newtonsoft.Json;
    using TradingApiClient.Devices;
    using TradingSoftware.Core.Models;

    public class RemoveClientsStrategy : ICommandStrategy
    {
        private readonly IOutputDevice outputDevice;
        private readonly IInputDevice inputDevice;
        private readonly IHttpRequestManager httpRequestManager;


        public RemoveClientsStrategy(
            IOutputDevice outputDevice,
            IHttpRequestManager httpRequestManager,
            IInputDevice inputDevice)
        {
            this.inputDevice = inputDevice;
            this.outputDevice = outputDevice;
            this.httpRequestManager = httpRequestManager;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.clientsRemove)
            {
                return true;
            }

            return false;
        }

        public void Execute()
        {
            this.outputDevice.WriteLine("Write client ID you want to remove:");
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

            var clientToDelete = new Client { ClientID = clientID };
            HttpContent contentString = new StringContent(JsonConvert.SerializeObject(clientToDelete), Encoding.UTF8, "application/json");

            string url = "http://localhost/clients/remove";
            var result = this.httpRequestManager.PostAsync(url, contentString);
            this.outputDevice.WriteLine(result.ToString());
        }
    }
}
