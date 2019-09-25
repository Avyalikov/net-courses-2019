namespace TradingApp
{
    using Interfaces;
    using Services;
    class MainLoop : IMainLoop
    {
        private readonly IInputOutputModule ioModule;
        private readonly ICommandParser commandParser;

        public MainLoop(InputOutputModule ioModule, ICommandParser commandParser)
        {
            this.ioModule = ioModule;
            this.commandParser = commandParser;
        }

        public void Start()
        {
            string command;
            this.commandParser.Parse("help");
            do
            {
                command = this.ioModule.ReadInput();
                this.commandParser.Parse(command);
            }
            while (command.ToLower() != "exit");
        }
    }
}
