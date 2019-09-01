namespace Traiding.WebAPIConsole.Models.Repositories
{
    using System.Linq;
    using Traiding.Core.Models;
    using Traiding.Core.Repositories;

    public class ShareTypeTableRepository : IShareTypeTableRepository
    {
        private readonly StockExchangeDBContext dBContext;

        public ShareTypeTableRepository(StockExchangeDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public void Add(ShareTypeEntity entity)
        {
            this.dBContext.ShareTypes.Add(entity);
        }

        public bool Contains(ShareTypeEntity entity)
        {
            return this.dBContext.ShareTypes.Any(s =>
            s.Name == entity.Name);
        }

        public bool ContainsById(int shareTypeId)
        {
            return this.dBContext.ShareTypes.Any(n => n.Id == shareTypeId);
        }

        public ShareTypeEntity Get(int entityId)
        {
            return this.dBContext.ShareTypes.First(n => n.Id == entityId); // it will fall here if we can't find
        }

        public void SaveChanges()
        {
            this.dBContext.SaveChanges();
        }

        public void SetCost(int entityId, decimal newCost)
        {
            var shareType = this.dBContext.ShareTypes.First(b => b.Id == entityId); // it will fall here if we can't find
            shareType.Cost = newCost;
        }

        public void SetName(int entityId, string newName)
        {
            var shareType = this.dBContext.ShareTypes.First(b => b.Id == entityId); // it will fall here if we can't find
            shareType.Name = newName;
        }
    }
}
