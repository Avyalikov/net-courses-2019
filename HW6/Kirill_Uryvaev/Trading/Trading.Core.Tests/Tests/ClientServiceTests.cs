using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Trading.Core.Services;
using Trading.Core.Repositories;
using Trading.Core.DataTransferObjects;

namespace Trading.Core.Tests
{
    /// <summary>
    /// Summary description for ClientServiceTests
    /// </summary>
    [TestClass]
    public class ClientServiceTests
    {
        IClientRepository clientRepository;
        [TestInitialize]
        public void Initialize()
        {
            clientRepository = Substitute.For<IClientRepository>();
            clientRepository.When(w => w.Add(Arg.Any<ClientEntity>())).Do(x => x.Arg<ClientEntity>().ClientID = 1);
            clientRepository.LoadClientByID(1).Returns(new ClientEntity()
            {
                ClientID = 1,
                ClientFirstName = "Josh",
                ClientLastName = "Smith",
                PhoneNumber = "80000000000",
                ClientBalance = 123
            });
            clientRepository.LoadAllClients().Returns(new List<ClientEntity>()
            {
                new ClientEntity()
                {
                    ClientFirstName = "Jack",
                    ClientBalance = 1000
                },
                new ClientEntity()
                {
                    ClientFirstName = "Lana",
                    ClientBalance = 0
                },
                new ClientEntity()
                {
                    ClientFirstName = "Percy",
                    ClientBalance = -120
                },
            });
        }

        [TestMethod]
        public void ShouldRegisterClient()
        {
            //Arrange
            ClientService clientService = new ClientService(clientRepository);
            ClientRegistrationInfo clientInfo = new ClientRegistrationInfo()
            {
                FirstName = "Josh",
                LastName = "Smith",
                PhoneNumber = "80000000000"
            };
            //Act
            int id = clientService.RegisterClient(clientInfo);
            //Assert
            clientRepository.Received(1).Add(Arg.Is<ClientEntity>(
                w => w.ClientFirstName == clientInfo.FirstName
                && w.ClientLastName == clientInfo.LastName
                && w.PhoneNumber == clientInfo.PhoneNumber));
            clientRepository.Received(1).SaveChanges();
            Assert.AreEqual(1, id);
        }

        [TestMethod]
        public void ShouldChangeClientMoney()
        {
            //Arrange
            ClientService clientService = new ClientService(clientRepository);
            int clientID = 1;
            int amount = 100;
            //Act
            clientService.ChangeMoney(clientID, amount);
            //Assert
            clientRepository.Received(1).LoadClientByID(1);
            clientRepository.Received(1).SaveChanges();
        }
        [TestMethod]
        public void ShouldNotChangeClientMoney()
        {
            //Arrange
            ClientService clientService = new ClientService(clientRepository);
            int clientID = 143;
            int amount = 100;
            //Act
            clientService.ChangeMoney(clientID, amount);
            //Assert
            clientRepository.Received(1).LoadClientByID(143);
            clientRepository.DidNotReceive().SaveChanges();
        }
        [TestMethod]
        public void ShouldReturnAllClients()
        {
            //Arrange
            ClientService clientService = new ClientService(clientRepository);
            //Act
            var clients = clientService.GetAllClients();
            //Assert
            clientRepository.Received(1).LoadAllClients();
            Assert.AreEqual(3, clients.Count());
        }
        [TestMethod]
        public void ShouldReturnClientsWithZeroBalance()
        {
            //Arrange
            ClientService clientService = new ClientService(clientRepository);
            //Act
            var clients = clientService.GetClientsFromOrangeZone();
            //Assert
            clientRepository.Received(1).LoadAllClients();
            Assert.AreEqual(clients.Count(), clients.Where(x=>x.ClientBalance==0).Count());
        }
        [TestMethod]
        public void ShouldReturnClientsWithNegativeBalance()
        {
            //Arrange
            ClientService clientService = new ClientService(clientRepository);
            //Act
            var clients = clientService.GetClientsFromBlackZone();
            //Assert
            clientRepository.Received(1).LoadAllClients();
            Assert.AreEqual(clients.Count(), clients.Where(x => x.ClientBalance < 0).Count());
        }
    }
}
