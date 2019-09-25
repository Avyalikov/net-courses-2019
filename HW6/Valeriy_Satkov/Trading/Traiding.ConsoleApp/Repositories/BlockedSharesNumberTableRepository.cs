namespace Traiding.ConsoleApp.Repositories
{
    using System.Linq;
    using Traiding.Core.Models;
    using Traiding.Core.Repositories;

    public class BlockedSharesNumberTableRepository : IBlockedSharesNumberTableRepository
    {
        private readonly StockExchangeDBContext dBContext;

        public BlockedSharesNumberTableRepository(StockExchangeDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public void Add(BlockedSharesNumberEntity entity)
        {
            this.dBContext.BlockedSharesNumbers.Add(entity);
        }

        //public bool Contains(BlockedSharesNumberEntity entity)
        //{
        //    return this.dBContext.BlockedSharesNumbers.Any(bn =>
        //    bn.Operation == entity.Operation);
        //}

        public void Remove(int entityId)
        {
            var sharesNumber = this.dBContext.BlockedSharesNumbers.First(bn => bn.Id == entityId); // it will fall here if we can't find
            this.dBContext.BlockedSharesNumbers.Remove(sharesNumber);
        }

        public void SaveChanges()
        {
            this.dBContext.SaveChanges();
        }
    }
}
