namespace TradingApp.Core.Repositories
{
    using TradingApp.Core.Models;

    public interface IPortfolioTableRepository
    {
        void AddNewUsersShares(PortfolioEntity args);
        void SaveChanges();
        bool Contains(PortfolioEntity entity);
    }
}
