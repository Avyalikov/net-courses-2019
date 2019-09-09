using TradingSimulator.Core.Dto;

namespace TradingSimulator.ConsoleApp.Interfaces
{
    public interface IValidator
    {
        bool TraderInfoValidate(TraderInfo traderInfo);
        bool StockToTraderValidate(string traderName, string stockName);
    }
}