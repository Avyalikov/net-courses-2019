using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using trading_software;
using NSubstitute;
using System.Linq;

namespace trading_software.Tests
{
    [TestClass]
    public class DataBaseInitializerTest
    {
        [TestMethod]
        public void ReduceAssetsRandomClientTest()
        {
            // Arrange
            var clientManagerMock = Substitute.For<IClientManager>();
            var stockManagerMock = Substitute.For<IStockManager>();

            var sut = new DataBaseInitializer(
                clientManagerMock,
                stockManagerMock
                );

            // Act
            sut.Initiate();

            // Asserts
            clientManagerMock.Received(21).AddClient(Arg.Any<Client>());
            stockManagerMock.Received(20).AddStock(Arg.Any<Stock>());
        }
    }
}
