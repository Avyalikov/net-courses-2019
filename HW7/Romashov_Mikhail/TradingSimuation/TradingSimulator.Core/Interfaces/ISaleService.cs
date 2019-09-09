using TradingSimulator.Core.Dto;

namespace TradingSimulator.Core.Interfaces
{
    public interface ISaleService
    {
        void HandleBuy(BuyArguments args);
        void AdditionStockToCustomer(BuyArguments args);
        void SaveHistory(BuyArguments args);
    }
}