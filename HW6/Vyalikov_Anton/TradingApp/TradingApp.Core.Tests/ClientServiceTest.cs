namespace TradingApp.Core.Tests
{
    using TradingApp.Core.DTO;
    using TradingApp.Core.Models;
    using TradingApp.Core.Services;
    using TradingApp.Core.Repos;
    using NSubstitute;
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ClientServiceTest
    {
        [TestMethod]
        public void RegisterClientTest()
        {
            // Arrange
            var clientRepositoryMock = Substitute.For<IClientRepository>();

            var sut = new ClientService(clientRepositoryMock);

            ClientRegistrationData client = new ClientRegistrationData()
            {
                ClientName = "Martin Walker",
                ClientPhone = "8800555",
                Balance = (decimal)6021023
            };

            // Act
            sut.RegisterClient(client);

            // Asserts
            clientRepositoryMock.Received(1).Insert(Arg.Is<Client>(x => x.Name == client.ClientName
                && x.PhoneNumber == client.ClientPhone && x.Balance == client.Balance));
        }

        [TestMethod]
        public void GetAllClientsTest()
        {
            // Arrange
            var clientRepositoryMock = Substitute.For<IClientRepository>();

            var sut = new ClientService(clientRepositoryMock);

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

            var sut = new ClientService(clientRepositoryMock);

            int clientID = 12;
            clientRepositoryMock
                .DoesClientExists(Arg.Is<int>(clientID))
                .Returns(true);

            // Act
            sut.GetClientName(clientID);

            // Asserts
            clientRepositoryMock.Received(1).GetClientName(Arg.Is<int>(clientID));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),
           "There is no clients with id 10")]
        public void GetClientNameThatDidntExistsTest()
        {
            // Arrange
            var clientRepositoryMock = Substitute.For<IClientRepository>();

            var sut = new ClientService(clientRepositoryMock);

            int clientID = 10;
            clientRepositoryMock
                .DoesClientExists(Arg.Is<int>(clientID))
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

            var sut = new ClientService(clientRepositoryMock);

            string clientName = "Martin Walker";
            clientRepositoryMock
                .DoesClientExists(Arg.Is<string>(clientName))
                .Returns(true);

            // Act
            sut.GetClientIDByName(clientName);

            // Asserts
            clientRepositoryMock.Received(1).GetClientID(Arg.Is<string>(clientName));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),
           "There is no clients with name Martin Walker")]
        public void GetClientIDDidntExistTest()
        {
            // Arrange
            var clientRepositoryMock = Substitute.For<IClientRepository>();

            var sut = new ClientService(clientRepositoryMock);

            string clientName = "Martin Walker";
            clientRepositoryMock
                .DoesClientExists(Arg.Is<string>(clientName))
                .Returns(false);

            // Act
            sut.GetClientIDByName(clientName);

            // Asserts
            clientRepositoryMock.DidNotReceive().GetClientID(Arg.Is<string>(clientName));
        }

        [TestMethod]
        public void ChangeBalanceTest()
        {
            // Arrange
            var clientRepositoryMock = Substitute.For<IClientRepository>();
            var sut = new ClientService(clientRepositoryMock);
            int clientID = 10;
            decimal money = 300;

            clientRepositoryMock
                .DoesClientExists(Arg.Is<int>(clientID))
                .Returns(true);

            // Act
            sut.ChangeBalance(clientID, money);

            // Asserts
            clientRepositoryMock.Received(1).ChangeBalance(Arg.Is<int>(clientID), Arg.Is<decimal>(money));
        }  
    }
}
