namespace TradingApp.Commands
{
    using System;
    using Interfaces;
    using Services;

    class HelpCommand : ICommand
    {
        private readonly IInputOutputModule ioModule;

        public HelpCommand(IInputOutputModule ioModule)
        {
            this.ioModule = ioModule;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.Help)
            {
                return true;
            }

            return false;
        }

        public void Execute()
        {
            foreach (Command command in Enum.GetValues(typeof(Command)))
            {
                this.ioModule.WriteOutput(command.ToString());
            }
        }
    }
}
