namespace TradingApp.Core.Interfaces
{
    using DTO;
    using Models;
    using System.Collections.Generic;

    public interface IPortfoliosService
    {
        void RegisterPortfolio(PortfolioData portfolioData);
        void ChangeAmountOfShares(ClientsPortfolios portfolios);
        IEnumerable<ClientsPortfolios> GetAllPortfolios();
    }
}
