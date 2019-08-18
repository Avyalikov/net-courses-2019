using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using trading_software;
using NSubstitute;
using System.Linq;

namespace trading_software.Tests
{
    [TestClass]
    public class CommandParserTests
    {
        [TestMethod]
        public void ManualAddClientTest()
        {
            // Arrange
            var clientManagerMock = Substitute.For<IClientManager>();
            var stockManagerMock = Substitute.For<IStockManager>();
            var transactionManagerMock = Substitute.For<ITransactionManager>();
            var blockOfSharesManagerMock = Substitute.For<IBlockOfSharesManager>();
            var dbInitializerMock = Substitute.For<IDataBaseInitializer>();
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            var timeManagerMock = Substitute.For<ITimeManager>();

            var sut = new CommandParser(
                clientManagerMock,
                stockManagerMock,
                transactionManagerMock,
                blockOfSharesManagerMock,
                dbInitializerMock,
                outpuDeviceMock,
                timeManagerMock);
            
            // Act
            sut.Parse("ManualAddClient");

            // Asserts
            clientManagerMock.Received(1).ManualAddClient();
            dbInitializerMock.DidNotReceive();
            transactionManagerMock.DidNotReceive();
            blockOfSharesManagerMock.DidNotReceive();
            stockManagerMock.DidNotReceive();
            dbInitializerMock.DidNotReceive();
            timeManagerMock.DidNotReceive();
        }


        [TestMethod]
        public void ManualAddStockTest()
        {
            // Arrange
            var clientManagerMock = Substitute.For<IClientManager>();
            var stockManagerMock = Substitute.For<IStockManager>();
            var transactionManagerMock = Substitute.For<ITransactionManager>();
            var blockOfSharesManagerMock = Substitute.For<IBlockOfSharesManager>();
            var dbInitializerMock = Substitute.For<IDataBaseInitializer>();
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            var timeManagerMock = Substitute.For<ITimeManager>();

            var sut = new CommandParser(
                clientManagerMock,
                stockManagerMock,
                transactionManagerMock,
                blockOfSharesManagerMock,
                dbInitializerMock,
                outpuDeviceMock,
                timeManagerMock);

            // Act
            sut.Parse("ManualAddStock");

            // Asserts
            clientManagerMock.DidNotReceive();
            stockManagerMock.Received(1).ManualAddStock();
            dbInitializerMock.DidNotReceive();
            transactionManagerMock.DidNotReceive();
            blockOfSharesManagerMock.DidNotReceive();
            dbInitializerMock.DidNotReceive();
            timeManagerMock.DidNotReceive();
        }


        [TestMethod]
        public void ManualAddTransactionTest()
        {
            // Arrange
            var clientManagerMock = Substitute.For<IClientManager>();
            var stockManagerMock = Substitute.For<IStockManager>();
            var transactionManagerMock = Substitute.For<ITransactionManager>();
            var blockOfSharesManagerMock = Substitute.For<IBlockOfSharesManager>();
            var dbInitializerMock = Substitute.For<IDataBaseInitializer>();
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            var timeManagerMock = Substitute.For<ITimeManager>();

            var sut = new CommandParser(
                clientManagerMock,
                stockManagerMock,
                transactionManagerMock,
                blockOfSharesManagerMock,
                dbInitializerMock,
                outpuDeviceMock,
                timeManagerMock);

            // Act
            sut.Parse("ManualAddTransaction");

            // Asserts
            clientManagerMock.DidNotReceive();
            stockManagerMock.DidNotReceive();
            dbInitializerMock.DidNotReceive();
            transactionManagerMock.Received(1).ManualAddTransaction();
            blockOfSharesManagerMock.DidNotReceive();
            dbInitializerMock.DidNotReceive();
            timeManagerMock.DidNotReceive();
        }


        [TestMethod]
        public void ManualAddSharesTest()
        {
            // Arrange
            var clientManagerMock = Substitute.For<IClientManager>();
            var stockManagerMock = Substitute.For<IStockManager>();
            var transactionManagerMock = Substitute.For<ITransactionManager>();
            var blockOfSharesManagerMock = Substitute.For<IBlockOfSharesManager>();
            var dbInitializerMock = Substitute.For<IDataBaseInitializer>();
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            var timeManagerMock = Substitute.For<ITimeManager>();

            var sut = new CommandParser(
                clientManagerMock,
                stockManagerMock,
                transactionManagerMock,
                blockOfSharesManagerMock,
                dbInitializerMock,
                outpuDeviceMock,
                timeManagerMock);

            // Act
            sut.Parse("ManualAddShares");

            // Asserts
            clientManagerMock.DidNotReceive();
            stockManagerMock.DidNotReceive();
            dbInitializerMock.DidNotReceive();
            transactionManagerMock.DidNotReceive();
            blockOfSharesManagerMock.Received(1).ManualAddNewShare();
            dbInitializerMock.DidNotReceive();
            timeManagerMock.DidNotReceive();
        }

        [TestMethod]
        public void ReadAllClientsTest()
        {
            // Arrange
            var clientManagerMock = Substitute.For<IClientManager>();
            var stockManagerMock = Substitute.For<IStockManager>();
            var transactionManagerMock = Substitute.For<ITransactionManager>();
            var blockOfSharesManagerMock = Substitute.For<IBlockOfSharesManager>();
            var dbInitializerMock = Substitute.For<IDataBaseInitializer>();
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            var timeManagerMock = Substitute.For<ITimeManager>();

            var sut = new CommandParser(
                clientManagerMock,
                stockManagerMock,
                transactionManagerMock,
                blockOfSharesManagerMock,
                dbInitializerMock,
                outpuDeviceMock,
                timeManagerMock);

            // Act
            sut.Parse("ReadAllClients");

            // Asserts
            clientManagerMock.Received(1).ReadAllClients();
            stockManagerMock.DidNotReceive();
            dbInitializerMock.DidNotReceive();
            transactionManagerMock.DidNotReceive();
            blockOfSharesManagerMock.DidNotReceive();
            dbInitializerMock.DidNotReceive();
            timeManagerMock.DidNotReceive();
        }

        [TestMethod]
        public void ReadAllStocksTest()
        {
            // Arrange
            var clientManagerMock = Substitute.For<IClientManager>();
            var stockManagerMock = Substitute.For<IStockManager>();
            var transactionManagerMock = Substitute.For<ITransactionManager>();
            var blockOfSharesManagerMock = Substitute.For<IBlockOfSharesManager>();
            var dbInitializerMock = Substitute.For<IDataBaseInitializer>();
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            var timeManagerMock = Substitute.For<ITimeManager>();

            var sut = new CommandParser(
                clientManagerMock,
                stockManagerMock,
                transactionManagerMock,
                blockOfSharesManagerMock,
                dbInitializerMock,
                outpuDeviceMock,
                timeManagerMock);

            // Act
            sut.Parse("ReadAllStocks");

            // Asserts
            clientManagerMock.DidNotReceive();
            stockManagerMock.Received(1).ReadAllStocks();
            dbInitializerMock.DidNotReceive();
            transactionManagerMock.DidNotReceive();
            blockOfSharesManagerMock.DidNotReceive();
            dbInitializerMock.DidNotReceive();
            timeManagerMock.DidNotReceive();
        }

        [TestMethod]
        public void ReadAllTransactionsTest()
        {
            // Arrange
            var clientManagerMock = Substitute.For<IClientManager>();
            var stockManagerMock = Substitute.For<IStockManager>();
            var transactionManagerMock = Substitute.For<ITransactionManager>();
            var blockOfSharesManagerMock = Substitute.For<IBlockOfSharesManager>();
            var dbInitializerMock = Substitute.For<IDataBaseInitializer>();
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            var timeManagerMock = Substitute.For<ITimeManager>();

            var sut = new CommandParser(
                clientManagerMock,
                stockManagerMock,
                transactionManagerMock,
                blockOfSharesManagerMock,
                dbInitializerMock,
                outpuDeviceMock,
                timeManagerMock);

            // Act
            sut.Parse("ReadAllTransactions");

            // Asserts
            clientManagerMock.DidNotReceive();
            stockManagerMock.DidNotReceive();
            dbInitializerMock.DidNotReceive();
            transactionManagerMock.Received(1).ReadAllTransactions();
            blockOfSharesManagerMock.DidNotReceive();
            dbInitializerMock.DidNotReceive();
            timeManagerMock.DidNotReceive();
        }

        [TestMethod]
        public void ReadAllSharesTest()
        {
            // Arrange
            var clientManagerMock = Substitute.For<IClientManager>();
            var stockManagerMock = Substitute.For<IStockManager>();
            var transactionManagerMock = Substitute.For<ITransactionManager>();
            var blockOfSharesManagerMock = Substitute.For<IBlockOfSharesManager>();
            var dbInitializerMock = Substitute.For<IDataBaseInitializer>();
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            var timeManagerMock = Substitute.For<ITimeManager>();

            var sut = new CommandParser(
                clientManagerMock,
                stockManagerMock,
                transactionManagerMock,
                blockOfSharesManagerMock,
                dbInitializerMock,
                outpuDeviceMock,
                timeManagerMock);

            // Act
            sut.Parse("ReadAllShares");

            // Asserts
            clientManagerMock.DidNotReceive();
            stockManagerMock.DidNotReceive();
            dbInitializerMock.DidNotReceive();
            transactionManagerMock.DidNotReceive();
            blockOfSharesManagerMock.Received(1).ShowAllShares();
            dbInitializerMock.DidNotReceive();
            timeManagerMock.DidNotReceive();
        }

        [TestMethod]
        public void MakeRandomTransactionTest()
        {
            // Arrange
            var clientManagerMock = Substitute.For<IClientManager>();
            var stockManagerMock = Substitute.For<IStockManager>();
            var transactionManagerMock = Substitute.For<ITransactionManager>();
            var blockOfSharesManagerMock = Substitute.For<IBlockOfSharesManager>();
            var dbInitializerMock = Substitute.For<IDataBaseInitializer>();
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            var timeManagerMock = Substitute.For<ITimeManager>();

            var sut = new CommandParser(
                clientManagerMock,
                stockManagerMock,
                transactionManagerMock,
                blockOfSharesManagerMock,
                dbInitializerMock,
                outpuDeviceMock,
                timeManagerMock);

            // Act
            sut.Parse("MakeRandomTransaction");

            // Asserts
            clientManagerMock.DidNotReceive();
            stockManagerMock.DidNotReceive();
            dbInitializerMock.DidNotReceive();
            transactionManagerMock.Received(1).MakeRandomTransaction();
            blockOfSharesManagerMock.DidNotReceive();
            dbInitializerMock.DidNotReceive();
            timeManagerMock.DidNotReceive();
        }

        [TestMethod]
        public void InitiateDBTest()
        {
            // Arrange
            var clientManagerMock = Substitute.For<IClientManager>();
            var stockManagerMock = Substitute.For<IStockManager>();
            var transactionManagerMock = Substitute.For<ITransactionManager>();
            var blockOfSharesManagerMock = Substitute.For<IBlockOfSharesManager>();
            var dbInitializerMock = Substitute.For<IDataBaseInitializer>();
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            var timeManagerMock = Substitute.For<ITimeManager>();

            var sut = new CommandParser(
                clientManagerMock,
                stockManagerMock,
                transactionManagerMock,
                blockOfSharesManagerMock,
                dbInitializerMock,
                outpuDeviceMock,
                timeManagerMock);

            // Act
            sut.Parse("InitiateDB");

            // Asserts
            clientManagerMock.DidNotReceive();
            stockManagerMock.DidNotReceive();
            dbInitializerMock.DidNotReceive();
            transactionManagerMock.DidNotReceive(); ;
            blockOfSharesManagerMock.DidNotReceive();
            dbInitializerMock.Received(1).Initiate();
            timeManagerMock.DidNotReceive();
        }

        [TestMethod]
        public void BankruptRandomClientTest()
        {
            // Arrange
            var clientManagerMock = Substitute.For<IClientManager>();
            var stockManagerMock = Substitute.For<IStockManager>();
            var transactionManagerMock = Substitute.For<ITransactionManager>();
            var blockOfSharesManagerMock = Substitute.For<IBlockOfSharesManager>();
            var dbInitializerMock = Substitute.For<IDataBaseInitializer>();
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            var timeManagerMock = Substitute.For<ITimeManager>();

            var sut = new CommandParser(
                clientManagerMock,
                stockManagerMock,
                transactionManagerMock,
                blockOfSharesManagerMock,
                dbInitializerMock,
                outpuDeviceMock,
                timeManagerMock);

            // Act
            sut.Parse("BankruptRandomClient");

            // Asserts
            clientManagerMock.Received(1).BankruptRandomClient();
            stockManagerMock.DidNotReceive();
            dbInitializerMock.DidNotReceive();
            transactionManagerMock.DidNotReceive(); ;
            blockOfSharesManagerMock.DidNotReceive();
            dbInitializerMock.DidNotReceive();
            timeManagerMock.DidNotReceive();
        }

        [TestMethod]
        public void ShowOrangeClientsTest()
        {
            // Arrange
            var clientManagerMock = Substitute.For<IClientManager>();
            var stockManagerMock = Substitute.For<IStockManager>();
            var transactionManagerMock = Substitute.For<ITransactionManager>();
            var blockOfSharesManagerMock = Substitute.For<IBlockOfSharesManager>();
            var dbInitializerMock = Substitute.For<IDataBaseInitializer>();
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            var timeManagerMock = Substitute.For<ITimeManager>();

            var sut = new CommandParser(
                clientManagerMock,
                stockManagerMock,
                transactionManagerMock,
                blockOfSharesManagerMock,
                dbInitializerMock,
                outpuDeviceMock,
                timeManagerMock);

            // Act
            sut.Parse("ShowOrangeClients");

            // Asserts
            clientManagerMock.Received(1).ShowOrangeZone();
            stockManagerMock.DidNotReceive();
            dbInitializerMock.DidNotReceive();
            transactionManagerMock.DidNotReceive(); ;
            blockOfSharesManagerMock.DidNotReceive();
            dbInitializerMock.DidNotReceive();
            timeManagerMock.DidNotReceive();
        }


        [TestMethod]
        public void ShowBlackClientsTest()
        {
            // Arrange
            var clientManagerMock = Substitute.For<IClientManager>();
            var stockManagerMock = Substitute.For<IStockManager>();
            var transactionManagerMock = Substitute.For<ITransactionManager>();
            var blockOfSharesManagerMock = Substitute.For<IBlockOfSharesManager>();
            var dbInitializerMock = Substitute.For<IDataBaseInitializer>();
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            var timeManagerMock = Substitute.For<ITimeManager>();

            var sut = new CommandParser(
                clientManagerMock,
                stockManagerMock,
                transactionManagerMock,
                blockOfSharesManagerMock,
                dbInitializerMock,
                outpuDeviceMock,
                timeManagerMock);

            // Act
            sut.Parse("ShowBlackClients");

            // Asserts
            clientManagerMock.Received(1).ShowBlackClients();
            stockManagerMock.DidNotReceive();
            dbInitializerMock.DidNotReceive();
            transactionManagerMock.DidNotReceive(); ;
            blockOfSharesManagerMock.DidNotReceive();
            dbInitializerMock.DidNotReceive();
            timeManagerMock.DidNotReceive();
        }

        [TestMethod]
        public void ReduceAssetsRandomClientTest()
        {
            // Arrange
            var clientManagerMock = Substitute.For<IClientManager>();
            var stockManagerMock = Substitute.For<IStockManager>();
            var transactionManagerMock = Substitute.For<ITransactionManager>();
            var blockOfSharesManagerMock = Substitute.For<IBlockOfSharesManager>();
            var dbInitializerMock = Substitute.For<IDataBaseInitializer>();
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            var timeManagerMock = Substitute.For<ITimeManager>();

            var sut = new CommandParser(
                clientManagerMock,
                stockManagerMock,
                transactionManagerMock,
                blockOfSharesManagerMock,
                dbInitializerMock,
                outpuDeviceMock,
                timeManagerMock);

            // Act
            sut.Parse("ReduceAssetsRandomClient");

            // Asserts
            clientManagerMock.Received(1).ReduceAssetsRandomClient();
            stockManagerMock.DidNotReceive();
            dbInitializerMock.DidNotReceive();
            transactionManagerMock.DidNotReceive(); ;
            blockOfSharesManagerMock.DidNotReceive();
            dbInitializerMock.DidNotReceive();
            timeManagerMock.DidNotReceive();
        }

        [TestMethod]
        public void StartRandomTransactionThreadTest()
        {
            // Arrange
            var clientManagerMock = Substitute.For<IClientManager>();
            var stockManagerMock = Substitute.For<IStockManager>();
            var transactionManagerMock = Substitute.For<ITransactionManager>();
            var blockOfSharesManagerMock = Substitute.For<IBlockOfSharesManager>();
            var dbInitializerMock = Substitute.For<IDataBaseInitializer>();
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            var timeManagerMock = Substitute.For<ITimeManager>();

            var sut = new CommandParser(
                clientManagerMock,
                stockManagerMock,
                transactionManagerMock,
                blockOfSharesManagerMock,
                dbInitializerMock,
                outpuDeviceMock,
                timeManagerMock);

            // Act
            sut.Parse("StartSimulationWithRandomTransactions");

            // Asserts
            clientManagerMock.DidNotReceive();
            stockManagerMock.DidNotReceive();
            dbInitializerMock.DidNotReceive();
            transactionManagerMock.DidNotReceive(); ;
            blockOfSharesManagerMock.DidNotReceive();
            dbInitializerMock.DidNotReceive();
            timeManagerMock.Received(1).StartRandomTransactionThread();
        }

        [TestMethod]
        public void StopRandomTransactionThreadTest()
        {
            // Arrange
            var clientManagerMock = Substitute.For<IClientManager>();
            var stockManagerMock = Substitute.For<IStockManager>();
            var transactionManagerMock = Substitute.For<ITransactionManager>();
            var blockOfSharesManagerMock = Substitute.For<IBlockOfSharesManager>();
            var dbInitializerMock = Substitute.For<IDataBaseInitializer>();
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            var timeManagerMock = Substitute.For<ITimeManager>();

            var sut = new CommandParser(
                clientManagerMock,
                stockManagerMock,
                transactionManagerMock,
                blockOfSharesManagerMock,
                dbInitializerMock,
                outpuDeviceMock,
                timeManagerMock);

            // Act
            sut.Parse("StopSimulationWithRandomTransactions");

            // Asserts
            clientManagerMock.DidNotReceive();
            stockManagerMock.DidNotReceive();
            dbInitializerMock.DidNotReceive();
            transactionManagerMock.DidNotReceive(); ;
            blockOfSharesManagerMock.DidNotReceive();
            dbInitializerMock.DidNotReceive();
            timeManagerMock.Received(1).StopRandomTransactionThread();
        }
    }
}
