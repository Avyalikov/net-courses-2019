namespace Traiding.ConsoleApp.Repositories
{
    using System.Linq;
    using Traiding.Core.Models;
    using Traiding.Core.Repositories;

    public class ShareTableRepository : IShareTableRepository
    {
        private readonly StockExchangeDBContext dBContext;

        public ShareTableRepository(StockExchangeDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public void Add(ShareEntity entity)
        {
            this.dBContext.Shares.Add(entity);
        }

        public bool Contains(ShareEntity entity)
        {
            return this.dBContext.Shares.Any(s =>
            s.CompanyName == entity.CompanyName 
            && s.Type == entity.Type);
        }

        public bool ContainsById(int entityId)
        {
            return this.dBContext.Shares.Any(s => s.Id == entityId);
        }

        public ShareEntity Get(int entityId)
        {
            return this.dBContext.Shares.First(s => s.Id == entityId); // it will fall here if we can't find
        }

        public int GetSharesCount()
        {
            return this.dBContext.Shares.Count();
        }

        public void SaveChanges()
        {
            this.dBContext.SaveChanges();
        }

        public void SetCompanyName(int entityId, string newCompanyName)
        {
            var share = this.dBContext.Shares.First(s => s.Id == entityId); // it will fall here if we can't find
            share.CompanyName = newCompanyName;
        }

        public void SetType(int entityId, ShareTypeEntity newType)
        {
            var share = this.dBContext.Shares.First(s => s.Id == entityId); // it will fall here if we can't find
            share.Type = newType;
        }
    }
}
