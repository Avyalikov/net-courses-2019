namespace TradingApp.Logic
{
    using System;
    using System.Linq;
    using TradingApp.Data;
    using TradingApp.Data.Models;

    public class UserLogic
    {
        private readonly IAppDbContext dbProvider;

        public UserLogic(IAppDbContext dbProvider)
        {
            this.dbProvider = dbProvider;
        }

        public IQueryable<User> ListUsers()
        {
            var users = dbProvider.Users
                .Include("UserShare.Share");

            return users;
        }

        public IQueryable<User> OrangeZone()
        {
            var orangeZone = dbProvider.Users
                .Include("UserShare.Share")
                .Where(u => u.Balance == 0);

            return orangeZone;
        }

        public IQueryable<User> BlackZone()
        {
            return dbProvider.Users
                .Include("UserShare.Share")
                .Where(u => u.Balance < 0);
        }

        public string AddUser(User user)
        {         
            dbProvider.Users.Add(user);
            dbProvider.SaveChanges();
            return $"ДОБАВЛЕН НОВЫЙ ПОЛЬЗОВАТЕЛЬ: {user.SurName} {user.Name} с балансом {user.Balance} и телефоном {user.Phone}";
        }
    }
}