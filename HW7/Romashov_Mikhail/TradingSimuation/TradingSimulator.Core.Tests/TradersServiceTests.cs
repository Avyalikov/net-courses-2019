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
        ITraderTableRepository traderTableRepository;
        TradersService tradersService;

        [TestInitialize]
        public void Initialize()
        {
            traderTableRepository = Substitute.For<ITraderTableRepository>();
            tradersService = new TradersService(this.traderTableRepository);
        }
        [TestMethod]
        public void ShouldRegisterNewTrader()
        {
            //Arrange
           
            TraderInfo trader = new TraderInfo();

            trader.Name = "Monica";
            trader.Surname = "Belucci";
            trader.PhoneNumber = "+79110000000";
            trader.Balance = 143000.0M;

            //Act
            var traderID = tradersService.RegisterNewTrader(trader);

            //Assert
            traderTableRepository.Received(1).Add(Arg.Is<TraderEntityDB>(
                w => w.Name == trader.Name
                && w.Surname == trader.Surname
                && w.PhoneNumber == trader.PhoneNumber
                && w.Balance == trader.Balance));
            traderTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "This trader Monica Belucci has been registered.")]
        public void ShouldNotRegisterNewTraderIfExists()
        {
            //Arrange
            TraderInfo trader = new TraderInfo();

            trader.Name = "Monica";
            trader.Surname = "Belucci";
            trader.PhoneNumber = "+79110000000";
            trader.Balance = 143000.0M;

            //Act
            tradersService.RegisterNewTrader(trader);
            traderTableRepository.Contains(Arg.Is<TraderEntityDB>(
                w => w.Name == trader.Name
                && w.Surname == trader.Surname
                && w.PhoneNumber == trader.PhoneNumber
                && w.Balance == trader.Balance)).Returns(true);
            tradersService.RegisterNewTrader(trader);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Can`t get trader by this Id = 55.")]
      
        public void ShouldThrowExceptionIfCantFindTraderById()
        {
            //Arrange 
            traderTableRepository.ContainsById(Arg.Is<int>(55)).Returns(false);

            //Act
            var traders = tradersService.GetTraderById(55);
        }

        [TestMethod]
        public void ShouldGetTraderInfoById()
        {
            //Arrange 
            traderTableRepository.ContainsById(Arg.Is<int>(55)).Returns(true);

            //Act
            var traders = tradersService.GetTraderById(55);

            //Assert
            traderTableRepository.Received(1).GetById(55);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Can`t get trader by this Name = Celentano.")]

        public void ShouldThrowExceptionIfCantFindTraderByName()
        {
            //Arrange 
            traderTableRepository.ContainsByName(Arg.Is<String>("Celentano")).Returns(false);

            //Act
            var traders = tradersService.GetTraderByName("Celentano");
        }

        [TestMethod]
        public void ShouldGetTraderInfoByName()
        {
            //Arrange 
            traderTableRepository.ContainsByName(Arg.Is<string>("Adriano")).Returns(true);

            //Act
            var traders = tradersService.GetTraderByName("Adriano");

            //Assert
            traderTableRepository.Received(1).GetByName("Adriano");
        }
    }
}
