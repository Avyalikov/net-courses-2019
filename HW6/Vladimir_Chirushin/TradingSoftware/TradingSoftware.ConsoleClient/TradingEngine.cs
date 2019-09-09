namespace TradingSoftware.ConsoleClient
{
    using TradingSoftware.ConsoleClient.Devices;
    using TradingSoftware.ConsoleClient.Services;

    public class TradingEngine : ITradingEngine
    {
        private readonly IInputDevice inputDevice;
        private readonly ICommandParser commandParser;

        public TradingEngine(
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