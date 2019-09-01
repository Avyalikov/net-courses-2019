namespace TradingApiClient.Services.CommandStrategy
{
    using Newtonsoft.Json;
    using TradingApiClient.Devices;

    public class ReadSharesStrategy : ICommandStrategy
    {
        private readonly IOutputDevice outputDevice;
        private readonly IInputDevice inputDevice;
        private readonly IHttpRequestManager httpRequestManager;


        public ReadSharesStrategy(
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
            if (command == Command.shares)
            {
                return true;
            }

            return false;
        }

        public async void Execute()
        {
            this.outputDevice.WriteLine("Write clientID you want to read shares for:");
            string clientIdInput = this.inputDevice.ReadLine();

            string url = $"http://localhost/shares?clientId={clientIdInput}";

            var result = this.httpRequestManager.GetAsync(url).Content.ReadAsStringAsync();
            dynamic dynObj = JsonConvert.DeserializeObject(await result);

            this.outputDevice.WriteLine($"Client {dynObj.clientName.ToString()} has shares:");
            foreach (var data in dynObj.shareWithPrice)
            {
                this.outputDevice.WriteLine(data.ToString()); 
            }
        }
    }
}
