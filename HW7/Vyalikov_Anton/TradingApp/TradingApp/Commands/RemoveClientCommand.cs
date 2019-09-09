namespace TradingApp.Commands
{
    using Interfaces;
    using Services;
    using TradingApp.Core.Models;
    using Newtonsoft.Json;
    using System.Net.Http;
    using System.Text;

    class RemoveClientCommand : ICommand
    {
        private readonly IInputOutputModule ioModule;
        private readonly IHTTPRequestService httpRequestService;

        public RemoveClientCommand(IInputOutputModule ioModule, IHTTPRequestService httpRequestService)
        {
            this.ioModule = ioModule;
            this.httpRequestService = httpRequestService;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.RemoveClient)
            {
                return true;
            }
            return false;
        }

        public void Execute()
        {
            string url = "http://localhost/clients/remove";
            int clientID = 0;

            this.ioModule.WriteOutput("Write client ID, that you want to remove:");
            while (true)
            {
                if (int.TryParse(this.ioModule.ReadInput(), out clientID))
                {
                    break;
                }
                this.ioModule.WriteOutput("Please enter valid client ID.");
            }

            var clientToDelete = new Client
            {
                ClientID = clientID
            };

            HttpContent contentString = new StringContent(JsonConvert.SerializeObject(clientToDelete), Encoding.UTF8, "application/json");
            var result = this.httpRequestService.PostAsync(url, contentString);
            this.ioModule.WriteOutput(result.ToString());
        }
    }
}
