namespace TradingApp.Core.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using System;
    using TradingApp.Core.Models;
    using TradingApp.Core.Repositories;
    using TradingApp.Core.Services;

    [TestClass]
    public class TransactionServiceTests
    {
        private IRepository<ShareEntity> shareTableRepository;
        private IRepository<TransactionEntity> transactionTableRepository;
        private IRepository<TraderEntity> traderTableRepository;
        private IRepository<StockEntity> stockTableRepository;

        [TestInitialize]
        public void Initialize()
        {
            shareTableRepository = Substitute.For<IRepository<ShareEntity>>();
            transactionTableRepository = Substitute.For<IRepository<TransactionEntity>>();
            traderTableRepository = Substitute.For<IRepository<TraderEntity>>();
            stockTableRepository = Substitute.For<IRepository<StockEntity>>();
        }
        [TestMethod]
        public void ShouldAddTransaction()
        {
            //Arrange
            TraderEntity seller = new TraderEntity { Id = 1, Balance = 50 };
            traderTableRepository.GetById(Arg.Is<int>(1)).Returns(seller);
            TraderEntity buyer = new TraderEntity { Id = 2, Balance = 500 };
            traderTableRepository.GetById(Arg.Is<int>(2)).Returns(buyer);
            StockEntity stock = new StockEntity { Id = 1, PricePerUnit = 10 };
            stockTableRepository.GetById(Arg.Is<int>(1)).Returns(stock);
            ShareEntity share = new ShareEntity
            {
                Id = 1,
                Amount = 10,
                Owner = seller,
                ShareType = new ShareTypeEntity { Multiplier = 2 },
                Stock = stock
            };
            shareTableRepository.GetById(Arg.Is<int>(1)).Returns(share);
            decimal dealPrice = share.Amount * stock.PricePerUnit * share.ShareType.Multiplier;
            var transactionService = new TransactionService(
                shareTableRepository,
                transactionTableRepository,
                traderTableRepository,
                stockTableRepository);

            // Act
            transactionService.MakeShareTransaction(1, 2, 1);

            // Assert
            transactionTableRepository.Received(1).Add(Arg.Any<TransactionEntity>());
            shareTableRepository.Received(1).Save(share);
            traderTableRepository.Received(1).Save(buyer);
            traderTableRepository.Received(1).Save(seller);
            Assert.IsTrue(share.Owner.Id == 2);
            Assert.IsTrue(seller.Balance == 50 + dealPrice);
            Assert.IsTrue(buyer.Balance == 500 - dealPrice);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "Cant make a transaction because buyer dont have enough money.")]
        public void ShouldNotMakeTransactionIfBuyerDontHaveEnoughMoney()
        {
            // Arrange
            TraderEntity seller = new TraderEntity { Id = 1, Balance = 50 };
            traderTableRepository.GetById(Arg.Is<int>(1)).Returns(seller);
            TraderEntity buyer = new TraderEntity { Id = 2, Balance = 70 };
            traderTableRepository.GetById(Arg.Is<int>(2)).Returns(buyer);
            StockEntity stock = new StockEntity { Id = 1, PricePerUnit = 10 };
            stockTableRepository.GetById(Arg.Is<int>(1)).Returns(stock);
            ShareEntity share = new ShareEntity
            {
                Id = 1,
                Amount = 10,
                ShareType = new ShareTypeEntity { Multiplier = 2 },
                Stock = stock
            };
            shareTableRepository.GetById(Arg.Is<int>(1)).Returns(share);

            var transactionService = new TransactionService(
                shareTableRepository,
                transactionTableRepository,
                traderTableRepository,
                stockTableRepository);

            // Act
            transactionService.MakeShareTransaction(1, 2, 1);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "Cant make a transaction because buyer and seller is a same trader.")]
        public void ShouldNotMakeTransactionIfSellerIsABuyer()
        {
            // Arrange
            int sellerId = 1;
            int buyerId = 1;
            var transactionService = new TransactionService(
                shareTableRepository,
                transactionTableRepository,
                traderTableRepository,
                stockTableRepository);

            // Act
            transactionService.MakeShareTransaction(sellerId, buyerId, 1);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "There's no user with given id in data source.")]
        public void ShouldNotMakeTransactionIfThereIsNoBuyer()
        {
            // Arrange
            int sellerId = 1;
            int buyerId = 2;

            var transactionService = new TransactionService(
                shareTableRepository,
                transactionTableRepository,
                traderTableRepository,
                stockTableRepository);

            // Act
            transactionService.MakeShareTransaction(sellerId, buyerId, 1);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "There's no user with given id in data source.")]
        public void ShouldNotMakeTransactionIfThereIsNoSeller()
        {
            // Arrange
            int sellerId = 1;
            int buyerId = 2;
            TraderEntity buyer = new TraderEntity { Id = 2, Balance = 70 };
            traderTableRepository.GetById(Arg.Is<int>(2)).Returns(buyer);

            var transactionService = new TransactionService(
                shareTableRepository,
                transactionTableRepository,
                traderTableRepository,
                stockTableRepository);

            // Act
            transactionService.MakeShareTransaction(sellerId, buyerId, 1);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "Seller dont have shares with given Id.")]
        public void ShouldNotMakeTransactionIfSellerDontHaveChosenShare()
        {
            // Arrange
            int sellerId = 1;
            int buyerId = 2;
            int shareId = 1;
            TraderEntity seller = new TraderEntity { Id = 1, Balance = 50 };
            traderTableRepository.GetById(Arg.Is<int>(1)).Returns(seller);
            TraderEntity buyer = new TraderEntity { Id = 2, Balance = 70 };
            traderTableRepository.GetById(Arg.Is<int>(2)).Returns(buyer);
            StockEntity stock = new StockEntity { Id = 1, PricePerUnit = 10 };
            stockTableRepository.GetById(Arg.Is<int>(1)).Returns(stock);
            ShareEntity share = new ShareEntity
            {
                Id = 1,
                Amount = 10,
                ShareType = new ShareTypeEntity { Multiplier = 2 },
                Stock = stock,
                Owner = buyer
            };
            shareTableRepository.GetById(Arg.Is<int>(1)).Returns(share);

            var transactionService = new TransactionService(
                shareTableRepository,
                transactionTableRepository,
                traderTableRepository,
                stockTableRepository);

            // Act
            transactionService.MakeShareTransaction(sellerId, buyerId, shareId);
        }
    }
}
