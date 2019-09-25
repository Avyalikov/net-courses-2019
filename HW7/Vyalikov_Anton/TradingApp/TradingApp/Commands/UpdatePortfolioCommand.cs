namespace TradingApp.Commands
{
    using Interfaces;
    using Services;
    using TradingApp.Core.Models;
    using Newtonsoft.Json;
    using System.Net.Http;
    using System.Text;

    class UpdatePortfolioCommand : ICommand
    {
        private readonly IInputOutputModule ioModule;
        private readonly IHTTPRequestService httpRequestService;

        public UpdatePortfolioCommand(IInputOutputModule ioModule, IHTTPRequestService httpRequestService)
        {
            this.ioModule = ioModule;
            this.httpRequestService = httpRequestService;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.UpdatePortfolio)
            {
                return true;
            }
            return false;
        }

        public void Execute()
        {
            string url = "http://localhost/clients/update";
            int clientID = 0;
            int shareID = 0;
            int newAmountOfShares = 0;

            this.ioModule.WriteOutput("Write client ID, that you want to update:");
            while (true)
            {
                if (int.TryParse(this.ioModule.ReadInput(), out clientID))
                {
                    break;
                }
                this.ioModule.WriteOutput("Please enter valid client ID.");
            }

            this.ioModule.WriteOutput("Write share ID:");
            while (true)
            {
                if (int.TryParse(this.ioModule.ReadInput(), out shareID))
                {
                    break;
                }
                this.ioModule.WriteOutput("Please enter valid share ID.");
            }

            this.ioModule.WriteOutput("Write amount of shares:");
            while (true)
            {
                if (int.TryParse(this.ioModule.ReadInput(), out newAmountOfShares))
                {
                    break;
                }
                this.ioModule.WriteOutput("Please enter valid amount of shares.");
            }

            ClientPortfolio portfolioToUpdate = new ClientPortfolio
            {
                ClientID = clientID,
                ShareID = shareID,
                AmountOfShares = newAmountOfShares
            };

            HttpContent contentString = new StringContent(JsonConvert.SerializeObject(portfolioToUpdate), Encoding.UTF8, "application/json");
            var result = this.httpRequestService.PostAsync(url, contentString);
            this.ioModule.WriteOutput(result.ToString());
        }
    }
}
