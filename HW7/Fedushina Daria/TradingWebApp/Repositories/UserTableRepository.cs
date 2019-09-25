using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingApp.Core.Dto;
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

        public bool Contains(int entityId)
        {
            var entity = this.dbContext.Users.Find(entityId);
            return this.dbContext.Users.Any(f=>
            f.Name == entity.Name &&
            f.ID == entity.ID &&
            f.Surname == entity.Surname &&
            f.PhoneNumber == entity.PhoneNumber);
        }

        public bool ContainsInfo(UserRegistrationInfo userInfo)
        {
            return this.dbContext.Users.Any(f =>
            f.Name == userInfo.Name &&
            f.Surname == userInfo.Surname &&
            f.PhoneNumber == userInfo.PhoneNumber);
        }

        public int Count()
        {
            return this.dbContext.Users.Count();
        }

        public void Delete(int id)
        {
            var entity = this.dbContext.Users.Find(id);
            this.dbContext.Users.Remove(entity);
            this.dbContext.SaveChanges();
        }

        public UserEntity Get(int userId)
        {
            return this.dbContext.Users.First(f=>f.ID==userId);
        }

        public IEnumerable<UserEntity> GetAll()
        {
            List<UserEntity> arr = new List<UserEntity>();
            for (int i = 1; i < this.dbContext.Users.Count()+1; i++)
            {
                arr.Add(this.dbContext.Users.Find(i));
            }
            return arr;
        }

        public int GetId(UserRegistrationInfo userInfo)
        {
            var entity = this.dbContext.Users.First(f =>
            f.Name == userInfo.Name &&
            f.Surname == userInfo.Surname &&
            f.PhoneNumber == userInfo.PhoneNumber);
            return entity.ID;
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }

        public bool Update(UserEntity user)
        {
            var result = this.dbContext.Users.SingleOrDefault(f => f.ID == user.ID);
            if (result != null)
            {
                try
                {
                    result.Name = user.Name;
                    result.Surname = user.Surname;
                    result.PhoneNumber = user.PhoneNumber;
                    this.dbContext.SaveChanges();
                    return true;
                }
                catch
                {
                    throw new ArgumentException("Can't add new user to the base");
                }
            }
            else
            {
                throw new ArgumentException($"Can't find user with ID {user.ID}");
            }
            
            
        }
    }
}
