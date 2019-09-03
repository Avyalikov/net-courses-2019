namespace TradingApp.Core.Services
{
    using DTO;
    using Interfaces;
    using Models;
    using Repos;
    using System.Collections.Generic;

    public class PortfoliosService : IPortfoliosService
    {
        private readonly IPortfolioRepository portfolioRepository;

        public PortfoliosService(IPortfolioRepository portfolioRepository)
        {
            this.portfolioRepository = portfolioRepository;
        }

        public void RegisterPortfolio(PortfolioData portfolioData)
        {
            var newPortfolio = new ClientPortfolio()
            {
                ClientID = portfolioData.ClientID,
                ShareID = portfolioData.ShareID,
                AmountOfShares = portfolioData.AmountOfShares
            };

            portfolioRepository.Insert(newPortfolio);
            portfolioRepository.SaveChanges();
        }

        public void ChangeAmountOfShares(ClientPortfolio portfolios)
        {
            var newSharesForClient = portfolioRepository.GetPortfolioByClientID(portfolios.ClientID);

            if (newSharesForClient != null)
            {
                newSharesForClient.AmountOfShares += portfolios.AmountOfShares;
            }

            else
            {
                newSharesForClient = new ClientPortfolio()
                {
                    ClientID = portfolios.ClientID,
                    ShareID = portfolios.ShareID,
                    AmountOfShares = portfolios.AmountOfShares,
                };
                portfolioRepository.Insert(newSharesForClient);
            }

            portfolioRepository.SaveChanges();
        }

        public IEnumerable<ClientPortfolio> GetAllPortfolios()
        {
            return portfolioRepository.GetAllPortfolios();
        }
    }
}
