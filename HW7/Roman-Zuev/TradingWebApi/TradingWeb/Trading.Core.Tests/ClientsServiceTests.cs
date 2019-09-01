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
        ISharesTableRepository sharesTableRepository;
        ClientsService clientsService;
        [TestInitialize]
        public void Initialize()
        {
            clientTableRepository = Substitute.For<IClientTableRepository>();
            sharesTableRepository = Substitute.For<ISharesTableRepository>();
            clientTableRepository.GetById(1).Returns(new ClientEntity()
            {
                Id = 1,
                Balance = 0M,
                Name = "Vlad Blood"
            });
            clientTableRepository.GetAllInOrangeZone()
                .Returns(new List<ClientEntity>());
            clientTableRepository.GetAllInBlackZone()
                .Returns(new List<ClientEntity>());
            clientsService = new ClientsService(clientTableRepository, sharesTableRepository);
        }
        [TestMethod]
        public void ShouldRegisterNewClient()
        {
            //Arrange
            ClientRegistrationInfo args = new ClientRegistrationInfo();
            args.Name = "Alex Grind";
            args.Phone = "+3 893 212 11 21";
            //Act
            int clientId = clientsService.RegisterNew(args);
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
            clientsService.RegisterNew(args);
            clientTableRepository.Contains(Arg.Is<ClientEntity>(
                s => s.Name == args.Name
                && s.Phone == args.Phone)).Returns(true);

            //Assert
            clientsService.RegisterNew(args);
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
            && s.Balance == balanceBeforeChange + args.AmountToPut));
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
            var clientsWhichHaveZeroBalance = clientTableRepository.GetAllInOrangeZone();
            IEnumerable<ClientEntity> clientsInOrangeZone;
            //Act
            clientsInOrangeZone = clientsService.GetAllInOrangeZone();

            //Assert
            Assert.AreEqual(clientsWhichHaveZeroBalance, clientsInOrangeZone);
        }
        [TestMethod]
        public void ShouldGetClientsInBlackZone()
        {
            //Arrange
            var clientsWhichHaveLessThanZeroBalance = clientTableRepository.GetAllInBlackZone();
            IEnumerable<ClientEntity> clientsInBlackZone;
            //Act
            clientsInBlackZone = clientsService.GetAllInBlackZone();

            //Assert
            Assert.AreEqual(clientsWhichHaveLessThanZeroBalance, clientsInBlackZone);
        }

        [TestMethod]
        public void ShouldUpdateClientInfo()
        {
            //Arrange
            ClientRegistrationInfo args = new ClientRegistrationInfo();
            args.Name = "Alex Grind";
            args.Phone = "+3 893 212 11 21";
            int clientId = 1;
            clientTableRepository.ContainsById(Arg.Is<int>(clientId)).Returns(true);
            //Act
            clientsService.UpdateInfo(clientId, args);
            //Assert
            clientTableRepository.Received(1).Change(Arg.Is<ClientEntity>(
                s => s.Name == args.Name
                && s.Phone == args.Phone));
            clientTableRepository.Received(1).SaveChanges(); 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Client with Id 1 doesn't exist")]
        public void ShouldNotUpdateClientInfo()
        {
            //Arrange
            ClientRegistrationInfo args = new ClientRegistrationInfo();
            args.Name = "Alex Grind";
            args.Phone = "+3 893 212 11 21";
            int clientId = 1;
            clientTableRepository.ContainsById(Arg.Is<int>(clientId)).Returns(false);
            //Act
            clientsService.UpdateInfo(clientId, args);
            //Assert

        }

        [TestMethod]
        public void ShouldGetTopList()
        {
            //Arrange
            int top = 10;
            int page = 1;
            clientTableRepository.Count.Returns(20);
            //Act
            clientsService.GetTop(top, page);
            //Assert
            clientTableRepository.Received(1).GetTop(top * page);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Empty page")]
        public void ShouldNotGetTopList()
        {
            //Arrange
            int top = 10;
            int page = 2;
            clientTableRepository.Count.Returns(5);
            //Act
            clientsService.GetTop(top, page);
            //Assert
        }

        [TestMethod]
        public void ShouldRemoveClientById()
        {
            //Arrange
            int clientId = 1;
            clientTableRepository.GetById(clientId).Returns(new ClientEntity() { Name = "Sad", Id=clientId });
            clientTableRepository.ContainsById(Arg.Is<int>(1)).Returns(true);
            //Act
            clientsService.RemoveById(clientId);
            //Assert
            clientTableRepository.Received(1).Remove(Arg.Is<ClientEntity>(c =>
            c.Id == clientId && c.Name == "Sad"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Client with Id 1 doesn't exist")]
        public void ShouldNotRemoveClientById()
        {
            //Arrange
            int clientId = 1;
            clientTableRepository.GetById(clientId).Returns(new ClientEntity() { Name = "Sad", Id = clientId });
            clientTableRepository.ContainsById(Arg.Is<int>(clientId)).Returns(false);
            //Act
            clientsService.RemoveById(clientId);
            //Assert
        }

        [TestMethod]
        public void ShouldGetBalanceAndGreenZone()
        {
            //Arrange
            int clientId = 1;
            clientTableRepository.ContainsById(Arg.Is<int>(clientId)).Returns(true);
            clientTableRepository.GetById(clientId).Returns(new ClientEntity()
            { Balance = 500, Id = clientId});
            string shouldBe = string.Format($"Balance: 500, Zone: Green");
            //Act
            string test = clientsService.GetBalance(clientId);
            //Assert
            Assert.AreEqual(shouldBe, test);
        }

        [TestMethod]
        public void ShouldGetBalanceAndBlackZone()
        {
            //Arrange
            int clientId = 1;
            clientTableRepository.ContainsById(Arg.Is<int>(clientId)).Returns(true);
            clientTableRepository.GetById(clientId).Returns(new ClientEntity()
            { Balance = -100, Id = clientId });
            string shouldBe = string.Format($"Balance: -100, Zone: Black");
            //Act
            string test = clientsService.GetBalance(clientId);
            //Assert
            Assert.AreEqual(shouldBe, test);
        }

        [TestMethod]
        public void ShouldGetBalanceAndOrangeZone()
        {
            //Arrange
            int clientId = 1;
            clientTableRepository.ContainsById(Arg.Is<int>(clientId)).Returns(true);
            clientTableRepository.GetById(clientId).Returns(new ClientEntity()
            { Balance = 0, Id = clientId });
            string shouldBe = string.Format($"Balance: 0, Zone: Orange");
            //Act
            string test = clientsService.GetBalance(clientId);
            //Assert
            Assert.AreEqual(shouldBe, test);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Client with Id 1 doesn't exist")]
        public void ShouldNotGetBalance()
        {
            //Arrange
            int clientId = 1;
            clientTableRepository.ContainsById(Arg.Is<int>(clientId)).Returns(false);

            //Act
            string test = clientsService.GetBalance(clientId);
            //Assert

        }

        [TestMethod]
        public void ShouldGetClientSharesById()
        {
            //Arrange
            int clientId = 1;
            clientTableRepository.ContainsById(Arg.Is<int>(clientId)).Returns(true);
            clientTableRepository.GetById(clientId).Returns(new ClientEntity()
            { Id = clientId,
              Portfolio = new List<ClientSharesEntity>()
              {
                  new ClientSharesEntity()
                  {
                      Shares = new SharesEntity()
                      {
                          Id = 1,
                          Price = 100
                      },
                      Quantity = 50
                  }
              }
            });
            sharesTableRepository.GetById(1).Returns(new SharesEntity()
            {
                Id = 1,
                Price = 100
            });
            //Act
            var test = clientsService.GetClientSharesById(clientId);
            //Assert
            Assert.IsTrue(test.ContainsKey(sharesTableRepository.GetById(1)));
            Assert.IsTrue(test.Values.Contains(50));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Client with Id 1 doesn't exist")]
        public void ShouldNotGetClientSharesById()
        {
            //Arrange
            int clientId = 1;
            clientTableRepository.ContainsById(Arg.Is<int>(clientId)).Returns(false);
            //Act
            var test = clientsService.GetClientSharesById(clientId);
            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Client have no shares left")]
        public void ShouldNotGetClientSharesByIdWhenNoSharesLeft()
        {
            //Arrange
            int clientId = 1;
            clientTableRepository.ContainsById(Arg.Is<int>(clientId)).Returns(true);
            clientTableRepository.GetById(clientId).Returns(new ClientEntity()
            {
                Id = clientId,
                Portfolio = new List<ClientSharesEntity>()
                {
        
                }
            });
            //Act
            var test = clientsService.GetClientSharesById(clientId);
            //Assert
        }
    }
}
