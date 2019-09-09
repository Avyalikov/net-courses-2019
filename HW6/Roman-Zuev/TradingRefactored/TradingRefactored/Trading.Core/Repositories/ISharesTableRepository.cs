using Trading.Core.Models;

namespace Trading.Core.Repositories
{
    public interface ISharesTableRepository
    {
        bool ContainsById(int sharesId);
        SharesEntity GetById(int sharesId);
        int Count { get; }
        SharesEntity this[int i] { get; }
    }
}