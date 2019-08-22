using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Traiding.Core.Dto;
using Traiding.Core.Models;
using Traiding.Core.Repositories;
using Traiding.Core.Services;

namespace Traiding.Core.Tests
{
    [TestClass]
    public class BlockedMoneysServiceTests
    {
        /* Implemented in Sale service tests
         */
        //[TestMethod]
        //public void ShouldCreateNewBlockedMoneyItem()
        //{
        //    // Arrange
        //    var blockedMoneyTableRepository = Substitute.For<IBlockedMoneyTableRepository>();
        //    BlockedMoneysService blockedMoneysService = new BlockedMoneysService(blockedMoneyTableRepository);
        //    BlockedMoneyRegistrationInfo args = new BlockedMoneyRegistrationInfo();
        //    args.ClientBalance = new BalanceEntity()
        //    {
        //        Id = 45,
        //        Client = new ClientEntity()
        //        {
        //            Id = 5,
        //            CreatedAt = DateTime.Now,
        //            FirstName = "John",
        //            LastName = "Snickers",
        //            PhoneNumber = "+7956244636652",
        //            Status = true
        //        },
        //        Amount = 20000.00M,
        //        Status = true
        //    };
        //    args.Operation = new OperationEntity()
        //    {
        //        Id = 2
        //    };
        //    args.Total = 10000.00M;

        //    // Act
        //    var blockedMoneyId = blockedMoneysService.Create(args);

        //    // Assert
        //    blockedMoneyTableRepository.Received(1).Add(Arg.Is<BlockedMoneyEntity>(
        //        bm => bm.ClientBalance == args.ClientBalance
        //        && bm.Operation == args.Operation
        //        && bm.Customer == args.ClientBalance.Client
        //        && bm.Total == args.Total));
        //    blockedMoneyTableRepository.Received(1).SaveChanges();
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException), "I didn't get exception it's wrong!")]
        //public void ShouldNotRegisterNewBlockedMoneyItemIfItExists()
        //{
        //    // Arrange
        //    var blockedMoneyTableRepository = Substitute.For<IBlockedMoneyTableRepository>();
        //    BlockedMoneysService blockedMoneysService = new BlockedMoneysService(blockedMoneyTableRepository);
        //    BlockedMoneyRegistrationInfo args = new BlockedMoneyRegistrationInfo();
        //    args.ClientBalance = new BalanceEntity()
        //    {
        //        Id = 45,
        //        Client = new ClientEntity()
        //        {
        //            Id = 5,
        //            CreatedAt = DateTime.Now,
        //            FirstName = "John",
        //            LastName = "Snickers",
        //            PhoneNumber = "+7956244636652",
        //            Status = true
        //        },
        //        Amount = 20000.00M,
        //        Status = true
        //    };
        //    args.Operation = new OperationEntity()
        //    {
        //        Id = 2
        //    };
        //    args.Total = 10000.00M;

        //    // Act
        //    blockedMoneysService.Create(args);

        //    blockedMoneyTableRepository.Contains(Arg.Is<BlockedMoneyEntity>( // Now Contains returns true (table contains blocked money with this data)
        //        bm => bm.ClientBalance == args.ClientBalance
        //        && bm.Operation == args.Operation
        //        && bm.Customer == args.ClientBalance.Client
        //        && bm.Total == args.Total)).Returns(true);

        //    blockedMoneysService.Create(args); // Try to reg. same twice and get exception

        //    // Assert
        //}

        //[TestMethod]
        //public void ShouldGetBlockedMoneyItemInfo()
        //{
        //    // Arrange
        //    var blockedMoneyTableRepository = Substitute.For<IBlockedMoneyTableRepository>();
        //    int testId = 55;
        //    blockedMoneyTableRepository.ContainsById(Arg.Is(testId)).Returns(true);
        //    BlockedMoneysService blockedMoneysService = new BlockedMoneysService(blockedMoneyTableRepository);            

        //    // Act
        //    var sharesNumberInfo = blockedMoneysService.Get(testId);

        //    // Assert
        //    blockedMoneyTableRepository.Received(1).Get(testId);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException), "I didn't get exception it's wrong!")]
        //public void ShouldThrowExceptionIfCantFindSpecificBlockedMoneyItem()
        //{
        //    // Arrange
        //    var blockedMoneyTableRepository = Substitute.For<IBlockedMoneyTableRepository>();
        //    int testId = 55;
        //    blockedMoneyTableRepository.ContainsById(Arg.Is(testId)).Returns(false); // Now Contains returns false (table don't contains blocked money with this Id)
        //    BlockedMoneysService blockedMoneysService = new BlockedMoneysService(blockedMoneyTableRepository);

        //    // Act
        //    blockedMoneysService.ContainsById(testId); // Try to get blocked money and get exception

        //    // Assert
        //}

        //[TestMethod]
        //public void ShouldRemoveBlockedMoneyItem()
        //{
        //    // Arrange
        //    var blockedMoneyTableRepository = Substitute.For<IBlockedMoneyTableRepository>();
        //    int testId = 55;
        //    blockedMoneyTableRepository.ContainsById(Arg.Is(testId)).Returns(true);
        //    BlockedMoneysService blockedMoneysService = new BlockedMoneysService(blockedMoneyTableRepository);

        //    // Act
        //    blockedMoneysService.Remove(testId);

        //    // Assert
        //    blockedMoneyTableRepository.Received(1).Remove(testId);
        //    blockedMoneyTableRepository.Received(1).SaveChanges();
        //}
    }    
}
