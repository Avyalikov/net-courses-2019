using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using trading_software;
using NSubstitute;
using System.Collections.Generic;

namespace trading_software.Tests
{
    [TestClass]
    public class TradingEngineTests_NSubstitute
    {
        [TestMethod]
        public void TradingEngineShowMenu()
        {
            // Arrange
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            var inputDeviceMock = Substitute.For<IInputDevice>();
            var tableDrawerMock = Substitute.For<ITableDrawer>();
            var clientManagerMock = Substitute.For<IClientManager>();
            var stockManagerMock = Substitute.For<IStockManager>();
            var transactionManagerMock = Substitute.For<ITransactionManager>();
            var blockOfSharesManagerMock = Substitute.For<IBlockOfSharesManager>();
            var dbInitializerMock = Substitute.For<IDataBaseInitializer>();
            var commandParserMock = Substitute.For<ICommandParser>();
            
            var sut = new TradingEngine(
                outpuDeviceMock,
                inputDeviceMock,
                tableDrawerMock,
                clientManagerMock,
                stockManagerMock,
                transactionManagerMock,
                blockOfSharesManagerMock,
                dbInitializerMock,
                commandParserMock);

            Stack<string> temp = new Stack<string>();
            Stack<string> commands = new Stack<string>();

            foreach (Command command in Enum.GetValues(typeof(Command)))
            {
                temp.Push(command.ToString());
            }

            for(int i = temp.Count-1; i>=0; i--)
            {
                commands.Push(temp.Pop());
            }
            string quitCommand = "quit";

            // Act
            sut.Run();

            // Asserts
            outpuDeviceMock
                .Received()
                .WriteLine(commands.Pop());

            inputDeviceMock
                .Received()
                .ReadLine()
                .Returns(quitCommand);
            commandParserMock
                .Received()
                .Parse(Arg.Is<string>(w => w == quitCommand));
        }
    }
}
