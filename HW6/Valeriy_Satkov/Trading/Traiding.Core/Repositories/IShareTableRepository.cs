namespace Traiding.Core.Repositories
{
    using Traiding.Core.Models;

    public interface IShareTableRepository
    {
        bool Contains(ShareEntity entity);
        void Add(ShareEntity entity);
        void SaveChanges();        
        bool ContainsById(int entityId);
        ShareEntity Get(int entityId);
        void SetCompanyName(int entityId, string newCompanyName);
        void SetType(int entityId, ShareTypeEntity newType);
        int GetSharesCount();
    }
}
