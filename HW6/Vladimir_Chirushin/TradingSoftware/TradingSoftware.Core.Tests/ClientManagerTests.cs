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
        public void AddClientTest()
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
        public void AddClientParametrsTest()
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
        public void GetAllClientsTest()
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
        public void GetClientNameTest()
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
        public void GetClientNameThatDidntExistTest()
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
        public void GetClientIDTest()
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
        public void GetClientIDDidntExistTest()
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
        public void IsClientIDExistTest()
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
        public void IsClientNameExistTest()
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
        public void GetNumberOfClientsTest()
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
        public void GetClientBalanceTest()
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
        public void GetClientBalanceDidntExistTest()
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
        public void ChangeBalanceTest()
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
        public void ChangeBalanceDidntExistTest()
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
    }
}