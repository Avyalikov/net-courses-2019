namespace TradingAppWebAPI
{
    using TradingApp.Core.Models;
    using TradingApp.Core.Repos;
    using System.Collections.Generic;
    using System.Linq;

    class PortfolioRepository : DBComm, IPortfolioRepository
    {
        private readonly DBContext dBContext;

        public PortfolioRepository(DBContext dBContext) : base(dBContext)
        {
            this.dBContext = dBContext;
        }

        public void Insert(ClientPortfolio portfolio)
        {
            dBContext.ClientsPortfolios.Add(portfolio);
        }

        public ClientPortfolio GetPortfolioByClientID(int clientID)
        {
            return dBContext.ClientsPortfolios.Where(x => x.ClientID == clientID).FirstOrDefault();
        }

        public bool DoesClientGetRequiredShares(int clientID, int shareID)
        {
            return dBContext.ClientsPortfolios.Where(x => x.ClientID == clientID && x.ShareID == shareID).FirstOrDefault().AmountOfShares != null;
        }

        public IEnumerable<ClientPortfolio> GetAllPortfolios()
        {
            return dBContext.ClientsPortfolios;
        }

        public void ChangeAmountOfShares(ClientPortfolio portfolios)
        {
            dBContext.ClientsPortfolios.Where(x => x.ClientID == portfolios.ClientID).FirstOrDefault().AmountOfShares += portfolios.AmountOfShares;
            dBContext.SaveChanges();
        }

        public void Remove(ClientPortfolio portfolio)
        {
            var portfolioToRemove = dBContext.ClientsPortfolios.Where(x => x.ShareID == portfolio.ShareID && x.ClientID == portfolio.ClientID).FirstOrDefault();
            dBContext.ClientsPortfolios.Remove(portfolioToRemove);
            dBContext.SaveChanges();
        }

        public void Update(ClientPortfolio newPortfolio)
        {
            var oldPortfolio = GetPortfolioByClientID(newPortfolio.ClientID);
            dBContext.Entry(oldPortfolio).CurrentValues.SetValues(newPortfolio);
            dBContext.SaveChanges();
        }
    }
}
