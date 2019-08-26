using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using TradingApp.Core.Dto;
using TradingApp.Core.Models;
using TradingApp.Core.Repositories;
using TradingApp.Core.Services;

namespace TradingApp.Core.Tests
{

    [TestClass]
    public class TransactionServiceTests
    {
        IHistoryTableRepository historyTableRepository;
        IBalanceTableRepository balanceTableRepository;
        IStockTableRepository stockTableRepository;

        [TestMethod]
        public void ShouldLogInHistoryTable()
        {
            //Arrange

            historyTableRepository = Substitute.For<IHistoryTableRepository>();
            balanceTableRepository = Substitute.For<IBalanceTableRepository>();
            stockTableRepository = Substitute.For<IStockTableRepository>();
            TransactionService transactionService = new TransactionService
                (
                this.historyTableRepository, 
                this.balanceTableRepository, 
                this.stockTableRepository
                );
            var args = new TransactionInfo();
            args.SellerID = 22;
            args.SellerBalanceID = "2201";
            args.BuyerID = 7;
            args.BuyerBalanceID = "701";
            args.StockAmount = 2;
            args.StockID = 5;
            args.dateTime = DateTime.Now;

            var stock = new StockEntity();
            stock.ID = 5;
            stock.Price = 200;
            stock.Type = "StockTypeA";

            //Act
            transactionService.TransactionHistoryLogger(args, stock);

            //Assert
            historyTableRepository.Received(1).Add(Arg.Is<TransactionHistoryEntity>(w =>
            w.SellerBalanceID == args.SellerBalanceID &&
            w.BuyerBalanceID == args.BuyerBalanceID &&
            w.StockName == stock.Type &&
            w.StockAmount == args.StockAmount &&
            w.TransactionQuantity == args.StockAmount * stock.Price &&
            w.TimeOfTransaction == args.dateTime));
            historyTableRepository.Received(1).SaveChanges();

        }


        [TestMethod]
        public void ShouldChangeBalanceOfTheSeller()
        {
            //Arrange

            historyTableRepository = Substitute.For<IHistoryTableRepository>();
            balanceTableRepository = Substitute.For<IBalanceTableRepository>();
            stockTableRepository = Substitute.For<IStockTableRepository>();
            TransactionService transactionService = new TransactionService(this.historyTableRepository, this.balanceTableRepository, this.stockTableRepository);
            var args = new TransactionInfo();
            args.SellerID = 22;
            args.SellerBalanceID = "2201";
            args.BuyerID = 7;
            args.BuyerBalanceID = "701";
            args.StockAmount = 2;
            args.StockID = 5;
            args.dateTime = DateTime.Now;

            var stock = new StockEntity();
            stock.ID = 5;
            stock.Price = 200;
            stock.Type = "StockTypeA";

            stockTableRepository.Get(5).Returns(stock);

            var sellerBalance = new BalanceEntity();
            sellerBalance.BalanceID = "2201";
            sellerBalance.Balance = 1000;
            sellerBalance.StockID = 3;
            sellerBalance.StockAmount = 2;
            sellerBalance.UserID = 22;
            sellerBalance.CreatedAt = DateTime.Now;

            balanceTableRepository.Get("2201").Returns(sellerBalance);
            

            //Act
            transactionService.MakeSell(args,stock);
            //Assert
            balanceTableRepository.Received(1).Change(Arg.Is<BalanceEntity>(w =>
            w.Balance == sellerBalance.Balance &&
            w.BalanceID == sellerBalance.BalanceID &&
            w.CreatedAt == sellerBalance.CreatedAt &&
            w.StockID == sellerBalance.StockID &&
            w.StockAmount == sellerBalance.StockAmount &&
            w.UserID == sellerBalance.UserID));
            balanceTableRepository.Received(1).SaveChanges();

        } 
       [TestMethod]
       public void ShouldChangeBalanceOfTheBuyer()
        {
            //Arrange

            historyTableRepository = Substitute.For<IHistoryTableRepository>();
            balanceTableRepository = Substitute.For<IBalanceTableRepository>();
            stockTableRepository = Substitute.For<IStockTableRepository>();
            TransactionService transactionService = new TransactionService(this.historyTableRepository, this.balanceTableRepository, this.stockTableRepository);
            var args = new TransactionInfo();
            args.SellerID = 22;
            args.SellerBalanceID = "2201";
            args.BuyerID = 7;
            args.BuyerBalanceID = "701";
            args.StockAmount = 2;
            args.StockID = 5;
            args.dateTime = DateTime.Now;

            var stock = new StockEntity();
            stock.ID = 5;
            stock.Price = 200;
            stock.Type = "StockTypeA";

            stockTableRepository.Get(5).Returns(stock);

            var buyerBalance = new BalanceEntity();
            buyerBalance.BalanceID = "701";
            buyerBalance.Balance = 1000;
            buyerBalance.StockID = 3;
            buyerBalance.StockAmount = 2;
            buyerBalance.UserID = 7;
            buyerBalance.CreatedAt = DateTime.Now;

            balanceTableRepository.Get("701").Returns(buyerBalance);

            //Act
            transactionService.MakeBuy(args,stock);
            //Assert
            balanceTableRepository.Received(1).Change(Arg.Is<BalanceEntity>(w =>
            w.Balance == buyerBalance.Balance &&
            w.BalanceID == buyerBalance.BalanceID &&
            w.CreatedAt == buyerBalance.CreatedAt &&
            w.StockID == buyerBalance.StockID &&
            w.StockAmount == buyerBalance.StockAmount &&
            w.UserID == buyerBalance.UserID));
            balanceTableRepository.Received(1).SaveChanges();


        }

    }
}