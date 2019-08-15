using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using trading_software;
using NSubstitute;

namespace trading_software.Tests
{
    [TestClass]
    public class TradingEngineTests_NSubstitute
    {
        [TestMethod]
        public void TradingEngineShowMenu()
        {
        //    var outpuDeviceMock = Substitute.For<IOutputDevice>();
        //    var inputDeviceMock = Substitute.For<IInputDevice>();
        //    var tableDrawerMock = Substitute.For<ITableDrawer>();
        //    var clientManagerMock = Substitute.For<IClientManager>();
        //    var stockManagerMock = Substitute.For<IStockManager>();
        //    var transactionManagerMock = Substitute.For<ITransactionManager>();
        //    var blockOfSharesManagerMock = Substitute.For<IBlockOfSharesManager>();
        //    var dbInitializerMock = Substitute.For<IDataBaseInitializer>();
        //    // Arrange
        //    var sut = new TradingEngine(
        //        outpuDeviceMock,
        //        inputDeviceMock,
        //        tableDrawerMock,
        //        clientManagerMock,
        //        stockManagerMock,
        //        transactionManagerMock,
        //        blockOfSharesManagerMock,
        //        dbInitializerMock);
        //    // Act
        //    sut.Run();
        //
        //    // Asserts
        //
        //    outpuDeviceMock.Received(1).WriteLine(Arg.Is<string>(w => w == "MenuShowed"));
        //    inputDeviceMock.Received(2).ReadKey();
        }
    }
}
