namespace TradingApp.Logic
{
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
            var users = dbProvider.Users.ToList();

            return users.AsQueryable();
        }

        public IQueryable<User> OrangeZone()
        {
            var orangeZone = dbProvider.Users
                .Where(u => u.Balance == 0)
                .ToList();

            return orangeZone.AsQueryable();
        }

        public IQueryable<User> BlackZone()
        {
            var blackZone = dbProvider.Users
                .Where(u => u.Balance < 0)
                .ToList();
            return blackZone.AsQueryable();
        }

        public string AddUser(User user)
        {         
            dbProvider.Users.Add(user);
            dbProvider.SaveChanges();
            return $"ДОБАВЛЕН НОВЫЙ ПОЛЬЗОВАТЕЛЬ: {user.SurName} {user.Name} с балансом {user.Balance} и телефоном {user.Phone}";
        }
    }
}