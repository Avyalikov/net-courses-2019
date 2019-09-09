using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using stockSimulator.Core.Models;
using stockSimulator.Core.Repositories;
using stockSimulator.Core.Services;

namespace stockSimulator.Core.Tests
{
    [TestClass]
    public class CheckZoneOfClientsTest
    {
        IClientTableRepository clientTableRepository;

        [TestInitialize]
        public void Initialize()
        {
            this.clientTableRepository = Substitute.For<IClientTableRepository>();
            IEnumerable<ClientEntity> clients = new List<ClientEntity>()
            {
                new ClientEntity
                {
                    Name = "Vasiliy",
                    Surname = "Pupkin",
                    PhoneNumber = "+7123",
                    AccountBalance = 356
                },
                new ClientEntity
                {
                    Name = "Oleg",
                    Surname = "Larchik",
                    PhoneNumber = "+8954",
                    AccountBalance = 1
                },
                new ClientEntity
                {
                    Name = "Larisa",
                    Surname = "Vitchuck",
                    PhoneNumber = "+6414",
                    AccountBalance = 0
                },
                new ClientEntity
                {
                    Name = "Luis",
                    Surname = "Vatson",
                    PhoneNumber = "+36584",
                    AccountBalance = -1
                },
                new ClientEntity
                {
                    Name = "Medik",
                    Surname = "Tompon",
                    PhoneNumber = "+6519",
                    AccountBalance = -6412
                },
            };
            this.clientTableRepository.GetClients().Returns(clients);
        }

        [TestMethod]
        public void ShouldReturnListOfClientsInGreenZone()
        {
            //Arrange
            ClientService clientService = new ClientService(clientTableRepository);

            //Act
            var clients = clientService.GetClientsWithPositiveBalance();

            //Assert
            this.clientTableRepository.Received(1).GetClients();
            IEnumerable<ClientEntity> clientsWithMoney = new List<ClientEntity>
            {
                new ClientEntity
                {
                    Name = "Vasiliy",
                    Surname = "Pupkin",
                    PhoneNumber = "+7123",
                    AccountBalance = 356
                },
                new ClientEntity
                {
                    Name = "Oleg",
                    Surname = "Larchik",
                    PhoneNumber = "+8954",
                    AccountBalance = 1
                }
            };

            Assert.AreEqual(clientsWithMoney, clients);
            this.clientTableRepository.Received(1).SaveChanges();
        }
        [TestMethod]
        public void ShouldReturnListOfClientsInOrangeZone()
        {
            //Arrange
            ClientService clientService = new ClientService(clientTableRepository);

            //Act
            var clients = clientService.GetClientsWithZeroBalance();

            //Assert
            this.clientTableRepository.Received(1).GetClients();
            IEnumerable<ClientEntity> clientsWithoutMoney = new List<ClientEntity>
            {
                new ClientEntity
                {
                    Name = "Larisa",
                    Surname = "Vitchuck",
                    PhoneNumber = "+6414",
                    AccountBalance = 0
                }
            };

            Assert.AreEqual(clientsWithoutMoney, clients);
            this.clientTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        public void ShouldReturnListOfClientsInBlackZone()
        {
            //Arrange
            ClientService clientService = new ClientService(clientTableRepository);

            //Act
            var clients = clientService.GetClientsWithNegativeBalance();

            //Assert
            this.clientTableRepository.Received(1).GetClients();
            IEnumerable<ClientEntity> clientsWithDebts = new List<ClientEntity>
            {
                new ClientEntity
                {
                    Name = "Luis",
                    Surname = "Vatson",
                    PhoneNumber = "+36584",
                    AccountBalance = -1
                },
                new ClientEntity
                {
                    Name = "Medik",
                    Surname = "Tompon",
                    PhoneNumber = "+6519",
                    AccountBalance = -6412
                }
            };

            Assert.AreEqual(clientsWithDebts, clients);
            this.clientTableRepository.Received(1).SaveChanges();
        }
    }
}
