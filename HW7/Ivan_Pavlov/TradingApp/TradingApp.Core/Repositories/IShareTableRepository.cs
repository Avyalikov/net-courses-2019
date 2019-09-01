namespace TradingApp.Core.Repositories
{
    using System.Collections.Generic;
    using TradingApp.Core.Models;

    public interface IShareTableRepository
    {
        ICollection<ShareEntity> GetAllShares();
        ICollection<ShareEntity> GetUsersShares(int userId);
        void Add(ShareEntity shareToAdd);
        void SaveChanges();
        bool Contains(ShareEntity entity);
        ShareEntity GetShareById(int id);
        void Remove(int id);
    }
}
