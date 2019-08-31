using Newtonsoft.Json;
using System;
using TradingApiClient.Devices;

namespace TradingApiClient.Services.CommandStrategy
{
    public class ReadClientsStrategy : ICommandStrategy
    {
        private readonly IOutputDevice outputDevice;
        private readonly IHttpRequestManager httpRequestManager;


        public ReadClientsStrategy(
            IOutputDevice outputDevice,
            IHttpRequestManager httpRequestManager)
        {
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

        public void Execute()
        {
            var result = httpRequestManager.Get("http://localhost/clients?top=10&page=1");
            dynamic dynObj = JsonConvert.DeserializeObject(result);

            string numberColumnName = "#";
            string nameColumnName = "Name";
            string phoneNumberColumnName = "Phone Number";
            string balanceColumnName = "Balance";

            this.outputDevice.WriteLine($"___________________________________________________________");
            this.outputDevice.WriteLine($"|{numberColumnName,4}|{nameColumnName,22}|{phoneNumberColumnName,14}|{balanceColumnName,14}|");
            this.outputDevice.WriteLine($"|----|----------------------|--------------|--------------|");
            foreach (var data in dynObj)
            {
                this.outputDevice.WriteLine($"|{data.clientID.ToString(),4}|{data.name.ToString(),22}|{nameColumnName,14}|{data.balance.ToString(),14}|");
            }

            this.outputDevice.WriteLine($"|____|______________________|______________|______________|");
            outputDevice.WriteLine(result);
        }
    }
}
