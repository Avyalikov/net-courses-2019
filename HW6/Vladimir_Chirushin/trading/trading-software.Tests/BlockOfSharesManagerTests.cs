using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using trading_software;
using NSubstitute;

namespace trading_software.Tests
{
    [TestClass]
    public class BlockOfSharesManagerTests
    {
        [TestMethod]
        public void AddShareBlockTest()
        {
            // Arrange
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            var inputDeviceMock = Substitute.For<IInputDevice>();
            var tableDrawerMock = Substitute.For<ITableDrawer>();
        
            var sut = new BlockOfSharesManager(
                inputDeviceMock,
                outpuDeviceMock,
                tableDrawerMock);

            BlockOfShares block = new BlockOfShares
            {
                ClientID = 1,
                StockID = 2,
                Amount = 1
            };
            int clientID = 1;
            int stockID = 2;
            int amount = 3;

            // Act
            sut.AddShare(clientID, stockID, amount);

            // Asserts
            sut.Received(1).AddShare(Arg.Is<BlockOfShares>(b => b == block));
        }
    }
}