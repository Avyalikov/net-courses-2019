using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using trading_software;
using NSubstitute;
using System.Linq;

namespace trading_software.Tests
{
    [TestClass]
    public class StockManagerTests
    {
        /*       [TestMethod]
              public void ManualAddStockTest()
               {
                   // Arrange
                   var inputDeviceMock = Substitute.For<IInputDevice>();
                   var outpuDeviceMock = Substitute.For<IOutputDevice>();
                   var tableDrawerMock = Substitute.For<ITableDrawer>();
                   var dataBaseDeviceMock = Substitute.For<IDataBaseDevice>();

                   var sut = new StockManager(
                       inputDeviceMock, 
                       outpuDeviceMock,
                       tableDrawerMock,
                       dataBaseDeviceMock);

                   // Act
                   sut.ManualAddStock();

                   // Asserts
                   outpuDeviceMock.Received(1).WriteLine(Arg.Is<string>(w => w == "Write Stock Type:"));
                   inputDeviceMock
                        .ReadLine()
                        .Returns("Umbrella");
                   outpuDeviceMock.Received(1).WriteLine(Arg.Is<string>(w => w == "Write stock price:"));
                   inputDeviceMock
                        .ReadLine()
                        .Returns("123321");
                   sut.Received(1)
                       .AddStock(
                           Arg.Is<string>(w => w == "Umbrella"),
                           Arg.Is<decimal>(w => w == 123321)
                       );
               }*/

        [TestMethod]
        public void AddStockParametersTest()
        {
            // Arrange
            var inputDeviceMock = Substitute.For<IInputDevice>();
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            var tableDrawerMock = Substitute.For<ITableDrawer>();
            var dataBaseDeviceMock = Substitute.For<IDataBaseDevice>();

            var sut = new StockManager(
                inputDeviceMock,
                outpuDeviceMock,
                tableDrawerMock,
                dataBaseDeviceMock);

            IQueryable<Stock> stocks =
                new[]
                {
                    new Stock
                    {
                        StockID = 1,
                        StockType = "Umbrella",
                        Price = (decimal) 23154
                    },
                    new Stock
                    {
                        StockID = 2,
                        StockType = "Weyland-Yutani",
                        Price = (decimal) 642134
                    }
                }.AsQueryable();
            
            string stockName = "Umbrella";
            decimal stockPrice = 23154;

            // Act
            sut.AddStock(stockName, stockPrice);

            // Asserts
            dataBaseDeviceMock
                .Received(1)
                .IsStockExist(Arg.Is<string>(st => st == stockName));
            dataBaseDeviceMock
                .Received(1)
                .Add(Arg.Is<Stock>(st => st.StockType == stockName &&
                                         st.Price == stockPrice));
        }

        [TestMethod]
        public void AddStockTest()
        {
            // Arrange
            var inputDeviceMock = Substitute.For<IInputDevice>();
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            var tableDrawerMock = Substitute.For<ITableDrawer>();
            var dataBaseDeviceMock = Substitute.For<IDataBaseDevice>();

            var sut = new StockManager(
                inputDeviceMock,
                outpuDeviceMock,
                tableDrawerMock,
                dataBaseDeviceMock);
            Stock stock = new Stock { StockType = "Weyland-Yutani", Price = (decimal)15316 };
            // Act
            sut.AddStock(stock);

            // Asserts
            dataBaseDeviceMock
                .Received(1)
                .IsStockExist(Arg.Is<string>(st => st == stock.StockType));
            dataBaseDeviceMock
                .Received(1)
                .Add(stock);
            }

        [TestMethod]
        public void SelectRandomIDTest()
        {
            // Arrange
            var inputDeviceMock = Substitute.For<IInputDevice>();
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            var tableDrawerMock = Substitute.For<ITableDrawer>();
            var dataBaseDeviceMock = Substitute.For<IDataBaseDevice>();

            var sut = new StockManager(
                inputDeviceMock,
                outpuDeviceMock,
                tableDrawerMock,
                dataBaseDeviceMock);

        }


        [TestMethod]
        public void ReadAllStockTest()
        {
            // Arrange
            var inputDeviceMock = Substitute.For<IInputDevice>();
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            var tableDrawerMock = Substitute.For<ITableDrawer>();
            var dataBaseDeviceMock = Substitute.For<IDataBaseDevice>();

            var sut = new StockManager(
                inputDeviceMock,
                outpuDeviceMock,
                tableDrawerMock,
                dataBaseDeviceMock);

            // Act
            sut.ReadAllStocks();

            // Asserts

        }
    }
}

