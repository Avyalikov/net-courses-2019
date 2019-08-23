using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Trading.Core.Services;
using Trading.Core.DataTransferObjects;
using Trading.Core;

namespace Trading.Core.Tests
{
    /// <summary>
    /// Summary description for TradingOperationTests
    /// </summary>
    [TestClass]
    public class TradingOperationTests
    {
        IClientService clientService;
        IShareService shareService;
        IClientsSharesService clientsSharesService;

        [TestInitialize]
        public void Initialize()
        {
            clientService = Substitute.For<IClientService>();
            shareService = Substitute.For<IShareService>();
            clientsSharesService = Substitute.For<IClientsSharesService>();
            shareService.GetAllShares().Returns(new List<ShareEntity>()
            {
                new ShareEntity()
                {
                    ShareID = 1,
                    ShareCost = 10
                }
            });
        }

        [TestMethod]
        public void ShouldBuyAndSellShares()
        {
            //Arrange
            TradingOperationService operationService = new TradingOperationService(
                clientService, shareService, clientsSharesService);
            int firstClientID = 1;
            int secondClientID = 2;
            ClientsSharesEntity clientsShares = new ClientsSharesEntity()
            {
                ClientID = 1,
                ShareID = 1,
                Amount = 10
            };
            int numberOfSoldShares = 10;
            //Act
            operationService.SellAndBuyShares(firstClientID, secondClientID, clientsShares, numberOfSoldShares);
            //Assert
            clientsSharesService.Received(2).ChangeClientsSharesAmount(Arg.Any<ClientsSharesInfo>());
            clientService.Received(2).ChangeMoney(Arg.Any<int>(), Arg.Any<decimal>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Cannot sell shares to yourself")]        
        public void ShouldNotBuyAndSellSharesYourself()
        {
            //Arrange
            TradingOperationService operationService = new TradingOperationService(
                clientService, shareService, clientsSharesService);
            int firstClientID = 1;
            int secondClientID = 1;
            ClientsSharesEntity clientsShares = new ClientsSharesEntity()
            {
                ClientID = 1,
                ShareID = 1,
                Amount = 10
            };
            int numberOfSoldShares = 10;
            //Act
            operationService.SellAndBuyShares(firstClientID, secondClientID, clientsShares, numberOfSoldShares);
            //Assert
            clientsSharesService.DidNotReceive().ChangeClientsSharesAmount(Arg.Any<ClientsSharesInfo>());
            clientService.DidNotReceive().ChangeMoney(Arg.Any<int>(), Arg.Any<decimal>());
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Cannot sell 100 that more than client have 10")]
        public void ShouldNotBuyAndSellSharesMoreThanHave()
        {
            //Arrange
            TradingOperationService operationService = new TradingOperationService(
                clientService, shareService, clientsSharesService);
            int firstClientID = 1;
            int secondClientID = 2;
            ClientsSharesEntity clientsShares = new ClientsSharesEntity()
            {
                ClientID = 1,
                ShareID = 1,
                Amount = 10
            };
            int numberOfSoldShares = 100;
            //Act
            operationService.SellAndBuyShares(firstClientID, secondClientID, clientsShares, numberOfSoldShares);
            //Assert
            clientsSharesService.DidNotReceive().ChangeClientsSharesAmount(Arg.Any<ClientsSharesInfo>());
            clientService.DidNotReceive().ChangeMoney(Arg.Any<int>(), Arg.Any<decimal>());
        }
    }
}
