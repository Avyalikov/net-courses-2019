namespace Traiding.WebAPIConsole.Models.Repositories
{
    using System.Linq;
    using Traiding.Core.Models;
    using Traiding.Core.Repositories;

    public class BlockedMoneyTableRepository : IBlockedMoneyTableRepository
    {
        private readonly StockExchangeDBContext dBContext;

        public BlockedMoneyTableRepository(StockExchangeDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public void Add(BlockedMoneyEntity entity)
        {
            this.dBContext.BlockedMoneys.Add(entity);
        }

        public void Remove(int entityId)
        {
            var sharesNumber = this.dBContext.BlockedMoneys.First(bm => bm.Id == entityId); // it will fall here if we can't find
            this.dBContext.BlockedMoneys.Remove(sharesNumber);
        }

        public void SaveChanges()
        {
            this.dBContext.SaveChanges();
        }
    }
}
