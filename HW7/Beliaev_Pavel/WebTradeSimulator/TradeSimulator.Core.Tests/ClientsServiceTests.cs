namespace TradeSimulator.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using TradeSimulator.Core.Dto;
    using TradeSimulator.Core.Models;
    using TradeSimulator.Core.Repositories;
    using TradeSimulator.Core.Services;

    [TestClass]
    public class ClientsServiceTests
    {
        IAccountTableRepository accountTableRepository = Substitute.For<IAccountTableRepository>();
        IClientsTableRepository clientsTableRepository = Substitute.For<IClientsTableRepository>();
        IStockPriceTableRepository stockPriceTableRepository = Substitute.For<IStockPriceTableRepository>();
        IStockOfClientTableRepository stockOfClientTableRepository = Substitute.For<IStockOfClientTableRepository>();
        ILogger logger = Substitute.For<ILogger>();

        [TestMethod]
        public void ShouldRegisterNewClient()
        {
            //Arrange
            ClientsService clientsService = new ClientsService(clientsTableRepository, accountTableRepository, stockPriceTableRepository, stockOfClientTableRepository, logger);
            ClientInfo args = new ClientInfo();
            args.Name = "John";
            args.Surname = "Jaymson";
            args.PhoneNumber = "+78908901234";
            //Act
            var clientId = clientsService.RegisterNewClient(args);
            //Assert
            clientsTableRepository.Received(1).Add(Arg.Is<ClientEntity>(
                w => w.Name == args.Name
                && w.Surname == args.Surname
                && w.PhoneNumber == args.PhoneNumber));
            clientsTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "This client already registered")]
        public void ShouldNotRegisterNewClientIfItExists()
        {
            //Arrange
            var clientsTableRepository = Substitute.For<IClientsTableRepository>();
            ClientsService clientsService = new ClientsService(clientsTableRepository, accountTableRepository, stockPriceTableRepository, stockOfClientTableRepository, logger);
            ClientInfo args = new ClientInfo();
            args.Name = "John";
            args.Surname = "Jaymson";
            args.PhoneNumber = "+78908901234";
            //Act
            int clientId = clientsService.RegisterNewClient(args);

            ClientEntity ShouldReturn = new ClientEntity()
            {
                Id = clientId,
                Name = args.Name,
                Surname = args.Surname,
                PhoneNumber = args.PhoneNumber
            };

            clientsTableRepository.GetClientByNameAndSurname(Arg.Is<string>(
                w => w == args.Name), Arg.Is<string>(
                w => w == args.Surname)).Returns(ShouldReturn);

            clientsService.RegisterNewClient(args);
        }

        [TestMethod]
        public void ShouldOpenAccountForNewClient()
        {
            //Arrange
            ClientsService clientsService = new ClientsService(clientsTableRepository, accountTableRepository, stockPriceTableRepository, stockOfClientTableRepository, logger);
            ClientInfo args = new ClientInfo();
            args.Name = "John";
            args.Surname = "Jaymson";
            args.PhoneNumber = "+78908901234";

            AccountInfo argsForAcc = new AccountInfo();
            argsForAcc.Balance = 10m;
            argsForAcc.Zone = "white";

            //Act
            argsForAcc.ClientId = clientsService.RegisterNewClient(args);
            clientsService.CreateNewAccountForNewClient(argsForAcc);

            //Assert
            accountTableRepository.Received(1).Add(Arg.Is<AccountEntity>(
                w => w.Balance == argsForAcc.Balance
                && w.ClientId == argsForAcc.ClientId
                && w.Zone == argsForAcc.Zone));
            accountTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "This client already have an account")]
        public void ShouldNotOpenAccountForNewClientIfItExists()
        {
            //Arrange
            ClientsService clientsService = new ClientsService(clientsTableRepository, accountTableRepository, stockPriceTableRepository, stockOfClientTableRepository, logger);
            ClientInfo args = new ClientInfo();
            args.Name = "John";
            args.Surname = "Jaymson";
            args.PhoneNumber = "+78908901234";

            AccountInfo argsForAcc = new AccountInfo();
            argsForAcc.Balance = 10m;
            argsForAcc.Zone = "white";

            AccountEntity ShouldReturnAcc = new AccountEntity();
            ShouldReturnAcc.Balance = 10m;
            ShouldReturnAcc.Stocks = new List<StockOfClientEntity>();
            ShouldReturnAcc.Zone = "white";

            //Act
            argsForAcc.ClientId = clientsService.RegisterNewClient(args);

            ShouldReturnAcc.AccountId = clientsService.CreateNewAccountForNewClient(argsForAcc);

            ShouldReturnAcc.ClientId = argsForAcc.ClientId;

            accountTableRepository.GetAccountByClientId(Arg.Is<int>(w => w == argsForAcc.ClientId)).Returns(ShouldReturnAcc);

            clientsService.CreateNewAccountForNewClient(argsForAcc);
        }

        [TestMethod]
        public void ShouldRegisterStockForNewClient()
        {
            //Arrange
            ClientsService clientsService = new ClientsService(clientsTableRepository, accountTableRepository, stockPriceTableRepository, stockOfClientTableRepository, logger);
            StockOfClientInfo args = new StockOfClientInfo()
            {
                AccountId = 1,
                TypeOfStocks = "A",
                quantityOfStocks = 10
            };
            StockPriceEntity stockPrice = new StockPriceEntity();
            stockPriceTableRepository.GetStockPriceEntityByStockType(Arg.Is<string>(w => w == args.TypeOfStocks.ToUpper())).Returns(stockPrice);

            //Act
            clientsService.RegisterStockForNewClient(args);

            //Assert
            stockOfClientTableRepository.Received(1).Add(Arg.Is<StockOfClientEntity>(
                w => w.AccountId == args.AccountId
                && w.TypeOfStocks == args.TypeOfStocks
                && w.quantityOfStocks == args.quantityOfStocks));
            stockOfClientTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "StockPrice Table doesnt contain this type of stock")]
        public void ShouldNotRegisterStockForNewClientIfItDoesntExists()
        {
            //Arrange
            ClientsService clientsService = new ClientsService(clientsTableRepository, accountTableRepository, stockPriceTableRepository, stockOfClientTableRepository, logger);
            StockOfClientInfo args = new StockOfClientInfo()
            {
                AccountId = 1,
                TypeOfStocks = "A",
                quantityOfStocks = 10
            };
            StockPriceEntity stockPrice = null;
            stockPriceTableRepository.GetStockPriceEntityByStockType(Arg.Is<string>(w => w == args.TypeOfStocks.ToUpper())).Returns(stockPrice);

            //Act
            clientsService.RegisterStockForNewClient(args);

            //Assert
            stockOfClientTableRepository.Received(1).Add(Arg.Is<StockOfClientEntity>(
                w => w.AccountId == args.AccountId
                && w.TypeOfStocks == args.TypeOfStocks
                && w.quantityOfStocks == args.quantityOfStocks));
            stockOfClientTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        public void ShouldRegisterNewTypeOfStock()
        {
            //Arrange
            ClientsService clientsService = new ClientsService(clientsTableRepository, accountTableRepository, stockPriceTableRepository, stockOfClientTableRepository, logger);
            StockPriceInfo args = new StockPriceInfo()
            {
                TypeOfStock = "A",
                PriceOfStock = 10
            };
            StockPriceEntity stockPrice = null;
            stockPriceTableRepository.GetStockPriceEntityByStockType(Arg.Is<string>(w => w == args.TypeOfStock.ToUpper())).Returns(stockPrice);

            //Act
            clientsService.RegisterNewTypeOfStock(args);

            //Assert
            stockPriceTableRepository.Received(1).Add(Arg.Is<StockPriceEntity>(
                w => w.TypeOfStock == args.TypeOfStock
                && w.PriceOfStock == args.PriceOfStock));
            stockPriceTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "StockPrice Table already contain this type of stock")]
        public void ShouldNotRegisterNewTypeOfStockIfItExists()
        {
            //Arrange
            ClientsService clientsService = new ClientsService(clientsTableRepository, accountTableRepository, stockPriceTableRepository, stockOfClientTableRepository, logger);
            StockPriceInfo args = new StockPriceInfo()
            {
                TypeOfStock = "A",
                PriceOfStock = 10
            };
            StockPriceEntity stockPrice = new StockPriceEntity();
            stockPriceTableRepository.GetStockPriceEntityByStockType(Arg.Is<string>(w => w == args.TypeOfStock.ToUpper())).Returns(stockPrice);

            //Act
            clientsService.RegisterNewTypeOfStock(args);

            //Assert
            stockPriceTableRepository.Received(1).Add(Arg.Is<StockPriceEntity>(
                w => w.TypeOfStock == args.TypeOfStock
                && w.PriceOfStock == args.PriceOfStock));
            stockPriceTableRepository.Received(1).SaveChanges();
        }
    }
}
