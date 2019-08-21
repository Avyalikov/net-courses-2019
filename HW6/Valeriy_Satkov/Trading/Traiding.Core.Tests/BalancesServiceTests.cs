using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Traiding.Core.Models;
using Traiding.Core.Repositories;
using Traiding.Core.Services;

namespace Traiding.Core.Tests
{
    [TestClass]
    public class BalancesServiceTests
    {
        [TestMethod]
        public void ShouldRegisterNewBalance()
        {
            // Arrange
            var balanceTableRepository = Substitute.For<IBalanceTableRepository>();
            BalancesService balancesService = new BalancesService(balanceTableRepository);
            BalanceRegistrationInfo args = new BalanceRegistrationInfo();            
            args.Client = new ClientEntity()
            {
                Id = 5,
                CreatedAt = DateTime.Now,
                FirstName = "John",
                LastName = "Snickers",
                PhoneNumber = "+7956244636652",
                Status = true
            };
            args.Amount = 5000.00M;
            args.Status = true;

            // Act
            var shareId = balancesService.RegisterNewBalance(args);

            // Assert
            balanceTableRepository.Received(1).Add(Arg.Is<BalanceEntity>(
                b => b.Client == args.Client
                && b.Amount == args.Amount
                && b.Status == args.Status));
            balanceTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "I didn't get exception it's wrong!")]
        public void ShouldNotRegisterNewBalanceIfItExists()
        {
            // Arrange
            var balanceTableRepository = Substitute.For<IBalanceTableRepository>();
            BalancesService balancesService = new BalancesService(balanceTableRepository);
            BalanceRegistrationInfo args = new BalanceRegistrationInfo();
            args.Client = new ClientEntity()
            {
                Id = 5,
                CreatedAt = DateTime.Now,
                FirstName = "John",
                LastName = "Snickers",
                PhoneNumber = "+7956244636652",
                Status = true
            };
            args.Amount = 5000.00M;
            args.Status = true;

            // Act
            balancesService.RegisterNewBalance(args);

            balanceTableRepository.Contains(Arg.Is<BalanceEntity>( // Now Contains returns true (table contains this balance of client)
                b => b.Client == args.Client
                && b.Amount == args.Amount
                && b.Status == args.Status)).Returns(true);

            balancesService.RegisterNewBalance(args); // Try to reg. same twice and get exception

            // Assert
        }

        [TestMethod]
        public void ShouldGetBalanceInfo()
        {
            // Arrange
            var balanceTableRepository = Substitute.For<IBalanceTableRepository>();
            int testId = 55;
            balanceTableRepository.ContainsById(Arg.Is(testId)).Returns(true);
            BalancesService balancesService = new BalancesService(balanceTableRepository);            

            // Act
            var balanceInfo = balancesService.GetBalance(testId);

            // Assert
            balanceTableRepository.Received(1).Get(testId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "I didn't get exception it's wrong!")]
        public void ShouldThrowExceptionIfCantFindBalance()
        {
            // Arrange
            var balanceTableRepository = Substitute.For<IBalanceTableRepository>();
            int testId = 55;
            balanceTableRepository.ContainsById(Arg.Is(testId)).Returns(false); // Now Contains returns false (table don't contains share type with this Id)
            BalancesService balancesService = new BalancesService(balanceTableRepository);

            // Act
            balancesService.ContainsById(testId); // Try to get share type and get exception

            // Assert
        }

        [TestMethod]
        public void ShouldChangeAmount()
        {
            // Arrange
            var balanceTableRepository = Substitute.For<IBalanceTableRepository>();
            int testId = 55;
            balanceTableRepository.ContainsById(Arg.Is(testId)).Returns(true);
            BalancesService balancesService = new BalancesService(balanceTableRepository);
            decimal newAmount = 5000.00M;

            // Act
            balancesService.ChangeBalance(testId, newAmount);

            // Assert
            balanceTableRepository.Received(1).ChangeAmount(testId, newAmount);
            balanceTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        public void ShouldGetZeroBalances()
        {
            // Arrange
            var balanceTableRepository = Substitute.For<IBalanceTableRepository>();
            BalancesService balancesService = new BalancesService(balanceTableRepository);

            // Act
            var zeroBalances = balancesService.GetZeroBalances();

            // Assert
            balanceTableRepository.Received(1).GetZeroBalances();
        }

        [TestMethod]
        public void ShouldGetNegativeBalances()
        {
            // Arrange
            var balanceTableRepository = Substitute.For<IBalanceTableRepository>();
            BalancesService balancesService = new BalancesService(balanceTableRepository);

            // Act
            var zeroBalances = balancesService.GetNegativeBalances();

            // Assert
            balanceTableRepository.Received(1).GetNegativeBalances();
        }
    }
}
