namespace TradingApp.Commands
{
    using Interfaces;
    using Services;
    using TradingApp.Core.Models;
    using Newtonsoft.Json;
    using System.Net.Http;
    using System;
    using System.Text;

    class MakeDealCommand : ICommand
    {
        private readonly IInputOutputModule ioModule;
        private readonly IHTTPRequestService httpRequestService;
        private readonly ICommandParser commandParser;

        public MakeDealCommand(IInputOutputModule ioModule, IHTTPRequestService httpRequestService, ICommandParser commandParser)
        {
            this.ioModule = ioModule;
            this.httpRequestService = httpRequestService;
            this.commandParser = commandParser;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.MakeDeal)
            {
                return true;
            }

            return false;
        }

        public void Execute()
        {
            string url = "http://localhost/deal/make";
            int sellerID = 0;
            int buyerID = 0;
            int shareID = 0;
            int amountOfShares = 0;

            this.ioModule.Clear();
            this.commandParser.Parse(Command.GetClients.ToString());
            this.ioModule.WriteOutput("Choose seller ID:");
            while (true)
            {
                if (int.TryParse(this.ioModule.ReadInput(), out sellerID))
                {
                    break;
                }
                this.ioModule.WriteOutput("Please enter valid seller ID.");
            }

            this.ioModule.Clear();
            this.commandParser.Parse(Command.GetClients.ToString());
            this.ioModule.WriteOutput("Choose buyer ID:");
            
            while (true)
            {
                if (int.TryParse(this.ioModule.ReadInput(), out buyerID))
                {
                    break;
                }
                this.ioModule.WriteOutput("Please enter valid buyer ID.");
            }

            this.ioModule.Clear();
            this.commandParser.Parse(Command.GetPortfolios.ToString());
            this.ioModule.WriteOutput("Choose share ID:");
            
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
                if (int.TryParse(this.ioModule.ReadInput(), out amountOfShares))
                {
                    break;
                }
                this.ioModule.WriteOutput("Please enter valid balance.");
            }

            Transaction transaction = new Transaction
            {
                BuyerID = buyerID,
                SellerID = sellerID,
                ShareID = shareID,
                AmountOfShares = amountOfShares,
                Date = DateTime.Now
            };

            HttpContent contentString = new StringContent(JsonConvert.SerializeObject(transaction), Encoding.UTF8, "application/json");
            var result = this.httpRequestService.PostAsync(url, contentString);
            this.ioModule.WriteOutput(result.ToString());
        }
    }
}
