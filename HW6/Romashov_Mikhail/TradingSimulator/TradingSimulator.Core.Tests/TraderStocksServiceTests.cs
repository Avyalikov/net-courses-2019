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
        [TestMethod]
        public void ShouldAddStockToTrader()
        {
            //Arrange
            var traderStocksTableRepository = Substitute.For<ITraderStockTableRepository>();
            TraderStocksService traderStockService = new TraderStocksService(traderStocksTableRepository);
            TraderInfo trader = new TraderInfo();
            StockInfo stock = new StockInfo();

            trader.Id = 2;
            //trader.Name = "Monica";
            //trader.Surname = "Belucci";
            //trader.PhoneNumber = "+79110000000";

            stock.Id = 3;
            //stock.Name = "Intel";
            stock.Count = 2;
            //Act
            var traderStockID = traderStockService.AddNewStockToTrader(trader, stock);

            //Assert
            traderStocksTableRepository.Received(1).Add(Arg.Is<StockToTraderEntity>(
                w => w.TraderId == trader.Id
                && w.StockId == stock.Id
                && w.StockCount == stock.Count));
            traderStocksTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "This stock has been added for trader.")]
        public void ShouldNotAddNewStockToTraderIfExists()
        {
            //Arrange
            var traderStocksTableRepository = Substitute.For<ITraderStockTableRepository>();
            TraderStocksService traderStockService = new TraderStocksService(traderStocksTableRepository);
            TraderInfo trader = new TraderInfo();
            StockInfo stock = new StockInfo();

            trader.Id = 2;
            //trader.Name = "Monica";
            //trader.Surname = "Belucci";
            //trader.PhoneNumber = "+79110000000";

            stock.Id = 3;
            //stock.Name = "Intel";
            stock.Count = 2;
            //Act
            var traderStockID = traderStockService.AddNewStockToTrader(trader, stock);

            traderStocksTableRepository.Contains(Arg.Is<StockToTraderEntity>(
                w => w.TraderId == trader.Id
                && w.StockId == stock.Id)).Returns(true);
            traderStockService.AddNewStockToTrader(trader, stock);
        }
    }


}
