using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using TradingSimulator.Core.Dto;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Repositories;
using TradingSimulator.Core.Services;

namespace TradingSimulator.Core.Tests
{
    [TestClass]
    public class TradersServiceTests
    {
        [TestMethod]
        public void ShouldRegisterNewTrader()
        {
            //Arrange
            var traderTableRepository = Substitute.For<ITraderTableRepository>();
            TradersService tradersService = new TradersService(traderTableRepository);
            TraderInfo trader = new TraderInfo();

            trader.Name = "Monica";
            trader.Surname = "Belucci";
            trader.PhoneNumber = "+79110000000";
            trader.Balance = 143000.0M;

            //Act
            var traderID = tradersService.RegisterNewTrader(trader);

            //Assert
            traderTableRepository.Received(1).Add(Arg.Is<TraderEntity>(
                w => w.Name == trader.Name
                && w.Surname == trader.Surname
                && w.PhoneNumber == trader.PhoneNumber
                && w.Balance == trader.Balance));
            traderTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "This trader has been registered.")]
        public void ShouldNotRegisterNewTraderIfExists()
        {
            //Arrange
            var traderTableRepository = Substitute.For<ITraderTableRepository>();
            TradersService tradersService = new TradersService(traderTableRepository);
            TraderInfo trader = new TraderInfo();

            trader.Name = "Monica";
            trader.Surname = "Belucci";
            trader.PhoneNumber = "+79110000000";
            trader.Balance = 143000.0M;

            //Act
            tradersService.RegisterNewTrader(trader);
            traderTableRepository.Contains(Arg.Is<TraderEntity>(
                w => w.Name == trader.Name
                && w.Surname == trader.Surname
                && w.PhoneNumber == trader.PhoneNumber
                && w.Balance == trader.Balance)).Returns(true);
            tradersService.RegisterNewTrader(trader);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Can`t get trader by this Id.")]
      
        public void ShouldThrowExceptionIfCantFindTrader()
        {
            //Arrange 
            var traderTableRepository = Substitute.For<ITraderTableRepository>();
            traderTableRepository.ContainsById(Arg.Is<int>(55)).Returns(false);
            TradersService tradersService = new TradersService(traderTableRepository);

            //Act
            var traders = tradersService.GetTraders(55);
        }

        [TestMethod]
        public void ShouldGetTraderInfo()
        {
            //Arrange 
            var traderTableRepository = Substitute.For<ITraderTableRepository>();
            traderTableRepository.ContainsById(Arg.Is<int>(55)).Returns(true);
            TradersService tradersService = new TradersService(traderTableRepository);

            //Act
            var traders = tradersService.GetTraders(55);

            //Assert
            traderTableRepository.Received(1).Get(55);
        }
    }
}
