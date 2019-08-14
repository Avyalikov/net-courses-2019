namespace Trading.Logic
{
    using System;
    using System.Linq;
    using System.Text;
    using Trading.Infrastructure;
    using Trading.View;

    static class User
    {
        public static string ListUsers()
        {
            StringBuilder sb = new StringBuilder();

            using (AppDbContext db = new AppDbContext())
            {
                var InfoByUsers = db.Users.Include("UserStocks.Stock");
              
                foreach (var item in InfoByUsers)
                {
                    sb.AppendLine(item.ToString());
                }
            }
            return sb.ToString();
        }

        public static void AddUser()
        {
            Models.User user = new Models.User
            {
                SurName = ValidStringValue(CreateUser.EnterSurname, false),

                Name = ValidStringValue(CreateUser.EnterName, false),

                Phone = ValidPhoneValue(CreateUser.EnterPhone, false),

                Balance = ValidBalanceValue(CreateUser.EnterBalance, false)
            };

            using (AppDbContext db = new AppDbContext())
            {
                db.Users.Add(user);
                db.SaveChanges();                
                CreateUser.UserCreated(user.ToString());
                Logger.Log.Info("add new user" + user.ToString());
            }
        }

        private static string ValidStringValue(Func<bool, string> func, bool check) 
        {
            string value;
            value = func(check);
            if (string.IsNullOrWhiteSpace(value))
                return ValidStringValue(func, true);
            return value;
        }

        private static string ValidPhoneValue(Func<bool, string> func, bool check)
        {
            string value;
            value = ValidStringValue(func, check);
            if (value.Any(code => code < '0' || code > '9'))
                return ValidPhoneValue(func, true);
            return value;
        }

        private static decimal ValidBalanceValue(Func<bool, string> func, bool check)
        {
            string value;
            value = ValidStringValue(func, check);
            if (decimal.TryParse(value, out decimal balance))
                return balance;
            return ValidBalanceValue(func, true);
        }
    }
}
