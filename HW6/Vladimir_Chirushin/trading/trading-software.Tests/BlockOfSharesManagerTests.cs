using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using trading_software;
using NSubstitute;
using System.Collections.Generic;

namespace trading_software.Tests
{
    [TestClass]
    public class BlockOfSharesManagerTests
    {
        [TestMethod]
        public void AddShareBlockWithParametersTest()
        {
            // Arrange
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            var inputDeviceMock = Substitute.For<IInputDevice>();
            var tableDrawerMock = Substitute.For<ITableDrawer>();
            var dataBaseDevice = Substitute.For<IDataBaseDevice>();

            var sut = new BlockOfSharesManager(
                inputDeviceMock,
                outpuDeviceMock,
                tableDrawerMock,
                dataBaseDevice);

            BlockOfShares block = new BlockOfShares
            {
                ClientID = 1,
                StockID = 2,
                Amount = 3
            };
            int clientID = 1;
            int stockID = 2;
            int amount = 3;

            // Act
            sut.AddShare(clientID, stockID, amount);

            // Asserts
            //dataBaseDevice.Received(1).Add(Arg.Is<BlockOfShares>(b => b == block));
        }

        [TestMethod]
        public void AddShareBlockTest()
        {
            // Arrange
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            var inputDeviceMock = Substitute.For<IInputDevice>();
            var tableDrawerMock = Substitute.For<ITableDrawer>();
            var dataBaseDevice = Substitute.For<IDataBaseDevice>();

            var sut = new BlockOfSharesManager(
                inputDeviceMock,
                outpuDeviceMock,
                tableDrawerMock,
                dataBaseDevice);

            BlockOfShares block = new BlockOfShares
            {
                ClientID = 1,
                StockID = 2,
                Amount = 3
            };

            // Act
            sut.AddShare(block);

            // Asserts
            dataBaseDevice.Received(1).Add(Arg.Is<BlockOfShares>(b => b == block));
        }
        /*
        [TestMethod]
        public void ManualAddNewShareTest()
        {

            // Arrange
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            var inputDeviceMock = Substitute.For<IInputDevice>();
            var tableDrawerMock = Substitute.For<ITableDrawer>();
            var dataBaseDevice = Substitute.For<IDataBaseDevice>();

            var sut = new BlockOfSharesManager(
                inputDeviceMock,
                outpuDeviceMock,
                tableDrawerMock,
                dataBaseDevice);

            BlockOfShares block = new BlockOfShares
            {
                ClientID = 1,
                StockID = 2,
                Amount = 3
            };

            Dictionary<string, string> answers = new Dictionary<string, string>
            {
                { "StockType", "Umbrella" },
                { "ClientName", "Martin Eden" },
                { "Amount", "31415" }
            };

            // Act
            sut.ManualAddNewShare();

            // Asserts
            outpuDeviceMock.Received(1).WriteLine(Arg.Is<string>(w => w == "Write Stock Type:"));
            inputDeviceMock.ReadLine().Returns((info) =>
            {
                return answers["StockType"];
            });
            dataBaseDevice
                .GetStockID(Arg.Is<string>(w => w == "Umbrella"))
                .Returns((info) => (int) 35);

            outpuDeviceMock.Received(1).WriteLine(Arg.Is<string>(w => w == "Write client name:"));
            inputDeviceMock.ReadLine().Returns((info) =>
            {
                return answers["phone"];
            });
            dataBaseDevice.Received(1)
                .GetClientID(Arg.Is<string>(w => w == "Martin Eden"))
                .Returns((info) =>
                {
                    return 1;
                });

            outpuDeviceMock.Received(1).WriteLine(Arg.Is<string>(w => w == "Write stocks amount:"));
            inputDeviceMock.ReadLine().Returns((info) =>
            {
                return answers["phone"];
            });
        }
        */

        [TestMethod]
        public void CreateRandomShare()
        {
            // Arrange
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            var inputDeviceMock = Substitute.For<IInputDevice>();
            var tableDrawerMock = Substitute.For<ITableDrawer>();
            var dataBaseDeviceMock = Substitute.For<IDataBaseDevice>();

            var sut = new BlockOfSharesManager(
                inputDeviceMock,
                outpuDeviceMock,
                tableDrawerMock,
                dataBaseDeviceMock);


            const int maxNumberOfClients = 7;
            const int maxNumberOfStocks = 12;
            const int maxAmountOfShares = 16;

            // Act
            sut.CreateRandomShare();


            // Asserts
            dataBaseDeviceMock
                .Received(1)
                .GetNumberOfClients()
                .Returns((info) => maxNumberOfClients);
            dataBaseDeviceMock
                .Received(1)
                .GetNumberOfStocks()
                .Returns((info) => maxNumberOfStocks);
            sut
                .Received(1)
                .AddShare(
                    Arg.Is<int>(c => c < maxNumberOfClients), 
                    Arg.Is<int>(s => s < maxNumberOfStocks), 
                    Arg.Is<int>(a => a < maxAmountOfShares)
                    );
        }
    }
    
}