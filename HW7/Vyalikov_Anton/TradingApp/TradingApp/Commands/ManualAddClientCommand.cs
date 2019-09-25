namespace TradingApp.Commands
{
    using Interfaces;
    using Services;
    using TradingApp.Core.Models;
    using Newtonsoft.Json;
    using System.Net.Http;
    using System.Text;

    class ManualAddClientCommand : ICommand
    {
        private readonly IInputOutputModule ioModule;
        private readonly IHTTPRequestService httpRequestService;

        public ManualAddClientCommand(IInputOutputModule ioModule, IHTTPRequestService httpRequestService)
        {
            this.ioModule = ioModule;
            this.httpRequestService = httpRequestService;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.ManualAddClient)
            {
                return true;
            }
            return false;
        }

        public void Execute()
        {
            string url = "http://localhost/clients/add";
            decimal balance = 0;

            this.ioModule.WriteOutput("Write name:");
            string name = this.ioModule.ReadInput();

            this.ioModule.WriteOutput("Write phone number:");
            string phoneNumber = this.ioModule.ReadInput();

            this.ioModule.WriteOutput("Write balance:");
            while (true)
            {
                if (decimal.TryParse(this.ioModule.ReadInput(), out balance))
                {
                    break;
                }
                this.ioModule.WriteOutput("Please enter valid balance.");
            }

            var client = new Client
            {
                Name = name,
                PhoneNumber = phoneNumber,
                Balance = balance
            };

            HttpContent contentString = new StringContent(JsonConvert.SerializeObject(client), Encoding.UTF8, "application/json");
            var result = this.httpRequestService.PostAsync(url, contentString);
            this.ioModule.WriteOutput(result.ToString());
        }
    }
}
