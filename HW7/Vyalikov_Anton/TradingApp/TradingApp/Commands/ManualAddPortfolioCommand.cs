namespace TradingApp.Commands
{
    using Interfaces;
    using Services;
    using TradingApp.Core.Models;
    using Newtonsoft.Json;
    using System.Net.Http;
    using System.Text;

    class ManualAddPortfolioCommand : ICommand
    {
        private readonly IInputOutputModule ioModule;
        private readonly IHTTPRequestService httpRequestService;

        public ManualAddPortfolioCommand(IInputOutputModule ioModule, IHTTPRequestService httpRequestService)
        {
            this.ioModule = ioModule;
            this.httpRequestService = httpRequestService;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.ManualAddPortfolio)
            {
                return true;
            }

            return false;
        }

        public void Execute()
        {
            string url = "http://localhost/clients/add";
            int shareID = 0;
            int clientID = 0;
            int amountOfShares = 0;

            this.ioModule.WriteOutput("Write share ID:");
            while (true)
            {
                if (int.TryParse(this.ioModule.ReadInput(), out shareID))
                {
                    break;
                }
                this.ioModule.WriteOutput("Please enter valid share ID.");
            }

            this.ioModule.WriteOutput("Write client ID:");
            while (true)
            {
                if (int.TryParse(this.ioModule.ReadInput(), out clientID))
                {
                    break;
                }
                this.ioModule.WriteOutput("Please enter valid client ID.");
            }

            this.ioModule.WriteOutput("Write amount of shares:");
            while (true)
            {
                if (int.TryParse(this.ioModule.ReadInput(), out amountOfShares))
                {
                    break;
                }
                this.ioModule.WriteOutput("Please enter valid amount of shares.");
            }

            ClientPortfolio clientPortfolio = new ClientPortfolio
            {
                ClientID = clientID,
                ShareID = shareID,
                AmountOfShares = amountOfShares
            };

            HttpContent contentString = new StringContent(JsonConvert.SerializeObject(clientPortfolio), Encoding.UTF8, "application/json");
            var result = this.httpRequestService.PostAsync(url, contentString);
            this.ioModule.WriteOutput(result.ToString());
        }
    }
}
