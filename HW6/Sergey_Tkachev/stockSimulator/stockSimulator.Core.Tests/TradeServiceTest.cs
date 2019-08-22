using System;
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
        [TestMethod]
        public void ShouldSubstractMoneyAndAddStocks()
        {
            //Arrange
            var clientTableRepository = Substitute.For<IClientTableRepository>();
            var stockTableRepository = Substitute.For<IStockTableRepository>();
            var stockClientRepository = Substitute.For<IStockOfClientsTableRepository>();
            ClientService clientService = new ClientService(clientTableRepository);
            ClientRegistrationInfo clArgs = new ClientRegistrationInfo();
            StockService stockService = new StockService(stockTableRepository);
            StockRegistrationInfo stArgs = new StockRegistrationInfo();
            EditCleintStockService editCleintStockService = new EditCleintStockService(stockClientRepository);
            EditStockOfClientInfo editArgs = new EditStockOfClientInfo();
            TradeInfo tradeInfo = new TradeInfo();
            TransactionService transactionService = new TransactionService(clientTableRepository, 
                                                                            stockTableRepository, 
                                                                            stockClientRepository);

            clArgs.Name = "Serj";
            clArgs.Surname = "Tankian";
            clArgs.PhoneNumber = "+7228133705";
            clArgs.AccountBalance = 100;
            var clientA_ID = clientService.RegisterNewClient(clArgs);

            clArgs.Name = "Chester ";
            clArgs.Surname = "Bennington ";
            clArgs.PhoneNumber = "+7228133705";
            clArgs.AccountBalance = 50;
            var clientB_ID = clientService.RegisterNewClient(clArgs);

            stArgs.Name = "Yandex";
            stArgs.Type = "P";
            stArgs.Cost = 10;
            var stock_ID = stockService.RegisterNewStock(stArgs);

            editArgs.Client_ID = clientB_ID;
            editArgs.Stock_ID = stock_ID;
            editArgs.AmountOfStocks = 5;
            editCleintStockService.Edit(editArgs);

            //Act
            tradeInfo.Customer_ID = clientA_ID;
            tradeInfo.Seller_ID = clientB_ID;
            tradeInfo.Stock_ID = stock_ID;
            tradeInfo.Amount = 5;
            transactionService.Trade(tradeInfo);

            //Assert
            transactionService.Received(1).Add(Arg.Is<ClientEntity>(
                c => c.Name == args.Name
                && c.Surname == args.Surname
                && c.PhoneNumber == args.PhoneNumber
                && c.AccountBalance == args.AccountBalance));
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
