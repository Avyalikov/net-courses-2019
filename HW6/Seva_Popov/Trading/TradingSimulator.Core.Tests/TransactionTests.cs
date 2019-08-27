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

        [TestMethod]
        public void ShouldGetSharesList()
        {
            var transactionRepositories = Substitute.For<ITransactionRepositories>();
            TransactionService transactionService = new TransactionService(transactionRepositories);
            UserEntity userEntity = new UserEntity();

            var sharesList = transactionService.SelectSharesToThUserEntity(userEntity);

            transactionRepositories.Received(1).GetUserAndShareList(userEntity);
        }

        [TestMethod]
        public void ShouldUpdateUserTable()
        {
            var transactionRepositories = Substitute.For<ITransactionRepositories>();
            TransactionService transactionService = new TransactionService(transactionRepositories);
            UserEntity userEntity1 = new UserEntity() { Balance = 1000 };
            UserEntity userEntity2 = new UserEntity();
            AddingSharesToThUserEntity addingSharesToThUserEntity1 = new AddingSharesToThUserEntity(); 
            AddingSharesToThUserEntity addingSharesToThUserEntity2 = new AddingSharesToThUserEntity();

            transactionService.StockPurchaseTransaction(userEntity1, userEntity2, 10, 3, addingSharesToThUserEntity1, addingSharesToThUserEntity2);

            transactionRepositories.Received(1).UpdateUserTable(Arg.Is<UserEntity>(w => w.Balance == userEntity1.Balance));
            transactionRepositories.Received(1).SaveChanges();
        }
    }
}
