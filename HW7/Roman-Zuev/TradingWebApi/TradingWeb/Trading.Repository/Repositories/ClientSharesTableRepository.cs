namespace Trading.Repository.Repositories
{
    using System.Data.Entity;
    using Trading.Core.Models;
    using Trading.Core.Repositories;
    using Trading.Repository.Context;

    public class ClientSharesTableRepository : IClientSharesTableRepository
    {
        private readonly TradesEmulatorDbContext dbContext;

        public ClientSharesTableRepository(TradesEmulatorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(ClientSharesEntity newShares)
        {
            this.dbContext.ClientShares.Add(newShares);
        }

        public void Update(ClientSharesEntity buyersItem)
        {
            this.dbContext.Entry(buyersItem).State = EntityState.Modified;
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }
    }
}