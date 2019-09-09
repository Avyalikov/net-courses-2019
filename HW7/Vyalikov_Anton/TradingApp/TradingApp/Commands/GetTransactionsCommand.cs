namespace TradingApp.Commands
{
    using Interfaces;
    using Services;
    using Newtonsoft.Json;

    class GetTransactionsCommand : ICommand
    {
        private readonly IInputOutputModule ioModule;
        private readonly IHTTPRequestService httpRequestService;

        public GetTransactionsCommand(IInputOutputModule ioModule, IHTTPRequestService httpRequestService)
        {
            this.ioModule = ioModule;
            this.httpRequestService = httpRequestService;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.GetTransactions)
            {
                return true;
            }

            return false;
        }

        public async void Execute()
        {
            this.ioModule.WriteOutput("Write clientID you want to read transactions for:");
            string clientId = this.ioModule.ReadInput();

            this.ioModule.WriteOutput("Write amount of tops transactions you want to get:");
            string tops = this.ioModule.ReadInput();

            string url = $"http://localhost/transactions?clientID={clientId}&top={tops}";

            var result = this.httpRequestService.GetAsync(url).Content.ReadAsStringAsync();
            dynamic dynObject = JsonConvert.DeserializeObject(await result);


            string idColumn = "#";
            string dateColumn = "Date and Time";
            string sellerColumn = "Seller";
            string buyerColumn = "Buyer";
            string shareColumn = "Share";
            string amountColumn = "Quanity";
            string totalColumn = "Share Price";

            this.ioModule.WriteOutput($"_________________________________________________________________________________________________________________");
            this.ioModule.WriteOutput($"|{idColumn,4}|{dateColumn,20}|{sellerColumn,22}|{buyerColumn,22}|{shareColumn,22}|{amountColumn,4}|{totalColumn,11}|");
            this.ioModule.WriteOutput($"|----|--------------------|----------------------|----------------------|----------------------|----|-----------|");

            foreach (var data in dynObject)
            {
                this.ioModule.WriteOutput($"|{data.transactionID,4}|{data.dateTime,20}|{data.sellerName,22}|{data.buyerName,22}|{data.shareType,22}|{data.shareAmount,4}|{data.sharePrice,10}$|");
            }

            this.ioModule.WriteOutput($"|____|____________________|______________________|______________________|______________________|____|___________|");
        }
    }
}
