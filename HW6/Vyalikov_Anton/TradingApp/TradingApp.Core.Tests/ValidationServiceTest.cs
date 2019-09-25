namespace TradingApp.Core.Tests
{
    using TradingApp.Core.DTO;
    using TradingApp.Core.Interfaces;
    using TradingApp.Core.Models;
    using TradingApp.Core.Services;
    using TradingApp.Core.Repos;
    using NSubstitute;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ValidationServiceTest
    {
        [TestMethod]
        public void ValidateClientRegistrationData()
        {
            //Arrange
            var clientReposMock = Substitute.For<IClientRepository>();
            var sharesReposMock = Substitute.For<ISharesRepository>();
            var portfolioReposMock = Substitute.For<IPortfolioRepository>();
            var loggerMock = Substitute.For<ILogger>();

            var sut = new ValidationService(
                clientReposMock,
                sharesReposMock,
                portfolioReposMock);

            ClientRegistrationData clientData = new ClientRegistrationData()
            {
                ClientName = "Ivan Ivanov",
                ClientPhone = "880005553",
                Balance = (decimal)21421412.00
            };

            //Act
            var isValid = sut.ValidateClientRegistrationData(clientData, loggerMock);
            
            //Assert
            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void NotValidateClientRegistrationData()
        {
            //Arrange
            var clientReposMock = Substitute.For<IClientRepository>();
            var sharesReposMock = Substitute.For<ISharesRepository>();
            var portfolioReposMock = Substitute.For<IPortfolioRepository>();
            var loggerMock = Substitute.For<ILogger>();

            var sut = new ValidationService(
                clientReposMock,
                sharesReposMock,
                portfolioReposMock);

            ClientRegistrationData clientData = new ClientRegistrationData()
            {
                ClientName = "123",
                ClientPhone = "iPhone",
                Balance = 4363747
            };

            //Act
            var isValid = sut.ValidateClientRegistrationData(clientData, loggerMock);

            //Assert
            Assert.AreEqual(false, isValid);
        }

        [TestMethod]
        public void ValidateShareRegistrationData()
        {
            //Arrange
            var clientReposMock = Substitute.For<IClientRepository>();
            var sharesReposMock = Substitute.For<ISharesRepository>();
            var portfolioReposMock = Substitute.For<IPortfolioRepository>();
            var loggerMock = Substitute.For<ILogger>();

            var sut = new ValidationService(
                clientReposMock,
                sharesReposMock,
                portfolioReposMock);

            ShareRegistrationData shareData = new ShareRegistrationData()
            {
                ShareType = "Orange",
                SharePrice = (decimal)200.00
            };

            //Act
            var isValid = sut.ValidateShareRegistrationData(shareData, loggerMock);
            
            //Assert
            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void NotValidateShareRegistrationData()
        {
            //Arrange
            var clientReposMock = Substitute.For<IClientRepository>();
            var sharesReposMock = Substitute.For<ISharesRepository>();
            var portfolioReposMock = Substitute.For<IPortfolioRepository>();
            var loggerMock = Substitute.For<ILogger>();

            var sut = new ValidationService(
                clientReposMock,
                sharesReposMock,
                portfolioReposMock);

            ShareRegistrationData shareData = new ShareRegistrationData()
            {
                ShareType = "754234",
                SharePrice = 0
            };

            //Act
            var isValid = sut.ValidateShareRegistrationData(shareData, loggerMock);

            //Assert
            Assert.AreEqual(false, isValid);
        }

        [TestMethod]
        public void ValidatePortfolioData()
        {
            //Arrange
            var clientReposMock = Substitute.For<IClientRepository>();
            var sharesReposMock = Substitute.For<ISharesRepository>();
            var portfolioReposMock = Substitute.For<IPortfolioRepository>();
            var loggerMock = Substitute.For<ILogger>();

            var sut = new ValidationService(
                clientReposMock,
                sharesReposMock,
                portfolioReposMock);

            PortfolioData portfolioData = new PortfolioData()
            {
                ClientID = 1,
                ShareID = 2,
                AmountOfShares = 20
            };

            //Act
            var isValid = sut.ValidatePortfolioData(portfolioData, loggerMock);

            //Assert
            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void NotValidatePortfolioData()
        {
            //Arrange
            var clientReposMock = Substitute.For<IClientRepository>();
            var sharesReposMock = Substitute.For<ISharesRepository>();
            var portfolioReposMock = Substitute.For<IPortfolioRepository>();
            var loggerMock = Substitute.For<ILogger>();

            var sut = new ValidationService(
                clientReposMock,
                sharesReposMock,
                portfolioReposMock);

            PortfolioData portfolioData = new PortfolioData()
            {
                ClientID = 1,
                ShareID = 2,
                AmountOfShares = -5
            };

            //Act
            var isValid = sut.ValidatePortfolioData(portfolioData, loggerMock);

            //Assert
            Assert.AreEqual(false, isValid);
        }

        [TestMethod]
        public void ValidateClientData()
        {
            //Arrange
            var clientReposMock = Substitute.For<IClientRepository>();
            var sharesReposMock = Substitute.For<ISharesRepository>();
            var portfolioReposMock = Substitute.For<IPortfolioRepository>();
            var loggerMock = Substitute.For<ILogger>();

            var sut = new ValidationService(
                clientReposMock,
                sharesReposMock,
                portfolioReposMock);

            Clients client = new Clients()
            {
                ClientID = 1,
                Name = "Ivan Ivanov",
                PhoneNumber = "880005553",
                Balance = (decimal)21421412.00
            };

            //Act
            var isValid = sut.ValidateClientData(client, loggerMock);

            //Assert
            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void NotValidateClientData()
        {
            //Arrange
            var clientReposMock = Substitute.For<IClientRepository>();
            var sharesReposMock = Substitute.For<ISharesRepository>();
            var portfolioReposMock = Substitute.For<IPortfolioRepository>();
            var loggerMock = Substitute.For<ILogger>();

            var sut = new ValidationService(
                clientReposMock,
                sharesReposMock,
                portfolioReposMock);

            Client client = new Client()
            {
                ClientID = -2,
                Name = "Ivan Ivanov",
                PhoneNumber = "880005553",
                Balance = (decimal)21421412.00
            };

            //Act
            var isValid = sut.ValidateClientData(client, loggerMock);

            //Assert
            Assert.AreEqual(false, isValid);
        }

        [TestMethod]
        public void ValidateClientsAmount()
        {
            //Arrange
            var clientReposMock = Substitute.For<IClientRepository>();
            var sharesReposMock = Substitute.For<ISharesRepository>();
            var portfolioReposMock = Substitute.For<IPortfolioRepository>();
            var loggerMock = Substitute.For<ILogger>();

            var sut = new ValidationService(
                clientReposMock,
                sharesReposMock,
                portfolioReposMock);

            IEnumerable<Client> clients = new List<Client>()
            {
                new Client(),
                new Client()
            };

            //Act
            var isValid = sut.ValidateClientsAmount(clients, loggerMock);

            //Assert
            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void NotValidateClientsAmount()
        {
            //Arrange
            var clientReposMock = Substitute.For<IClientRepository>();
            var sharesReposMock = Substitute.For<ISharesRepository>();
            var portfolioReposMock = Substitute.For<IPortfolioRepository>();
            var loggerMock = Substitute.For<ILogger>();

            var sut = new ValidationService(
                clientReposMock,
                sharesReposMock,
                portfolioReposMock);

            IEnumerable<Client> clients = new List<Client>();

            //Act
            var isValid = sut.ValidateClientsAmount(clients, loggerMock);

            //Assert
            Assert.AreEqual(false, isValid);
        }

        [TestMethod]
        public void ValidateTransaction()
        {
            //Arrange
            var clientReposMock = Substitute.For<IClientRepository>();
            var sharesReposMock = Substitute.For<ISharesRepository>();
            var portfolioReposMock = Substitute.For<IPortfolioRepository>();
            var loggerMock = Substitute.For<ILogger>();

            var sut = new ValidationService(
                clientReposMock,
                sharesReposMock,
                portfolioReposMock);

            var transaction = new Transaction
            {
                SellerID = 3,
                BuyerID = 5,
                ShareID = 9,
                AmountOfShares = 1
            };
            
            //Act
            var isValid = sut.ValidateTransaction(transaction, loggerMock);

            //Assert
            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void NotValidateTransactionByID()
        {
            //Arrange
            var clientReposMock = Substitute.For<IClientRepository>();
            var sharesReposMock = Substitute.For<ISharesRepository>();
            var portfolioReposMock = Substitute.For<IPortfolioRepository>();
            var loggerMock = Substitute.For<ILogger>();

            var sut = new ValidationService(
                clientReposMock,
                sharesReposMock,
                portfolioReposMock);

            var transaction = new Transaction
            {
                SellerID = 3,
                BuyerID = 3,
                ShareID = 9,
                AmountOfShares = 1
            };

            //Act
            var isValid = sut.ValidateTransaction(transaction, loggerMock);

            //Assert
            Assert.AreEqual(false, isValid);
        }
    }
}
