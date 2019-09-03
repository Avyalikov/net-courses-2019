namespace TradingApp.Commands
{
    using Interfaces;
    using Services;
    using Newtonsoft.Json;

    class GetBalanceCommand : ICommand
    {
        private readonly IInputOutputModule ioModule;
        private readonly IHTTPRequestService httpRequestService;

        public GetBalanceCommand(IInputOutputModule ioModule, IHTTPRequestService httpRequestService)
        {
            this.ioModule = ioModule;
            this.httpRequestService = httpRequestService;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.GetBalance)
            {
                return true;
            }
            return false;
        }

        public async void Execute()
        {
            string url = "http://localhost/balances?clientID={clientID}";
            int clientID = 0;

            this.ioModule.WriteOutput("Write client ID whose balance you want to get:");
            while (true)
            {
                if (int.TryParse(this.ioModule.ReadInput(), out clientID))
                {
                    break;
                }
                this.ioModule.WriteOutput("Please enter valid client ID.");
            }

            var result = this.httpRequestService.GetAsync(url).Content.ReadAsStringAsync();
            dynamic dynObj = JsonConvert.DeserializeObject(await result);

            this.ioModule.WriteOutput($"Client: {dynObj.name}");
            this.ioModule.WriteOutput($"Balance: {dynObj.balance}");
            this.ioModule.WriteOutput($"Status: {dynObj.status}");
        }
    }
}
