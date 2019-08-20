namespace trading_software
{
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
            commandParser.ShowMenu();
            do
            {
                commandString = inputDevice.ReadLine();
                commandParser.Parse(commandString);
            }
            while (commandString.ToLower() != "quit");
        }
    }
}