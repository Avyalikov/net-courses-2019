using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Traiding.Core.Repositories;
using Traiding.Core.Services;
using Traiding.Core.Models;

namespace Traiding.Core.Tests
{
    [TestClass]
    public class BlockedSharesNumbersServiceTests
    {
        [TestMethod]
        public void ShouldCreateNewBlockedSharesNumberItem()
        {
            // Arrange
            var blockedSharesNumberTableRepository = Substitute.For<IBlockedSharesNumberTableRepository>();
            BlockedSharesNumbersService blockedSharesNumbersService = new BlockedSharesNumbersService(blockedSharesNumberTableRepository);
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
            var blockedSharesNumberId = blockedSharesNumbersService.Create(args);

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
            BlockedSharesNumbersService blockedSharesNumbersService = new BlockedSharesNumbersService(blockedSharesNumberTableRepository);
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
            blockedSharesNumbersService.Create(args);

            blockedSharesNumberTableRepository.Contains(Arg.Is<BlockedSharesNumberEntity>( // Now Contains returns true (table contains blocked shares number with this data)
                bn => bn.ClientSharesNumber == args.ClientSharesNumber
                && bn.Operation == args.Operation
                && bn.Seller == args.ClientSharesNumber.Client
                && bn.Share == args.Share
                && bn.ShareTypeName == args.ShareTypeName
                && bn.Cost == args.Cost
                && bn.Number == args.Number)).Returns(true);

            blockedSharesNumbersService.Create(args); // Try to reg. same twice and get exception

            // Assert
        }

        [TestMethod]
        public void ShouldGetBlockedSharesNumberItemInfo()
        {
            // Arrange
            var blockedSharesNumberTableRepository = Substitute.For<IBlockedSharesNumberTableRepository>();
            int testId = 55;
            blockedSharesNumberTableRepository.ContainsById(Arg.Is(testId)).Returns(true);
            BlockedSharesNumbersService blockedSharesNumbersService = new BlockedSharesNumbersService(blockedSharesNumberTableRepository);

            // Act
            var sharesNumberInfo = blockedSharesNumbersService.Get(testId);

            // Assert
            blockedSharesNumberTableRepository.Received(1).Get(testId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "I didn't get exception it's wrong!")]
        public void ShouldThrowExceptionIfCantFindSpecificBlockedMoneyItem()
        {
            // Arrange
            var blockedSharesNumberTableRepository = Substitute.For<IBlockedSharesNumberTableRepository>();
            int testId = 55;
            blockedSharesNumberTableRepository.ContainsById(Arg.Is(testId)).Returns(false); // Now Contains returns false (table don't contains blocked money with this Id)
            BlockedSharesNumbersService blockedSharesNumbersService = new BlockedSharesNumbersService(blockedSharesNumberTableRepository);

            // Act
            blockedSharesNumbersService.ContainsById(testId); // Try to get blocked money and get exception

            // Assert
        }

        [TestMethod]
        public void ShouldRemoveBlockedSharesNumberItem()
        {
            // Arrange
            var blockedSharesNumberTableRepository = Substitute.For<IBlockedSharesNumberTableRepository>();
            int testId = 55;
            blockedSharesNumberTableRepository.ContainsById(Arg.Is(testId)).Returns(true);
            BlockedSharesNumbersService blockedSharesNumbersService = new BlockedSharesNumbersService(blockedSharesNumberTableRepository);

            // Act
            blockedSharesNumbersService.Remove(testId);

            // Assert
            blockedSharesNumberTableRepository.Received(1).Remove(testId);
            blockedSharesNumberTableRepository.Received(1).SaveChanges();
        }
    }
}
