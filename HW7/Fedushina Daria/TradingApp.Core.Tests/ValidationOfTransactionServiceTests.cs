using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using TradingApp.Core.Dto;
using TradingApp.Core.Models;
using TradingApp.Core.Repositories;
using TradingApp.Core.Services;

namespace TradingApp.Core.Tests
{
    [TestClass]
    public class ValidationOfTransactionServiceTests
    {
        [TestMethod]
        public void ShouldCheckPermissionToSell()
        {
            //Arrange
            var balanceTableRepository = Substitute.For<IBalanceTableRepository>();
            ValidationOfTransactionService validationService = new ValidationOfTransactionService(balanceTableRepository);
            var args = new BalanceInfo();
            args.Balance = 100;
            args.UserID = 22;
            BalanceEntity balanceEntity = new BalanceEntity() {
                Balance = 1000,
                BalanceID = "2201",
                CreatedAt = DateTime.Now,
                StockID = 2,
                StockAmount =2,
                UserID = 22
            };
            balanceTableRepository.GetAll(22).Returns(new List<BalanceEntity>() { balanceEntity });

            //Act
            validationService.CheckPermissionToSell(args.UserID);
            //Average
            balanceTableRepository.Received(1).GetAll(Arg.Is<int>(args.UserID));
            balanceTableRepository.Received(1);
        }
    }

}

