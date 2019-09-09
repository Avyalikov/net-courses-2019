using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using TradingApp.Core.Dto;
using TradingApp.Core.Models;
using TradingApp.Core.Repositories;
using TradingApp.Core.Services;


namespace TradingApp.Core.Tests
{
    [TestClass]
    public class StocksServiceTests
    {
        [TestMethod]
        public void ShouldRegisterNewStock()
        {
            //Arrange
            var stockTableRepository = Substitute.For<IStockTableRepository>();
            StocksService stockService = new StocksService(stockTableRepository);
            StockRegistrationInfo args = new StockRegistrationInfo();
            args.Type = "StockTypeA";
            args.Price = 10;

            //Act
            var stockId = stockService.RegisterNewStock(args);

            //Assert
            stockTableRepository.Received(1).Add(Arg.Is<StockEntity>(w =>
            w.Type == args.Type
            && w.Price == args.Price));
            stockTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "This stock exists already.Can't continue")]
        public void ShouldNotRegisterNewStockIfItExists()
        {
            //Arrange
            var stockTableRepository = Substitute.For<IStockTableRepository>();
            StocksService stockService = new StocksService(stockTableRepository);
            StockRegistrationInfo args = new StockRegistrationInfo();
            args.Type = "StockTypeA";
            args.Price = 10;

            //Act
            stockService.RegisterNewStock(args);

            stockTableRepository.Contains(Arg.Is<StockEntity>(w =>
            w.Type == args.Type
            && w.Price == args.Price)).Returns(true);
            stockService.RegisterNewStock(args);

        }

        [TestMethod]
        public void ShouldGetStockInfo()
        {
            //Arrange
            var stockTableRepository = Substitute.For<IStockTableRepository>();
            stockTableRepository.Contains(Arg.Is(11)).Returns(true);
            StocksService stockService = new StocksService(stockTableRepository);

            // Act
            var stock = stockService.GetStock(11);

            //Assert
            stockTableRepository.Received(1).Get(11);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Can't find stock with this Id. It might be not exists")]
        public void ShouldGetExceptionIfStockNotRegistered()
        {
            //Arrange
            var stockTableRepository = Substitute.For<IStockTableRepository>();
            stockTableRepository.Contains(Arg.Is(11)).Returns(false);
            StocksService stockService = new StocksService(stockTableRepository);

            // Act
            var stock = stockService.GetStock(11);
        }
    }

}
