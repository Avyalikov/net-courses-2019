namespace Traiding.Core.Repositories
{
    using Traiding.Core.Models;

    public interface IShareTypeTableRepository
    {
        bool Contains(ShareTypeEntity entity);
        void Add(ShareTypeEntity entity);
        void SaveChanges();
        ShareTypeEntity Get(int entityId);
        void SetName(int entityId, string newName);
        void SetCost(int entityId, decimal newCost);
        bool ContainsById(int shareTypeId);
    }
}
