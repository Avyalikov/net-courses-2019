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
        IStockTableRepository stockTableRepository;
        StockService stockService;

        [TestInitialize]
        public void Initialize()
        {
            stockTableRepository = Substitute.For<IStockTableRepository>();
            stockService = new StockService(this.stockTableRepository);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Can`t get stock by this Id = 55.")]

        public void ShouldThrowExceptionIfCantFindStockById()
        {
            //Arrange 
            stockTableRepository.ContainsById(Arg.Is<int>(55)).Returns(false);

            //Act
            var stocks = stockService.GetStockById(55);
        }

        [TestMethod]
        public void ShouldGetStockInfoById()
        {
            stockTableRepository.ContainsById(Arg.Is<int>(55)).Returns(true);

            //Act
            var stocks = stockService.GetStockById(55);

            //Assert
            stockTableRepository.Received(1).GetById(55);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Can`t get stock by this name = Unit tests.")]

        public void ShouldThrowExceptionIfCantFindStockByName()
        {
            //Arrange 
            stockTableRepository.ContainsByName(Arg.Is<string>("Unit tests")).Returns(false);

            //Act
            var stocks = stockService.GetStockByName("Unit tests");
        }

        [TestMethod]
        public void ShouldGetStockInfoByName()
        {
            //Arrange
            stockTableRepository.ContainsByName(Arg.Is<string>("Intel stock")).Returns(true);

            //Act
            var stocks = stockService.GetStockByName("Intel stock");

            //Assert
            stockTableRepository.Received(1).GetByName("Intel stock");
        }
    }
}
