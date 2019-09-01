namespace TradingApiClient.Services.CommandStrategy
{
    using System.Net.Http;
    using System.Text;
    using Newtonsoft.Json;
    using TradingApiClient.Devices;
    using TradingSoftware.Core.Models;

    public class RemoveSharesStrategy : ICommandStrategy
    {
        private readonly IOutputDevice outputDevice;
        private readonly IHttpRequestManager httpRequestManager;
        private readonly IInputDevice inputDevice;

        public RemoveSharesStrategy(
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
            if (command == Command.sharesRemove)
            {
                return true;
            }

            return false;
        }

        public void Execute()
        {
            string url = "http://localhost/shares/remove";

            this.outputDevice.WriteLine("Write Share ID:");
            int shareID = 0;
            while (true)
            {
                if (int.TryParse(this.inputDevice.ReadLine(), out shareID))
                {
                    break;
                }
                else
                {
                    this.outputDevice.WriteLine("Please enter valid share ID");
                }
            }


            this.outputDevice.WriteLine("Write Client ID:");
            int clientID = 0;
            while (true)
            {
                if (int.TryParse(this.inputDevice.ReadLine(), out clientID))
                {
                    break;
                }
                else
                {
                    this.outputDevice.WriteLine("Please enter valid client ID");
                }
            }


            var blockOfShare = new BlockOfShares { ClientID = clientID, ShareID = shareID };
            HttpContent contentString = new StringContent(JsonConvert.SerializeObject(blockOfShare), Encoding.UTF8, "application/json");
            var result = this.httpRequestManager.PostAsync(url, contentString);
            this.outputDevice.WriteLine(result.ToString());
        }
    }
}
