namespace TradingApp.Core.ServicesInterfaces
{
    using TradingApp.Core.Dto;
    using TradingApp.Core.Models;


    public interface IPortfolioServices
    {
        void AddNewUsersShares(PortfolioInfo args);
        void ChangeAmountOfShares(PortfolioEntity args, int value);
    }
}