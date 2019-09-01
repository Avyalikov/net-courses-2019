namespace TradingSimulator.Core.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using System.Collections.Generic;
    using TradingSimulator.Core.Dto;
    using TradingSimulator.Core.Interfaces;
    using TradingSimulator.Core.Repositories;
    using TradingSimulator.Core.Services;

    [TestClass]
    public class TraderServiceTests
    {
        private IPhraseProvider provider;
        private ITraderRepository traderRep;
        private ILoggerService logger;

        [TestInitialize]
        public void Initialize()
        {
            this.traderRep = Substitute.For<ITraderRepository>();
            this.provider = Substitute.For<IPhraseProvider>();
            this.logger = Substitute.For<ILoggerService>();
        }

        [TestMethod]
        public void ShouldNotAddTraderWithIncorrectEmptyName()
        {// Arrange
            TraderService traderService = new TraderService(
                this.provider,
                this.traderRep,
                this.logger);

            // Act
            traderService.AddTrader("", "Nikulin", "+7(981)000-00-00", "5000");

            // Assert
            provider.Received(1).GetPhrase(Phrase.EmptyName);
        }
        [TestMethod]
        public void ShouldNotAddTraderWithIncorrectEmptySyrname()
        {// Arrange
            TraderService traderService = new TraderService(
                this.provider,
                this.traderRep,
                this.logger);

            // Act
            traderService.AddTrader("Evgeniy", "", "+7(981)000-00-00", "5000");

            // Assert
            provider.Received(1).GetPhrase(Phrase.EmptyName);
        }
        [TestMethod]
        public void ShouldNotAddTraderWithIncorrectNameNotLetter()
        {// Arrange
            TraderService traderService = new TraderService(
                this.provider,
                this.traderRep,
                this.logger);

            // Act
            traderService.AddTrader("123", "Nikulin", "+7(981)000-00-00", "5000");

            // Assert
            provider.Received(1).GetPhrase(Phrase.NameNotLetter);
        }
        [TestMethod]
        public void ShouldNotAddTraderWithIncorrectSurnameNotLetter()
        {// Arrange
            TraderService traderService = new TraderService(
                this.provider,
                this.traderRep,
                this.logger);

            // Act
            traderService.AddTrader("Evgeniy", "123", "+7(981)000-00-00", "5000");

            // Assert
            provider.Received(1).GetPhrase(Phrase.NameNotLetter);
        }
        [TestMethod]
        public void ShouldNotAddTraderWithIncorrectLongName()
        {// Arrange
            TraderService traderService = new TraderService(
                this.provider,
                this.traderRep,
                this.logger);

            // Act
            traderService.AddTrader("EvgeniyEvgeniy", "NikulinNikulin", "+7(981)000-00-00", "5000");

            // Assert
            provider.Received(1).GetPhrase(Phrase.LongName);
        }
        [TestMethod]
        public void ShouldNotAddTraderWithIncorrectPhonePlus()
        {// Arrange
            TraderService traderService = new TraderService(
                this.provider,
                this.traderRep,
                this.logger);

            // Act
            traderService.AddTrader("Evgeniy", "Nikulin", "8(981)000-00-00", "5000");

            // Assert
            provider.Received(1).GetPhrase(Phrase.PhonePlus);
        }
        [TestMethod]
        public void ShouldNotAddTraderWithIncorrectPhoneRegion()
        {// Arrange
            TraderService traderService = new TraderService(
                this.provider,
                this.traderRep,
                this.logger);

            // Act
            traderService.AddTrader("Evgeniy", "Nikulin", "+7-000-00-00", "5000");

            // Assert
            provider.Received(1).GetPhrase(Phrase.PhoneRegion);
        }
        [TestMethod]
        public void ShouldNotAddTraderWithIncorrectPhoneIsLetter()
        {// Arrange
            TraderService traderService = new TraderService(
                this.provider,
                this.traderRep,
                this.logger);

            // Act
            traderService.AddTrader("Evgeniy", "Nikulin", "My phone", "5000");

            // Assert
            provider.Received(1).GetPhrase(Phrase.PhoneIsLetter);
        }
        [TestMethod]
        public void ShouldNotAddTraderWithIncorrectMoneyIsNumber()
        {// Arrange
            TraderService traderService = new TraderService(
                this.provider,
                this.traderRep,
                this.logger);

            // Act
            traderService.AddTrader("Evgeniy", "Nikulin", "+7(981)000-00-00", "five thousands");

            // Assert
            provider.Received(1).GetPhrase(Phrase.MoneyIsNumber);
        }
        [TestMethod]
        public void ShouldAddTrader()
        {// Arrange
            TraderService traderService = new TraderService(
                this.provider,
                this.traderRep,
                this.logger);

            // Act
            string Name = "Evgeniy";
            string Surname = "Nikulin";
            string Phone = "+7(981)000-00-00";
            string Money = "5000";

            traderService.AddTrader(Name, Surname, Phone, Money);

            // Assert
            traderRep.Received(1).Push(Arg.Any<Trader>());
            traderRep.Received(1).SaveChanges();
            logger.Received(1).Info($"Trader {Name} {Surname} with {Phone} phone and {Money} money is added");
            provider.Received(1).GetPhrase(Phrase.Success);
        }

        [TestMethod]
        public void ShoulGetTraderStatusGreen()
        {// Arrange
            TraderService traderService = new TraderService(
                this.provider,
                this.traderRep,
                this.logger);

            var trader = new Trader()
            {
                Id = 0,
                name = "Evgeniy",
                surname = "Nikulin",
                phone = "+7()",
                money = 5000m,
            };

            traderRep.GetTrader(0).Returns(trader);

            // Act

            var result = traderService.GetTraderStatus(0);

            // Assert
            Assert.AreEqual("Green status", result);
        }
        [TestMethod]
        public void ShoulGetTraderStatusOrange()
        {// Arrange
            TraderService traderService = new TraderService(
                this.provider,
                this.traderRep,
                this.logger);

            var trader = new Trader()
            {
                Id = 0,
                name = "Evgeniy",
                surname = "Nikulin",
                phone = "+7()",
                money = 0,
            };

            traderRep.GetTrader(0).Returns(trader);

            // Act

            var result = traderService.GetTraderStatus(0);

            // Assert
            Assert.AreEqual("Orange status", result);
        }
        [TestMethod]
        public void ShoulGetTraderStatusBlack()
        {// Arrange
            TraderService traderService = new TraderService(
                this.provider,
                this.traderRep,
                this.logger);

            var trader = new Trader()
            {
                Id = 0,
                name = "Evgeniy",
                surname = "Nikulin",
                phone = "+7()",
                money = -100,
            };

            traderRep.GetTrader(0).Returns(trader);

            // Act

            var result = traderService.GetTraderStatus(0);

            // Assert
            Assert.AreEqual("Black status", result);
        }


        [TestMethod]
        public void ShoulGetTradersTop10Page1()
        {// Arrange
            TraderService traderService = new TraderService(
                this.provider,
                this.traderRep,
                this.logger);

            var traders = new List<Trader>()
            {
                { new Trader(){ name = "Brian", surname = "Kelly"} },
                { new Trader(){ name = "Yves", surname = "Guillemot"} },
                { new Trader(){ name = "Fusajiro", surname = "Yamauchi"} },
                { new Trader(){ name = "Trip", surname = "Hawkins"} },
                { new Trader(){ name = "Akio", surname = "Morita"} },
            };

            traderService.TradersList.Returns(traders);

            // Act

            var result = traderService.GetTradersPerPage(10, 1);

            // Assert
            Assert.AreEqual(5, result.Count);
            Assert.AreEqual("Brian", result[0].name);
            Assert.AreEqual("Kelly", result[0].surname);
            Assert.AreEqual("Yves", result[1].name);
            Assert.AreEqual("Guillemot", result[1].surname);
            Assert.AreEqual("Fusajiro", result[2].name);
            Assert.AreEqual("Yamauchi", result[2].surname);
            Assert.AreEqual("Trip", result[3].name);
            Assert.AreEqual("Hawkins", result[3].surname);
            Assert.AreEqual("Akio", result[4].name);
            Assert.AreEqual("Morita", result[4].surname);
        }
        [TestMethod]
        public void ShoulGetTradersTop2Page1()
        {// Arrange
            TraderService traderService = new TraderService(
                this.provider,
                this.traderRep,
                this.logger);

            var traders = new List<Trader>()
            {
                { new Trader(){ name = "Brian", surname = "Kelly"} },
                { new Trader(){ name = "Yves", surname = "Guillemot"} },
                { new Trader(){ name = "Fusajiro", surname = "Yamauchi"} },
                { new Trader(){ name = "Trip", surname = "Hawkins"} },
                { new Trader(){ name = "Akio", surname = "Morita"} },
            };

            traderService.TradersList.Returns(traders);

            // Act

            var result = traderService.GetTradersPerPage(2, 1);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Brian", result[0].name);
            Assert.AreEqual("Kelly", result[0].surname);
            Assert.AreEqual("Yves", result[1].name);
            Assert.AreEqual("Guillemot", result[1].surname);
        }
        [TestMethod]
        public void ShoulGetTradersTop2Page2()
        {// Arrange
            TraderService traderService = new TraderService(
                this.provider,
                this.traderRep,
                this.logger);

            var traders = new List<Trader>()
            {
                { new Trader(){ name = "Brian", surname = "Kelly"} },
                { new Trader(){ name = "Yves", surname = "Guillemot"} },
                { new Trader(){ name = "Fusajiro", surname = "Yamauchi"} },
                { new Trader(){ name = "Trip", surname = "Hawkins"} },
                { new Trader(){ name = "Akio", surname = "Morita"} },
            };

            traderService.TradersList.Returns(traders);

            // Act

            var result = traderService.GetTradersPerPage(2, 2);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Fusajiro", result[0].name);
            Assert.AreEqual("Yamauchi", result[0].surname);
            Assert.AreEqual("Trip", result[1].name);
            Assert.AreEqual("Hawkins", result[1].surname);
        }
        [TestMethod]
        public void ShoulGetTradersTop2Page3()
        {// Arrange
            TraderService traderService = new TraderService(
                this.provider,
                this.traderRep,
                this.logger);

            var traders = new List<Trader>()
            {
                { new Trader(){ name = "Brian", surname = "Kelly"} },
                { new Trader(){ name = "Yves", surname = "Guillemot"} },
                { new Trader(){ name = "Fusajiro", surname = "Yamauchi"} },
                { new Trader(){ name = "Trip", surname = "Hawkins"} },
                { new Trader(){ name = "Akio", surname = "Morita"} },
            };

            traderService.TradersList.Returns(traders);

            // Act

            var result = traderService.GetTradersPerPage(2, 3);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Akio", result[0].name);
            Assert.AreEqual("Morita", result[0].surname);
        }
    }
}