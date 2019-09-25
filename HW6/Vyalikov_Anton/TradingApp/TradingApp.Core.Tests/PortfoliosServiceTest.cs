namespace TradingApp.Core.Tests
{
    using TradingApp.Core.DTO;
    using TradingApp.Core.Models;
    using TradingApp.Core.Services;
    using TradingApp.Core.Repos;
    using NSubstitute;
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PortfoliosServiceTest
    {
        [TestMethod]
        public void RegisterPortfolioTest()
        {
            // Arrange
            var portfoliosRepositoryMock = Substitute.For<IPortfolioRepository>();

            var sut = new PortfoliosService(portfoliosRepositoryMock);

            PortfolioData portfolio = new PortfolioData
            {
                ClientID = 10,
                ShareID = 10,
                AmountOfShares = 100
            };

            ClientPortfolio portfolios = new ClientPortfolio
            {
                ClientID = 10,
                ShareID = 10,
                AmountOfShares = 100
            };

            // Act
            sut.RegisterPortfolio(portfolio);

            // Asserts
            portfoliosRepositoryMock.Received(1).Insert(Arg.Is<ClientPortfolio>(x => x.ClientID == portfolio.ClientID 
            && x.ShareID == portfolio.ShareID && x.AmountOfShares == portfolio.AmountOfShares));
        }

        [TestMethod]
        public void ChangeAmountOfSharesTest()
        {
            // Arrange
            var portfoliosRepositoryMock = Substitute.For<IPortfolioRepository>();

            var sut = new PortfoliosService(portfoliosRepositoryMock);

            ClientPortfolio portfolios = new ClientPortfolio
            {
                ClientID = 10,
                ShareID = 10,
                AmountOfShares = 100
            };

            // Act
            sut.ChangeAmountOfShares(portfolios);

            // Asserts
            portfoliosRepositoryMock.Received(1).ChangeAmountOfShares(Arg.Is<ClientPortfolio>(x => x.ClientID == portfolios.ClientID));
        }

        [TestMethod]
        public void GetAllPortfoliosTest()
        {
            // Arrange
            var portfoliosRepositoryMock = Substitute.For<IPortfolioRepository>();
            var sut = new PortfoliosService(portfoliosRepositoryMock);

            // Act
            sut.GetAllPortfolios();

            // Asserts
            portfoliosRepositoryMock.Received(1).GetAllPortfolios();
        }
    }
}
