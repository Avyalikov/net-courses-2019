using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;

namespace trading_software.Tests
{
    [TestClass]
    public class ClientManagerTests
    {
        [TestMethod]
        public void IsExistTest()
        {
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            var inputDeviceMock = Substitute.For<IInputDevice>();
            var tableDrawerMock = Substitute.For<ITableDrawer>();
          
            var sut = new ClientManager(
                inputDeviceMock,
                outpuDeviceMock,
                tableDrawerMock);


            // Act
            //sut.Received();
        }
        [TestMethod]
        public void ManualAddClientTest()
        {
            // Arrange
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            var inputDeviceMock = Substitute.For<IInputDevice>();
            var tableDrawerMock = Substitute.For<ITableDrawer>();
            Dictionary<string, string> answers = new Dictionary<string, string>
            {
                { "name", "Martin Eden" },
                { "phone", "555-55-55" },
                { "balance", "6021023" }
            };

            var sut = new ClientManager(
                inputDeviceMock,
                outpuDeviceMock,
                tableDrawerMock);


            // Act
            sut.ManualAddClient();

            // Asserts
            outpuDeviceMock.Received(1).WriteLine(Arg.Is<string>(w => w == "Write name:"));
            inputDeviceMock.ReadLine().Returns((info) =>
            {
                return answers["name"];
            });
            outpuDeviceMock.Received(1).WriteLine(Arg.Is<string>(w => w == "Write PhoneNumber:"));
            inputDeviceMock.ReadLine().Returns((info) =>
            {
                return answers["phone"];
            });
            outpuDeviceMock.Received(1).WriteLine(Arg.Is<string>(w => w == "Write Balance:"));
            inputDeviceMock.ReadLine().Returns((info) =>
            {
                return answers["balance"];
            });
            sut.Received(1).AddClient(answers["name"], answers["phone"], (decimal)6021023);
        }
    }
}
