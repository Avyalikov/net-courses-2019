namespace TradingApp.OwinHostApi.Repositories
{
    using System.Linq;
    using TradingApp.Core.Models;
    using TradingApp.Core.Repositories;

    public class PortfolioTableRepository : IPortfolioTableRepository
    {
        private readonly TradingAppDb db;

        public PortfolioTableRepository(TradingAppDb db)
        {
            this.db = db;
        }

        public void AddNewUsersShares(PortfolioEntity args)
        {
            this.db.Portfolio.Add(args);
        }

        public bool Contains(PortfolioEntity entity)
        {
            return this.db.Portfolio.Any(
                p => p.Share == entity.Share &&
                p.UserEntityId == entity.UserEntityId &&
                p.ShareId == entity.ShareId
                );
        }

        public void SaveChanges()
        {
            this.db.SaveChanges();
        }
    }
}
