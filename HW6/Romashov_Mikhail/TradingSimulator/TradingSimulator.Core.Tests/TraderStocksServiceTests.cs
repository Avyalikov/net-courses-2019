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
    public class TraderStocksServiceTests
    {
        ITraderStockTableRepository traderStocksTableRepository;
        TraderStocksService traderStockService;

        [TestInitialize]
        public void Initialize()
        {
            traderStocksTableRepository = Substitute.For<ITraderStockTableRepository>();
            traderStockService = new TraderStocksService(this.traderStocksTableRepository);
        }
        [TestMethod]
        public void ShouldAddStockToTrader()
        {
            //Arrange
            TraderInfo trader = new TraderInfo();
            StockInfo stock = new StockInfo();

            trader.Id = 2;
            stock.Id = 3;
            stock.Count = 2;

            //Act
            var traderStockID = traderStockService.AddNewStockToTrader(trader, stock);

            //Assert
            traderStocksTableRepository.Received(1).Add(Arg.Is<StockToTraderEntityDB>(
                w => w.TraderId == trader.Id
                && w.StockId == stock.Id
                && w.StockCount == stock.Count));
            traderStocksTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "This stock Intel has been added for trader Monica Belucci.")]
        public void ShouldNotAddNewStockToTraderIfExists()
        {
            //Arrange
            TraderInfo trader = new TraderInfo();
            StockInfo stock = new StockInfo();

            trader.Id = 2;
            trader.Name = "Monica";
            trader.Surname = "Belucci";
            trader.PhoneNumber = "+79110000000";

            stock.Id = 3;
            stock.Name = "Intel";
            stock.Count = 2;
            //Act
            var traderStockID = traderStockService.AddNewStockToTrader(trader, stock);

            traderStocksTableRepository.Contains(Arg.Is<StockToTraderEntityDB>(
                w => w.TraderId == trader.Id
                && w.StockId == stock.Id)).Returns(true);
            traderStockService.AddNewStockToTrader(trader, stock);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Can`t find item by this id = 55.")]

        public void ShouldThrowExceptionIfCantFindTraderStockById()
        {
            //Arrange 
            traderStocksTableRepository.ContainsById(Arg.Is<int>(55)).Returns(false);

            //Act
            var traderStock = traderStockService.GetTraderStockById(55);
        }

        [TestMethod]
        public void ShouldGetTraderStockById()
        {
            //Arrange 
            traderStocksTableRepository.ContainsById(Arg.Is<int>(55)).Returns(true);

            //Act
            var traders = traderStockService.GetTraderStockById(55);

            //Assert
            traderStocksTableRepository.Received(1).GetTraderStockById(55);
        }
    }
}
