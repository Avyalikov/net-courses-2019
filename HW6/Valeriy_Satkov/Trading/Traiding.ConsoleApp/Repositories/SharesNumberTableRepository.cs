namespace Traiding.ConsoleApp.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Traiding.Core.Models;
    using Traiding.Core.Repositories;

    public class SharesNumberTableRepository : ISharesNumberTableRepository
    {
        private readonly StockExchangeDBContext dBContext;

        public SharesNumberTableRepository(StockExchangeDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public void Add(SharesNumberEntity entity)
        {
            this.dBContext.SharesNumbers.Add(entity);
        }

        public void ChangeNumber(int entityId, int newNumber)
        {
            var sharesNumber = this.dBContext.SharesNumbers.First(n => n.Id == entityId); // it will fall here if we can't find
            sharesNumber.Number = newNumber;
        }

        public void SaveChanges()
        {
            this.dBContext.SaveChanges();
        }

        public SharesNumberEntity SearchSharesNumberForBuy(int shareId, int requiredSharesNumber)
        {
            return this.dBContext.SharesNumbers.FirstOrDefault(n => n.Share.Id == shareId && n.Number >= requiredSharesNumber); // it will not fall here if we can't find
        }

        public SharesNumberEntity SearchSharesNumberForAddition(int clientId, int shareId)
        {
            var sharesNumber = this.dBContext.SharesNumbers.FirstOrDefault(n => n.Client.Id == clientId && n.Share.Id == shareId); // it will not fall here if we can't find
            return sharesNumber;
        }

        public IEnumerable<SharesNumberEntity> GetByClient(int clientId)
        {
            var sharesNumbers = this.dBContext.SharesNumbers.Where(n => n.Client.Id == clientId);
            return sharesNumbers;
        }

        public IEnumerable<SharesNumberEntity> GetByShare(int shareId)
        {
            var sharesNumbers = this.dBContext.SharesNumbers.Where(n => n.Share.Id == shareId);
            return sharesNumbers;
        }

        public void Remove(int entityId)
        {
            var sharesNumber = this.dBContext.SharesNumbers.First(n => n.Id == entityId); // it will fall here if we can't find
            this.dBContext.SharesNumbers.Remove(sharesNumber);
        }
    }
}
