namespace TradingApp.Commands
{
    using Interfaces;
    using Services;
    using Newtonsoft.Json;

    class GetClientsCommand : ICommand
    {
        private readonly IInputOutputModule ioModule;
        private readonly IHTTPRequestService httpRequestService;

        public GetClientsCommand(IInputOutputModule ioModule, IHTTPRequestService httpRequestService)
        {
            this.ioModule = ioModule;
            this.httpRequestService = httpRequestService;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.GetClients)
            {
                return true;
            }

            return false;
        }

        public async void Execute()
        {
            this.ioModule.WriteOutput("Write number of tops clients:");
            string tops = this.ioModule.ReadInput();

            this.ioModule.WriteOutput("Write number of client pages you want to get:");
            string pages = this.ioModule.ReadInput();

            string url = $"http://localhost/clients?top={tops}&page={pages}";

            var result = this.httpRequestService.GetAsync(url).Content.ReadAsStringAsync();
            dynamic dynObject = JsonConvert.DeserializeObject(await result);

            string idColumn = "#";
            string nameColumn = "Name";
            string phoneNumberColumn = "Phone Number";
            string balanceColumn = "Balance";

            this.ioModule.WriteOutput($"___________________________________________________________");
            this.ioModule.WriteOutput($"|{idColumn,4}|{nameColumn,22}|{phoneNumberColumn,14}|{balanceColumn,14}|");
            this.ioModule.WriteOutput($"|----|----------------------|--------------|--------------|");

            foreach (var data in dynObject)
            {
                this.ioModule.WriteOutput($"|{data.clientID.ToString(),4}|{data.name.ToString(),22}|{data.phoneNumber.ToString(),14}|{data.balance.ToString(),14}|");
            }

            this.ioModule.WriteOutput($"|____|______________________|______________|______________|");
        }
    }
}
