namespace Trading.TradesEmulator.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using Trading.Core.Models;
    using Trading.Core.Repositories;
    using Trading.TradesEmulator.Models;
    using System.Linq;
    public class SharesTableRepository : ISharesTableRepository
    {
        private readonly TradesEmulatorDbContext dbContext;

        public SharesTableRepository(TradesEmulatorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public SharesEntity this[int i] => this.dbContext.Shares.ToList()[i];

        public int Count => this.dbContext.Shares.ToList().Count;

        public bool ContainsById(int sharesId)
        {
            return this.dbContext.Shares.Any(c => c.Id == sharesId);
        }

        public SharesEntity GetById(int sharesId)
        {
            return this.dbContext.Shares.First(c => c.Id == sharesId);
        }
    }
}