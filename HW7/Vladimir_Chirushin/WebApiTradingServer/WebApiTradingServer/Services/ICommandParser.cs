namespace WebApiTradingServer.Services
{
    public interface ICommandParser
    {
        void Parse(string commandString);
    }
}