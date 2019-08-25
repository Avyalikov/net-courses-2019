using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Trading.Core.Repositories;
using Trading.Core.Services;
using Trading.Core.DTO;
using Trading.Core.Model;


namespace TradingApp.Tests
{
    [TestClass]
    public class ClientStockServiceTest
    {
        ITableRepository<ClientStock> clientStockTableRepository;
        

        [TestInitialize]
        public void Initialize()
        {
            clientStockTableRepository = Substitute.For<ITableRepository<ClientStock>>();
            clientStockTableRepository.FindByPK(1, 1).Returns(new ClientStock()
            {
                ClientID = 1,
                StockID = 1,
                Quantity = 10
            });
          
        }



        [TestMethod]
        public void ShouldAddNewClientStock()
        {
            //Arrange
            ClientStockService clientStockService = new ClientStockService(clientStockTableRepository);
            ClientStockInfo clientStockInfo = new ClientStockInfo
            {
                ClientId = 1,
                StockId = 1,
                Amount = 10
            };
            //Act
            clientStockService.AddClientStock(clientStockInfo);
            //Assert
            clientStockTableRepository.Received(1).Add(Arg.Is<ClientStock>(
                w => w.ClientID == 1 &&
                w.StockID == 1 &&
                w.Quantity == 10
                ));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "This clientstock exists.Can't continue")]
        public void ShouldNotAddNewClientStockIfItExists()
        {
            // Arrange

            ClientStockService clientStockService = new ClientStockService(clientStockTableRepository);
            ClientStockInfo clientStockInfo = new ClientStockInfo
            {
                ClientId = 1,
                StockId = 1,
                Amount = 10
            };
            // Act
            clientStockService.AddClientStock(clientStockInfo);

            clientStockTableRepository.ContainsDTO(Arg.Is<ClientStock>(
                 w => w.ClientID == 1 &&
                w.StockID == 1 &&
                w.Quantity == 10)).Returns(true);

            clientStockService.AddClientStock(clientStockInfo);
        }

        [TestMethod]
        public void ShouldGetClientStockInfo()
        {
            // Arrange

            ClientStockService clientStockService = new ClientStockService(clientStockTableRepository);
            clientStockTableRepository.ContainsByPK( 1, 1 ).Returns(true);


            // Act
            var clientStock = clientStockService.GetEntityByCompositeID(1, 1);

            // Assert
            clientStockTableRepository.Received(1).FindByPK(1, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "ClientStock  doesn't exist")]
        public void ShouldThrowExceptionIfCantFindClientStock()
        {
            // Arrange

            ClientStockService clientStockService = new ClientStockService(clientStockTableRepository);
            clientStockTableRepository.ContainsByPK( 1, 1 ).Returns(false);

            // Act
            var clientStock = clientStockService.GetEntityByCompositeID(1, 1);


        }
          [TestMethod]
         public void ShouldEditClientStockAmount()
          {
              // Arrange

              ClientStockService clientStockService = new ClientStockService(clientStockTableRepository);
              clientStockTableRepository.ContainsByPK(1,1).Returns(true);
              int clientId = 1;
            int stockId = 1;

              int amount = 5;
              ClientStock clientStock = new ClientStock()
              {
                  ClientID = 1,
                 StockID=1,
                  Quantity = 1000,
              };

              // Act
              clientStockService.EditClientStocksAmount(clientId, stockId,amount);

              // Assert
              clientStockTableRepository.Received(1).FindByPK(clientId,stockId);
              clientStock.Quantity += amount;
              clientStockTableRepository.Received(1).SaveChanges();

          }
    }
}


     

