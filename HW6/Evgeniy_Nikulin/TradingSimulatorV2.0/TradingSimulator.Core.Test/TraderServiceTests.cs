namespace TradingSimulator.Core.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
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
    }
}
