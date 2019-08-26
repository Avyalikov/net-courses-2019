using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using NSubstitute;
using TradingApp.Core.Dto;
using TradingApp.Core.Models;
using TradingApp.Core.Repositories;
using TradingApp.Core.Services;

namespace TradingApp.Core.Tests
{
    [TestClass]
    public class BalancesServiceTests
    {
        [TestMethod]
        public void ShouldCreateBalance()
        {
            //Arrange
            var balanceTableRepository = Substitute.For<IBalanceTableRepository>();
            BalancesService balanceService = new BalancesService(balanceTableRepository);
            BalanceInfo args = new BalanceInfo();
            args.UserID = 22;
            args.Balance = 1000;
            args.StockID = 1;
            args.StockAmount = 1; 

            //Act
            balanceService.CreateBalance(args);
            //Assert
            balanceTableRepository.Received(1).Add(Arg.Is<BalanceEntity>(w=>w.UserID==args.UserID &&
            w.Balance==args.Balance &&
            w.StockID==args.StockID &&
            w.StockAmount == args.StockAmount));
            balanceTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Balance with that number is already exists. Can't continue")]
        public void ShouldNotCreateNewBalanceIfItExists()
        {
            //Arrange
            var balanceTableRepository = Substitute.For<IBalanceTableRepository>();
            BalancesService balanceService = new BalancesService(balanceTableRepository);
            BalanceInfo args = new BalanceInfo();
            args.UserID = 22;
            args.Balance = 1000;
            args.StockID = 1;
            args.StockAmount = 1;

            //Act
            balanceService.CreateBalance(args); 

            balanceTableRepository.Contains(Arg.Is<BalanceEntity>(w => w.UserID == args.UserID &&
            w.Balance == args.Balance &&
            w.StockID == args.StockID &&
            w.StockAmount == args.StockAmount)).Returns(true);
            balanceService.CreateBalance(args);

        }

        [TestMethod]
        public void ShouldGetBalanceInfo()
        {
            //Arrange
            var balanceTableRepository = Substitute.For<IBalanceTableRepository>();
            balanceTableRepository.Contains(Arg.Is("2201")).Returns(true);
            BalancesService balanceService = new BalancesService(balanceTableRepository);

            // Act
            balanceService.Get("2201");

            //Assert
            balanceTableRepository.Received(1).Get("2201");

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Can't find balance with this Id. It might be not exists")]
        public void ShouldGetExceptionIfBalanceNotExists()
        {
            //Arrange
            var balanceTableRepository = Substitute.For<IBalanceTableRepository>();
            balanceTableRepository.Contains(Arg.Is("1102")).Returns(false);
            BalancesService balanceService = new BalancesService(balanceTableRepository);

            // Act
             balanceService.Get("1102");
        }
    }

}

