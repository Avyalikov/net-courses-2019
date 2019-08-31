using System;
using System.Collections.Generic;
using System.Text;
using TradingSimulator.Core.Models;

namespace TradingSimulator.Core.Services
{
    public interface IUserService
    {
        IEnumerable<UserEntity> Get();
        UserEntity Add(UserEntity user);
        UserEntity Delete(int id);
        UserEntity Put(UserEntity user);
        IEnumerable<UserEntity> GetUserBlackList();
        IEnumerable<UserEntity> GetUserOrangeList();
    }
}
