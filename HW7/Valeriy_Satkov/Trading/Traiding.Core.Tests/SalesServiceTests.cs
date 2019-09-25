namespace Traiding.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using Traiding.Core.Dto;
    using Traiding.Core.Models;
    using Traiding.Core.Repositories;
    using Traiding.Core.Services;

    [TestClass]
    public class SalesServiceTests
    {
        IOperationTableRepository operationTableRepository;
        IBalanceTableRepository balanceTableRepository;
        ISharesNumberTableRepository sharesNumberTableRepository;
        IBlockedMoneyTableRepository blockedMoneyTableRepository;        
        IBlockedSharesNumberTableRepository blockedSharesNumberTableRepository;
        IShareTableRepository shareTableRepository;

        [TestInitialize]
        public void Initialize()
        {
            this.operationTableRepository = Substitute.For<IOperationTableRepository>();

            this.balanceTableRepository = Substitute.For<IBalanceTableRepository>();

            this.sharesNumberTableRepository = Substitute.For<ISharesNumberTableRepository>();

            this.blockedMoneyTableRepository = Substitute.For<IBlockedMoneyTableRepository>();

            this.blockedSharesNumberTableRepository = Substitute.For<IBlockedSharesNumberTableRepository>();

            this.shareTableRepository = Substitute.For<IShareTableRepository>();
        }

        /* 'Operation' methods
         */
        [TestMethod]
        public void ShouldCreateEmptyOperation()
        {
            // Arrange            
            SalesService salesService = new SalesService(
                this.operationTableRepository, 
                this.balanceTableRepository,
                this.blockedMoneyTableRepository,
                this.sharesNumberTableRepository,
                this.blockedSharesNumberTableRepository,
                this.shareTableRepository);

            // Act
            var operation = salesService.CreateOperation();

            // Assert
            this.operationTableRepository.Received(1).Add(Arg.Is<OperationEntity>(
                o => o.Id == operation.Id 
                && o.Customer == null
                && o.Seller == null
                && o.Share == null
                && o.ShareTypeName == null
                && o.Cost == 1 
                && o.Number == 1 
                && o.Total == 1));
            this.operationTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        public void ShouldFillOperationColumns()
        {
            // Arrange
            SalesService salesService = new SalesService(
                this.operationTableRepository,
                this.balanceTableRepository,
                this.blockedMoneyTableRepository,
                this.sharesNumberTableRepository,
                this.blockedSharesNumberTableRepository,
                this.shareTableRepository);
            var testBlockedMoney = new BlockedMoneyEntity()
            {
                Id = 2,
                CreatedAt = DateTime.Now,
                ClientBalance = new BalanceEntity() { Id = 2 },
                Operation = new OperationEntity() { Id = 1 },
                Customer = new ClientEntity() { Id = 2 },
                Total = 1000.00M
            };
            var testBlockedSharesNumber = new BlockedSharesNumberEntity()
            {
                Id = 2,
                CreatedAt = DateTime.Now,
                ClientSharesNumber = new SharesNumberEntity() { Id = 4},
                Operation = new OperationEntity() { Id = 1 },
                Seller = new ClientEntity() { Id = 1 },
                Share = new ShareEntity() { Id = 3 },
                ShareTypeName = "sharename",
                Cost = 500,
                Number = 3,
            };

            // Act
            salesService.FillOperationColumns(testBlockedMoney, testBlockedSharesNumber);

            // Assert
            operationTableRepository.Received(1).FillAllColumns(testBlockedMoney, testBlockedSharesNumber, Arg.Any<DateTime>());
            operationTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        public void ShouldRemoveOperation()
        {
            // Arrange
            SalesService salesService = new SalesService(
                this.operationTableRepository,
                this.balanceTableRepository,
                this.blockedMoneyTableRepository,
                this.sharesNumberTableRepository,
                this.blockedSharesNumberTableRepository,
                this.shareTableRepository);
            var testOperation = new OperationEntity() { Id = 500 };

            // Act
            salesService.RemoveOperation(testOperation);

            // Assert
            this.operationTableRepository.Received(1).Remove(testOperation.Id);
            operationTableRepository.Received(1).SaveChanges();
        }

        /* 'Balance' methods
         */
        [TestMethod]
        public void ShouldChangeBalanceAmount()
        {
            // Arrange
            SalesService salesService = new SalesService(
                this.operationTableRepository,
                this.balanceTableRepository,
                this.blockedMoneyTableRepository,
                this.sharesNumberTableRepository,
                this.blockedSharesNumberTableRepository,
                this.shareTableRepository);
            var testBalance = new BalanceEntity()
            {
                Id = 10,
                Amount = 30000.00M,
                Client = new ClientEntity() { Id = 10},
                Status = true
            };
            decimal newAmount = 35000.00M;

            // Act
            bool flag = salesService.ChangeBalance(testBalance, newAmount);

            // Assert
            this.balanceTableRepository.Received(1).ChangeAmount(testBalance.Id, newAmount);
            this.balanceTableRepository.Received(1).SaveChanges();
            if (!flag) throw new ArgumentException("The flag is false");
        }

        [TestMethod]
        public void ShouldSearchBalanceByClientId()
        {
            // Arrange
            int testClientId = 60;

            this.balanceTableRepository.SearchBalanceByClientId(Arg.Is(testClientId)).Returns(new BalanceEntity()
            {
                Id = 61,
                Client = new ClientEntity() { Id = testClientId },
                Amount = 10000.00M,
                Status = true
            });
            SalesService salesService = new SalesService(
                this.operationTableRepository,
                this.balanceTableRepository,
                this.blockedMoneyTableRepository,
                this.sharesNumberTableRepository,
                this.blockedSharesNumberTableRepository,
                this.shareTableRepository);            

            // Act
            var balance = salesService.SearchBalanceByClientId(testClientId);

            // Assert
            this.balanceTableRepository.Received(1).SearchBalanceByClientId(testClientId);
            if (balance.Client.Id != testClientId) throw new ArgumentException("Client Id in balance is wrong!");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "I didn't get exception it's wrong!")]
        public void ShouldThrowExceptionIfCantFindBalanceByClientId()
        {
            // Arrange
            int testClientId = 65;
            BalanceEntity testNullBalance = null;
            this.balanceTableRepository.SearchBalanceByClientId(Arg.Is(testClientId)).Returns(testNullBalance);
            SalesService salesService = new SalesService(
                this.operationTableRepository,
                this.balanceTableRepository,
                this.blockedMoneyTableRepository,
                this.sharesNumberTableRepository,
                this.blockedSharesNumberTableRepository,
                this.shareTableRepository);

            // Act
            var balance = salesService.SearchBalanceByClientId(testClientId);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "I didn't get exception it's wrong!")]
        public void ShouldThrowExceptionIfGetBlockedBalance()
        {
            // Arrange
            int testClientId = 65;
            this.balanceTableRepository.SearchBalanceByClientId(Arg.Is(testClientId)).Returns(new BalanceEntity()
            {
                Id = 65,
                Client = new ClientEntity() { Id = testClientId },
                Amount = 10000.00M,
                Status = false
            });
            SalesService salesService = new SalesService(
                this.operationTableRepository,
                this.balanceTableRepository,
                this.blockedMoneyTableRepository,
                this.sharesNumberTableRepository,
                this.blockedSharesNumberTableRepository,
                this.shareTableRepository);

            // Act
            var balance = salesService.SearchBalanceByClientId(testClientId);

            // Assert
        }

        /* 'Blocked money' methods
         */
        [TestMethod]
        public void ShouldCreateNewBlockedMoneyItem()
        {
            // Arrange
            SalesService salesService = new SalesService(
                this.operationTableRepository,
                this.balanceTableRepository,
                this.blockedMoneyTableRepository,
                this.sharesNumberTableRepository,
                this.blockedSharesNumberTableRepository,
                this.shareTableRepository);
            BlockedMoneyRegistrationInfo args = new BlockedMoneyRegistrationInfo();
            args.ClientBalance = new BalanceEntity()
            {
                Id = 45,
                Client = new ClientEntity()
                {
                    Id = 5,
                    CreatedAt = DateTime.Now,
                    FirstName = "John",
                    LastName = "Snickers",
                    PhoneNumber = "+7956244636652",
                    Status = true
                },
                Amount = 20000.00M,
                Status = true
            };
            args.Operation = new OperationEntity()
            {
                Id = 2
            };
            args.Total = 10000.00M;

            // Act
            var blockedMoneyId = salesService.CreateBlockedMoney(args);

            // Assert
            blockedMoneyTableRepository.Received(1).Add(Arg.Is<BlockedMoneyEntity>(
                bm => bm.ClientBalance == args.ClientBalance
                && bm.Operation == args.Operation
                && bm.Customer == args.ClientBalance.Client
                && bm.Total == args.Total));
            blockedMoneyTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        public void ShouldRemoveBlockedMoneyItem()
        {
            // Arrange
            SalesService salesService = new SalesService(
                this.operationTableRepository,
                this.balanceTableRepository,
                this.blockedMoneyTableRepository,
                this.sharesNumberTableRepository,
                this.blockedSharesNumberTableRepository,
                this.shareTableRepository);
            BlockedMoneyEntity blockedMoney = new BlockedMoneyEntity() { Id = 4 };

            // Act
            salesService.RemoveBlockedMoney(blockedMoney);

            // Assert
            blockedMoneyTableRepository.Received(1).Remove(blockedMoney.Id);
            blockedMoneyTableRepository.Received(1).SaveChanges();
        }

        /* 'Shares number' methods
         */
        [TestMethod]
        public void ShouldCreateNewSharesNumber()
        {
            // Arrange
            SalesService salesService = new SalesService(
                this.operationTableRepository,
                this.balanceTableRepository,
                this.blockedMoneyTableRepository,
                this.sharesNumberTableRepository,
                this.blockedSharesNumberTableRepository,
                this.shareTableRepository);
            SharesNumberRegistrationInfo args = new SharesNumberRegistrationInfo();
            args.Client = new ClientEntity()
            {
                Id = 5,
                CreatedAt = DateTime.Now,
                FirstName = "John",
                LastName = "Snickers",
                PhoneNumber = "+7956244636652",
                Status = true
            };
            args.Share = new ShareEntity()
            {
                Id = 2,
                CreatedAt = DateTime.Now,
                CompanyName = "Simple Company",
                Type = new ShareTypeEntity()
                {
                    Id = 4,
                    Name = "not so cheap",
                    Cost = 1200.00M,
                    Status = true
                },
                Status = true
            };
            args.Number = 20;

            // Act
            var shareId = salesService.CreateSharesNumber(args);

            // Assert
            sharesNumberTableRepository.Received(1).Add(Arg.Is<SharesNumberEntity>(
                n => n.Client == args.Client
                && n.Share == args.Share
                && n.Number == args.Number));
            sharesNumberTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        public void ShouldSearchSharesNumberForBuy()
        {
            // Arrange            
            int shareId = 55;
            int reqNumber = 5;
            sharesNumberTableRepository.SearchSharesNumberForBuy(Arg.Is(shareId), Arg.Is(reqNumber)).Returns(new SharesNumberEntity()
            {
                Id = 3,
                Share = new ShareEntity() { Id = shareId },
                Number = reqNumber + 1
            });
            SalesService salesService = new SalesService(
                this.operationTableRepository,
                this.balanceTableRepository,
                this.blockedMoneyTableRepository,
                this.sharesNumberTableRepository,
                this.blockedSharesNumberTableRepository,
                this.shareTableRepository);

            // Act
            var sharesNumberResult = salesService.SearchSharesNumberForBuy(shareId, reqNumber);

            // Assert
            this.sharesNumberTableRepository.Received(1).SearchSharesNumberForBuy(shareId, reqNumber);
            if (sharesNumberResult.Share.Id != shareId 
                || sharesNumberResult.Number < reqNumber) throw new ArgumentException("ShareId or Number in founded shares number is wrong");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "I didn't get exception it's wrong!")]
        public void ShouldThrowExceptionIfCantFindSharesNumberForBuy()
        {
            // Arrange
            int shareId = 55;
            int reqNumber = 5;
            SharesNumberEntity nullSharesNumber = null;
            sharesNumberTableRepository.SearchSharesNumberForBuy(Arg.Is(shareId), Arg.Is(reqNumber)).Returns(nullSharesNumber);
            SalesService salesService = new SalesService(
                this.operationTableRepository,
                this.balanceTableRepository,
                this.blockedMoneyTableRepository,
                this.sharesNumberTableRepository,
                this.blockedSharesNumberTableRepository,
                this.shareTableRepository);

            // Act
            var sharesNumberResult = salesService.SearchSharesNumberForBuy(shareId, reqNumber);

            // Assert
        }

        [TestMethod]
        public void ShouldSearchSharesNumberForAddition()
        {
            // Arrange            
            ShareEntity share = new ShareEntity() { Id = 55 };
            ClientEntity client = new ClientEntity() { Id = 3 };
            sharesNumberTableRepository.SearchSharesNumberForAddition(Arg.Is(client.Id), Arg.Is(share.Id)).Returns(new SharesNumberEntity()
            {
                Id = 3,
                Client = new ClientEntity() { Id = client.Id },
                Share = new ShareEntity() { Id = share.Id }
            });
            SalesService salesService = new SalesService(
                this.operationTableRepository,
                this.balanceTableRepository,
                this.blockedMoneyTableRepository,
                this.sharesNumberTableRepository,
                this.blockedSharesNumberTableRepository,
                this.shareTableRepository);

            // Act
            var sharesNumberResult = salesService.SearchOrCreateSharesNumberForAddition(client, share);

            // Assert
            this.sharesNumberTableRepository.Received(1).SearchSharesNumberForAddition(client.Id, share.Id);
            if (sharesNumberResult.Client.Id != client.Id
                || sharesNumberResult.Share.Id != share.Id) throw new ArgumentException("Client ID or Share ID in founded shares number is wrong");
        }

        [TestMethod]
        public void ShouldCreateSharesNumberForAdditionIfCantSearch()
        {
            // Arrange            
            ShareEntity share = new ShareEntity() { Id = 55 };
            ClientEntity client = new ClientEntity() { Id = 3 };
            SharesNumberEntity nullSharesNumber = null;
            sharesNumberTableRepository.SearchSharesNumberForAddition(Arg.Is(client.Id), Arg.Is(share.Id)).Returns(nullSharesNumber);
            SalesService salesService = new SalesService(
                this.operationTableRepository,
                this.balanceTableRepository,
                this.blockedMoneyTableRepository,
                this.sharesNumberTableRepository,
                this.blockedSharesNumberTableRepository,
                this.shareTableRepository);

            // Act
            var sharesNumberResult = salesService.SearchOrCreateSharesNumberForAddition(client, share);

            // Assert
            this.sharesNumberTableRepository.Received(1).SearchSharesNumberForAddition(client.Id, share.Id);
            this.sharesNumberTableRepository.Received(1).Add(Arg.Any<SharesNumberEntity>());
            this.sharesNumberTableRepository.Received(1).SaveChanges();
        }        

        [TestMethod]
        public void ShouldChangeSharesNumber()
        {
            // Arrange
            SalesService salesService = new SalesService(
                this.operationTableRepository,
                this.balanceTableRepository,
                this.blockedMoneyTableRepository,
                this.sharesNumberTableRepository,
                this.blockedSharesNumberTableRepository,
                this.shareTableRepository);
            var sharesNumber = new SharesNumberEntity()
            {
                Id = 4,
                Number = 34
            };
            int newNumber = 45;

            // Act
            bool flag = salesService.ChangeSharesNumber(sharesNumber, newNumber);

            // Assert
            this.sharesNumberTableRepository.Received(1).ChangeNumber(sharesNumber.Id, newNumber);
            this.sharesNumberTableRepository.Received(1).SaveChanges();
            if (!flag) throw new ArgumentException("The flag is false");
        }

        [TestMethod]
        public void ShouldRemoveShareNumber()
        {
            // Arrange
            SalesService salesService = new SalesService(
                this.operationTableRepository,
                this.balanceTableRepository,
                this.blockedMoneyTableRepository,
                this.sharesNumberTableRepository,
                this.blockedSharesNumberTableRepository,
                this.shareTableRepository);
            SharesNumberEntity sharesNumber = new SharesNumberEntity() { Id = 68 };

            // Act
            salesService.RemoveSharesNumber(sharesNumber);

            // Assert
            this.sharesNumberTableRepository.Received(1).Remove(sharesNumber.Id);
            this.sharesNumberTableRepository.Received(1).SaveChanges();
        }

        /* 'Blocked shares number' methods
         */
        [TestMethod]
        public void ShouldCreateNewBlockedSharesNumberItem()
        {
            // Arrange
            SalesService salesService = new SalesService(
                this.operationTableRepository,
                this.balanceTableRepository,
                this.blockedMoneyTableRepository,
                this.sharesNumberTableRepository,
                this.blockedSharesNumberTableRepository,
                this.shareTableRepository);
            BlockedSharesNumberRegistrationInfo args = new BlockedSharesNumberRegistrationInfo();

            var tempShare = new ShareEntity()
            {
                Id = 2,
                CreatedAt = DateTime.Now,
                CompanyName = "Simple Company",
                Type = new ShareTypeEntity()
                {
                    Id = 4,
                    Name = "not so cheap",
                    Cost = 1200.00M,
                    Status = true
                },
                Status = true
            };

            args.ClientSharesNumber = new SharesNumberEntity()
            {
                Id = 30,
                Client = new ClientEntity()
                {
                    Id = 5,
                    CreatedAt = DateTime.Now,
                    FirstName = "John",
                    LastName = "Snickers",
                    PhoneNumber = "+7956244636652",
                    Status = true
                },
                Share = tempShare,
                Number = 7
            };
            args.Operation = new OperationEntity()
            {
                Id = 2
            };
            args.Number = 5;

            // Act
            var blockedSharesNumberId = salesService.CreateBlockedSharesNumber(args);

            // Assert
            blockedSharesNumberTableRepository.Received(1).Add(Arg.Is<BlockedSharesNumberEntity>(
                bn => bn.ClientSharesNumber == args.ClientSharesNumber
                && bn.Operation == args.Operation
                && bn.Seller == args.ClientSharesNumber.Client
                && bn.Share == tempShare
                && bn.ShareTypeName == tempShare.Type.Name
                && bn.Cost == tempShare.Type.Cost
                && bn.Number == args.Number));
            blockedSharesNumberTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        public void ShouldRemoveBlockedSharesNumberItem()
        {
            // Arrange                        
            SalesService salesService = new SalesService(
                this.operationTableRepository,
                this.balanceTableRepository,
                this.blockedMoneyTableRepository,
                this.sharesNumberTableRepository,
                this.blockedSharesNumberTableRepository,
                this.shareTableRepository);
            var blockedSharesNumber = new BlockedSharesNumberEntity() { Id = 43 };

            // Act
            salesService.RemoveBlockedSharesNumber(blockedSharesNumber);

            // Assert
            blockedSharesNumberTableRepository.Received(1).Remove(blockedSharesNumber.Id);
            blockedSharesNumberTableRepository.Received(1).SaveChanges();
        }

        /* 'Share' methods
         */
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "I didn't get exception it's wrong!")]
        public void ShouldThrowExceptionIfShareStatusIsFalse()
        {
            SalesService salesService = new SalesService(
                this.operationTableRepository,
                this.balanceTableRepository,
                this.blockedMoneyTableRepository,
                this.sharesNumberTableRepository,
                this.blockedSharesNumberTableRepository,
                this.shareTableRepository);
            var share = new ShareEntity()
            {
                Id = 3,
                CompanyName = "testCompany",
                CreatedAt = DateTime.Now,
                Type = new ShareTypeEntity()
                {
                    Id = 5,
                    Cost = 1000.0M,
                    Name = "typename",
                    Status = true
                },
                Status = false                
            };

            // Act
            salesService.CheckShareAndShareTypeStatuses(share);

            // Assert            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "I didn't get exception it's wrong!")]
        public void ShouldThrowExceptionIfShareTypeStatusIsFalse()
        {
            SalesService salesService = new SalesService(
                this.operationTableRepository,
                this.balanceTableRepository,
                this.blockedMoneyTableRepository,
                this.sharesNumberTableRepository,
                this.blockedSharesNumberTableRepository,
                this.shareTableRepository);
            var share = new ShareEntity()
            {
                Id = 3,
                CompanyName = "testCompany",
                CreatedAt = DateTime.Now,
                Type = new ShareTypeEntity()
                {
                    Id = 5,
                    Cost = 1000.0M,
                    Name = "typename",
                    Status = false
                },
                Status = true
            };

            // Act
            salesService.CheckShareAndShareTypeStatuses(share);

            // Assert            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "I didn't get exception it's wrong!")]
        public void ShouldThrowExceptionIfCantFindShare()
        {
            // Arrange
            
            int testId = 54;
            this.shareTableRepository.ContainsById(Arg.Is(testId)).Returns(false); // Now Contains returns false (table don't contains share type with this Id)
            SalesService salesService = new SalesService(
                this.operationTableRepository,
                this.balanceTableRepository,
                this.blockedMoneyTableRepository,
                this.sharesNumberTableRepository,
                this.blockedSharesNumberTableRepository,
                this.shareTableRepository);

            // Act
            salesService.GetShare(testId); // Try to get share type and get exception

            // Assert
        }

        [TestMethod]
        public void ShouldGetShareInfo()
        {
            // Arrange
            int testId = 55;
            this.shareTableRepository.ContainsById(Arg.Is(testId)).Returns(true);
            SalesService salesService = new SalesService(
                this.operationTableRepository,
                this.balanceTableRepository,
                this.blockedMoneyTableRepository,
                this.sharesNumberTableRepository,
                this.blockedSharesNumberTableRepository,
                this.shareTableRepository);

            // Act
            var shareInfo = salesService.GetShare(testId);

            // Assert
            this.shareTableRepository.Received(1).Get(testId);
        }
    }
}
