namespace trading_software
{
    public interface ICommandParser
    {
        void Parse(string commandString);
        void ShowMenu();
    }
}