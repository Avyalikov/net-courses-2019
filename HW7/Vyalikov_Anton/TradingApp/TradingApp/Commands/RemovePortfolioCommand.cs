namespace TradingApp.Commands
{
    using Interfaces;
    using Services;
    using TradingApp.Core.Models;
    using Newtonsoft.Json;
    using System.Net.Http;
    using System.Text;

    class RemovePortfolioCommand : ICommand
    {
        private readonly IInputOutputModule ioModule;
        private readonly IHTTPRequestService httpRequestService;

        public RemovePortfolioCommand(IInputOutputModule ioModule, IHTTPRequestService httpRequestService)
        {
            this.ioModule = ioModule;
            this.httpRequestService = httpRequestService;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.RemovePortfolio)
            {
                return true;
            }

            return false;
        }

        public void Execute()
        {
            string url = "http://localhost/clients/remove";
            int clientID = 0;
            int shareID = 0;

            this.ioModule.WriteOutput("Write client ID, that you want to remove:");
            while (true)
            {
                if (int.TryParse(this.ioModule.ReadInput(), out clientID))
                {
                    break;
                }
                this.ioModule.WriteOutput("Please enter valid client ID.");
            }

            this.ioModule.WriteOutput("Write share ID, that you want to remove:");
            while (true)
            {
                if (int.TryParse(this.ioModule.ReadInput(), out shareID))
                {
                    break;
                }
                this.ioModule.WriteOutput("Please enter valid share ID.");
            }

            var portfolioToDelete = new ClientPortfolio
            {
                ClientID = clientID,
                ShareID = shareID
            };

            HttpContent contentString = new StringContent(JsonConvert.SerializeObject(portfolioToDelete), Encoding.UTF8, "application/json");
            var result = this.httpRequestService.PostAsync(url, contentString);
            this.ioModule.WriteOutput(result.ToString());
        }
    }
}
