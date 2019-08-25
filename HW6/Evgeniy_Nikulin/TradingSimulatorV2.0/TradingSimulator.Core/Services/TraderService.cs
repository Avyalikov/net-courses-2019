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
        private readonly ILoggerService logger;

        public TraderService(
            IPhraseProvider provider,
            ITraderRepository traderRep,
            ILoggerService logger)
        {
            this.provider = provider;
            this.traderRep = traderRep;
            this.logger = logger;
        }

        public List<Trader> TradersList { get => traderRep.GetTradersList(); }
        public List<Trader> WhiteList { get => this.TradersList.Where(tl => tl.money > 0).ToList(); }
        public List<Trader> OrangeList { get => this.TradersList.Where(tl => tl.money == 0).ToList(); }
        public List<Trader> BlackList { get => this.TradersList.Where(tl => tl.money < 0).ToList(); }

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
    }
}

