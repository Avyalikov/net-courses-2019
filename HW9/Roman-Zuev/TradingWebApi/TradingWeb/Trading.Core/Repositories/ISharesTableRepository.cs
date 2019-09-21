using Trading.Core.Models;

namespace Trading.Core.Repositories
{
    public interface ISharesTableRepository
    {
        SharesEntity this[int i] { get; }

        int Count { get; }

        void Add(SharesEntity sharesToAdd);
        bool Contains(SharesEntity shares);
        bool ContainsById(int sharesId);
        SharesEntity GetById(int sharesId);
        void Remove(SharesEntity sharesToRemove);
        void SaveChanges();
        void Update(SharesEntity sharesToUpdate);
    }
}