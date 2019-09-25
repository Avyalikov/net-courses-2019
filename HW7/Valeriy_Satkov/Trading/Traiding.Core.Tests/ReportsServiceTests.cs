namespace Traiding.Core.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using System;
    using System.Collections.Generic;
    using Traiding.Core.Models;
    using Traiding.Core.Repositories;
    using Traiding.Core.Services;

    [TestClass]
    public class ReportsServiceTests
    {
        IOperationTableRepository operationTableRepository;
        IBalanceTableRepository balanceTableRepository;
        ISharesNumberTableRepository sharesNumberTableRepository;
        IShareTableRepository shareTableRepository;
        IClientTableRepository clientTableRepository;
        ReportsService reportsService;

        [TestInitialize]
        public void Initialize()
        {
            var clients = new List<ClientEntity>()
            {
                new ClientEntity() { Id = 1, CreatedAt = DateTime.Now, LastName = "Pavlov", FirstName = "Ivan", PhoneNumber = "+7(812)5551243", Status = true },
                new ClientEntity() { Id = 2, CreatedAt = DateTime.Now, LastName = "Mechnikov", FirstName = "Ilya", PhoneNumber = "+33(0)140205317", Status = true },
                new ClientEntity() { Id = 3, CreatedAt = DateTime.Now, LastName = "Bunin", FirstName = "Ivan", PhoneNumber = "+33(0)420205320", Status = true },
                new ClientEntity() { Id = 4, CreatedAt = DateTime.Now, LastName = "Semyonov", FirstName = "Nikolay", PhoneNumber = "+7(495)4652317", Status = true },
                new ClientEntity() { Id = 5, CreatedAt = DateTime.Now, LastName = "Pasternak", FirstName = "Boris", PhoneNumber = "+7(495)4368173", Status = true },
                new ClientEntity() { Id = 6, CreatedAt = DateTime.Now, LastName = "Cherenkov", FirstName = "Pavel", PhoneNumber = "+7(495)3246421", Status = true },
                new ClientEntity() { Id = 7, CreatedAt = DateTime.Now, LastName = "Tamm", FirstName = "Igor", PhoneNumber = "+7(495)7523146", Status = true },
                new ClientEntity() { Id = 8, CreatedAt = DateTime.Now, LastName = "Frank", FirstName = "Ilya", PhoneNumber = "+7(495)7924194", Status = true },
                new ClientEntity() { Id = 9, CreatedAt = DateTime.Now, LastName = "Landau", FirstName = "Lev", PhoneNumber = "+7(495)7924194", Status = true }
            };
            var balances = new List<BalanceEntity>()
            {
                new BalanceEntity() { Id = 1, Client = clients[0], Amount = 138000, Status = true },
                new BalanceEntity() { Id = 2, Client = clients[1], Amount = 142000, Status = true },
                new BalanceEntity() { Id = 3, Client = clients[2], Amount = 130000, Status = true },
                new BalanceEntity() { Id = 4, Client = clients[3], Amount = 0, Status = true },
                new BalanceEntity() { Id = 5, Client = clients[4], Amount = 0, Status = true },
                new BalanceEntity() { Id = 6, Client = clients[5], Amount = -139700, Status = true },
                new BalanceEntity() { Id = 7, Client = clients[6], Amount = 139700, Status = true },
                new BalanceEntity() { Id = 8, Client = clients[7], Amount = -131000, Status = true },
                new BalanceEntity() { Id = 9, Client = clients[8], Amount = 0, Status = true },
            };
            var shareTypesList = new List<ShareTypeEntity>()
            {
                new ShareTypeEntity() { Id = 1, Name = "Cheap", Cost=1000, Status = true},
                new ShareTypeEntity() { Id = 2, Name = "Middle", Cost=4000, Status = true},
                new ShareTypeEntity() { Id = 3, Name = "Expensive", Cost=10000, Status = true}
            };
            var sharesList = new List<ShareEntity>()
            {
                new ShareEntity() { Id = 1, CreatedAt = DateTime.Now, CompanyName = "Microsoft", Type = shareTypesList[2]/*Expensive*/, Status = true},
                new ShareEntity() { Id = 2, CreatedAt = DateTime.Now, CompanyName = "Apple", Type = shareTypesList[1]/*Middle*/, Status = true},
                new ShareEntity() { Id = 3, CreatedAt = DateTime.Now, CompanyName = "Yandex", Type = shareTypesList[1]/*Middle*/, Status = true}
            };
            var sharesNumbers = new List<SharesNumberEntity>()
            {
                new SharesNumberEntity()
                {
                    Id = 1,
                    Client = clients[0], // Id = 1
                    Share = sharesList[0], // Microsoft
                    Number = 50
                },
                new SharesNumberEntity()
                {
                    Id = 2,
                    Client = clients[1], // Id = 2
                    Share = sharesList[0], // Microsoft
                    Number = 30
                },
                new SharesNumberEntity()
                {
                    Id = 3,
                    Client = clients[1], // Id = 2
                    Share = sharesList[1], // Apple
                    Number = 40
                },
                new SharesNumberEntity()
                {
                    Id = 4,
                    Client = clients[1], // Id = 2
                    Share = sharesList[2], // Yandex
                    Number = 30
                },
                new SharesNumberEntity()
                {
                    Id = 5,
                    Client = clients[0], // Id = 1
                    Share = sharesList[1], // Apple
                    Number = 20
                },
                new SharesNumberEntity()
                {
                    Id = 6,
                    Client = clients[5], // Id = 6
                    Share = sharesList[2], // Yandex
                    Number = 30
                },
                new SharesNumberEntity()
                {
                    Id = 7,
                    Client = clients[4], // Id = 5
                    Share = sharesList[0], // Microsoft
                    Number = 40
                },
                new SharesNumberEntity()
                {
                    Id = 8,
                    Client = clients[3], // Id = 4
                    Share = sharesList[0], // Microsoft
                    Number = 25
                }
            };
            var operations = new List<OperationEntity>()
            {
                new OperationEntity()
                {
                    Id = 1,
                    DebitDate = DateTime.Now,
                    Customer = clients[1],
                    ChargeDate = DateTime.Now,
                    Seller = clients[0],
                    Share = sharesList[0], // 'Service' share
                    ShareTypeName = sharesList[0].Type.Name, // 'Middle'
                    Cost = sharesList[0].Type.Cost, // 'Middle' cost
                    Number = 12,
                    Total = 12 * sharesList[0].Type.Cost
                },
                new OperationEntity()
                {
                    Id = 2,
                    DebitDate = DateTime.Now,
                    Customer = clients[5],
                    ChargeDate = DateTime.Now,
                    Seller = clients[2],
                    Share = sharesList[0], // 'Service' share
                    ShareTypeName = sharesList[0].Type.Name, // 'Middle'
                    Cost = sharesList[0].Type.Cost, // 'Middle' cost
                    Number = 12,
                    Total = 12 * sharesList[0].Type.Cost
                },
                new OperationEntity()
                {
                    Id = 3,
                    DebitDate = DateTime.Now,
                    Customer = clients[1],
                    ChargeDate = DateTime.Now,
                    Seller = clients[0],
                    Share = sharesList[1], // 'Service' share
                    ShareTypeName = sharesList[0].Type.Name, // 'Middle'
                    Cost = sharesList[1].Type.Cost, // 'Middle' cost
                    Number = 12,
                    Total = 12 * sharesList[0].Type.Cost
                },
                new OperationEntity()
                {
                    Id = 4,
                    DebitDate = DateTime.Now,
                    Customer = clients[1],
                    ChargeDate = DateTime.Now,
                    Seller = clients[0],
                    Share = sharesList[0], // 'Service' share
                    ShareTypeName = sharesList[0].Type.Name, // 'Middle'
                    Cost = sharesList[0].Type.Cost, // 'Middle' cost
                    Number = 12,
                    Total = 12 * sharesList[0].Type.Cost
                },
                new OperationEntity()
                {
                    Id = 5,
                    DebitDate = DateTime.Now,
                    Customer = clients[1],
                    ChargeDate = DateTime.Now,
                    Seller = clients[0],
                    Share = sharesList[0], // 'Service' share
                    ShareTypeName = sharesList[0].Type.Name, // 'Middle'
                    Cost = sharesList[0].Type.Cost, // 'Middle' cost
                    Number = 12,
                    Total = 12 * sharesList[0].Type.Cost
                }
            };

            this.operationTableRepository = Substitute.For<IOperationTableRepository>();
            this.operationTableRepository.GetByClient(Arg.Is(2), Arg.Is(4)).Returns(new List<OperationEntity>()
            {
                operations[0], operations[2], operations[3], operations[4]
            });


            this.balanceTableRepository = Substitute.For<IBalanceTableRepository>();
            this.balanceTableRepository.GetNegativeBalances().Returns(new List<BalanceEntity>()
            {
                balances[5], balances[7]
            });
            this.balanceTableRepository.GetZeroBalances().Returns(new List<BalanceEntity>()
            {
                balances[3], balances[4], balances[8]
            });


            this.sharesNumberTableRepository = Substitute.For<ISharesNumberTableRepository>();
            this.sharesNumberTableRepository.GetByClient(Arg.Is(4)).Returns(new List<SharesNumberEntity>()
            {
                sharesNumbers[7]
            });
            this.sharesNumberTableRepository.GetByShare(Arg.Is(4)).Returns(new List<SharesNumberEntity>()
            {
                sharesNumbers[3], sharesNumbers[5]
            });
            
            this.shareTableRepository = Substitute.For<IShareTableRepository>();
            this.shareTableRepository.GetSharesCount().Returns(sharesList.Count);

            this.clientTableRepository = Substitute.For<IClientTableRepository>();
            this.clientTableRepository.GetClientsCount().Returns(clients.Count);
            this.clientTableRepository.Take(Arg.Is(5), Arg.Is(1)).Returns(new List<ClientEntity>()
            {
                clients[0], clients[1], clients[2], clients[3], clients[4]
            });

        }

        [TestMethod]
        public void ShouldGetClientOperations()
        {
            // Arrange            
            int testClientId = 2;
            int number = 4;
            reportsService = new ReportsService(
                operationTableRepository: this.operationTableRepository,
            sharesNumberTableRepository: this.sharesNumberTableRepository,
            balanceTableRepository: this.balanceTableRepository,
            shareTableRepository: this.shareTableRepository,
            clientTableRepository: this.clientTableRepository);
            

            // Act
            List<OperationEntity> operationsOfClient = new List<OperationEntity>();
            operationsOfClient.AddRange(reportsService.GetOperationByClient(testClientId, number));

            // Assert
            this.operationTableRepository.Received(1).GetByClient(testClientId, number);
            for (int i = 0; i < number; i++)
            {
                if (operationsOfClient[i].Customer.Id != testClientId) throw new ArgumentException("Wrong Id in result item");
            }            
        }

        [TestMethod]
        public void ShouldGetSharesNumbersByClient()
        {
            // Arrange
            reportsService = new ReportsService(
                operationTableRepository: this.operationTableRepository,
            sharesNumberTableRepository: this.sharesNumberTableRepository,
            balanceTableRepository: this.balanceTableRepository,
            shareTableRepository: this.shareTableRepository,
            clientTableRepository: this.clientTableRepository);
            int testClientId = 4;

            // Act
            var sharesNumbersOfClient = reportsService.GetSharesNumberByClient(testClientId);

            // Assert
            this.sharesNumberTableRepository.Received(1).GetByClient(testClientId);
            foreach (var sharesNumber in sharesNumbersOfClient)
            {
                if (sharesNumber.Client.Id != testClientId) throw new ArgumentException("Wrong Id in result item");
            }
        }

        [TestMethod]
        public void ShouldGetSharesNumbersByShare()
        {
            // Arrange
            reportsService = new ReportsService(
                operationTableRepository: this.operationTableRepository,
            sharesNumberTableRepository: this.sharesNumberTableRepository,
            balanceTableRepository: this.balanceTableRepository,
            shareTableRepository: this.shareTableRepository,
            clientTableRepository: this.clientTableRepository);
            int testShareId = 3;

            // Act
            var sharesNumbersByShare = reportsService.GetSharesNumberByShare(testShareId);

            // Assert
            var testOperations = this.sharesNumberTableRepository.Received(1).GetByShare(testShareId);
            foreach (var sharesNumber in sharesNumbersByShare)
            {
                if (sharesNumber.Share.Id != testShareId) throw new ArgumentException("Wrong Id in result item");
            }
        }

        [TestMethod]
        public void ShouldGetZeroBalances()
        {
            // Arrange
            reportsService = new ReportsService(
                operationTableRepository: this.operationTableRepository,
            sharesNumberTableRepository: this.sharesNumberTableRepository,
            balanceTableRepository: this.balanceTableRepository,
            shareTableRepository: this.shareTableRepository,
            clientTableRepository: this.clientTableRepository);

            // Act
            var zeroBalances = reportsService.GetZeroBalances();

            // Assert
            var zeroBalancesFromTable = this.balanceTableRepository.Received(1).GetZeroBalances();            
            foreach (var zeroBalance in zeroBalances)
            {
                if(zeroBalance.GetType().Name != "String") throw new ArgumentException("Result item is not a string type");
            }
            //foreach (var zeroBalance in zeroBalancesFromTable)
            //{
            //    if (zeroBalance.Amount != 0) throw new ArgumentException("Amount in result item is not zero");
            //}
        }

        [TestMethod]
        public void ShouldGetNegativeBalances()
        {
            // Arrange
            reportsService = new ReportsService(
                operationTableRepository: this.operationTableRepository,
            sharesNumberTableRepository: this.sharesNumberTableRepository,
            balanceTableRepository: this.balanceTableRepository,
            shareTableRepository: this.shareTableRepository,
            clientTableRepository: this.clientTableRepository);

            // Act
            var negativeBalances = reportsService.GetNegativeBalances();

            // Assert
            this.balanceTableRepository.Received(1).GetNegativeBalances();
            foreach (var negativeBalance in negativeBalances)
            {
                if (negativeBalance.Amount >= 0) throw new ArgumentException("Amount in result item is not negative");
            }            
        }

        [TestMethod]
        public void ShouldGetClientsCount()
        {
            // Arrange
            reportsService = new ReportsService(
                operationTableRepository: this.operationTableRepository,
            sharesNumberTableRepository: this.sharesNumberTableRepository,
            balanceTableRepository: this.balanceTableRepository,
            shareTableRepository: this.shareTableRepository,
            clientTableRepository: this.clientTableRepository);

            // Act
            var count = reportsService.GetClientsCount();

            // Assert
            this.clientTableRepository.Received(1).GetClientsCount();
            if (count != 9) throw new ArgumentException("Result count is not 9");            
        }

        [TestMethod]
        public void ShouldGetSharesCount()
        {
            // Arrange
            reportsService = new ReportsService(
                operationTableRepository: this.operationTableRepository,
            sharesNumberTableRepository: this.sharesNumberTableRepository,
            balanceTableRepository: this.balanceTableRepository,
            shareTableRepository: this.shareTableRepository,
            clientTableRepository: this.clientTableRepository);

            // Act
            var count = reportsService.GetSharesCount();

            // Assert
            this.shareTableRepository.Received(1).GetSharesCount();
            if (count != 3) throw new ArgumentException("Result count is not 9");
        }

        [TestMethod]
        public void ShouldGetFirstClients()
        {
            // Arrange
            reportsService = new ReportsService(
                operationTableRepository: this.operationTableRepository,
            sharesNumberTableRepository: this.sharesNumberTableRepository,
            balanceTableRepository: this.balanceTableRepository,
            shareTableRepository: this.shareTableRepository,
            clientTableRepository: this.clientTableRepository);

            // Act
            List<ClientEntity> clients = new List<ClientEntity>();
            clients.AddRange(reportsService.GetFirstClients(5, 1));

            // Assert
            this.clientTableRepository.Received(1).Take(5, 1);
            for (int i = 0; i < 5; i++)
            {
                if (clients[i].Id != i+1) throw new ArgumentException("Wrong id in list of clients.");
            }
        }
    }    
}
