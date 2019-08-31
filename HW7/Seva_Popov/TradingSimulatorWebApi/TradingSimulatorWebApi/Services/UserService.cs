using System;
using System.Collections.Generic;
using System.Text;
using TradingSimulator.Core.Models;
using TradingSimulatorWebApi.Data;
using TradingSimulatorWebApi.Repositories;

namespace TradingSimulator.Core.Services
{
    public class UserService : IUserService
    {
        private readonly TradingSimulatorDbContext dbContext;

        public UserService(TradingSimulatorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<UserEntity> Get()
        {
            UserTableRepository userTableRepository = new UserTableRepository(dbContext);
            return userTableRepository.Get();
        }
        public UserEntity Add(UserEntity user)
        {
            UserTableRepository userTableRepository = new UserTableRepository(dbContext);
            userTableRepository.Add(user);
            return user;
        }

        public UserEntity Delete(int id)
        {
            UserTableRepository userTableRepository = new UserTableRepository(dbContext);
            return userTableRepository.Delete(id);
        }

        public UserEntity Put(UserEntity user)
        {
            UserTableRepository userTableRepository = new UserTableRepository(dbContext);
            return userTableRepository.Put(user);
        }
    }
}
