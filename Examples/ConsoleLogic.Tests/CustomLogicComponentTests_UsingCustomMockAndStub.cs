using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConsoleLogic.Tests
{
    [TestClass]
    public class CustomLogicComponentTests_CustomStubsMocks
    {
        [TestMethod]
        public void ShouldReadFirstArgument()
        {
            // Arrange
            IOutputWriter writerMock = new TestOutputWriter();
            IInputReader readerMock = new TestInputReader();
            var sut = new CustomLogicComponent(readerMock, writerMock);

            // Act
            sut.DivideExample();

            // Assert
            Assert.IsTrue(((TestOutputWriter)writerMock)
                .DoesItReceive("Enter a number to be divided: "), "First phrase is incorrrect");
            Assert.AreEqual(2,
                ((TestInputReader)readerMock).GetReadLineCallsCount());
        }

        [TestMethod]
        public void ShouldReadSecondArgument()
        {
            // Arrange
            IOutputWriter writerMock = new TestOutputWriter();
            IInputReader readerMock = new TestInputReader();
            var sut = new CustomLogicComponent(readerMock, writerMock);

            // Act
            sut.DivideExample();

            // Assert
            Assert.IsTrue(((TestOutputWriter)writerMock)
                .DoesItReceive("Enter another number to be divided"), "Second phrase is incorrrect");
            Assert.AreEqual(2,
                ((TestInputReader)readerMock).GetReadLineCallsCount());
        }

        [TestMethod]
        public void ShouldProvideCorrectCalculationResults()
        {
            // Arrange
            IOutputWriter writerMock = new TestOutputWriter();
            IInputReader readerMock = new TestInputReader();
            var cut = new CustomLogicComponent(readerMock, writerMock);

            // Act
            cut.DivideExample();

            // Assert
            Assert.IsTrue(((TestOutputWriter)writerMock)
                .DoesItReceive("The result is: 2"), "Calculation is incorrect." + ((TestOutputWriter)writerMock).ReceivedCalls());
        }

    }

    internal class TestInputReader : IInputReader
    {
        private int readLinesCallsAmount = 0;

        public string ReadLine()
        {
            readLinesCallsAmount++;

            if (readLinesCallsAmount == 1)
            {
                return "5";
            }

            if (readLinesCallsAmount == 2)
            {
                return "2";
            }
            
            throw new NotImplementedException();
        }

        public int GetReadLineCallsCount()
        {
            return this.readLinesCallsAmount;
        }
    }

    internal class TestOutputWriter : IOutputWriter
    {
        private List<string> receivedArguments = new List<string>();

        public void Write(string message)
        {
            receivedArguments.Add(message);
        }

        internal bool DoesItReceive(string phrase)
        {
            return receivedArguments.Contains(phrase);
        }

        internal string ReceivedCalls()
        {
           return string.Join(";", receivedArguments);
        }
    }
}
