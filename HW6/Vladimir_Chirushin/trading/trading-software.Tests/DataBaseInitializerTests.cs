using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

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
            var blockOfSharesManagerMock = Substitute.For<IBlockOfSharesManager>();

            var sut = new DataBaseInitializer(
                clientManagerMock,
                stockManagerMock,
                blockOfSharesManagerMock
                );

            // Act
            sut.Initiate();

            // Asserts
            clientManagerMock.Received(21).AddClient(Arg.Any<Client>());
            stockManagerMock.Received(20).AddStock(Arg.Any<Stock>());
            blockOfSharesManagerMock.Received(200).CreateRandomShare();
        }
    }
}
