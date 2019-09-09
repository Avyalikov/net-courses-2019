using Trading.Core.Models;

namespace Trading.Core.Repositories
{
    public interface IClientSharesTableRepository
    {
        void SaveChanges();
        void Add(ClientSharesEntity newShares);
        void Update(ClientSharesEntity clientShares);
    }
}