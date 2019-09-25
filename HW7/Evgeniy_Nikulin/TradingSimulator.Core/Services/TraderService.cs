namespace TradingSimulator.Core.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Dto;
    using Interfaces;
    using Repositories;

    public class TraderService : ITraderService
    {
        private readonly IPhraseProvider provider;
        private readonly ITraderRepository traderRep;
        private readonly IShareRepository shareRep;
        private readonly ILoggerService logger;

        public TraderService(
            IPhraseProvider provider,
            ITraderRepository traderRep,
            IShareRepository shareRep,
            ILoggerService logger)
        {
            this.provider = provider;
            this.traderRep = traderRep;
            this.shareRep = shareRep;
            this.logger = logger;
        }

        public TraderService(IPhraseProvider provider, ITraderRepository traderRep, ILoggerService logger)
        {
            this.provider = provider;
            this.traderRep = traderRep;
            this.logger = logger;
        }

        public List<Trader> TradersList { get => this.traderRep.GetTradersList(); }
        public List<Trader> GreenList { get => this.TradersList.Where(tl => tl.money > 0).ToList(); }
        public List<Trader> OrangeList { get => this.TradersList.Where(tl => tl.money == 0).ToList(); }
        public List<Trader> BlackList { get => this.TradersList.Where(tl => tl.money < 0).ToList(); }

        public List<Trader> GetTradersPerPage(int top, int page)
        {
            var traders = this.TradersList;

            var temp = new List<Trader>();

            int start = top * (page - 1);

            for (int i = start; i < start + top; i++)
            {
                try
                {
                    temp.Add(traders[i]);
                }
                catch
                {
                    break;
                }
            }

            return temp;
        }

        public int GetTraderCount()
        {
            return traderRep.GetTraderCount();
        }

        public string GetTraderStatus(int TraderId)
        {
            Trader trader;
            try
            {
                trader = traderRep.GetTrader(TraderId);
            }
            catch (System.Exception e)
            {
                return e.Message;
            }

            return trader.money == 0 ?
                "Orange status" :
                trader.money > 0 ?
                    "Green status" :
                    "Black status";
        }

        public List<Share> GetShareList(int traderId) =>
            shareRep.GetShareList(traderId);

        public string AddTrader(string Name, string Surname, string Phone, string money) 
        {
            if (Name == string.Empty || Surname == string.Empty)
            {
                return provider.GetPhrase(Phrase.EmptyName);
            }
            if (!Name.Any(char.IsLetter) || !Surname.Any(char.IsLetter))
            {
                return provider.GetPhrase(Phrase.NameNotLetter);
            }
            string fullName = $"{Name}{Surname}";
            if (fullName.Count() > 20)
            {
                return provider.GetPhrase(Phrase.LongName);
            }
            if (Phone.Any(char.IsLetter))
            {
                return provider.GetPhrase(Phrase.PhoneIsLetter);
            }
            if (!Phone.StartsWith("+"))
            {
                return provider.GetPhrase(Phrase.PhonePlus);
            }
            if (!Phone.Contains("("))
            {
                return provider.GetPhrase(Phrase.PhoneRegion);
            }

            decimal Money = default;
            if (!decimal.TryParse(money, out Money))
            {
                return provider.GetPhrase(Phrase.MoneyIsNumber);
            }

            Trader trader = new Trader()
            {
                name = Name,
                surname = Surname,
                phone = Phone,
                money = Money,
            };

            traderRep.Push(trader);
            traderRep.SaveChanges();

            logger.Info($"Trader {Name} {Surname} with {Phone} phone and {Money} money is added");

            return provider.GetPhrase(Phrase.Success);
        }

        public string ChangeTrader(int traderId, string newName, string newSurname, string newPhone, string newMoney)
        {
            var trader = traderRep.GetTrader(traderId);

            if (newName == string.Empty || newSurname == string.Empty)
            {
                return provider.GetPhrase(Phrase.EmptyName);
            }
            if (!newName.Any(char.IsLetter) || !newSurname.Any(char.IsLetter))
            {
                return provider.GetPhrase(Phrase.NameNotLetter);
            }
            string fullName = $"{newName}{newSurname}";
            if (fullName.Count() > 20)
            {
                return provider.GetPhrase(Phrase.LongName);
            }
            if (newPhone.Any(char.IsLetter))
            {
                return provider.GetPhrase(Phrase.PhoneIsLetter);
            }
            if (!newPhone.StartsWith("+"))
            {
                return provider.GetPhrase(Phrase.PhonePlus);
            }
            if (!newPhone.Contains("("))
            {
                return provider.GetPhrase(Phrase.PhoneRegion);
            }

            decimal Money = default;
            if (!decimal.TryParse(newMoney, out Money))
            {
                return provider.GetPhrase(Phrase.MoneyIsNumber);
            }

            trader.name = newName;
            trader.surname = newSurname;
            trader.phone = newPhone;
            trader.money = Money;

            traderRep.Push(trader);
            traderRep.SaveChanges();

            logger.Info($"Trader with {traderId}ID changed to {newName} {newSurname} with {newPhone} phone and {newMoney} money");

            return provider.GetPhrase(Phrase.Success);
        }

        public void Remove(int traderId) =>
            traderRep.Remove(traderRep.GetTrader(traderId));
    }
}