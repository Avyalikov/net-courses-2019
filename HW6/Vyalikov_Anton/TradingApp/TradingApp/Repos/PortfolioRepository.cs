namespace TradingApp.Repos
{
    using TradingApp.Core.Models;
    using TradingApp.Core.Repos;
    using Services;
    using System.Collections.Generic;
    using System.Linq;

    class PortfolioRepository : DBComm, IPortfolioRepository
    {
        private readonly DBContext dBContext;

        public PortfolioRepository(DBContext dBContext) : base(dBContext)
        {
            this.dBContext = dBContext;
        }

        public void Insert(ClientsPortfolios portfolio)
        {
            dBContext.ClientsPortfolios.Add(portfolio);
        }

        public ClientsPortfolios GetPortfolioByClientID(int clientID)
        {
            return dBContext.ClientsPortfolios.Where(x => x.ClientID == clientID).FirstOrDefault();
        }

        public bool DoesClientGetRequiredShares(int clientID, int shareID)
        {
            return dBContext.ClientsPortfolios.Where(x => x.ClientID == clientID && x.ShareID == shareID).FirstOrDefault().AmountOfShares != null;
        }

        public IEnumerable<ClientsPortfolios> GetAllPortfolios()
        {
            return dBContext.ClientsPortfolios;
        }

        public void ChangeAmountOfShares(ClientsPortfolios portfolios)
        {
            dBContext.ClientsPortfolios.Where(x => x.ClientID == portfolios.ClientID).FirstOrDefault().AmountOfShares += portfolios.AmountOfShares;
            dBContext.SaveChanges();
        }
    }
}
