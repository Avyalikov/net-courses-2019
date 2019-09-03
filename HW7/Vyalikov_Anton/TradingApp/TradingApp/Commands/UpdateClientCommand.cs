namespace TradingApp.Commands
{
    using Interfaces;
    using Services;
    using TradingApp.Core.Models;
    using Newtonsoft.Json;
    using System.Net.Http;
    using System.Text;

    class UpdateClientCommand : ICommand
    {
        private readonly IInputOutputModule ioModule;
        private readonly IHTTPRequestService httpRequestService;

        public UpdateClientCommand(IInputOutputModule ioModule, IHTTPRequestService httpRequestService)
        {
            this.ioModule = ioModule;
            this.httpRequestService = httpRequestService;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.UpdateClient)
            {
                return true;
            }
            return false;
        }

        public void Execute()
        {
            string url = "http://localhost/clients/update";
            int clientID = 0;
            decimal newBalance = 0;

            this.ioModule.WriteOutput("Write client ID, that you want to update:");
            while (true)
            {
                if (int.TryParse(this.ioModule.ReadInput(), out clientID))
                {
                    break;
                }
                this.ioModule.WriteOutput("Please enter valid client ID.");
            }

            this.ioModule.WriteOutput("Write new name:");
            string newName = this.ioModule.ReadInput();

            this.ioModule.WriteOutput("Write new phone number:");
            string newPhoneNumber = this.ioModule.ReadInput();

            this.ioModule.WriteOutput("Write new balance:");
            while (true)
            {
                if (decimal.TryParse(this.ioModule.ReadInput(), out newBalance))
                {
                    break;
                }
                this.ioModule.WriteOutput("Please enter valid balance.");
            }

            var clientToUpdate = new Client
            {
                ClientID = clientID,
                Name = newName,
                PhoneNumber = newPhoneNumber,
                Balance = newBalance
            };

            HttpContent contentString = new StringContent(JsonConvert.SerializeObject(clientToUpdate), Encoding.UTF8, "application/json");
            var result = this.httpRequestService.PostAsync(url, contentString);
            this.ioModule.WriteOutput(result.ToString());
        }
    }
}
