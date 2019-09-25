namespace TradingApiClient.Services.CommandStrategy
{
    using Newtonsoft.Json;
    using TradingApiClient.Devices;

    public class BalancesStrategy : ICommandStrategy
    {
        private readonly IOutputDevice outputDevice;
        private readonly IHttpRequestManager httpRequestManager;
        private readonly IInputDevice inputDevice;

        public BalancesStrategy(
            IOutputDevice outputDevice,
            IHttpRequestManager httpRequestManager,
            IInputDevice inputDevice)
        {
            this.outputDevice = outputDevice;
            this.httpRequestManager = httpRequestManager;
            this.inputDevice = inputDevice;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.balances)
            {
                return true;
            }

            return false;
        }

        public async void Execute()
        {
            
            this.outputDevice.WriteLine("Write Client Id you want to get balance:");
            string clientID = this.inputDevice.ReadLine();

            string url = $"http://localhost/balances?clientID={clientID}";

            var result = this.httpRequestManager.GetAsync(url).Content.ReadAsStringAsync();
            dynamic dynObj = JsonConvert.DeserializeObject(await result);
            
            this.outputDevice.WriteLine($"Client: {dynObj.name}");
            this.outputDevice.WriteLine($"Balance: {dynObj.balance}");
            this.outputDevice.WriteLine($"Status: {dynObj.status}");
        }
    }
}
