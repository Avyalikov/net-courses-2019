namespace TradingApiClient.Services.CommandStrategy
{
    using System.Net.Http;
    using System.Text;
    using Newtonsoft.Json;
    using TradingApiClient.Devices;
    using TradingSoftware.Core.Dto;

    public class DealMakeStrategy : ICommandStrategy
    {
        private readonly IOutputDevice outputDevice;
        private readonly IInputDevice inputDevice;
        private readonly IHttpRequestManager httpRequestManager;
        private readonly ICommandParser commandParser;

        public DealMakeStrategy(
            IOutputDevice outputDevice,
            IHttpRequestManager httpRequestManager,
            IInputDevice inputDevice,
            ICommandParser commandParser)
        {
            this.inputDevice = inputDevice;
            this.outputDevice = outputDevice;
            this.httpRequestManager = httpRequestManager;
            this.commandParser = commandParser;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.dealMake)
            {
                return true;
            }

            return false;
        }

        public void Execute()
        {
            string url = "http://localhost/deal/make";
            this.outputDevice.Clear();
            this.commandParser.Parse(Command.clients.ToString());
            this.outputDevice.WriteLine("Select seller ID:");
            int sellerID = 0;
            while (true)
            {
                if (int.TryParse(this.inputDevice.ReadLine(), out sellerID))
                {
                    break;
                }
                else
                {
                    this.outputDevice.WriteLine("Please enter valid seller ID");
                }
            }

            this.outputDevice.Clear();
            this.commandParser.Parse(Command.clients.ToString());
            this.outputDevice.WriteLine("Select buyer ID:");
            int buyerID = 0;
            while (true)
            {
                if (int.TryParse(this.inputDevice.ReadLine(), out buyerID))
                {
                    break;
                }
                else
                {
                    this.outputDevice.WriteLine("Please enter valid buyer ID");
                }
            }

            this.outputDevice.Clear();
            this.commandParser.Parse(Command.shares.ToString());
            this.outputDevice.WriteLine("Select share ID:");
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

            this.outputDevice.WriteLine("Write share amount:");
            int stockAmount = 0;
            while (true)
            {
                if (int.TryParse(this.inputDevice.ReadLine(), out stockAmount))
                {
                    break;
                }
                else
                {
                    this.outputDevice.WriteLine("Please enter valid balance");
                }
            }

            TransactionsMakeData transactionToMake = new TransactionsMakeData { sellerID = sellerID, buyerID = buyerID, shareID = shareID, shareAmount = stockAmount };

            HttpContent contentString = new StringContent(JsonConvert.SerializeObject(transactionToMake), Encoding.UTF8, "application/json");
            var result = this.httpRequestManager.PostAsync(url, contentString);
            this.outputDevice.WriteLine(result.ToString());
        }
    }
}