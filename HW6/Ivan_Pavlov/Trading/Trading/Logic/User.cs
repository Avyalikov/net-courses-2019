namespace Trading.Logic
{
    using System;
    using System.Linq;
    using System.Text;
    using TradingData;
    using TradingView;
    using TradingView.Interface;

    internal static class User
    {
        private static readonly IView viewProvider = SettingsByLayers.viewProvider;
        private static readonly IDbProvider dbProvider = SettingsByLayers.dbProvider;

        public static string ListUsers()
        {
            StringBuilder sb = new StringBuilder();
            var InfoByUsers = dbProvider.ListUsers();
            foreach (var item in InfoByUsers)
            {
                sb.AppendLine(item.ToString());
            }
            return sb.ToString();
        }

        public static string OrangeZone()
        {
            StringBuilder sb = new StringBuilder();
            var Zone = dbProvider.OrangeZone();
                foreach (var item in Zone)
                {
                    sb.AppendLine(item.ToString());
                }            
            return sb.ToString();
        }

        public static string BlackZone()
        {
            StringBuilder sb = new StringBuilder();
            var Zone = dbProvider.BlackZone();
            foreach (var item in Zone)
            {
                sb.AppendLine(item.ToString());
            }
            return sb.ToString();
        }

        public static void AddUser()
        {
            TradingData.Models.User user = new TradingData.Models.User
            {
                SurName = ValidStringValue(viewProvider.EnterSurname, false),
                Name = ValidStringValue(viewProvider.EnterName, false),
                Phone = ValidPhoneValue(viewProvider.EnterPhone, false),
                Balance = ValidBalanceValue(viewProvider.EnterBalance, false)
            };

            dbProvider.AddUser(user);
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
                if (balance >= 0)
                    return balance;
            return ValidBalanceValue(func, true);
        }
    }
}
