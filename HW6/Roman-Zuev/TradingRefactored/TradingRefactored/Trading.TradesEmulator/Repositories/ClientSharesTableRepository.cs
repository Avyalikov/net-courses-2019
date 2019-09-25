namespace Trading.TradesEmulator.Repositories
{
    using System.Data.Entity;
    using Trading.Core.Models;
    using Trading.Core.Repositories;
    using Trading.TradesEmulator.Models;

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

        public void Change(ClientSharesEntity buyersItem)
        {
            this.dbContext.Entry(buyersItem).State = EntityState.Modified;
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }
    }
}