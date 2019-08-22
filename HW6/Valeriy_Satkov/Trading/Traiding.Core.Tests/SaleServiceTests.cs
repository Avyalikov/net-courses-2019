namespace Traiding.Core.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using Traiding.Core.Dto;
    using Traiding.Core.Models;
    using Traiding.Core.Repositories;
    using Traiding.Core.Services;

    [TestClass]
    public class SaleServiceTests
    {
        /* Sale
         * 0.  Get info about purchase from program (Customer, Seller, Number of Shares, Total (money))
         * 1.  Create empty operation
         * 2.1 Get Customer balance info
         * 2.2 - Customer balance amount
         * 3.  Create blocked money
         * 4.1 Get Seller shares number info
         * 4.2 - Seller shares number
         * 5.  Create blocked shares number // after that action purchase can't cancel
         * 6.1 Get Seller balance info
         * 6.2 + Seller balance amount
         * 7.1 Get Customer shares number info
         * 7.2 + Customer shares number
         * 8.  Fill operation columns
         * 9.  Remove blocked money
         * 10. Remove blocked shares number
         */

        /* 'Operation' methods
         */
        [TestMethod]
        public void ShouldCreateEmptyOperation()
        {
            // Arrange
            var operationTableRepository = Substitute.For<IOperationTableRepository>();
            SalesService salesService = new SalesService(operationTableRepository);

            // Act
            var operationId = salesService.CreateOperation();

            // Assert
            operationTableRepository.Received(1).Add(Arg.Is<OperationEntity>(
                bm => bm.Id == operationId));
            operationTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        public void ShouldGetOperationItemInfo()
        {
            // Arrange
            var operationTableRepository = Substitute.For<IOperationTableRepository>();
            int testId = 55;
            operationTableRepository.ContainsById(Arg.Is(testId)).Returns(true);
            SalesService salesService = new SalesService(operationTableRepository);

            // Act
            var itemInfo = salesService.GetOperation(testId);

            // Assert
            operationTableRepository.Received(1).Get(testId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "I didn't get exception it's wrong!")]
        public void ShouldThrowExceptionIfCantFindOperationItem()
        {
            // Arrange
            var operationTableRepository = Substitute.For<IOperationTableRepository>();
            int testId = 55;
            operationTableRepository.ContainsById(Arg.Is(testId)).Returns(false); // Now Contains returns false (table don't contains operation with this Id)
            SalesService salesService = new SalesService(operationTableRepository);

            // Act
            salesService.ContainsOperationById(testId); // Try to get operation and get exception

            // Assert
        }

        [TestMethod]
        public void ShouldFillOperationColumns()
        {
            // Arrange
            var operationTableRepository = Substitute.For<IOperationTableRepository>();
            int testOperationId = 9;
            operationTableRepository.ContainsById(Arg.Is(testOperationId)).Returns(true);
            SalesService salesService = new SalesService(operationTableRepository);
            int testBlockedMoneyId = 2;
            int testBlockedSharesNumberId = 7;

            // Act
            salesService.FillOperationColumns(testOperationId, testBlockedMoneyId, testBlockedSharesNumberId);

            // Assert
            operationTableRepository.Received(1).FillCustomerColumns(testOperationId, testBlockedMoneyId);
            operationTableRepository.Received(1).FillSellerColumns(testOperationId, testBlockedSharesNumberId);
            operationTableRepository.Received(1).SetChargeDate(testOperationId, Arg.Any<DateTime>());
            operationTableRepository.Received(1).SaveChanges();
        }        

        /* 'Balance' methods
         */
        [TestMethod]
        public void ShouldGetBalanceInfo()
        {
            // Arrange
            var balanceTableRepository = Substitute.For<IBalanceTableRepository>();
            int testId = 55;
            balanceTableRepository.ContainsById(Arg.Is(testId)).Returns(true);
            SalesService salesService = new SalesService(balanceTableRepository);

            // Act
            var itemInfo = salesService.GetBalance(testId);

            // Assert
            balanceTableRepository.Received(1).Get(testId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "I didn't get exception it's wrong!")]
        public void ShouldThrowExceptionIfCantFindBalanceItem()
        {
            // Arrange
            var balanceTableRepository = Substitute.For<IBalanceTableRepository>();
            int testId = 55;
            balanceTableRepository.ContainsById(Arg.Is(testId)).Returns(false); // Now Contains returns false (table don't contains share type with this Id)
            SalesService salesService = new SalesService(balanceTableRepository);

            // Act
            salesService.ContainsBalanceById(testId); // Try to get item info and get exception

            // Assert
        }

        [TestMethod]
        public void ShouldChangeBalanceAmount()
        {
            // Arrange
            var balanceTableRepository = Substitute.For<IBalanceTableRepository>();
            int testId = 55;
            balanceTableRepository.ContainsById(Arg.Is(testId)).Returns(true);
            SalesService salesService = new SalesService(balanceTableRepository);            
            decimal newAmount = 5000.00M;

            // Act
            salesService.ChangeBalance(testId, newAmount);

            // Assert
            balanceTableRepository.Received(1).ChangeAmount(testId, newAmount);
            balanceTableRepository.Received(1).SaveChanges();
        }

        /* 'Blocked money' methods
         */
        [TestMethod]
        public void ShouldCreateNewBlockedMoneyItem()
        {
            // Arrange
            var blockedMoneyTableRepository = Substitute.For<IBlockedMoneyTableRepository>();
            SalesService salesService = new SalesService(blockedMoneyTableRepository);
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
        [ExpectedException(typeof(ArgumentException), "I didn't get exception it's wrong!")]
        public void ShouldNotRegisterNewBlockedMoneyItemIfItExists()
        {
            // Arrange
            var blockedMoneyTableRepository = Substitute.For<IBlockedMoneyTableRepository>();
            SalesService salesService = new SalesService(blockedMoneyTableRepository);
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
            salesService.CreateBlockedMoney(args);

            blockedMoneyTableRepository.Contains(Arg.Is<BlockedMoneyEntity>( // Now Contains returns true (table contains blocked money with this data)
                bm => bm.ClientBalance == args.ClientBalance
                && bm.Operation == args.Operation
                && bm.Customer == args.ClientBalance.Client
                && bm.Total == args.Total)).Returns(true);

            salesService.CreateBlockedMoney(args); // Try to reg. same twice and get exception

            // Assert
        }

        [TestMethod]
        public void ShouldGetBlockedMoneyItemInfo()
        {
            // Arrange
            var blockedMoneyTableRepository = Substitute.For<IBlockedMoneyTableRepository>();
            int testId = 55;
            blockedMoneyTableRepository.ContainsById(Arg.Is(testId)).Returns(true);
            SalesService salesService = new SalesService(blockedMoneyTableRepository);

            // Act
            var itemInfo = salesService.GetBlockedMoney(testId);

            // Assert
            blockedMoneyTableRepository.Received(1).Get(testId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "I didn't get exception it's wrong!")]
        public void ShouldThrowExceptionIfCantFindSpecificBlockedMoneyItem()
        {
            // Arrange
            var blockedMoneyTableRepository = Substitute.For<IBlockedMoneyTableRepository>();
            int testId = 55;
            blockedMoneyTableRepository.ContainsById(Arg.Is(testId)).Returns(false); // Now Contains returns false (table don't contains blocked money with this Id)
            SalesService salesService = new SalesService(blockedMoneyTableRepository);

            // Act
            salesService.ContainsBlockedMoneyById(testId); // Try to get blocked money and get exception

            // Assert
        }

        [TestMethod]
        public void ShouldRemoveBlockedMoneyItem()
        {
            // Arrange
            var blockedMoneyTableRepository = Substitute.For<IBlockedMoneyTableRepository>();
            int testId = 55;
            blockedMoneyTableRepository.ContainsById(Arg.Is(testId)).Returns(true);
            SalesService salesService = new SalesService(blockedMoneyTableRepository);

            // Act
            salesService.RemoveBlockedMoney(testId);

            // Assert
            blockedMoneyTableRepository.Received(1).Remove(testId);
            blockedMoneyTableRepository.Received(1).SaveChanges();
        }

        /* 'Shares number' methods
         */
        [TestMethod]
        public void ShouldGetSharesNumberInfo()
        {
            // Arrange
            var sharesNumberTableRepository = Substitute.For<ISharesNumberTableRepository>();
            int testId = 55;
            sharesNumberTableRepository.ContainsById(Arg.Is(testId)).Returns(true);
            SalesService salesService = new SalesService(sharesNumberTableRepository);

            // Act
            var itemInfo = salesService.GetSharesNumber(testId);

            // Assert
            sharesNumberTableRepository.Received(1).Get(testId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "I didn't get exception it's wrong!")]
        public void ShouldThrowExceptionIfCantFindSharesNumber()
        {
            // Arrange
            var sharesNumberTableRepository = Substitute.For<ISharesNumberTableRepository>();
            int testId = 55;
            sharesNumberTableRepository.ContainsById(Arg.Is(testId)).Returns(false); // Now Contains returns false (table don't contains share number with this Id)
            SalesService salesService = new SalesService(sharesNumberTableRepository);

            // Act
            salesService.ContainsSharesNumberById(testId); // Try to get shares number and get exception

            // Assert
        }

        [TestMethod]
        public void ShouldChangeSharesNumber()
        {
            // Arrange
            var sharesNumberTableRepository = Substitute.For<ISharesNumberTableRepository>();
            int testId = 55;
            sharesNumberTableRepository.ContainsById(Arg.Is(testId)).Returns(true);
            SalesService salesService = new SalesService(sharesNumberTableRepository);
            int newNumber = 21;

            // Act
            salesService.ChangeSharesNumber(testId, newNumber);

            // Assert
            sharesNumberTableRepository.Received(1).ChangeNumber(testId, newNumber);
            sharesNumberTableRepository.Received(1).SaveChanges();
        }

        /* 'Blocked shares number' methods
         */
        [TestMethod]
        public void ShouldCreateNewBlockedSharesNumberItem()
        {
            // Arrange
            var blockedSharesNumberTableRepository = Substitute.For<IBlockedSharesNumberTableRepository>();
            SalesService salesService = new SalesService(blockedSharesNumberTableRepository);
            BlockedSharesNumberRegistrationInfo args = new BlockedSharesNumberRegistrationInfo();

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
                Share = new ShareEntity()
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
                },
                Number = 7
            };
            args.Operation = new OperationEntity()
            {
                Id = 2
            };
            args.Share = args.ClientSharesNumber.Share;
            args.ShareTypeName = args.ClientSharesNumber.Share.Type.Name;
            args.Cost = args.ClientSharesNumber.Share.Type.Cost;
            args.Number = 5;

            // Act
            var blockedSharesNumberId = salesService.CreateBlockedSharesNumber(args);

            // Assert
            blockedSharesNumberTableRepository.Received(1).Add(Arg.Is<BlockedSharesNumberEntity>(
                bn => bn.ClientSharesNumber == args.ClientSharesNumber
                && bn.Operation == args.Operation
                && bn.Seller == args.ClientSharesNumber.Client
                && bn.Share == args.Share
                && bn.ShareTypeName == args.ShareTypeName
                && bn.Cost == args.Cost
                && bn.Number == args.Number));
            blockedSharesNumberTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "I didn't get exception it's wrong!")]
        public void ShouldNotRegisterNewBlockedSharesNumberItemIfItExists()
        {
            // Arrange
            var blockedSharesNumberTableRepository = Substitute.For<IBlockedSharesNumberTableRepository>();
            SalesService salesService = new SalesService(blockedSharesNumberTableRepository);
            BlockedSharesNumberRegistrationInfo args = new BlockedSharesNumberRegistrationInfo();

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
                Share = new ShareEntity()
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
                },
                Number = 7
            };
            args.Operation = new OperationEntity()
            {
                Id = 2
            };
            args.Share = args.ClientSharesNumber.Share;
            args.ShareTypeName = args.ClientSharesNumber.Share.Type.Name;
            args.Cost = args.ClientSharesNumber.Share.Type.Cost;
            args.Number = 5;

            // Act
            salesService.CreateBlockedSharesNumber(args);

            blockedSharesNumberTableRepository.Contains(Arg.Is<BlockedSharesNumberEntity>( // Now Contains returns true (table contains blocked shares number with this data)
                bn => bn.ClientSharesNumber == args.ClientSharesNumber
                && bn.Operation == args.Operation
                && bn.Seller == args.ClientSharesNumber.Client
                && bn.Share == args.Share
                && bn.ShareTypeName == args.ShareTypeName
                && bn.Cost == args.Cost
                && bn.Number == args.Number)).Returns(true);

            salesService.CreateBlockedSharesNumber(args); // Try to reg. same twice and get exception

            // Assert
        }

        [TestMethod]
        public void ShouldGetBlockedSharesNumberItemInfo()
        {
            // Arrange
            var blockedSharesNumberTableRepository = Substitute.For<IBlockedSharesNumberTableRepository>();
            int testId = 55;
            blockedSharesNumberTableRepository.ContainsById(Arg.Is(testId)).Returns(true);
            SalesService salesService = new SalesService(blockedSharesNumberTableRepository);

            // Act
            var itemInfo = salesService.GetBlockedSharesNumber(testId);

            // Assert
            blockedSharesNumberTableRepository.Received(1).Get(testId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "I didn't get exception it's wrong!")]
        public void ShouldThrowExceptionIfCantFindSpecificBlockedSharesNumberItem()
        {
            // Arrange
            var blockedSharesNumberTableRepository = Substitute.For<IBlockedSharesNumberTableRepository>();
            int testId = 55;
            blockedSharesNumberTableRepository.ContainsById(Arg.Is(testId)).Returns(false); // Now Contains returns false (table don't contains blocked money with this Id)
            SalesService salesService = new SalesService(blockedSharesNumberTableRepository);

            // Act
            salesService.ContainsBlockedSharesNumberById(testId); // Try to get blocked money and get exception

            // Assert
        }

        [TestMethod]
        public void ShouldRemoveBlockedSharesNumberItem()
        {
            // Arrange
            var blockedSharesNumberTableRepository = Substitute.For<IBlockedSharesNumberTableRepository>();
            int testId = 55;
            blockedSharesNumberTableRepository.ContainsById(Arg.Is(testId)).Returns(true);
            SalesService salesService = new SalesService(blockedSharesNumberTableRepository);

            // Act
            salesService.RemoveBlockedSharesNumber(testId);

            // Assert
            blockedSharesNumberTableRepository.Received(1).Remove(testId);
            blockedSharesNumberTableRepository.Received(1).SaveChanges();
        }        
    }    
}
