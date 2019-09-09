namespace TradingApp.Core.Repositories
{
    using System.Collections.Generic;
    using TradingApp.Core.Models;

    public interface IUserTableRepository
    {
        void Add(UserEntity entity);
        void SaveChanges();
        bool Contains(UserEntity userToAdd);
        ICollection<UserEntity> GetAllUsers();
        UserEntity GetUserById(int id);
        ICollection<UserEntity> GetAllUsersWithZero();
        ICollection<UserEntity> GetAllUsersWithNegativeBalance();
        int Count();
        void Remove(int id);
    }
}
