using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Trading.Core.Dto;
using Trading.Core.Models;
using Trading.Core.Repositories;
using Trading.Core.Services;

namespace Trading.Core.Tests
{
    [TestClass]
    public class ClientsServiceTests
    {
        IClientTableRepository clientTableRepository;
        ClientsService clientsService;
        [TestInitialize]
        public void Initialize()
        {
            clientTableRepository = Substitute.For<IClientTableRepository>();
            clientTableRepository.GetById(1).Returns(new ClientEntity()
            {
                Id = 1,
                Balance = 0M,
                Name = "Vlad Blood"
            });
            clientTableRepository.GetClientsInOrangeZone()
                .Returns(new List<ClientEntity>());
            clientTableRepository.GetClientsInBlackZone()
                .Returns(new List<ClientEntity>());
            clientsService = new ClientsService(clientTableRepository);
        }
        [TestMethod]
        public void ShouldRegisterNewClient()
        {
            //Arrange
            ClientRegistrationInfo args = new ClientRegistrationInfo();
            args.Name = "Alex Grind";
            args.Phone = "+3 893 212 11 21";
            //Act
            int clientId = clientsService.RegisterNewClient(args);
            //Assert
            clientTableRepository.Received(1).Add(Arg.Is<ClientEntity>(
                s => s.Name == args.Name 
                && s.Phone == args.Phone));
            clientTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "This client has been already registered")]
        public void ShouldNotRegisterNewClientIfItExists()
        {
            //Arrange
            ClientRegistrationInfo args = new ClientRegistrationInfo();
            args.Name = "Alex Grind";
            args.Phone = "+3 893 212 11 21";
            //Act
            clientsService.RegisterNewClient(args);
            clientTableRepository.Contains(Arg.Is<ClientEntity>(
                s => s.Name == args.Name
                && s.Phone == args.Phone)).Returns(true);

            //Assert
            clientsService.RegisterNewClient(args);
        }
        [TestMethod]
        public void ShouldPutMoneyToBalance()
        {
            //Arrange
            ArgumentsForPutMoneyToBalance args = new ArgumentsForPutMoneyToBalance()
            {
                AmountToPut = 100M,
                ClientId = 1
            };
            var balanceBeforeChange = clientTableRepository.GetById(1).Balance;
            //Act
            clientTableRepository.ContainsById(args.ClientId).Returns(true);
            clientsService.PutMoneyToBalance(args);

            //Assert
            clientTableRepository.Received(1).Change(Arg.Is<ClientEntity>(s =>
            s.Id == args.ClientId
            && s.Balance == balanceBeforeChange+args.AmountToPut));
            clientTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Client with Id 1 doesn't exist")]
        public void ShouldNotPutMoneyToBalanceIfClientDoesntExist()
        {
            //Arrange
            var args = new ArgumentsForPutMoneyToBalance();

            //Act
            clientTableRepository.ContainsById(Arg.Is(args.ClientId)).Returns(false);

            //Assert
            clientsService.PutMoneyToBalance(args);
        }

        [TestMethod]
        public void ShouldGetClientsInOrangeZone()
        {
            //Arrange
            var clientsWhichHaveZeroBalance = clientTableRepository.GetClientsInOrangeZone();
            IEnumerable<ClientEntity> clientsInOrangeZone;
            //Act
            clientsInOrangeZone = clientsService.GetClientsInOrangeZone();

            //Assert
            Assert.AreEqual(clientsWhichHaveZeroBalance, clientsInOrangeZone);
        }
        [TestMethod]
        public void ShouldGetClientsInBlackZone()
        {
            //Arrange
            var clientsWhichHaveLessThanZeroBalance = clientTableRepository.GetClientsInBlackZone();
            IEnumerable<ClientEntity> clientsInBlackZone;
            //Act
            clientsInBlackZone = clientsService.GetClientsInBlackZone();

            //Assert
            Assert.AreEqual(clientsWhichHaveLessThanZeroBalance, clientsInBlackZone);
        }
    }
}
