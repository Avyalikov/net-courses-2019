namespace TradingApp.Core.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using TradingApp.Core.Dto;
    using TradingApp.Core.Models;
    using TradingApp.Core.Repositories;
    using TradingApp.Core.Services;

    [TestClass]
    public class PortfolioServiceTest
    {
        [TestMethod]
        public void ShouldAddNewUsersShares()
        {
            var portfolioTableRepository = Substitute.For<IPortfolioTableRepository>();
            PortfolioServices portfolio = new PortfolioServices(portfolioTableRepository);
            PortfolioInfo args = new PortfolioInfo()
            {
                UserId = 1,
                ShareId = 1,
                Amount = 50
            };

            portfolio.AddNewUsersShares(args);

            portfolioTableRepository.AddNewUsersShares(Arg.Is<PortfolioEntity>(
                us => us.UserEntityId == args.UserId &&
                us.ShareId == args.ShareId &&
                us.Amount == args.Amount));
            portfolioTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        public void ShouldChangeAmountOfShareByUser()
        {
            var portfolioTableRepository = Substitute.For<IPortfolioTableRepository>();
            PortfolioServices portfolio = new PortfolioServices(portfolioTableRepository);
            PortfolioEntity args = new PortfolioEntity()
            {
                UserEntityId = 1,
                ShareId = 1,
                Amount = 50
            };

            portfolio.ChangeAmountOfShares(args, -10);

            portfolioTableRepository.Contains(Arg.Is<PortfolioEntity>(
                u => u.UserEntityId == args.UserEntityId &&
                u.ShareId == args.ShareId &&
                u.Amount == args.Amount - 10)).Returns(true);
            portfolioTableRepository.Received(1).SaveChanges();
        }
    }
}
