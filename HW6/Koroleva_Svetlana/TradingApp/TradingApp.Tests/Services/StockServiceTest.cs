using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Trading.Core.Repositories;
using Trading.Core.Services;
using Trading.Core.DTO;
using Trading.Core.Model;


namespace TradingApp.Tests
{
    [TestClass]
    public class StockServiceTest
    {
        [TestMethod]
        public void ShouldAddNewStock()
        {
            //Arrange
            var stockTableRepository = Substitute.For<ITableRepository<Stock>>();
            StockService stockService = new StockService(stockTableRepository);
            StockInfo stockInfo = new StockInfo
            {
               IssuerId=1,
               ShareType=(StockInfo.StockType)StockType.Common,
               StockPrefix="KORS"
            };
            //Act
            stockService.AddStock(stockInfo);
            //Assert
            stockTableRepository.Received(1).Add(Arg.Is<Stock>(
                w => w.IssuerID==stockInfo.IssuerId&&
                w.StockType==(StockType)stockInfo.ShareType&&
                w.StockPrefix==stockInfo.StockPrefix
                ));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "This stock exists. Can't continue")]
        public void ShouldNotRegisterNewStockIfItExists()
        {
            // Arrange
            var stockTableRepository = Substitute.For<ITableRepository<Stock>>();
            StockService stockService = new StockService(stockTableRepository);
            StockInfo stockInfo = new StockInfo
            {
                IssuerId = 1,
                ShareType = (StockInfo.StockType)StockType.Common,
                StockPrefix = "KORS"
            };
            // Act
            stockService.AddStock(stockInfo);

            stockTableRepository.ContainsDTO(Arg.Is<Stock>(
                w => w.IssuerID == stockInfo.IssuerId &&
                w.StockType == (StockType)stockInfo.ShareType &&
                w.StockPrefix == stockInfo.StockPrefix)).Returns(true);

            stockService.AddStock(stockInfo);
        }

        [TestMethod]
        public void ShouldGetStockInfo()
        {
            // Arrange
            var stockTableRepository = Substitute.For<ITableRepository<Stock>>();
            StockService stockService = new StockService(stockTableRepository);
            stockTableRepository.ContainsByPK(Arg.Is(3)).Returns(true);


            // Act
            var stock = stockService.GetStockByID(3);

            // Assert
            stockTableRepository.Received(1).FindByPK(3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Stock doesn't exist")]
        public void ShouldThrowExceptionIfCantFindStock()
        {
            // Arrange
            var stockTableRepository = Substitute.For<ITableRepository<Stock>>();
            StockService stockService = new StockService(stockTableRepository);
            stockTableRepository.ContainsByPK(Arg.Is(3)).Returns(false);

            // Act
            var stock = stockService.GetStockByID(3);


        }
    }
}
