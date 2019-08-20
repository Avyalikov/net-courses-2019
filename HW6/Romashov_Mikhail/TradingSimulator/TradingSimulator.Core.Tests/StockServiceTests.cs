using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using TradingSimulator.Core.Dto;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Repositories;
using TradingSimulator.Core.Services;

namespace TradingSimulator.Core.Tests
{
    [TestClass]
    public class StockServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Can`t get stock by this Id.")]

        public void ShouldThrowExceptionIfCantFindTrader()
        {
            //Arrange 
            var stockTableRepository = Substitute.For<IStockTableRepository>();
            stockTableRepository.ContainsById(Arg.Is<int>(55)).Returns(false);
            StockService stockService = new StockService(stockTableRepository);

            //Act
            var stocks = stockService.GetStock(55);
        }

        [TestMethod]
        public void ShouldGetTraderInfo()
        {
            var stockTableRepository = Substitute.For<IStockTableRepository>();
            stockTableRepository.ContainsById(Arg.Is<int>(55)).Returns(true);
            StockService stockService = new StockService(stockTableRepository);

            //Act
            var stocks = stockService.GetStock(55);

            //Assert
            stockTableRepository.Received(1).Get(55);
        }
    }
}
