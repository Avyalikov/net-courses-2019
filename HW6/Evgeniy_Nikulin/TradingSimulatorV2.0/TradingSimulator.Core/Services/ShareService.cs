namespace TradingSimulator.Core.Services
{
    using Dto;
    using Interfaces;
    using Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ShareService : IShareService
    {
        private readonly IPhraseProvider provider;
        private readonly IShareRepository shareRep;
        private readonly ITraderRepository traderRep;
        private readonly ILoggerService logger;

        public ShareService(
            IPhraseProvider provider,
            IShareRepository shareRep,
            ITraderRepository traderRep,
            ILoggerService logger)
        {
            this.provider = provider;
            this.shareRep = shareRep;
            this.traderRep = traderRep;
            this.logger = logger;
        }

        public List<Share> GetShareList(string ownerId)
        {
            int OwnerId;
            if (!int.TryParse(ownerId, out OwnerId))
            {
                return null;
            }

            return shareRep.GetShareList(OwnerId);
        }

        public Share GetShareByIndex(int ownerId, int index)
        {
            var shares = shareRep.GetShareList(ownerId);

            if (shares.Count - 1 < index)
            {
                throw new IndexOutOfRangeException($"At trader with ID {ownerId} have {shares.Count} shares, but you try to get {index + 1}");
            }

            return shares[index];
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

            Share share = new Share()
            {
                name = shareName,
                price = Price,
                quantity = Quantity,
                ownerId = OwnerId,
            };

            var owner = traderRep.GetTrader(OwnerId);
            logger.Info($"{quantity} of {shareName} shares with {price} price is added to {owner.name} {owner.surname} trader");

            shareRep.Push(share);
            shareRep.SaveChanges();

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

            Share share = new Share()
            {
                id = ShareId,
                name = newName,
                price = NewPrice,
                ownerId = OwnerId,
            };

            var owner = traderRep.GetTrader(OwnerId);
            var oldShare = shareRep.GetShare(ShareId);
            logger.Info($"Share {oldShare.name} changed to {newName} with {newPrice} price at {owner.name} {owner.surname} trader");

            shareRep.Push(share);
            shareRep.SaveChanges();

            return provider.GetPhrase(Phrase.Success);
        }
    }
}