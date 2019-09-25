using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace ConsoleLogic.Tests
{
    [TestClass]
    public partial class CustomLogicComponentTests_NSubstitute
    {
        [TestMethod]
        public void ShouldReadFirstArgument()
        {
            // Arrange
            IOutputWriter writerMock = Substitute.For<IOutputWriter>();
            IInputReader readerMock = Substitute.For<IInputReader>();
            readerMock.ReadLine().Returns("10");

            var sut = new CustomLogicComponent(readerMock, writerMock);

            // Act
            sut.DivideExample();

            // Assert
            writerMock.Received(1).Write(Arg.Is<string>(w => w == "Enter a number to be divided: "));
            readerMock.Received(2).ReadLine();
        }

        [TestMethod]
        public void ShouldReadSecondArgument()
        {
            // Arrange
            IOutputWriter writerMock = Substitute.For<IOutputWriter>();
            IInputReader readerMock = Substitute.For<IInputReader>();
            readerMock.ReadLine().Returns("10");

            var sut = new CustomLogicComponent(readerMock, writerMock);

            // Act
            sut.DivideExample();

            // Assert
            writerMock.Received(1).Write(Arg.Is<string>(w => w == "Enter another number to be divided"));
            readerMock.Received(2).ReadLine();
        }

        [TestMethod]
        public void ShouldProvideCorrectCalculationResults()
        {
            // Arrange
            IOutputWriter writerMock = Substitute.For<IOutputWriter>();
            IInputReader readerStub = Substitute.For<IInputReader>();

            Stack<int> readLineRetVals = new Stack<int>();
            readLineRetVals.Push(2);
            readLineRetVals.Push(10);

            readerStub.ReadLine().Returns((info)=> 
            {
                return readLineRetVals.Pop().ToString();
            });

            var cut = new CustomLogicComponent(readerStub, writerMock);

            // Act
            cut.DivideExample();

            // Assert
            writerMock.Received(1).Write(Arg.Is<string>(w => w == "The result is: 5"));
            
        }

    }
     
}
