namespace TradingApp.View.View
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using TradingApp.Core.Dto;
    using TradingApp.Core.Models;
    using TradingApp.View.Interface;

    class UserView
    {
        private readonly IPhraseProvider phraseProvider;
        private readonly IIOProvider iOProvider;

        public UserView(IPhraseProvider phraseProvider, IIOProvider iOProvider)
        {
            this.phraseProvider = phraseProvider;
            this.iOProvider = iOProvider;
        }

        public void PrinaAllUsers(ICollection<UserEntity> users)
        {
            iOProvider.Clear();
            StringBuilder result = new StringBuilder();
            foreach (var user in users)
            {
                result.AppendLine($"Клиент {user.Name} {user.SurName} имеет баланс {user.Balance} и телефон {user.Phone}");
                if (user.UsersShares.Count == 0)
                    result.AppendLine($"\tУ пользователя еще нет акций");
                else
                    foreach (var item in user.UsersShares)
                    {
                        result.AppendLine($"\t{item.Share.Name} в кол-ве {item.Amount} по цене {item.Share.Price}");
                    }
                result.AppendLine();
            }
            result.AppendLine(phraseProvider.GetPhrase("BackToMain"));
            iOProvider.WriteLine(result.ToString());
        }

        public void PrintAllUsersInOrange(ICollection<UserEntity> users)
        {
            iOProvider.Clear();
            if (users.Count() == 0)
            {
                iOProvider.WriteLine(phraseProvider.GetPhrase("OrangeZoneNull"));
                iOProvider.WriteLine(phraseProvider.GetPhrase("BackToMain"));
            }
            else
            {
                StringBuilder result = new StringBuilder();
                result.AppendLine(phraseProvider.GetPhrase("OrangeZoneList"));
                foreach (var user in users)
                {
                    result.AppendLine($"Клиент {user.Name} {user.SurName} имеет баланс {user.Balance} и телефон {user.Phone}");
                }
                result.AppendLine(phraseProvider.GetPhrase("BackToMain"));
                iOProvider.WriteLine(result.ToString());
            }           
        }

        public void PrintAllUsersInBlack(ICollection<UserEntity> users)
        {
            iOProvider.Clear();
            if (users.Count() == 0)
            {
                iOProvider.WriteLine(phraseProvider.GetPhrase("BlackZoneNull"));
                iOProvider.WriteLine(phraseProvider.GetPhrase("BackToMain"));
            }
            else
            {
                StringBuilder result = new StringBuilder();
                result.AppendLine(phraseProvider.GetPhrase("BlackZoneList"));
                foreach (var user in users)
                {
                    result.AppendLine($"Клиент {user.Name} {user.SurName} имеет баланс {user.Balance} и телефон {user.Phone}");
                }
                iOProvider.WriteLine(phraseProvider.GetPhrase("BackToMain"));
                iOProvider.WriteLine(result.ToString());
            }
        }

        public UserInfo CreateUser()
        {
            UserInfo user = new UserInfo()
            {
                SurName = EnterSurname(),
                Name = EnterName(),
                Balance = EnterBalance(),
                Phone = EnterPhone()
            };
            return user;
        }

        private string EnterSurname(bool Valid = false)
        {
            iOProvider.Clear();
            iOProvider.WriteLine(phraseProvider.GetPhrase("EnterSurname"));
            if (Valid)
                iOProvider.WriteLine(phraseProvider.GetPhrase("InputError"));

            string value;
            if (string.IsNullOrWhiteSpace(value = iOProvider.ReadLine()))
                return EnterName(true);
            else
                return value;
        }

        private string EnterName(bool Valid = false)
        {
            iOProvider.WriteLine(phraseProvider.GetPhrase("EnterName"));
            if (Valid)
                iOProvider.WriteLine(phraseProvider.GetPhrase("InputError"));

            string value;
            if (string.IsNullOrWhiteSpace(value = iOProvider.ReadLine()))
                return EnterName(true);
            else
                return value;
        }

        private string EnterPhone(bool Valid = false)
        {
            iOProvider.WriteLine(phraseProvider.GetPhrase("EnterPhone"));
            if (Valid)
                iOProvider.WriteLine(phraseProvider.GetPhrase("InputError"));

            string value;
            if (string.IsNullOrWhiteSpace(value = iOProvider.ReadLine()) ||
                value.Any(code => code < '0' || code > '9'))
                return EnterPhone(true);
            else
                return value;
        }

        private decimal EnterBalance(bool Valid = false)
        {
            iOProvider.WriteLine(phraseProvider.GetPhrase("EnterBalance"));
            if (Valid)
                iOProvider.WriteLine(phraseProvider.GetPhrase("InputError"));
            
            if (decimal.TryParse(iOProvider.ReadLine(), out decimal balance))
                if (balance >= 0)
                    return balance;
            return EnterBalance(true);
        }
    }

}

