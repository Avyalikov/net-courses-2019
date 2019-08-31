using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Repositories;
using TradingSimulatorWebApi.Data;

namespace TradingSimulatorWebApi.Repositories
{
    public class UserTableRepository : IUserTableRepository
    {
        private readonly TradingSimulatorDbContext dbContext;

        public UserTableRepository(TradingSimulatorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<UserEntity> Get()
        {
            return dbContext.Users.ToList();
        }

        public UserEntity Add(UserEntity user)
        {
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            return user;
        }

        public UserEntity Delete(int id)
        {
            UserEntity user = dbContext.Users.FirstOrDefault(x => x.Id == id);
            dbContext.Users.Remove(user);
            dbContext.SaveChanges();
            return user;
        }

        public UserEntity Put(UserEntity user)
        {
            dbContext.Update(user);
            dbContext.SaveChanges();
            return user;
        }
    }
}
