using System;
using System.Collections.Generic;
using System.Text;
using TradingSimulator.Core.Models;

namespace TradingSimulator.Core.Repositories
{
    public interface IUserTableRepository
    {
        bool Contains(UserEntity userEntity);
        bool ContainsById(int entityId);
        void Add(UserEntity userEntity);
        void SaveChanges();
        UserEntity Get(int userId);
    }
}
