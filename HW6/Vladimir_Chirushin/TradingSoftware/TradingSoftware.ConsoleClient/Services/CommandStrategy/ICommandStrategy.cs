namespace TradingSoftware.ConsoleClient.Services.CommandStrategy
{
    public interface ICommandStrategy
    {
        bool CanExecute(Command command);

        void Execute();
    }
}