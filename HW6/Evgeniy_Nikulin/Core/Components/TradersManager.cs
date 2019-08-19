namespace Core.Components
{
    using Core.Model;
    using Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    public class TradersManager : ITradersManager
    {
        private readonly IDbController db;
        private readonly IPhraseProvider provider;

        public List<Trader> TradersList { get => db.GetTradersList(); }
        public List<Trader> WhiteList { get => TradersList.Where(t => t.Reputation == provider.GetPhrase(Phrase.White)).Select(t => t).ToList(); }
        public List<Trader> OrangeList { get => TradersList.Where(t => t.Reputation == provider.GetPhrase(Phrase.Orange)).Select(t => t).ToList(); }
        public List<Trader> BlackList { get => TradersList.Where(t => t.Reputation == provider.GetPhrase(Phrase.Black)).Select(t => t).ToList(); }

        public TradersManager(IDbController db, IPhraseProvider provider)
        {
            this.db = db;
            this.provider = provider;
        }

        public string AddTrader(string Name, string Surname, string Phone, string money)
        {
            if (Name == string.Empty || Surname == string.Empty)
            {
                return provider.GetPhrase(Phrase.EmptyName);
            }
            string fullName = $"{Name}{Surname}";
            if (!fullName.Any(char.IsLetter))
            {
                return provider.GetPhrase(Phrase.NameNotLetter);
            }
            if (fullName.Count() > 20)
            {
                return provider.GetPhrase(Phrase.LongName);
            }
            if (!Phone.StartsWith("+"))
            {
                return provider.GetPhrase(Phrase.PhonePlus);
            }
            if (!Phone.Contains("("))
            {
                return provider.GetPhrase(Phrase.PhoneRegion);
            }
            if (Phone.Any(char.IsLetter))
            {
                return provider.GetPhrase(Phrase.PhoneIsLetter);
            }

            decimal Money = default;
            if (!decimal.TryParse(money, out Money))
            {
                return provider.GetPhrase(Phrase.MoneyIsNumber);
            }

            db.AddTrader(
                Name,
                Surname,
                Phone,
                Money,
                Money > 0m ? provider.GetPhrase(Phrase.White) :
                    Money == 0m ? provider.GetPhrase(Phrase.Orange) : provider.GetPhrase(Phrase.Black));

            return provider.GetPhrase(Phrase.Success);
        }

        public string AddShare(string shareName, string price, string quantity, string ownerId)
        {
            int OwnerId;
            if (!int.TryParse(ownerId, out OwnerId))
            {
                return provider.GetPhrase(Phrase.IncorrectID);
            }
            if (!shareName.Any(char.IsLetter))
            {
                return provider.GetPhrase(Phrase.ShareIsLetter);
            }
            decimal Price = default;
            if (!decimal.TryParse(price, out Price))
            {
                return provider.GetPhrase(Phrase.PriceIsLetter);
            }
            int Quantity = default;
            if (!int.TryParse(quantity, out Quantity))
            {
                return provider.GetPhrase(Phrase.QuantityIsLetter);
            }

            db.AddShareToTrader(shareName, Price, Quantity, OwnerId);

            return provider.GetPhrase(Phrase.Success);
        }

        public string ChangeShare(string shareId, string newName, string newPrice, string ownerId)
        {
            int ShareId;
            if (!int.TryParse(shareId, out ShareId))
            {
                return provider.GetPhrase(Phrase.IncorrectID);
            }
            int OwnerId;
            if (!int.TryParse(ownerId, out OwnerId))
            {
                return provider.GetPhrase(Phrase.IncorrectID);
            }
            if (!newName.Any(char.IsLetter))
            {
                return provider.GetPhrase(Phrase.ShareIsLetter);
            }
            decimal NewPrice = default;
            if (!decimal.TryParse(newPrice, out NewPrice))
            {
                return provider.GetPhrase(Phrase.PriceIsLetter);
            }

            db.ChangeShare(ShareId, newName, NewPrice, OwnerId);

            return provider.GetPhrase(Phrase.Success);
        }

        public List<Share> GetShareList(string ownerId)
        {
            int OwnerId;
            if (!int.TryParse(ownerId, out OwnerId))
            {
                return null;
            }

            return db.GetShareList(OwnerId);
        }
    }
}