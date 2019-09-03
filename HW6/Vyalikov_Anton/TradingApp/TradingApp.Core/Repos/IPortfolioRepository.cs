namespace TradingApp.Core.Repos
{
    using Models;
    using System.Collections.Generic;
    public interface IPortfolioRepository : IDBComm
    {
        void Insert(ClientPortfolio portfolio);
        IEnumerable<ClientPortfolio> GetAllPortfolios();
        bool DoesClientGetRequiredShares(int clientID, int shareID);
        void ChangeAmountOfShares(ClientPortfolio portfolios);
        ClientPortfolio GetPortfolioByClientID(int clientID);
    }
}
