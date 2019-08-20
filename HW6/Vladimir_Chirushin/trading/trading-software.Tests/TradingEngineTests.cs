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
            var inputDeviceMock = Substitute.For<IInputDevice>();
            var commandParserMock = Substitute.For<ICommandParser>();
            var outputDeviceMock = Substitute.For<IOutputDevice>();

            var sut = new TradingEngine(
                inputDeviceMock,
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

            inputDeviceMock
                .ReadLine()
                .Returns(quitCommand);

            // Act
            sut.Run();

            // Asserts
            outputDeviceMock
                .Received()
                .WriteLine(commands.Pop());

            commandParserMock
                .Received()
                .Parse(Arg.Is<string>(w => w == quitCommand));
        }
    }
}
