using System;
using System.Collections.Generic;
using System.Text;
using TradingSimulator.Core.Models;

namespace TradingSimulator.Core.Repositories
{
    public interface IUserTableRepository
    {
        IEnumerable<UserEntity> Get();
        UserEntity Add(UserEntity user);
        UserEntity Delete(int id);
        UserEntity Put(UserEntity user);
        IEnumerable<UserEntity> GetUserBlackList();
        IEnumerable<UserEntity> GetUserOrangeList();
    }
}
