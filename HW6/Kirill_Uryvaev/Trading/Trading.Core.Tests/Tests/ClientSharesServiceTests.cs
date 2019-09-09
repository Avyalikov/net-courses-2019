using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trading.Core.Services;
using Trading.Core.Repositories;
using Trading.Core.DataTransferObjects;
using NSubstitute;

namespace Trading.Core.Tests
{
    /// <summary>
    /// Summary description for ClientSharesServiceTests
    /// </summary>
    [TestClass]
    public class ClientSharesServiceTests
    {
        IClientsSharesRepository clientsSharesRepository;

        [TestInitialize]
        public void Initialize()
        {
            clientsSharesRepository = Substitute.For<IClientsSharesRepository>();
            clientsSharesRepository.LoadClientsSharesByID(Arg.Any<ClientsSharesInfo>()).Returns((callInfo) =>
            { if (callInfo.Arg<ClientsSharesInfo>().ClientID == 1 && callInfo.Arg<ClientsSharesInfo>().ShareID == 2)
                {
                    return new ClientsSharesEntity()
                    {
                        ClientID = 1,
                        ShareID = 2,
                        Amount = 15
                    };
                }
                else
                    return null;
            });
        }

        [TestMethod]
        public void ShouldAddSharesForClient()
        {
            //Arrange
            ClientsSharesService clientsSharesService = new ClientsSharesService(clientsSharesRepository);
            ClientsSharesInfo clientsSharesInfo = new ClientsSharesInfo()
            {
                ClientID = 1,
                ShareID = 2,
                Amount = 20
            };
            //Act
            var amount = clientsSharesService.ChangeClientsSharesAmount(clientsSharesInfo);

            //Assert
            clientsSharesRepository.Received(1).SaveChanges();
            Assert.AreEqual(35, amount);
        }

        [TestMethod]
        public void ShouldRegisterNewSharesForClient()
        {
            //Arrange
            ClientsSharesService clientsSharesService = new ClientsSharesService(clientsSharesRepository);
            ClientsSharesInfo clientsSharesInfo = new ClientsSharesInfo()
            {
                ClientID = 2,
                ShareID = 1,
                Amount = 20
            };
            //Act
            var amount = clientsSharesService.ChangeClientsSharesAmount(clientsSharesInfo);

            //Assert
            clientsSharesRepository.Received(1).Add(Arg.Is<ClientsSharesEntity>(
                w => w.ClientID == clientsSharesInfo.ClientID
                && w.ShareID == clientsSharesInfo.ShareID
                && w.Amount == clientsSharesInfo.Amount));
            clientsSharesRepository.Received(1).SaveChanges();
            Assert.AreEqual(20, amount);
        }
    }
}
