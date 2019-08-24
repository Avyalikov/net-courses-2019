namespace Traiding.Core.Repositories
{
    using Traiding.Core.Models;

    public interface IBlockedMoneyTableRepository
    {
        // bool Contains(BlockedMoneyEntity entity); // Compare by 
        void Add(BlockedMoneyEntity entity);
        void SaveChanges();
        // bool ContainsById(int entityId);
        // BlockedMoneyEntity Get(int entityId);
        void Remove(int entityId);
    }
}
