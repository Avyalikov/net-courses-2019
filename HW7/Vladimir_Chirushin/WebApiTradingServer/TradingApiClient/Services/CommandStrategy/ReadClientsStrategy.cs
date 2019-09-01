namespace TradingApiClient.Services.CommandStrategy
{
    using Newtonsoft.Json;
    using TradingApiClient.Devices;

    public class ReadClientsStrategy : ICommandStrategy
    {
        private readonly IOutputDevice outputDevice;
        private readonly IInputDevice inputDevice;
        private readonly IHttpRequestManager httpRequestManager;


        public ReadClientsStrategy(
            IOutputDevice outputDevice,
            IHttpRequestManager httpRequestManager,
            IInputDevice inputDevice)
        {
            this.inputDevice = inputDevice;
            this.outputDevice = outputDevice;
            this.httpRequestManager = httpRequestManager;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.clients)
            {
                return true;
            }

            return false;
        }

        public async void Execute()
        {
            this.outputDevice.WriteLine("Write number of 'tops' clients:");
            string topInput = this.inputDevice.ReadLine();

            this.outputDevice.WriteLine("Write number of client 'pages' you want to get:");
            string pageInput = this.inputDevice.ReadLine();

            string url = $"http://localhost/clients?top={topInput}&page={pageInput}";

            var result = httpRequestManager.GetAsync(url).Content.ReadAsStringAsync();
            dynamic dynObj = JsonConvert.DeserializeObject(await result);

            string numberColumnName = "#";
            string nameColumnName = "Name";
            string phoneNumberColumnName = "Phone Number";
            string balanceColumnName = "Balance";

            this.outputDevice.WriteLine($"___________________________________________________________");
            this.outputDevice.WriteLine($"|{numberColumnName,4}|{nameColumnName,22}|{phoneNumberColumnName,14}|{balanceColumnName,14}|");
            this.outputDevice.WriteLine($"|----|----------------------|--------------|--------------|");
            foreach (var data in dynObj)
            {
                this.outputDevice.WriteLine($"|{data.clientID.ToString(),4}|{data.name.ToString(),22}|{data.phoneNumber.ToString(),14}|{data.balance.ToString(),14}|");
            }

            this.outputDevice.WriteLine($"|____|______________________|______________|______________|");
        }
    }
}