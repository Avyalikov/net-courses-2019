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
        IStockOfClientsTableRepository stockOfClientsTableRepository;
        ITransactionHistoryTableRepository transactionHistoryTableRepository;
        EditCleintStockService editClientStockService;

        [TestInitialize]
        public void Initialize()
        {
            this.clientTableRepository = Substitute.For<IClientTableRepository>();
            this.stockTableRepository = Substitute.For<IStockTableRepository>();
            this.stockOfClientsTableRepository = Substitute.For<IStockOfClientsTableRepository>();
            this.transactionHistoryTableRepository = Substitute.For<ITransactionHistoryTableRepository>();
            this.editClientStockService = new EditCleintStockService(this.stockOfClientsTableRepository);


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

            stockOfClientsTableRepository.Get(2).Returns(new StockOfClientsEntity()
            {
                ID = 2,
                ClientID = 32,
                StockID = 1,
                Amount = 5
            });

            stockTableRepository.GetCost(Arg.Is<int>(1)).Returns(10);
            stockOfClientsTableRepository.GetAmount(Arg.Is<int>(5),
                                                Arg.Is<int>(1)).Returns(0);
            stockOfClientsTableRepository.GetAmount(Arg.Is<int>(32),
                                                Arg.Is<int>(1)).Returns(10);
            clientTableRepository.GetBalance(Arg.Is<int>(5)).Returns(100);
            clientTableRepository.GetBalance(Arg.Is<int>(32)).Returns(50);
            stockTableRepository.GetType(Arg.Is<int>(1)).Returns("P");
            
            

        }

        [TestMethod]
        public void ShouldSubstractMoneyAndAddStocks()
        {
            //Arrange
           stockOfClientsTableRepository.Contains(Arg.Is<StockOfClientsEntity>(sc => sc.Amount == 5
                                                                                    && sc.ClientID == 5
                                                                                    && sc.ID == 0
                                                                                    && sc.StockID == 1), out Arg.Is<int>(0)).Returns(false);

           
            TransactionService transactionService = new TransactionService(this.clientTableRepository,
                                                                            this.stockTableRepository,
                                                                            this.stockOfClientsTableRepository,
                                                                            this.transactionHistoryTableRepository,
                                                                            this.editClientStockService);

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
            this.clientTableRepository.Received(1).UpdateBalance(Arg.Is<int>(5), Arg.Is<decimal>(50));
            this.stockOfClientsTableRepository.Received(1).Add(Arg.Is<StockOfClientsEntity>(sc => sc.Amount == 5
                                                                                    && sc.ClientID == 5
                                                                                    && sc.StockID == 1));

            this.clientTableRepository.Received(2).SaveChanges();
        }

        [TestMethod]
        public void ShouldSubstractStocksAndAddMoney()
        {
            //Arrange
            TransactionService transactionService = new TransactionService(this.clientTableRepository,
                                                                           this.stockTableRepository,
                                                                           this.stockOfClientsTableRepository,
                                                                           this.transactionHistoryTableRepository,
                                                                           this.editClientStockService);
            stockOfClientsTableRepository.Contains(Arg.Is<StockOfClientsEntity>(sc => sc.Amount == 5
                                                                                 && sc.ClientID == 32
                                                                                 && sc.StockID == 1),
                                              out Arg.Any<int>())
                                              .Returns(x => {
                                                  x[1] = 2;
                                                  return true;
                                              });

            stockOfClientsTableRepository.Update(Arg.Is<int>(2),
                                               Arg.Is<StockOfClientsEntity>(sc => sc.Amount == 5
                                                                                  && sc.ClientID == 32
                                                                                  && sc.ID == 0
                                                                                  && sc.StockID == 1)).Returns("Stock of Client data was updated.");

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
            this.clientTableRepository.Received(1).UpdateBalance(Arg.Is<int>(32), Arg.Is<decimal>(100));
            this.stockOfClientsTableRepository.Received(1).Update(Arg.Is<int>(2), 
                                                            Arg.Is<StockOfClientsEntity>(sc => 
                                                                                     sc.Amount == 5
                                                                                  && sc.ClientID == 32
                                                                                  && sc.ID == 0
                                                                                  && sc.StockID == 1));

            this.clientTableRepository.Received(2).SaveChanges();
        }

        [TestMethod]
        public void ShouldAddEntryInHistoryTable()
        {
            //Arrange
            TransactionService transactionService = new TransactionService(this.clientTableRepository,
                                                                           this.stockTableRepository,
                                                                           this.stockOfClientsTableRepository,
                                                                           this.transactionHistoryTableRepository, 
                                                                           this.editClientStockService);

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
            this.transactionHistoryTableRepository.Received(1).Add(Arg.Is<HistoryEntity>(
                 h => h.CustomerID == 5
                 && h.StockAmount == 5
                 && h.StockID == 1
                 && h.SellerID == 32
                 && h.TransactionCost == 50
                 && h.StockType == "P"));

            this.transactionHistoryTableRepository.Received(1).SaveChanges();
        }
    }

    
}
