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
            var dataBaseDevice = Substitute.For<IDataBaseDevice>();
            var sut = new ClientManager(
                inputDeviceMock,
                outpuDeviceMock,
                tableDrawerMock,
                dataBaseDevice);


            // Act
            sut.Received();
        }

        [TestMethod]
        public void ManualAddClientTest()
        {
            // Arrange
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            var inputDeviceMock = Substitute.For<IInputDevice>();
            var tableDrawerMock = Substitute.For<ITableDrawer>();
            var dataBaseDevice = Substitute.For<IDataBaseDevice>();

            Dictionary<string, string> answers = new Dictionary<string, string>
            {
                { "name", "Martin Eden" },
                { "phone", "555-55-55" },
                { "balance", "6021023" }
            };

            var sut = new ClientManager(
                inputDeviceMock,
                outpuDeviceMock,
                tableDrawerMock,
                dataBaseDevice);


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


        [TestMethod]
        public void ShowBlackClientsTest()
        {
            // Arrange
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            var inputDeviceMock = Substitute.For<IInputDevice>();
            var tableDrawerMock = Substitute.For<ITableDrawer>();
            var dataBaseDevice = Substitute.For<IDataBaseDevice>();

            Dictionary<string, string> answers = new Dictionary<string, string>
            {
                { "name", "Martin Eden" },
                { "phone", "555-55-55" },
                { "balance", "6021023" }
            };
            Client Client1 = new Client
            {
                ClientID = 1,
                Name = "Martin Eden",
                PhoneNumber = "555-55-55",
                Balance = (decimal)-5
            };
            Client Client2 = new Client
            {
                ClientID = 2,
                Name = "Ruth Morse",
                PhoneNumber = "444-44-44",
                Balance = (decimal)-13509
            };
            var sut = new ClientManager(
                inputDeviceMock,
                outpuDeviceMock,
                tableDrawerMock,
                dataBaseDevice);


            // Act
            sut.ShowBlackClients();

            outpuDeviceMock
                .Received(1)
                .WriteLine(Arg.Is<string>(w => w == "Clients in 'Black' zone:"));
            dataBaseDevice.
                GetOrangeClients()
                .Returns((info)=>(new[] { Client1, Client2 }.AsEnumerable<Client>()));
            tableDrawerMock
                .Received(1)
                .Show(Arg.Is<IEnumerable<Client>>(enc => enc ==
                    new[] { Client1, Client2 }.AsEnumerable<Client>()
                    ));

        }


        [TestMethod]
        public void ShowOrangeClientsTest()
        {
            // Arrange
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            var inputDeviceMock = Substitute.For<IInputDevice>();
            var tableDrawerMock = Substitute.For<ITableDrawer>();
            var dataBaseDevice = Substitute.For<IDataBaseDevice>();

            Dictionary<string, string> answers = new Dictionary<string, string>
            {
                { "name", "Martin Eden" },
                { "phone", "555-55-55" },
                { "balance", "6021023" }
            };
            Client Client1 = new Client
            {
                ClientID = 1,
                Name = "Martin Eden",
                PhoneNumber = "555-55-55",
                Balance = (decimal)0
            };
            Client Client2 = new Client
            {
                ClientID = 2,
                Name = "Ruth Morse",
                PhoneNumber = "444-44-44",
                Balance = (decimal)0
            };
            var sut = new ClientManager(
                inputDeviceMock,
                outpuDeviceMock,
                tableDrawerMock,
                dataBaseDevice);
            IEnumerable<Client> query = new[] { Client1, Client2 }.AsEnumerable<Client>();

            // Act
            sut.ShowOrangeZone();

            outpuDeviceMock
                .Received(1)
                .WriteLine(Arg.Is<string>(w => w == "Clients in 'Orange' zone:"));
            dataBaseDevice.
                GetOrangeClients()
                .Returns((info) => (query));
            tableDrawerMock
                .Received(1)
                .Show(Arg.Is<Client[]>(clients => query
                        .SequenceEqual(clients)
                    ));
        }
    }
}
