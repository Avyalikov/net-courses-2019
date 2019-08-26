using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Repositories;
using TradingSimulator.Core.Services;

namespace TradingSimulator.Core.Tests
{
    [TestClass]
    public class TransactionTests
    {
        [TestMethod]
        public void ShouldAddANewTransactionToTheHistory()
        {
            var transactionRepositories = Substitute.For<ITransactionRepositories>(); 
            TransactionService transactionService = new TransactionService(transactionRepositories);
            TransactionHistoryEntity transactionHistoryEntity = new TransactionHistoryEntity()
            {
                DateTimeBay = DateTime.Now,
                SellerId = 1,
                CustomerId = 2,
                AmountShare = 100,
                Cost = 10
            };

            transactionService.RegisterNewTransactionHistory(transactionHistoryEntity);

            transactionRepositories.Received(1).Add(Arg.Is<TransactionHistoryEntity>(w =>
            w.SellerId == transactionHistoryEntity.SellerId &&
            w.CustomerId == transactionHistoryEntity.CustomerId &&
            w.AmountShare == transactionHistoryEntity.AmountShare &&
            w.Cost == transactionHistoryEntity.Cost
            ));
            transactionRepositories.Received(1).SaveChanges();
        }
    }
}
