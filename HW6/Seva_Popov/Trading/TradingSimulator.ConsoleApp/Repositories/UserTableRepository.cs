using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Repositories;

namespace TradingSimulator.ConsoleApp.Repositories
{
    public class UserTableRepository : IUserTableRepository
    {
        private readonly TradingSimulatorDbContext dbContext;

        public UserTableRepository(TradingSimulatorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(UserEntity userEntity)
        {
            this.dbContext.Users.Add(userEntity);
        }

        public bool Contains(UserEntity userEntity)
        {
            return this.dbContext.Users.Any(f =>
           f.Name == userEntity.Name
           && f.Surname == userEntity.Surname
           && f.Phone == userEntity.Phone);
        }

        //public bool ContainsById(int entityId)
        //{
        //    throw new NotImplementedException();
        //}

        //public UserEntity Get(int userId)
        //{
        //    throw new NotImplementedException();
        //}

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }
    }
}
