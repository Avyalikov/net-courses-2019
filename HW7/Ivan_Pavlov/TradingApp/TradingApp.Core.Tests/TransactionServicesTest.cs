namespace TradingApp.Core.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TradingApp.Core.Dto;
    using TradingApp.Core.Models;
    using TradingApp.Core.Repositories;
    using TradingApp.Core.Services;
    using TradingApp.Core.ServicesInterfaces;

    [TestClass]
    public class TransactionServicesTest
    {
        [TestMethod]
        public void ShouldAddNewTransaction()
        {
            var transactionTableRepository = Substitute.For<ITransactionTableRepository>();
            var userService = Substitute.For<IUsersService>();
            var portfolio = Substitute.For<IPortfolioServices>();
            TransactionServices transService = new TransactionServices(transactionTableRepository, userService, portfolio);
            TransactionStoryInfo args = new TransactionStoryInfo()
            {
                sellerId = 1,
                customerId = 2,
                shareId = 1,
                AmountOfShares = 10,
                DateTime = DateTime.Now,
                TransactionCost = 5000
            };

            int transactId = transService.AddNewTransaction(args);

            transactionTableRepository.Received(1).Add(Arg.Is<TransactionStoryEntity>(
                u => u.SellerId == args.sellerId &&
                u.CustomerId == args.customerId &&
                u.ShareId == args.shareId &&
                u.AmountOfShares == args.AmountOfShares &&
                u.DateTime == args.DateTime &&
                u.TransactionCost == args.TransactionCost));
            transactionTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        public void ShouldGetUsersTransactions()
        {
            var transactionTableRepository = Substitute.For<ITransactionTableRepository>();
            var userService = Substitute.For<IUsersService>();
            var portfolio = Substitute.For<IPortfolioServices>();
            TransactionServices transService = new TransactionServices(transactionTableRepository, userService, portfolio);
            int userId = 1;
            List<TransactionStoryEntity> args = new List<TransactionStoryEntity>(){
                new TransactionStoryEntity()
                    {
                        CustomerId = 1,
                        SellerId = 2,
                        AmountOfShares = 10,
                        TransactionCost = 100,
                        ShareId = 2
                    }
            };
            transactionTableRepository.GetTransactionsByUserId(userId).Returns(args);

            var userTransactions = transService.GetUsersTransaction(userId);
            Assert.AreEqual(userTransactions, args);
            transactionTableRepository.Received(1).GetTransactionsByUserId(userId);
        }
    }
}
