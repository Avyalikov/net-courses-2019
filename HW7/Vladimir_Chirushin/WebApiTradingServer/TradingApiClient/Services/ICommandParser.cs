namespace TradingApiClient.Services
{
    public interface ICommandParser
    {
        void Parse(string commandString);
    }
}