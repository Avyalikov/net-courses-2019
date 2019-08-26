namespace TradingSimulator.Core.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using System;
    using System.Collections.Generic;
    using TradingSimulator.Core.Dto;
    using TradingSimulator.Core.Interfaces;
    using TradingSimulator.Core.Repositories;
    using TradingSimulator.Core.Services;

    [TestClass]
    public class ShareServiceTests
    {
        private IPhraseProvider provider;
        private IShareRepository shareRep;
        private ITraderRepository traderRep;
        private ILoggerService logger;

        [TestInitialize]
        public void Initialize()
        {
            this.provider = Substitute.For<IPhraseProvider>();
            this.shareRep = Substitute.For<IShareRepository>();
            this.traderRep = Substitute.For<ITraderRepository>();
            this.logger = Substitute.For<ILoggerService>();
        }

        [TestMethod]
        public void ShouldGetShareList()
        {
            // Arrange
            ShareService transactionService = new ShareService(
                provider,
                shareRep,
                traderRep,
                logger);


            // Act
            transactionService.GetShareList("1");

            // Assert
            shareRep.Received(1).GetShareList(1);
        }
        [TestMethod]
        public void ShouldNotGetShareList()
        {
            // Arrange
            ShareService transactionService = new ShareService(
                this.provider,
                this.shareRep,
                this.traderRep,
                this.logger);

            // Act
            transactionService.GetShareList("q");

            // Assert
            shareRep.Received(0).GetShareList(1);
        }

        [TestMethod]
        public void ShouldGetShareByIndex()
        {
            // Arrange
            ShareService transactionService = new ShareService(
                this.provider,
                this.shareRep,
                this.traderRep,
                this.logger);

            List<Share> shares = new List<Share>()
            {
                new Share()
                {
                    name = "Ubisoft",
                    price = 10m,
                    quantity = 10,
                    ownerId = 1,
                }
            };

            transactionService.GetShareList("1").Returns(shares);

            // Act
            var result = transactionService.GetShareByIndex(1, 0);

            // Assert
            Assert.AreEqual(shares[0], result);
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException), "At trader with ID 1 have 1 shares, but you try to get 3")]
        public void ShouldNotGetShareByIndex()
        {
            // Arrange
            ShareService transactionService = new ShareService(
                this.provider,
                this.shareRep,
                this.traderRep,
                this.logger);

            List<Share> shares = new List<Share>()
            {
                new Share()
                {
                    name = "Ubisoft",
                    price = 10m,
                    quantity = 10,
                    ownerId = 1,
                }
            };

            transactionService.GetShareList("1").Returns(shares);

            // Act
            var result = transactionService.GetShareByIndex(1, 2);
        }

        [TestMethod]
        public void ShouldNotAddShareWithIncorrectID()
        {
            // Arrange
            ShareService transactionService = new ShareService(
                this.provider,
                this.shareRep,
                this.traderRep,
                this.logger);

            // Act
            transactionService.AddShare("Microsoft", "100", "10", "q");

            // Assert
            provider.Received(1).GetPhrase(Phrase.IncorrectID);
        }
        [TestMethod]
        public void ShouldNotAddShareWithShareIsLetter()
        {
            // Arrange
            ShareService transactionService = new ShareService(
                this.provider,
                this.shareRep,
                this.traderRep,
                this.logger);

            // Act
            transactionService.AddShare("123", "100", "10", "1");

            // Assert
            provider.Received(1).GetPhrase(Phrase.ShareIsLetter);
        }
        [TestMethod]
        public void ShouldNotAddShareWithPriceIsLetter()
        {
            // Arrange
            ShareService transactionService = new ShareService(
                this.provider,
                this.shareRep,
                this.traderRep,
                this.logger);

            // Act
            transactionService.AddShare("Microsoft", "one hundred", "10", "1");

            // Assert
            provider.Received(1).GetPhrase(Phrase.PriceIsLetter);
        }
        [TestMethod]
        public void ShouldNotAddShareWithQuantityIsLetter()
        {
            // Arrange
            ShareService transactionService = new ShareService(
                this.provider,
                this.shareRep,
                this.traderRep,
                this.logger);

            // Act
            transactionService.AddShare("Microsoft", "100", "ten", "1");

            // Assert
            provider.Received(1).GetPhrase(Phrase.QuantityIsLetter);
        }
        [TestMethod]
        public void ShouldAddShare()
        {
            // Arrange
            ShareService transactionService = new ShareService(
                this.provider,
                this.shareRep,
                this.traderRep,
                this.logger);

            Trader owner = new Trader()
            {
                name = "Evgeniy",
                surname = "Nikulin",
            };

            traderRep.GetTrader(Arg.Any<int>()).Returns(owner);

            // Act
            string shareName = "Microsoft";
            string price = "100";
            string quantity = "1";
            string ownerId = "1";

            transactionService.AddShare(shareName, price, quantity, ownerId);

            // Assert
            shareRep.Received(1).Push(Arg.Any<Share>());
            shareRep.Received(1).SaveChanges();
            logger.Info($"{quantity} of {shareName} shares with {price} price is added to {owner.name} {owner.surname} trader");
            provider.Received(1).GetPhrase(Phrase.Success);
        }

        [TestMethod]
        public void ShouldNotChangeShareWithIncorrectShareID()
        {
            // Arrange
            ShareService transactionService = new ShareService(
                this.provider,
                this.shareRep,
                this.traderRep,
                this.logger);

            // Act
            transactionService.ChangeShare("q", "Microsoft", "100", "1");

            // Assert
            provider.Received(1).GetPhrase(Phrase.IncorrectID);
        }
        [TestMethod]
        public void ShouldNotChangeShareWithIncorrectOwnerID()
        {
            // Arrange
            ShareService transactionService = new ShareService(
                this.provider,
                this.shareRep,
                this.traderRep,
                this.logger);

            // Act
            transactionService.ChangeShare("1", "Microsoft", "100", "q");

            // Assert
            provider.Received(1).GetPhrase(Phrase.IncorrectID);
        }
        [TestMethod]
        public void ShouldNotChangeShareWithShareIsLetter()
        {
            // Arrange
            ShareService transactionService = new ShareService(
                this.provider,
                this.shareRep,
                this.traderRep,
                this.logger);

            // Act
            transactionService.ChangeShare("1", "123", "100", "1");

            // Assert
            provider.Received(1).GetPhrase(Phrase.ShareIsLetter);
        }
        [TestMethod]
        public void ShouldNotChangeShareWithPriceIsLetter()
        {
            // Arrange
            ShareService transactionService = new ShareService(
                this.provider,
                this.shareRep,
                this.traderRep,
                this.logger);

            // Act
            transactionService.ChangeShare("1", "Microsoft", "hundred", "1");

            // Assert
            provider.Received(1).GetPhrase(Phrase.PriceIsLetter);
        }
        [TestMethod]
        public void ShouldChangeShare()
        {
            // Arrange
            ShareService transactionService = new ShareService(
                this.provider,
                this.shareRep,
                this.traderRep,
                this.logger);

            Trader owner = new Trader()
            {
                name = "Evgeniy",
                surname = "Nikulin",
            };
            Share oldShare = new Share()
            {
                name = "Ubisoft",
                price = 50m,
                quantity = 10,
                ownerId = 1,
            };

            traderRep.GetTrader(Arg.Any<int>()).Returns(owner);
            shareRep.GetShare(Arg.Any<int>()).Returns(oldShare);

            // Act
            string shareId = "1";
            string newName = "Microsoft";
            string newPrice = "100";
            string ownerId = "1";

            transactionService.ChangeShare(shareId, newName, newPrice, ownerId);

            // Assert
            shareRep.Received(1).Push(Arg.Any<Share>());
            shareRep.Received(1).SaveChanges();
            logger.Info($"Share {oldShare.name} changed to {newName} with {newPrice} price at {owner.name} {owner.surname} trader");
            provider.Received(1).GetPhrase(Phrase.Success);
        }
    }
}