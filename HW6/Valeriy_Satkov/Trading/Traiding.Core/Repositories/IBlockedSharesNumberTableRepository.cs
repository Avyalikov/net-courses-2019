namespace Traiding.Core.Repositories
{
    using Traiding.Core.Models;

    public interface IBlockedSharesNumberTableRepository
    {
        //bool Contains(BlockedSharesNumberEntity entity); // Compare by 
        void Add(BlockedSharesNumberEntity entity);
        void SaveChanges();
        // bool ContainsById(int entityId);
        // BlockedSharesNumberEntity Get(int entityId);
        void Remove(int entityId);
    }
}
