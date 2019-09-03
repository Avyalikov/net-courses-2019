namespace TradingApp.Commands
{
    using Interfaces;
    using Services;
    using Newtonsoft.Json;

    class GetPortfoliosCommand : ICommand
    {
        private readonly IInputOutputModule ioModule;
        private readonly IHTTPRequestService httpRequestService;

        public GetPortfoliosCommand(IInputOutputModule ioModule, IHTTPRequestService httpRequestService)
        {
            this.ioModule = ioModule;
            this.httpRequestService = httpRequestService;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.GetPortfolios)
            {
                return true;
            }

            return false;
        }

        public async void Execute()
        {
            this.ioModule.WriteOutput("Write client ID whose shares you want to get:");
            string clientId = this.ioModule.ReadInput();

            string url = $"http://localhost/shares?clientId={clientId}";

            var result = this.httpRequestService.GetAsync(url).Content.ReadAsStringAsync();
            dynamic dynObject = JsonConvert.DeserializeObject(await result);

            this.ioModule.WriteOutput($"Client {dynObject.clientName.ToString()} has shares:");
            foreach (var data in dynObject.shareWithPrice)
            {
                this.ioModule.WriteOutput(data.ToString());
            }
        }
    }
}
