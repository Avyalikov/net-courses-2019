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
    public class BalancesServiceTests
    {
        IBalanceTableRepository balanceTableRepository;
        ClientEntity newClient = new ClientEntity()
        {
            Id = 5,
            CreatedAt = DateTime.Now,
            FirstName = "John",
            LastName = "Snickers",
            PhoneNumber = "+7956244636652",
            Status = true
        };

        [TestMethod]
        public void ShouldRegisterNewBalance()
        {
            // Arrange
            this.balanceTableRepository = Substitute.For<IBalanceTableRepository>();
            BalancesService balancesService = new BalancesService(this.balanceTableRepository);
            BalanceRegistrationInfo args = new BalanceRegistrationInfo();            
            args.Client = this.newClient;
            args.Amount = 5000.00M;
            args.Status = true;

            // Act
            var shareId = balancesService.RegisterNewBalance(args);

            // Assert
            this.balanceTableRepository.Received(1).Add(Arg.Is<BalanceEntity>(
                b => b.Client == args.Client
                && b.Amount == args.Amount
                && b.Status == args.Status));
            this.balanceTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "I didn't get exception it's wrong!")]
        public void ShouldNotRegisterNewBalanceIfItExists()
        {
            // Arrange
            this.balanceTableRepository = Substitute.For<IBalanceTableRepository>();
            BalancesService balancesService = new BalancesService(this.balanceTableRepository);
            BalanceRegistrationInfo args = new BalanceRegistrationInfo();
            args.Client = this.newClient;
            args.Amount = 5000.00M;
            args.Status = true;

            // Act
            balancesService.RegisterNewBalance(args);

            this.balanceTableRepository.Contains(Arg.Is<BalanceEntity>( // Now Contains returns true (table contains this balance of client)
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
            this.balanceTableRepository = Substitute.For<IBalanceTableRepository>();
            int testId = 55;
            this.balanceTableRepository.ContainsById(Arg.Is(testId)).Returns(true);
            BalancesService balancesService = new BalancesService(this.balanceTableRepository);

            // Act
            var balanceInfo = balancesService.GetBalance(testId);

            // Assert
            this.balanceTableRepository.Received(1).Get(testId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "I didn't get exception it's wrong!")]
        public void ShouldThrowExceptionIfCantFindBalance()
        {
            // Arrange
            this.balanceTableRepository = Substitute.For<IBalanceTableRepository>();
            int testId = 55;
            this.balanceTableRepository.ContainsById(Arg.Is(testId)).Returns(false); // Now Contains returns false (table don't contains share type with this Id)
            BalancesService balancesService = new BalancesService(this.balanceTableRepository);

            // Act
            balancesService.ContainsById(testId); // Try to get share type and get exception

            // Assert
        }

        [TestMethod]
        public void ShouldChangeAmount()
        {
            // Arrange
            this.balanceTableRepository = Substitute.For<IBalanceTableRepository>();
            int testId = 55;
            this.balanceTableRepository.ContainsById(Arg.Is(testId)).Returns(true);
            BalancesService balancesService = new BalancesService(this.balanceTableRepository);
            decimal newAmount = 5000.00M;

            // Act
            balancesService.ChangeBalance(testId, newAmount);

            // Assert
            this.balanceTableRepository.Received(1).ChangeAmount(testId, newAmount);
            this.balanceTableRepository.Received(1).SaveChanges();
        }        
    }
}
