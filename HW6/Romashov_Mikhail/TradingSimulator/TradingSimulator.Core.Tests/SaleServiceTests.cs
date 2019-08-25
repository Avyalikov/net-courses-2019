using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using TradingSimulator.Core.Dto;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Repositories;
using TradingSimulator.Core.Services;

namespace TradingSimulator.Core.Tests
{
    [TestClass]
    public class SaleServiceTests
    {
        ITraderTableRepository traderTableRepository;
        IStockTableRepository stockTableRepository;
        ITraderStockTableRepository traderStockTableRepository;
        IHistoryTableRepository historyTableRepository;
        List<StockToTraderEntityDB> traderStocksTable;
        SaleService saleHandler;

        [TestInitialize]
        public void Initialize()
        {
            historyTableRepository = Substitute.For<IHistoryTableRepository>();
            traderStockTableRepository = Substitute.For<ITraderStockTableRepository>();
            traderTableRepository = Substitute.For<ITraderTableRepository>();

            traderTableRepository.GetById(5).Returns(new TraderEntityDB()
            {
                Id = 5,
                Name = "Muhamed",
                Surname = "Ali",
                Balance = 123123.0M

            });
            traderTableRepository.GetById(40).Returns(new TraderEntityDB()
            {
                Id = 40,
                Name = "Brad",
                Surname = "Pitt",
                Balance = 1243123.0M
            });

            stockTableRepository = Substitute.For<IStockTableRepository>();
            stockTableRepository.GetById(7).Returns(new StockEntityDB()
            {
                Id = 7,
                Name = "Pepsi",
                PricePerItem = 123.0M
            });
            stockTableRepository.GetById(20).Returns(new StockEntityDB()
            {
                Id = 20,
                Name = "Shmepsi",
                PricePerItem = 33.0M
            });

            this.traderStocksTable = new List<StockToTraderEntityDB>()
            {
                new StockToTraderEntityDB()
                {
                    Id = 1,
                    TraderId = 5,
                    StockId = 7,
                    StockCount = 4,
                    PricePerItem = 123.0M
                },
                new StockToTraderEntityDB()
                {
                    Id = 2,
                    TraderId = 5,
                    StockId = 20,
                    StockCount = 2,
                    PricePerItem = 33.0M
                },
                 new StockToTraderEntityDB()
                {
                    Id = 3,
                    TraderId = 40,
                    StockId = 7,
                    StockCount = 2,
                    PricePerItem = 33.0M
                }
            };

            traderStockTableRepository.GetStocksFromSeller(Arg.Any<BuyArguments>())
               .Returns((callInfo) =>
               {
                   var buyArguments = callInfo.Arg<BuyArguments>();

                   var retVal = this.traderStocksTable.First(w => w.TraderId == buyArguments.SellerID
                                                               && w.StockId == buyArguments.StockID);
                   return retVal;
               });

            traderStockTableRepository.ContainsSeller(Arg.Any<BuyArguments>())
               .Returns((callInfo) =>
               {
                   var buyArguments = callInfo.Arg<BuyArguments>();
                   try
                   {
                        var retVal = this.traderStocksTable.First(w => w.TraderId == buyArguments.SellerID
                                                               && w.StockId == buyArguments.StockID);
                   }
                   catch (Exception)
                   {
                       return false;
                   }
                   return true;
               });

            traderStockTableRepository.ContainsCustomer(Arg.Any<BuyArguments>())
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

            traderStockTableRepository.Contains(Arg.Any<StockToTraderEntityDB>())
               .Returns((callInfo) =>
               {
                   var stockToTrader = callInfo.Arg<StockToTraderEntityDB>();
                   try
                   {
                       var retVal = this.traderStocksTable.First(w => w.TraderId == stockToTrader.TraderId
                                                              && w.StockId == stockToTrader.StockId);
                   }
                   catch (Exception)
                   {
                       return false;
                   }
                   return true;
               });

            traderTableRepository.ContainsById(5).Returns(true);
            traderTableRepository.ContainsById(40).Returns(true);
            

            saleHandler = new SaleService(this.traderStockTableRepository, this.traderTableRepository, this.historyTableRepository);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Imposible to make a sale, because seller hasn`t this stock id = 99")]
        public void ShouldThrowExceptionIfHasntStock()
        {
            // Arrange
            var args = new BuyArguments()
            {
                SellerID = 5,
                CustomerID = 40,
                StockID = 99,
                StockCount = 15,
                PricePerItem = 123.0M
            };

            // Act
            saleHandler.HandleBuy(args);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Imposible to make a sale, because seller has only 4 stocks, but requested 15")]
        public void ShouldThrowExceptionIfStockAmountsIsNotEnough()
        {
            // Arrange
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
        public void ShouldSubtractStockFromSellerAfterSale()
        {
            //Arrange
            var args = new BuyArguments()
            {
                SellerID = 5,
                CustomerID = 40,
                StockID = 7,
                StockCount = 2,
                PricePerItem = 123.0M
            };

            //Act
            saleHandler.HandleBuy(args);

            //Assert
            this.traderStockTableRepository.Received(1).SubtractStockFromSeller(Arg.Any<BuyArguments>());
            this.traderStockTableRepository.Received(2).SaveChanges();
        }

        [TestMethod]
        public void ShouldAddStockToCustomerAfterBuyingIfExists()
        {
            //Arrange
            var args = new BuyArguments()
            {
                SellerID = 5,
                CustomerID = 40,
                StockID = 7, //Customer has this stock
                StockCount = 2,
                PricePerItem = 123.0M
            };

            //Act
            saleHandler.HandleBuy(args);
            
            //Assert
            this.traderStockTableRepository.Received(1).AdditionalStockToCustomer(Arg.Any<BuyArguments>());
            this.traderStockTableRepository.Received(2).SaveChanges();
        }

        [TestMethod]
        public void ShouldSubstractBalanceFromCustomer()
        {
            //Arrange
            var args = new BuyArguments()
            {
                SellerID = 5,
                CustomerID = 40,
                StockID = 7, //Customer has this stock
                StockCount = 2,
                PricePerItem = 123.0M
            };

            //Act
            saleHandler.HandleBuy(args);

            //Assert
            this.traderTableRepository.AdditionBalance(40, 246);
            this.traderStockTableRepository.Received(2).SaveChanges();
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Cant get trader by this id = 45.")]
        public void ShouldThrowExceptionIfNotContainsCustomerToSubstract()
        {
            //Arrange
            var args = new BuyArguments()
            {
                SellerID = 23,
                CustomerID = 45, //Bad value
                StockID = 7,
                StockCount = 2,
                PricePerItem = 123.0M
            };

            //Act
            saleHandler.HandleBuy(args);
        }

        [TestMethod]
        public void ShouldAdditionalBalanceToSeller()
        {
            //Arrange
            var args = new BuyArguments()
            {
                SellerID = 5,
                CustomerID = 40,
                StockID = 7, 
                StockCount = 2,
                PricePerItem = 123.0M
            };

            //Act
            saleHandler.HandleBuy(args);

            //Assert
            this.traderTableRepository.AdditionBalance(5, 246);
            this.traderStockTableRepository.Received(2).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Cant get trader by this id = 23.")]
        public void ShouldThrowExceptionIfNotContainsSellerToAdditional()
        { 
            //Arrange
            var args = new BuyArguments()
            {
                SellerID = 23, //Bad value
                CustomerID = 40,
                StockID = 7, 
                StockCount = 2,
                PricePerItem = 123.0M
            };

            //Act
            saleHandler.HandleBuy(args);
        }

        [TestMethod]
        public void ShouldAddStockToCustomerAfterBuyingIfNotExists()
        {
            //Arrange
            var args = new BuyArguments()
            {
                SellerID = 5,
                CustomerID = 40,
                StockID = 20, //Customer hasnt this stock
                StockCount = 2,
                PricePerItem = 100.0M
            };

            var entityToAdd = new StockToTraderEntityDB()
            {
                TraderId = args.CustomerID,
                StockId = args.StockID,
                StockCount = args.StockCount,
                PricePerItem = args.PricePerItem
            };

            //Act
            saleHandler.HandleBuy(args);

            //Assert
            this.traderStockTableRepository.Received(1).Add(Arg.Any<StockToTraderEntityDB>());
            this.traderStockTableRepository.Received(2).SaveChanges();
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