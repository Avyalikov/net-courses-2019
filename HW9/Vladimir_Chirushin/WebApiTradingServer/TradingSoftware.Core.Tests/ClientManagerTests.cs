namespace TradingSoftware.Core.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using TradingSoftware.Core.Models;
    using TradingSoftware.Core.Repositories;
    using TradingSoftware.Core.Services;

    [TestClass]
    public class ClientManagerTests
    {
        [TestMethod]
        public void ShouldAddClient()
        {
            // Arrange
            var clientRepositoryMock = Substitute.For<IClientRepository>();

            var sut = new ClientManager(clientRepositoryMock);

            Client client = new Client
            {
                Name = "Martin Eden",
                PhoneNumber = "555-55-55",
                Balance = (decimal)6021023
            };

            // Act
            sut.AddClient(client);

            // Asserts
            clientRepositoryMock.Received(1).Insert(Arg.Is<Client>(client));
        }

        [TestMethod]
        public void ShouldAddClientWithParametrs()
        {
            // Arrange
            var clientRepositoryMock = Substitute.For<IClientRepository>();

            var sut = new ClientManager(clientRepositoryMock);

            Client client = new Client
            {
                Name = "Martin Eden",
                PhoneNumber = "555-55-55",
                Balance = (decimal)6021023
            };
            string clientName = "Martin Eden";
            string clientPhoneNumber = "555-55-55";
            decimal clientBalance = (decimal)6021023;

            // Act
            sut.AddClient(clientName, clientPhoneNumber, clientBalance);

            // Asserts
            clientRepositoryMock.Received(1).Insert(Arg.Is<Client>(c => c.Name == clientName &&
                                                                      c.PhoneNumber == clientPhoneNumber &&
                                                                      c.Balance == clientBalance));
        }

        [TestMethod]
        public void ShouldGetAllClients()
        {  
            // Arrange
            var clientRepositoryMock = Substitute.For<IClientRepository>();

            var sut = new ClientManager(clientRepositoryMock);

            // Act
            sut.GetAllClients();

            // Asserts
            clientRepositoryMock.Received(1).GetAllClients();
        }

        [TestMethod]
        public void ShouldGetClientName()
        {
            // Arrange
            var clientRepositoryMock = Substitute.For<IClientRepository>();

            var sut = new ClientManager(clientRepositoryMock);

            int clientID = 12;
            clientRepositoryMock
                .IsClientExist(Arg.Is<int>(clientID))
                .Returns(true);

            // Act
            sut.GetClientName(clientID);

            // Asserts
            clientRepositoryMock.Received(1).GetClientName(Arg.Is<int>(clientID));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),
           "There is no clients with id 12")]
        public void ShouldThrowExceptionWhileGetingClientNameThatDidntExist()
        {
            // Arrange
            var clientRepositoryMock = Substitute.For<IClientRepository>();

            var sut = new ClientManager(clientRepositoryMock);

            int clientID = 12;
            clientRepositoryMock
                .IsClientExist(Arg.Is<int>(clientID))
                .Returns(false);

            // Act
            sut.GetClientName(clientID);
           
            // Asserts
            clientRepositoryMock.DidNotReceive().GetClientName(clientID);
        }

        [TestMethod]
        public void ShouldGetClientID()
        {
            // Arrange
            var clientRepositoryMock = Substitute.For<IClientRepository>();

            var sut = new ClientManager(clientRepositoryMock);

            string clientName = "Ruth Morse";
            clientRepositoryMock
                .IsClientExist(Arg.Is<string>(clientName))
                .Returns(true);

            // Act
            sut.GetClientID(clientName);

            // Asserts
            clientRepositoryMock.Received(1).GetClientID(Arg.Is<string>(clientName));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),
           "There is no clients with name Ruth Morse")]
        public void ShouldThrowExceptionWhileGetingClientIDDidntExistTest()
        {
            // Arrange
            var clientRepositoryMock = Substitute.For<IClientRepository>();

            var sut = new ClientManager(clientRepositoryMock);

            string clientName = "Ruth Morse";
            clientRepositoryMock
                .IsClientExist(Arg.Is<string>(clientName))
                .Returns(false);

            // Act
            sut.GetClientID(clientName);
            
            // Asserts
            clientRepositoryMock.DidNotReceive().GetClientID(Arg.Is<string>(clientName));
        }

        [TestMethod]
        public void ShouldCheckIsClientIDExist()
        {
            // Arrange
            var clientRepositoryMock = Substitute.For<IClientRepository>();

            var sut = new ClientManager(clientRepositoryMock);

            int clientID = 12;

            // Act
            sut.IsClientExist(clientID);

            // Asserts
            clientRepositoryMock.Received(1).IsClientExist(Arg.Is<int>(clientID));
        }

        [TestMethod]
        public void ShouldCheckIsClientNameExist()
        {
            // Arrange
            var clientRepositoryMock = Substitute.For<IClientRepository>();

            var sut = new ClientManager(clientRepositoryMock);

            string clientName = "Ruth Morse";

            // Act
            sut.IsClientExist(clientName);

            // Asserts
            clientRepositoryMock.Received(1).IsClientExist(Arg.Is<string>(clientName));
        }

        [TestMethod]
        public void ShouldGetNumberOfClients()
        {
            // Arrange
            var clientRepositoryMock = Substitute.For<IClientRepository>();

            var sut = new ClientManager(clientRepositoryMock);

            // Act
            sut.GetNumberOfClients();

            // Asserts
            clientRepositoryMock.Received(1).GetNumberOfClients();
        }

        [TestMethod]
        public void ShouldGetClientBalance()
        {
            // Arrange
            var clientRepositoryMock = Substitute.For<IClientRepository>();
            var sut = new ClientManager(clientRepositoryMock);
            int clientID = 12;
            clientRepositoryMock
                .IsClientExist(Arg.Is<int>(clientID))
                .Returns(true);

            // Act
            sut.GetClientBalance(clientID);

            // Asserts
            clientRepositoryMock.Received(1).GetClientBalance(clientID);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),
           "There is no clients with id 12")]
        public void ShouldThrowExceptionWhileGettingClientBalanceDidntExist()
        {
            // Arrange
            var clientRepositoryMock = Substitute.For<IClientRepository>();
            var sut = new ClientManager(clientRepositoryMock);
            int clientID = 12;
            clientRepositoryMock
                .IsClientExist(Arg.Is<int>(clientID))
                .Returns(false);

            // Act
            sut.GetClientBalance(clientID);
           
            // Asserts
            clientRepositoryMock.DidNotReceive().GetClientBalance(clientID);
        }

        [TestMethod]
        public void ShouldChangeBalance()
        {
            // Arrange
            var clientRepositoryMock = Substitute.For<IClientRepository>();
            var sut = new ClientManager(clientRepositoryMock);
            int clientID = 12;
            decimal amount = 300;

            clientRepositoryMock
                .IsClientExist(Arg.Is<int>(clientID))
                .Returns(true);

            // Act
            sut.ChangeBalance(clientID, amount);

            // Asserts
            clientRepositoryMock.Received(1).ChangeBalance(clientID, amount);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),
           "There is no clients with id 12")]
        public void ShouldThrowExxceptionWhileChangingBalanceDidntExist()
        {
            // Arrange
            var clientRepositoryMock = Substitute.For<IClientRepository>();
            var sut = new ClientManager(clientRepositoryMock);
            int clientID = 12;
            decimal amount = 300;

            clientRepositoryMock
                .IsClientExist(Arg.Is<int>(clientID))
                .Returns(false);

            // Act
                sut.ChangeBalance(clientID, amount);
                Assert.Fail("Expected Exception");

            // Asserts
            clientRepositoryMock.DidNotReceive().ChangeBalance(clientID, amount);
        }

        [TestMethod]
        public void ShouldDeleteClient()
        {
            // Arrange
            var clientRepositoryMock = Substitute.For<IClientRepository>();
            var sut = new ClientManager(clientRepositoryMock);
            var client = new Client
            {
                ClientID = 3,
                Name = "Kilgore Trout",
                PhoneNumber = "327-57-53",
                Balance = 145
            };

            clientRepositoryMock
                .IsClientExist(Arg.Is<string>(client.Name))
                .Returns(true);

            // Act
            sut.DeleteClient(client);

            // Asserts
            clientRepositoryMock.Received(1).Remove(client);
        }

        [TestMethod]
        public void ShouldNotCallDeleteForClientThatDoesntExist()
        {
            // Arrange
            var clientRepositoryMock = Substitute.For<IClientRepository>();
            var sut = new ClientManager(clientRepositoryMock);
            var client = new Client
            {
                ClientID = 3,
                Name = "Kilgore Trout",
                PhoneNumber = "327-57-53",
                Balance = 145
            };
            int clientID = 3;

            clientRepositoryMock
                .IsClientExist(Arg.Is<int>(clientID))
                .Returns(false);

            // Act
            sut.DeleteClient(client);

            // Asserts
            clientRepositoryMock.DidNotReceive().Remove(client);
        }

        [TestMethod]
        public void ShouldUpdateClient()
        {
            // Arrange
            var clientRepositoryMock = Substitute.For<IClientRepository>();
            var sut = new ClientManager(clientRepositoryMock);
            var client = new Client
            {
                ClientID = 3,
                Name = "Kilgore Trout",
                PhoneNumber = "327-57-53",
                Balance = 145
            };

            // Act
            sut.ClientUpdate(client);

            // Asserts
            clientRepositoryMock.Received(1).ClientUpdate(client);
        }

        [TestMethod]
        public void ShouldGetClientBalanceStatusGreen()
        {
            // Arrange
            var clientRepositoryMock = Substitute.For<IClientRepository>();
            var sut = new ClientManager(clientRepositoryMock);
            var client = new Client
            {
                ClientID = 3,
                Name = "Kilgore Trout",
                PhoneNumber = "327-57-53",
                Balance = 145
            };
            int clientID = 3;

            clientRepositoryMock
                .IsClientExist(Arg.Is<int>(clientID))
                .Returns(true);

            clientRepositoryMock
                .GetClientName(Arg.Is<int>(clientID))
                .Returns(client.Name);

            clientRepositoryMock
                .GetClientBalance(Arg.Is<int>(clientID))
                .Returns(145);

            // Act
            var result = sut.GetClientBalanceStatus(clientID);

            // Asserts
            Assert.AreEqual(result.Name, client.Name);
            Assert.AreEqual(result.Status, "Green");
            Assert.AreEqual(result.Balance, client.Balance);
        }

        [TestMethod]
        public void ShouldGetClientBalanceStatusOrange()
        {
            // Arrange
            var clientRepositoryMock = Substitute.For<IClientRepository>();
            var sut = new ClientManager(clientRepositoryMock);
            var client = new Client
            {
                ClientID = 3,
                Name = "Kilgore Trout",
                PhoneNumber = "327-57-53",
                Balance = 0
            };
            int clientID = 3;

            clientRepositoryMock
                .IsClientExist(Arg.Is<int>(clientID))
                .Returns(true);

            clientRepositoryMock
                .GetClientName(Arg.Is<int>(clientID))
                .Returns(client.Name);

            clientRepositoryMock
                .GetClientBalance(Arg.Is<int>(clientID))
                .Returns(0);

            // Act
            var result = sut.GetClientBalanceStatus(clientID);

            // Asserts
            Assert.AreEqual(result.Name, client.Name);
            Assert.AreEqual(result.Status, "Orange");
            Assert.AreEqual(result.Balance, client.Balance);
        }

        [TestMethod]
        public void ShouldGetClientBalanceStatusBlack()
        {
            // Arrange
            var clientRepositoryMock = Substitute.For<IClientRepository>();
            var sut = new ClientManager(clientRepositoryMock);
            var client = new Client
            {
                ClientID = 3,
                Name = "Kilgore Trout",
                PhoneNumber = "327-57-53",
                Balance = -98765
            };
            int clientID = 3;
            
            clientRepositoryMock
                .IsClientExist(Arg.Is<int>(clientID))
                .Returns(true);

            clientRepositoryMock
                .GetClientName(Arg.Is<int>(clientID))
                .Returns(client.Name);

            clientRepositoryMock
                .GetClientBalance(Arg.Is<int>(clientID))
                .Returns(-98765);

            // Act
            var result = sut.GetClientBalanceStatus(clientID);

            // Asserts
            Assert.AreEqual(result.Name, client.Name);
            Assert.AreEqual(result.Status, "Black");
            Assert.AreEqual(result.Balance, client.Balance);
        }
    }
}