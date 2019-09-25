namespace TradingApp.Core.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using System;
    using TradingApp.Core.Dto;
    using TradingApp.Core.Models;
    using TradingApp.Core.Repositories;
    using TradingApp.Core.Services;

    [TestClass]
    public class TransactionServicesTest
    {
        [TestMethod]
        public void ShouldAddNewTransaction()
        {
            var transactionTableRepository = Substitute.For<ITransactionTableRepository>();
            TransactionServices transService = new TransactionServices(transactionTableRepository);
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
    }
 }
