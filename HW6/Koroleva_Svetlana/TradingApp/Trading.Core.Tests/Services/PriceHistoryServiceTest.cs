using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Trading.Core.Repositories;
using Trading.Core.Services;
using Trading.Core.DTO;
using Trading.Core.Model;
using System.Collections.Generic;

namespace TradingApp.Tests
{
    [TestClass]
    public class PriceHistoryServiceTest
    {
        [TestMethod]
        public void ShouldAddNewPriceHistory()
        {
            //Arrange
            var phTableRepository = Substitute.For<ITableRepository<PriceHistory>>();
            var DTOMethods = Substitute.For<IDTOMethodsforPriceHistory>();
            PriceHistoryService phService = new PriceHistoryService(phTableRepository, DTOMethods);
            PriceInfo priceInfo = new PriceInfo
            {
                StockId=1,
                DateTimeBegin= new DateTime(2019, 8, 20, 09, 55, 00),
                DateTimeEnd= new DateTime(2019, 8, 20, 19, 30, 00),
                Price =200
            };
            //Act
            phService.AddPriceInfo(priceInfo);
            //Assert
            phTableRepository.Received(1).Add(Arg.Is<PriceHistory>(
                w => w.StockID==1&&
                w.DateTimeBegin == new DateTime(2019, 8, 20, 09, 55, 00)&&
                w.DateTimeEnd== new DateTime(2019, 8, 20, 19, 30, 00)&&
                w.Price==200
                ));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "This price history exists. Can't continue")]
        public void ShouldNotRegisterNewCPriceHistoryIfItExists()
        {
            // Arrange
            var phTableRepository = Substitute.For<ITableRepository<PriceHistory>>();
            var DTOMethods = Substitute.For<IDTOMethodsforPriceHistory>();
            PriceHistoryService phService = new PriceHistoryService(phTableRepository, DTOMethods);
            PriceInfo priceInfo = new PriceInfo
            {
                StockId = 1,
                DateTimeBegin = new DateTime(2019, 8, 20, 09, 55, 00),
                DateTimeEnd = new DateTime(2019, 8, 20, 19, 30, 00),
                Price = 200
            };
            // Act
            phService.AddPriceInfo(priceInfo);

            phTableRepository.ContainsDTO(Arg.Is<PriceHistory>(
                 w => w.StockID == 1 &&
                w.DateTimeBegin == new DateTime(2019, 8, 20, 09, 55, 00) &&
                w.DateTimeEnd == new DateTime(2019, 8, 20, 19, 30, 00) &&
                w.Price == 200)).Returns(true);
            phService.AddPriceInfo(priceInfo);
        }

        [TestMethod]
        public void ShouldGetPriceInfo()
        {
            // Arrange
            var phTableRepository = Substitute.For<ITableRepository<PriceHistory>>();
            var DTOMethods = Substitute.For<IDTOMethodsforPriceHistory>();
            PriceHistoryService phService = new PriceHistoryService(phTableRepository, DTOMethods);
            PriceHistory priceHist = new PriceHistory
            {
                PriceHistoryID = 1,
                StockID = 1,
                DateTimeBegin = new DateTime(2019, 8, 20, 09, 55, 00),
                DateTimeEnd = new DateTime(2019, 8, 20, 19, 30, 00),
                Price = 200
            };
            PriceArguments priceArguments = new PriceArguments()
            {
                StockId=1,
                DateTimeLookUp= new DateTime(2019, 8, 20, 09, 56, 00)
            };
            DTOMethods.FindEntitiesByRequestDTO(priceArguments).Returns(new List<PriceHistory> { priceHist });
           


            // Act
            var priceHistory = phService.GetStockPriceByDateTime(priceArguments);

            // Assert 
            var hist=DTOMethods.Received(1).FindEntitiesByRequestDTO(priceArguments);
        

        }


        [TestMethod]
        public void ShouldEditPriceDateEnd()
        {
            // Arrange
            var phTableRepository = Substitute.For<ITableRepository<PriceHistory>>();
            var DTOMethods = Substitute.For<IDTOMethodsforPriceHistory>();
            PriceHistoryService phService = new PriceHistoryService(phTableRepository, DTOMethods);

            PriceHistory lastpriceHist = new PriceHistory
            {
                PriceHistoryID = 1,
                StockID = 1,
                DateTimeBegin = new DateTime(2019, 8, 20, 09, 55, 00),
                DateTimeEnd = new DateTime(2019, 8, 20, 19, 30, 00),
                Price = 200
            };

            IEnumerable <PriceHistory > histories= new List<PriceHistory>(){ lastpriceHist};
            PriceArguments priceArguments = new PriceArguments()
            {
                StockId = 1,
                DateTimeLookUp = new DateTime(2019, 8, 20, 09, 56, 00)
            };

            int stockId = 1;
            DateTime DateTimeEnd = new DateTime(2019, 8, 20, 09, 56, 00);
            DTOMethods.FindEntitiesByRequest(stockId).Returns(new List<PriceHistory> { lastpriceHist });
            



            // Act
            phService.EditPriceDateEnd(stockId,DateTimeEnd);

            // Assert 
            var phistories = DTOMethods.FindEntitiesByRequest(stockId);
           lastpriceHist.DateTimeEnd = DateTimeEnd;
            phTableRepository.SaveChanges();


        }

    }
}
