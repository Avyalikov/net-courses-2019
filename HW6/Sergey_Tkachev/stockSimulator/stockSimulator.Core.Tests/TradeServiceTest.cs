using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using stockSimulator.Core.DTO;
using stockSimulator.Core.Models;
using stockSimulator.Core.Repositories;
using stockSimulator.Core.Services;

namespace stockSimulator.Core.Tests
{
    [TestClass]
    public class TradeServiceTest
    {
        IClientTableRepository clientTableRepository;
        IStockTableRepository stockTableRepository;
        IStockOfClientsTableRepository stockClientTableRepository;
        //List<ClientEntity> clients;
        //List<StockEntity> stocks;
        //List<StockOfClientsEntity> stocksOfClient;

        [TestInitialize]
        public void Initialize()
        {
            this.clientTableRepository = Substitute.For<IClientTableRepository>();
            this.stockTableRepository = Substitute.For<IStockTableRepository>();
            this.stockClientTableRepository = Substitute.For<IStockOfClientsTableRepository>();

            clientTableRepository.Get(5).Returns(new ClientEntity()
            {
                ID = 5,
                Name = "Serj",
                Surname = "Tankian",
                PhoneNumber = "+7228133705",
                AccountBalance = 100,
            });
            clientTableRepository.Get(32).Returns(new ClientEntity()
            {
                ID = 32,
                Name = "Chester",
                Surname = "Bennington",
                PhoneNumber = "+7228133705",
                AccountBalance = 50
            });

            stockTableRepository.Get(1).Returns(new StockEntity()
            {
                ID = 1,
                Name = "Yandex",
                Type = "P",
                Cost = 10
            });

            stockClientTableRepository.Get(2).Returns(new StockOfClientsEntity()
            {
                ID = 2,
                ClientID = 32,
                StockID = 1,
                AmountOfStocks = 5
            });
        }

        [TestMethod]
        public void ShouldSubstractMoneyAndAddStocks()
        {
            //Arrange
            //ClientService clientService = new ClientService(clientTableRepository);
            //ClientRegistrationInfo clArgs = new ClientRegistrationInfo();
            //StockService stockService = new StockService(stockTableRepository);
            //StockRegistrationInfo stArgs = new StockRegistrationInfo();
            //EditCleintStockService editCleintStockService = new EditCleintStockService(stockClientTableRepository);
            //EditStockOfClientInfo editArgs = new EditStockOfClientInfo();
            TransactionService transactionService = new TransactionService(this.clientTableRepository,
                                                                            this.stockTableRepository,
                                                                            this.stockClientTableRepository);

            //Act
            TradeInfo tradeInfo = new TradeInfo()
            {
                Customer_ID = 5,
                Seller_ID = 32,
                Stock_ID = 1,
                Amount = 5
            };
            transactionService.Trade(tradeInfo);

            //Assert
            clientTableRepository.Received(1).UpdateBalance(Arg.Is<int>(5), Arg.Is<decimal>(50));
            stockClientTableRepository.Received(1).UpdateAmount(Arg.Is<int>(5),
                                                                Arg.Is<int>(1),
                                                                Arg.Is<int>(5));

            clientTableRepository.Received(1).SaveChanges();
            clientTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        public void ShouldSubstractStocksAndAddMoney()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void ShouldAddEntryInHistoryTable()
        {
            throw new NotImplementedException();
        }
    }

    
}
