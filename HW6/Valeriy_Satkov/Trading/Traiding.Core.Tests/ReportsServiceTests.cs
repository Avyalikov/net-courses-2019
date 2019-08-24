namespace Traiding.Core.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using System;
    using Traiding.Core.Models;
    using Traiding.Core.Repositories;
    using Traiding.Core.Services;

    [TestClass]
    public class ReportsServiceTests
    {
        IOperationTableRepository operationTableRepository;
        ISharesNumberTableRepository sharesNumberTableRepository;
        IBalanceTableRepository balanceTableRepository;

        [TestMethod]
        public void ShouldGetClientOperations()
        {
            // Arrange
            this.operationTableRepository = Substitute.For<IOperationTableRepository>();
            ReportsService reportsService = new ReportsService(this.operationTableRepository);
            int testClientId = 43;

            // Act
            var sharesNumbersOfClient = reportsService.GetOperationByClient(testClientId);

            // Assert
            this.operationTableRepository.Received(1).GetByClient(testClientId);
        }

        [TestMethod]
        public void ShouldGetSharesNumbersByClient()
        {
            // Arrange
            this.sharesNumberTableRepository = Substitute.For<ISharesNumberTableRepository>();
            ReportsService reportsService = new ReportsService(this.sharesNumberTableRepository);
            int testClientId = 43;

            // Act
            var sharesNumbersOfClient = reportsService.GetSharesNumberByClient(testClientId);

            // Assert
            this.sharesNumberTableRepository.Received(1).GetByClient(testClientId);
        }

        [TestMethod]
        public void ShouldGetSharesNumbersByShare()
        {
            // Arrange
            this.sharesNumberTableRepository = Substitute.For<ISharesNumberTableRepository>();
            ReportsService reportsService = new ReportsService(this.sharesNumberTableRepository);
            int testShareId = 3;

            // Act
            var sharesNumbersOfType = reportsService.GetSharesNumberByShare(testShareId);

            // Assert
            this.sharesNumberTableRepository.Received(1).GetByShare(testShareId);
        }

        [TestMethod]
        public void ShouldGetZeroBalances()
        {
            // Arrange
            this.balanceTableRepository = Substitute.For<IBalanceTableRepository>();
            ReportsService reportsService = new ReportsService(this.balanceTableRepository);

            // Act
            var zeroBalances = reportsService.GetZeroBalances();

            // Assert
            this.balanceTableRepository.Received(1).GetZeroBalances();
        }

        [TestMethod]
        public void ShouldGetNegativeBalances()
        {
            // Arrange
            this.balanceTableRepository = Substitute.For<IBalanceTableRepository>();
            ReportsService reportsService = new ReportsService(this.balanceTableRepository);

            // Act
            var zeroBalances = reportsService.GetNegativeBalances();

            // Assert
            this.balanceTableRepository.Received(1).GetNegativeBalances();
        }
    }    
}
