using TradingApp.Core.Models;

namespace TradingApp.Core.Repositories
{
    public interface IUserTableRepository
    {
        bool Contains(UserEntity entity);
        bool Contains(string entityId);
        void Add(UserEntity entity);
        void SaveChanges();
        UserEntity Get(string userId);
    }
}