using System.Collections.Generic;
using TradingApp.Core.Dto;
using TradingApp.Core.Models;

namespace TradingApp.Core.Repositories
{
    public interface IUserTableRepository
    {
        bool Contains(UserEntity entity);
        bool Contains(int entityId);
        void Add(UserEntity entity);
        void SaveChanges();
        UserEntity Get(int userId);
        int GetId(UserRegistrationInfo userInfo);
        bool ContainsInfo(UserRegistrationInfo userInfo);
        int GetAll();
    }
}