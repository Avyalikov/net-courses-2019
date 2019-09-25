namespace TradingApp.Core.Interfaces
{
    using DTO;
    using Models;
    using System.Collections.Generic;

    public interface IPortfoliosService
    {
        void RegisterPortfolio(PortfolioData portfolioData);
        void ChangeAmountOfShares(ClientPortfolio portfolios);
        IEnumerable<ClientPortfolio> GetAllPortfolios();
    }
}
