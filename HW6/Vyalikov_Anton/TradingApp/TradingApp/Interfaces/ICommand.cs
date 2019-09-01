namespace TradingApp.Interfaces
{
    using Services;
    interface ICommand
    {
        bool CanExecute(Command command);

        void Execute();
    }
}
