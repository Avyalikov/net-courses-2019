using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using TradingSimulator.Core.Dto;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Repositories;
using TradingSimulator.Core.Services;

namespace ShopSimulator.Core.Tests
{
    [TestClass]
    public class SaleServiceTests
    {
        ITraderTableRepository traderTableRepository;
        IStockTableRepository stockTableRepository;
        ITraderStockTableRepository traderStockTableRepository;
        IHistoryTableRepository historyTableRepository;
        List<StockToTraderEntity> traderStocksTable;
        SaleService saleHandler;

        [TestInitialize]
        public void Initialize()
        {
            historyTableRepository = Substitute.For<IHistoryTableRepository>();
            traderStockTableRepository = Substitute.For<ITraderStockTableRepository>();
            traderTableRepository = Substitute.For<ITraderTableRepository>();
            traderTableRepository.Get(5).Returns(new TraderEntity()
            {
                Id = 5,
                Name = "Muhamed",
                Surname = "Ali",
                Balance = 123123.0M

            });
            traderTableRepository.Get(40).Returns(new TraderEntity()
            {
                Id = 40,
                Name = "Brad",
                Surname = "Pitt",
                Balance = 1243123.0M
            });

            stockTableRepository = Substitute.For<IStockTableRepository>();
            stockTableRepository.Get(7).Returns(new StockEntity()
            {
                Id = 7,
                Name = "Pepsi",
                PricePerItem = 123.0M
            });
            stockTableRepository.Get(20).Returns(new StockEntity()
            {
                Id = 20,
                Name = "Shmepsi",
                PricePerItem = 33.0M
            });

            this.traderStocksTable = new List<StockToTraderEntity>()
            {
                new StockToTraderEntity()
                {
                    Id = 1,
                    TraderId = 5,
                    StockId = 7,
                    StockCount = 4,
                    PricePerItem = 123.0M
                },
                new StockToTraderEntity()
                {
                    Id = 2,
                    TraderId = 5,
                    StockId = 20,
                    StockCount = 2,
                    PricePerItem = 33.0M
                }
            };

            traderStockTableRepository.FindStocksFromSeller(Arg.Any<BuyArguments>())
               .Returns((callInfo) =>
               {
                   var buyArguments = callInfo.Arg<BuyArguments>();

                   var retVal = this.traderStocksTable.First(w => w.TraderId == buyArguments.SellerID
                                                               && w.StockId == buyArguments.StockID);
                   return retVal;
               });

            traderStockTableRepository.Contains(Arg.Any<BuyArguments>())
               .Returns((callInfo) =>
               {
                   var buyArguments = callInfo.Arg<BuyArguments>();
                   try
                   {
                        var retVal = this.traderStocksTable.First(w => w.TraderId == buyArguments.CustomerID
                                                               && w.StockId == buyArguments.StockID);
                   }
                   catch (Exception)
                   {
                       return false;
                   }
                   return true;
               });

                saleHandler = new SaleService(this.traderStockTableRepository, this.traderTableRepository, this.historyTableRepository);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Imposible to make a sale, because seller has only 4 stocks, but requested 15")]
        public void ShouldThrowExceptionIfStockAmountsIsNotEnough()
        {
            // Arrange
 //           SaleService saleHandler = new SaleService(this.traderStockTableRepository, this.traderTableRepository, this.historyTableRepository);

            var args = new BuyArguments()
            {
                SellerID = 5,
                CustomerID = 40,
                StockID = 7,
                StockCount = 15,
                PricePerItem = 123.0M
            };

            // Act
            saleHandler.HandleBuy(args);
        }

        [TestMethod]
        public void ShouldAddStockToCustomerAfterBuyingIfExists()
        {
            //Arrange
            var args = new BuyArguments()
            {
                SellerID = 40,
                CustomerID = 5,
                StockID = 7,
                StockCount = 2,
                PricePerItem = 123.0M
            };

            //Act
            //saleHandler.HandleBuy(args);
            saleHandler.AdditionStockToCustomer(args);

            //Assert
            this.traderStockTableRepository.Received(1).SubtractStock(Arg.Any<BuyArguments>());
            this.traderStockTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        public void ShouldAddStockToCustomerAfterBuyingIfNotExists()
        {
            //Arrange
            var args = new BuyArguments()
            {
                SellerID = 40,
                CustomerID = 5,
                StockID = 6,
                StockCount = 2,
                PricePerItem = 100.0M
            };

            //Act
            saleHandler.AdditionStockToCustomer(args);

            //Assert
            this.traderStockTableRepository.Received(1).Add(Arg.Any<StockToTraderEntity>());
            this.traderStockTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        public void ShouldAddNewLineToHistory()
        {
            //Arrange
            var args = new BuyArguments()
            {
                SellerID = 40,
                CustomerID = 5,
                StockID = 7,
                StockCount = 2,
                PricePerItem = 123.0M
            };

            //Act
            saleHandler.SaveHistory(args);

            //Assert
            this.historyTableRepository.Received(1).Add(Arg.Is<HistoryEntity>(
                w => w.CustomerID == args.CustomerID
                && w.SellerID == args.SellerID
                && w.StockID == args.StockID
                && w.StockCount == args.StockCount
                && w.TotalPrice == (args.StockCount * args.PricePerItem)));
            this.historyTableRepository.Received(1).SaveChanges();
        }
    }
}