using TradingSimulator.Core.Dto;

namespace WebApiServer.Interfaces
{
    public interface IValidator
    {
        bool TraderInfoValidate(TraderInfo traderInfo);
        bool StockToTraderValidate(string traderName, string stockName);
    }
}