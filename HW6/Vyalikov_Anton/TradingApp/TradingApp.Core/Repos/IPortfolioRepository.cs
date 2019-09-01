namespace TradingApp.Core.Repos
{
    using Models;
    using System.Collections.Generic;
    public interface IPortfolioRepository : IDBComm
    {
        void Insert(ClientsPortfolios portfolio);
        IEnumerable<ClientsPortfolios> GetAllPortfolios();
        bool DoesClientGetRequiredShares(int clientID, int shareID);
        void ChangeAmountOfShares(ClientsPortfolios portfolios);
        ClientsPortfolios GetPortfolioByClientID(int clientID);
    }
}
