using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using trading_software;
using NSubstitute;
using System.Linq;
using trading_software.Services;

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
            var loggerServiceMock = Substitute.For<ILoggerService>();

            var sut = new CommandParser(
                clientManagerMock,
                stockManagerMock,
                transactionManagerMock,
                blockOfSharesManagerMock,
                dbInitializerMock,
                outpuDeviceMock,
                timeManagerMock,
                loggerServiceMock);
            
            // Act
            sut.Parse("ManualAddClient");

            // Asserts
            loggerServiceMock.RunWithExceptionLogging(
                Arg.Is<Action>(() => clientManagerMock.ManualAddClient())
                );
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
            var loggerServiceMock = Substitute.For<ILoggerService>();

            var sut = new CommandParser(
                clientManagerMock,
                stockManagerMock,
                transactionManagerMock,
                blockOfSharesManagerMock,
                dbInitializerMock,
                outpuDeviceMock,
                timeManagerMock,
                loggerServiceMock);

            // Act
            sut.Parse("ManualAddStock");

            // Asserts
            loggerServiceMock.RunWithExceptionLogging(
                Arg.Is<Action>(() => stockManagerMock.ManualAddStock())
                );
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
            var loggerServiceMock = Substitute.For<ILoggerService>();

            var sut = new CommandParser(
                clientManagerMock,
                stockManagerMock,
                transactionManagerMock,
                blockOfSharesManagerMock,
                dbInitializerMock,
                outpuDeviceMock,
                timeManagerMock,
                loggerServiceMock);

            // Act
            sut.Parse("ManualAddTransaction");

            // Asserts
            loggerServiceMock.RunWithExceptionLogging(
                Arg.Is<Action>(() => transactionManagerMock.ManualAddTransaction())
                ); 
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
            var loggerServiceMock = Substitute.For<ILoggerService>();

            var sut = new CommandParser(
                clientManagerMock,
                stockManagerMock,
                transactionManagerMock,
                blockOfSharesManagerMock,
                dbInitializerMock,
                outpuDeviceMock,
                timeManagerMock,
                loggerServiceMock);

            // Act
            sut.Parse("ManualAddShares");

            // Asserts
            loggerServiceMock.RunWithExceptionLogging(
                Arg.Is<Action>(() => blockOfSharesManagerMock.ManualAddNewShare())
                );
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
            var loggerServiceMock = Substitute.For<ILoggerService>();

            var sut = new CommandParser(
                clientManagerMock,
                stockManagerMock,
                transactionManagerMock,
                blockOfSharesManagerMock,
                dbInitializerMock,
                outpuDeviceMock,
                timeManagerMock,
                loggerServiceMock);

            // Act
            sut.Parse("ReadAllClients");

            // Asserts
            loggerServiceMock.RunWithExceptionLogging(
                Arg.Is<Action>(() => clientManagerMock.ReadAllClients())
                );
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
            var loggerServiceMock = Substitute.For<ILoggerService>();

            var sut = new CommandParser(
                clientManagerMock,
                stockManagerMock,
                transactionManagerMock,
                blockOfSharesManagerMock,
                dbInitializerMock,
                outpuDeviceMock,
                timeManagerMock,
                loggerServiceMock);

            // Act
            sut.Parse("ReadAllStocks");

            // Asserts
            loggerServiceMock.RunWithExceptionLogging(
                Arg.Is<Action>(() => stockManagerMock.ReadAllStocks())
                );
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
            var loggerServiceMock = Substitute.For<ILoggerService>();

            var sut = new CommandParser(
                clientManagerMock,
                stockManagerMock,
                transactionManagerMock,
                blockOfSharesManagerMock,
                dbInitializerMock,
                outpuDeviceMock,
                timeManagerMock,
                loggerServiceMock);

            // Act
            sut.Parse("ReadAllTransactions");

            // Asserts
            loggerServiceMock.RunWithExceptionLogging(
                Arg.Is<Action>(() => transactionManagerMock.ReadAllTransactions())
                );
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
            var loggerServiceMock = Substitute.For<ILoggerService>();

            var sut = new CommandParser(
                clientManagerMock,
                stockManagerMock,
                transactionManagerMock,
                blockOfSharesManagerMock,
                dbInitializerMock,
                outpuDeviceMock,
                timeManagerMock,
                loggerServiceMock);

            // Act
            sut.Parse("ReadAllShares");

            // Asserts
            loggerServiceMock.RunWithExceptionLogging(
                Arg.Is<Action>(() => blockOfSharesManagerMock.ShowAllShares())
                );
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
            var loggerServiceMock = Substitute.For<ILoggerService>();

            var sut = new CommandParser(
                clientManagerMock,
                stockManagerMock,
                transactionManagerMock,
                blockOfSharesManagerMock,
                dbInitializerMock,
                outpuDeviceMock,
                timeManagerMock,
                loggerServiceMock);

            // Act
            sut.Parse("MakeRandomTransaction");

            // Asserts
            loggerServiceMock.RunWithExceptionLogging(
                Arg.Is<Action>(() => transactionManagerMock.MakeRandomTransaction())
                );
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
            var loggerServiceMock = Substitute.For<ILoggerService>();

            var sut = new CommandParser(
                clientManagerMock,
                stockManagerMock,
                transactionManagerMock,
                blockOfSharesManagerMock,
                dbInitializerMock,
                outpuDeviceMock,
                timeManagerMock,
                loggerServiceMock);

            // Act
            sut.Parse("InitiateDB");

            // Asserts
            loggerServiceMock.RunWithExceptionLogging(
                Arg.Is<Action>(() => dbInitializerMock.Initiate())
                );
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
            var loggerServiceMock = Substitute.For<ILoggerService>();

            var sut = new CommandParser(
                clientManagerMock,
                stockManagerMock,
                transactionManagerMock,
                blockOfSharesManagerMock,
                dbInitializerMock,
                outpuDeviceMock,
                timeManagerMock,
                loggerServiceMock);

            // Act
            sut.Parse("BankruptRandomClient");

            // Asserts
            loggerServiceMock.RunWithExceptionLogging(
                Arg.Is<Action>(() => clientManagerMock.BankruptRandomClient())
                );
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
            var loggerServiceMock = Substitute.For<ILoggerService>();

            var sut = new CommandParser(
                clientManagerMock,
                stockManagerMock,
                transactionManagerMock,
                blockOfSharesManagerMock,
                dbInitializerMock,
                outpuDeviceMock,
                timeManagerMock,
                loggerServiceMock);

            // Act
            sut.Parse("ShowOrangeClients");

            // Asserts
            loggerServiceMock.RunWithExceptionLogging(
                Arg.Is<Action>(() => clientManagerMock.ShowOrangeZone())
                );
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
            var loggerServiceMock = Substitute.For<ILoggerService>();

            var sut = new CommandParser(
                clientManagerMock,
                stockManagerMock,
                transactionManagerMock,
                blockOfSharesManagerMock,
                dbInitializerMock,
                outpuDeviceMock,
                timeManagerMock,
                loggerServiceMock);

            // Act
            sut.Parse("ShowBlackClients");

            // Asserts
            loggerServiceMock.RunWithExceptionLogging(
                Arg.Is<Action>(() => clientManagerMock.ShowBlackClients())
                );
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
            var loggerServiceMock = Substitute.For<ILoggerService>();

            var sut = new CommandParser(
                clientManagerMock,
                stockManagerMock,
                transactionManagerMock,
                blockOfSharesManagerMock,
                dbInitializerMock,
                outpuDeviceMock,
                timeManagerMock,
                loggerServiceMock);

            // Act
            sut.Parse("ReduceAssetsRandomClient");

            // Asserts
            loggerServiceMock.RunWithExceptionLogging(
                Arg.Is<Action>(() => clientManagerMock.ReduceAssetsRandomClient())
                );
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
            var loggerServiceMock = Substitute.For<ILoggerService>();

            var sut = new CommandParser(
                clientManagerMock,
                stockManagerMock,
                transactionManagerMock,
                blockOfSharesManagerMock,
                dbInitializerMock,
                outpuDeviceMock,
                timeManagerMock,
                loggerServiceMock);

            // Act
            sut.Parse("StartSimulationWithRandomTransactions");

            // Asserts
            loggerServiceMock.RunWithExceptionLogging(
                Arg.Is<Action>(() => timeManagerMock.StartRandomTransactionThread())
                );
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
            var loggerServiceMock = Substitute.For<ILoggerService>();

            var sut = new CommandParser(
                clientManagerMock,
                stockManagerMock,
                transactionManagerMock,
                blockOfSharesManagerMock,
                dbInitializerMock,
                outpuDeviceMock,
                timeManagerMock,
                loggerServiceMock);

            // Act
            sut.Parse("StopSimulationWithRandomTransactions");

            // Asserts
            loggerServiceMock.RunWithExceptionLogging(
                Arg.Is<Action>(() => timeManagerMock.StopRandomTransactionThread())
                );
        }
    }
}
