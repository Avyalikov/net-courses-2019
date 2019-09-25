namespace TradingApp.View.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using TradingApp.Core.Models;
    using TradingApp.Core.Repositories;

    public class UserTableRepository : IUserTableRepository
    {
        private readonly TradingAppDb db;

        public UserTableRepository(TradingAppDb db)
        {
            this.db = db;
        }

        public void Add(UserEntity entity)
        {
            this.db.Users.Add(entity);
        }

        public bool Contains(UserEntity userToAdd)
        {
            return this.db.Users.Any(
                u => u.Name == userToAdd.Name &&
                u.SurName == userToAdd.SurName &&
                u.Phone == userToAdd.Phone &&
                u.Balance == userToAdd.Balance
            );
        }

        public int Count()
        {
            return this.db.Users.Count();
        }

        public ICollection<UserEntity> GetAllUsers()
        {
            return this.db.Users.ToList();
        }

        public ICollection<UserEntity> GetAllUsersWithNegativeBalance()
        {
            return db.Users.Where(u => u.Balance < 0).ToList();
        }

        public ICollection<UserEntity> GetAllUsersWithZero()
        {
            return db.Users.Where(u => u.Balance == 0).ToList();
        }

        public UserEntity GetUserById(int id)
        {
            return db.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        public void SaveChanges()
        {
            this.db.SaveChanges();
        }
    }
}
