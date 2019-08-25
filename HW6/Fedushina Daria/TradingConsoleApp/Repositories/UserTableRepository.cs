using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingApp.Core.Models;
using TradingApp.Core.Repositories;

namespace TradingConsoleApp.Repositories
{
    class UserTableRepository : IUserTableRepository
    {
        private readonly TradingAppDbContext dbContext;

        public UserTableRepository(TradingAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(UserEntity entity)
        {
            this.dbContext.Users.Add(entity);
        }

        public bool Contains(UserEntity entity)
        {
            return this.dbContext.Users.Any(f =>
            f.Name == entity.Name &&
            f.Surname == entity.Surname &&
            f.PhoneNumber == entity.PhoneNumber);
        }

        public bool Contains(string entityId)
        {
            var entity = this.dbContext.Users.Find(entityId);
            return this.dbContext.Users.Any(f=>
            f.Name == entity.Name &&
            f.ID == entity.ID &&
            f.Surname == entity.Surname &&
            f.PhoneNumber == entity.PhoneNumber);
        }

        public UserEntity Get(string userId)
        {
            return this.dbContext.Users.First(f=>f.ID==userId);
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }
    }
}
