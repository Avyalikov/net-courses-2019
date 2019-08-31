using Newtonsoft.Json;
using System;
using TradingApiClient.Devices;

namespace TradingApiClient.Services.CommandStrategy
{

    public class ReadSharesStrategy : ICommandStrategy
    {
        private readonly IOutputDevice outputDevice;
        private readonly IHttpRequestManager httpRequestManager;


        public ReadSharesStrategy(
            IOutputDevice outputDevice,
            IHttpRequestManager httpRequestManager)
        {
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

        public void Execute()
        {
            var result = httpRequestManager.Get("http://localhost/shares?clientId=1");
            dynamic dynObj = JsonConvert.DeserializeObject(result);

            outputDevice.WriteLine($"Client {dynObj.clientName.ToString()} has shares:");
            foreach (var data in dynObj.shareWithPrice)
            {
                outputDevice.WriteLine(data.ToString()); 
            }
        }
    }
}
