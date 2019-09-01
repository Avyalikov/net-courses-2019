namespace TradingApiClient
{
    using TradingApiClient.Devices;
    using TradingApiClient.Services;

    public class TradingApiClientEngine : ITradingApiClientEngine
    {
        private readonly IInputDevice inputDevice;
        private readonly ICommandParser commandParser;

        public TradingApiClientEngine(
            IInputDevice inputDevice,
            ICommandParser commandParser)
        {
            this.inputDevice = inputDevice;
            this.commandParser = commandParser;
        }

        public void Run()
        {
            string commandString;
            this.commandParser.Parse("help");
            do
            {
                commandString = this.inputDevice.ReadLine();
                this.commandParser.Parse(commandString);
            }
            while (commandString.ToLower() != "quit");
        }
    }
}
