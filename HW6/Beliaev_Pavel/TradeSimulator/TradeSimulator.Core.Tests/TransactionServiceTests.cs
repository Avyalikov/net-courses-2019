namespace TradeSimulator.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using TradeSimulator.Core.Dto;
    using TradeSimulator.Core.Models;
    using TradeSimulator.Core.Repositories;
    using TradeSimulator.Core.Services;

    [TestClass]
    public class TransactionServiceTests
    {
        IAccountTableRepository accountTableRepository = Substitute.For<IAccountTableRepository>();
        IHistoryTableRepository historyTableRepository = Substitute.For<IHistoryTableRepository>();
        IStockPriceTableRepository stockPriceTableRepository = Substitute.For<IStockPriceTableRepository>();
        IStockOfClientTableRepository stockOfClientTableRepository = Substitute.For<IStockOfClientTableRepository>();
        ILogger logger = Substitute.For<ILogger>();

        [TestMethod]
        public void ShouldMakeATrade()
        {
            //Arrange
            TradingService tradingService = new TradingService(accountTableRepository, stockPriceTableRepository, historyTableRepository, stockOfClientTableRepository, logger);
            TransactionInfo transactionInfo = new TransactionInfo();
            transactionInfo.BuyerId = 1;
            transactionInfo.SellerId = 2;
            transactionInfo.TypeOfStock = "AAA";
            transactionInfo.QuantityOfStocks = 20;
            StockPriceEntity stockPriceEntity = new StockPriceEntity()
            {
                TypeOfStock = transactionInfo.TypeOfStock,
                PriceOfStock = 4
            };
            AccountEntity buyer = new AccountEntity()
            {
                AccountId = 11,
                ClientId = 1,
                Balance = 100,
                Stocks = new List<StockOfClientEntity>()
            };
            AccountEntity seller = new AccountEntity()
            {
                AccountId = 22,
                ClientId = 2,
                Balance = 100,
                Stocks = new List<StockOfClientEntity>()
            };
            StockOfClientEntity stockForSell = new StockOfClientEntity()
            {
                Id = 222,
                TypeOfStocks = "AAA",
                AccountId = 22,
                AccountForStock = seller,
                quantityOfStocks = 30
            };

            stockPriceTableRepository.GetStockPriceEntityByStockType(Arg.Is<string>(
                w => w == transactionInfo.TypeOfStock)).Returns(stockPriceEntity);

            accountTableRepository.GetAccountByClientId(Arg.Is<int>(
                w => w == transactionInfo.BuyerId)).Returns(buyer);

            accountTableRepository.GetAccountByClientId(Arg.Is<int>(
                w => w == transactionInfo.SellerId)).Returns(seller);

            stockOfClientTableRepository.GetStockOfClientEntityByAccountIdAndType(Arg.Is<int>(
                w => w == seller.AccountId), Arg.Is<string>(
                w => w == transactionInfo.TypeOfStock)).Returns(stockForSell);

            stockOfClientTableRepository.GetStockOfClientEntityByAccountIdAndType(Arg.Is<int>(
                w => w == buyer.AccountId), Arg.Is<string>(
                w => w == transactionInfo.TypeOfStock)).Returns(stockForSell);
            //Act

            seller.Stocks.Add(stockForSell);
            tradingService.MakeATrade(transactionInfo);

            //Assert
            stockOfClientTableRepository.Received(1).Add(Arg.Is<StockOfClientEntity>(
                w => w.AccountForStock == buyer
                && w.quantityOfStocks == transactionInfo.QuantityOfStocks
                && w.TypeOfStocks == transactionInfo.TypeOfStock)
                );

            stockOfClientTableRepository.Received(2).SaveChanges();

            accountTableRepository.Received(1).Change(Arg.Is<AccountEntity>(buyer));
            accountTableRepository.Received(1).Change(Arg.Is<AccountEntity>(seller));
            accountTableRepository.Received(2).SaveChanges();

            historyTableRepository.Received(1).Add(Arg.Is<HistoryEntity>
                (w => w.BuyerId == transactionInfo.BuyerId
                && w.SellerId == transactionInfo.SellerId
                && w.QuantityOfStocks == transactionInfo.QuantityOfStocks
                && w.TypeOfStock == transactionInfo.TypeOfStock
                && w.FullPrice == 80
                ));
            historyTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "This TransactionInfo doesnt contain this type of stock")]
        public void ShouldNotMakeATradeTypeOfStockInArgsIsEmpty()
        {
            //Arrange
            TradingService tradingService = new TradingService(accountTableRepository, stockPriceTableRepository, historyTableRepository, stockOfClientTableRepository, logger);
            TransactionInfo transactionInfo = new TransactionInfo();
            transactionInfo.BuyerId = 1;
            transactionInfo.SellerId = 2;
            transactionInfo.QuantityOfStocks = 20;
            //Act
            tradingService.MakeATrade(transactionInfo);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "StockPrice Table doesnt contain this type of stock")]
        public void ShouldNotMakeATradeTypeOfStockInArgsDoesntExistsInStockPriceTable()
        {
            //Arrange
            TradingService tradingService = new TradingService(accountTableRepository, stockPriceTableRepository, historyTableRepository, stockOfClientTableRepository, logger);
            TransactionInfo transactionInfo = new TransactionInfo();
            transactionInfo.BuyerId = 1;
            transactionInfo.SellerId = 2;
            transactionInfo.TypeOfStock = "AAA";
            transactionInfo.QuantityOfStocks = 20;
            //Act
            tradingService.MakeATrade(transactionInfo);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The Seller doesnt have enough stock of the required type")]
        public void ShouldNotMakeATradeWhenSellerDoesntHaveEnoughStock()
        {
            //Arrange
            TradingService tradingService = new TradingService(accountTableRepository, stockPriceTableRepository, historyTableRepository, stockOfClientTableRepository, logger);
            TransactionInfo transactionInfo = new TransactionInfo();
            transactionInfo.BuyerId = 1;
            transactionInfo.SellerId = 2;
            transactionInfo.TypeOfStock = "AAA";
            transactionInfo.QuantityOfStocks = 20;
            StockPriceEntity stockPriceEntity = new StockPriceEntity()
            {
                TypeOfStock = transactionInfo.TypeOfStock,
                PriceOfStock = 4
            };
            AccountEntity buyer = new AccountEntity()
            {
                AccountId = 11,
                ClientId = 1,
                Balance = 100,
                Stocks = new List<StockOfClientEntity>()
            };
            AccountEntity seller = new AccountEntity()
            {
                AccountId = 22,
                ClientId = 2,
                Balance = 100,
                Stocks = new List<StockOfClientEntity>()
            };
            stockPriceTableRepository.GetStockPriceEntityByStockType(Arg.Is<string>(
                w => w == transactionInfo.TypeOfStock)).Returns(stockPriceEntity);

            accountTableRepository.GetAccountByClientId(Arg.Is<int>(
                w => w == transactionInfo.BuyerId)).Returns(buyer);

            accountTableRepository.GetAccountByClientId(Arg.Is<int>(
                w => w == transactionInfo.SellerId)).Returns(seller);
            //Act
            tradingService.MakeATrade(transactionInfo);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Account Table doesnt contain one of args Id")]
        public void ShouldNotMakeATradeWhenArgsDoesntConteinValidSellerIdOrBuyerId()
        {
            //Arrange
            TradingService tradingService = new TradingService(accountTableRepository, stockPriceTableRepository, historyTableRepository, stockOfClientTableRepository, logger);
            TransactionInfo transactionInfo = new TransactionInfo();
            transactionInfo.BuyerId = 1;
            transactionInfo.SellerId = 2;
            transactionInfo.TypeOfStock = "AAA";
            transactionInfo.QuantityOfStocks = 20;
            StockPriceEntity stockPriceEntity = new StockPriceEntity()
            {
                TypeOfStock = transactionInfo.TypeOfStock,
                PriceOfStock = 4
            };
            stockPriceTableRepository.GetStockPriceEntityByStockType(Arg.Is<string>(
                w => w == transactionInfo.TypeOfStock)).Returns(stockPriceEntity);
            //Act
            tradingService.MakeATrade(transactionInfo);
        }
    }
}
