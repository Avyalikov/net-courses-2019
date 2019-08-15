using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using trading_software;
using NSubstitute;
using System.Linq;

namespace trading_software.Tests
{
    [TestClass]
    public class TableDrawerTests
    {
        [TestMethod]
        public void ShowClient()
        {
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            // Arrange
            var sut = new TableDrawer(outpuDeviceMock);
            IQueryable<Client> clients =
                new[] {
                    new Client
                    {
                        ClientID = 1,
                        Name = "Martin Eden",
                        PhoneNumber = "555-55-55",
                        Balance = (decimal) 6021023
                    },
                    new Client
                    {
                        ClientID = 2,
                        Name = "Ruth Morse",
                        PhoneNumber = "444-44-44",
                        Balance = (decimal) 271828
                    }
                }.AsQueryable();
            // Act
            sut.Show(clients);

            // Asserts
            outpuDeviceMock.Received(1).WriteLine(Arg.Is<string>(w => w == "___________________________________________________________"));
            outpuDeviceMock.Received(1).WriteLine(Arg.Is<string>(w => w == "|   #|                  Name|  Phone Number|       Balance|"));
            outpuDeviceMock.Received(1).WriteLine(Arg.Is<string>(w => w == "|----|----------------------|--------------|--------------|"));
            outpuDeviceMock.Received(1).WriteLine(Arg.Is<string>(w => w == "|   1|           Martin Eden|     555-55-55|       6021023|"));
            outpuDeviceMock.Received(1).WriteLine(Arg.Is<string>(w => w == "|   2|            Ruth Morse|     444-44-44|        271828|"));
            outpuDeviceMock.Received(1).WriteLine(Arg.Is<string>(w => w == "|____|______________________|______________|______________|"));
        }

        [TestMethod]
        public void ShowStock()
        {
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            // Arrange
            var sut = new TableDrawer(outpuDeviceMock);
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

            // Act
            sut.Show(stocks);

            // Asserts
            outpuDeviceMock.Received(1).WriteLine(Arg.Is<string>(w => w == "____________________________________________"));
            outpuDeviceMock.Received(1).WriteLine(Arg.Is<string>(w => w == "|   #|            Stock Type|     Price ATM|"));
            outpuDeviceMock.Received(1).WriteLine(Arg.Is<string>(w => w == "|----|----------------------|--------------|"));
            outpuDeviceMock.Received(1).WriteLine(Arg.Is<string>(w => w == "|   1|              Umbrella|         23154|"));
            outpuDeviceMock.Received(1).WriteLine(Arg.Is<string>(w => w == "|   2|        Weyland-Yutani|        642134|"));
            outpuDeviceMock.Received(1).WriteLine(Arg.Is<string>(w => w == "|____|______________________|______________|"));
        }

        [TestMethod]
        public void ShowTransaction()
        {
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            // Arrange
            var sut = new TableDrawer(outpuDeviceMock);
            Transaction transaction1 =
                new Transaction
                {
                    TransactionID = 1,
                    dateTime = new DateTime(1955, 10, 26, 19, 0, 0, 0),
                    SellerID = 1,
                    BuyerID = 2,
                    StockID = 1,
                    Amount = 1
                };
            Transaction transaction2 =
                new Transaction
                {
                    TransactionID = 2,
                    dateTime = new DateTime(1985, 10, 26, 19, 0, 0, 0),
                    SellerID = 2,
                    BuyerID = 1,
                    StockID = 2,
                    Amount = 1
                };

            IQueryable<Transaction> transactions =
                new[]
                {
                    transaction1,
                    transaction2
                }.AsQueryable();

            // Act
            sut.Show(transactions);

            // Asserts
            outpuDeviceMock.Received(1).WriteLine(Arg.Is<string>(w => w == "_________________________________________________________________________________________________________________"));
            outpuDeviceMock.Received(1).WriteLine(Arg.Is<string>(w => w == "|   #|       Date and Time|                Seller|                 Buyer|                 Stock|Quan|Transaction|"));
            outpuDeviceMock.Received(1).WriteLine(Arg.Is<string>(w => w == "|----|--------------------|----------------------|----------------------|----------------------|----|-----------|"));
            outpuDeviceMock.Received(1).WriteLine(Arg.Is<string>(w => w == "|   1| 26.10.1955 19:00:00|           Martin Eden|            Ruth Morse|              Umbrella|   1|     23154$|"));
            outpuDeviceMock.Received(1).WriteLine(Arg.Is<string>(w => w == "|   2| 26.10.1985 19:00:00|            Ruth Morse|           Martin Eden|        Weyland-Yutani|   1|    642134$|"));
            outpuDeviceMock.Received(1).WriteLine(Arg.Is<string>(w => w == "|____|____________________|______________________|______________________|______________________|____|___________|"));
        }
    }
}
