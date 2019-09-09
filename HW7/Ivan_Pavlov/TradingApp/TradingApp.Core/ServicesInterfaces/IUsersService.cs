namespace TradingApp.Core.ServicesInterfaces
{
    using System.Collections.Generic;
    using TradingApp.Core.Dto;
    using TradingApp.Core.Models;

    public interface IUsersService
    {
        int AddNewUser(UserInfo args);
        void ChangeUserBalance(int id, decimal value);
        ICollection<UserEntity> GetAllUsers();
        ICollection<UserEntity> GetAllUsersWithNegativeBalance();
        ICollection<UserEntity> GetAllUsersWithZero();
        UserEntity GetCustomer(int userId, int sellerId);
        UserEntity GetSeller(int userId);
        UserEntity GetUserById(int id);
        void Update(int id, UserInfo user);
        void Remove(int id);
    }
}