namespace TradingApiClient.Services.CommandStrategy
{
    using Newtonsoft.Json;
    using TradingApiClient.Devices;

    public class TransacactionsStrategy : ICommandStrategy
    {
        private readonly IOutputDevice outputDevice;
        private readonly IInputDevice inputDevice;
        private readonly IHttpRequestManager httpRequestManager;

        public TransacactionsStrategy(
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
            if (command == Command.transactions)
            {
                return true;
            }

            return false;
        }

        public async void Execute()
        {
            this.outputDevice.WriteLine("Write clientID you want to read transactions for:");
            string clientIdInput = this.inputDevice.ReadLine();

            this.outputDevice.WriteLine("Write amount of top transactions you want to get:");
            string topInput = this.inputDevice.ReadLine();

            string url = $"http://localhost/transactions?clientID={clientIdInput}&top={topInput}";

            var result = this.httpRequestManager.GetAsync(url).Content.ReadAsStringAsync();
            dynamic dynObj = JsonConvert.DeserializeObject(await result);


            string numberNameColumn = "#";
            string dateTimeNameColumn = "Date and Time";
            string sellerNameColumn = "Seller";
            string buyerNameColumn = "Buyer";
            string shareNameColumn = "Share";
            string amountNameColumn = "Quan";
            string transactionAmountname = "Share Price";
           
            this.outputDevice.WriteLine($"_________________________________________________________________________________________________________________");
            this.outputDevice.WriteLine($"|{numberNameColumn,4}|{dateTimeNameColumn,20}|{sellerNameColumn,22}|{buyerNameColumn,22}|{shareNameColumn,22}|{amountNameColumn,4}|{transactionAmountname,11}|");
            this.outputDevice.WriteLine($"|----|--------------------|----------------------|----------------------|----------------------|----|-----------|");

            foreach (var data in dynObj)
            {
                 this.outputDevice.WriteLine($"|{data.transactionID,4}|{data.dateTime,20}|{data.sellerName,22}|{data.buyerName,22}|{data.shareType,22}|{data.shareAmount,4}|{data.sharePrice,10}$|");
            }

            this.outputDevice.WriteLine($"|____|____________________|______________________|______________________|______________________|____|___________|");
        }
    }
}
