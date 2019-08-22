namespace Traiding.Core.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;    
    using Traiding.Core.Repositories;
    using Traiding.Core.Services;
    //using System;
    //using Traiding.Core.Models;

    [TestClass]
    public class OperationsServiceTests
    {
        /* Implemented in Sale service tests
         */
        //[TestMethod]
        //public void ShouldCreateOperationItemWithId()
        //{
        //    // Arrange
        //    var operationTableRepository = Substitute.For<IOperationTableRepository>();
        //    OperationsService operationsService = new OperationsService(operationTableRepository);

        //    // Act
        //    var operationId = operationsService.Create();

        //    // Assert
        //    operationTableRepository.Received(1).Add(Arg.Is<OperationEntity>(
        //        bm => bm.Id == operationId));
        //    operationTableRepository.Received(1).SaveChanges();
        //}

        //[TestMethod]
        //public void ShouldGetOperationItemInfo()
        //{
        //    // Arrange
        //    var operationTableRepository = Substitute.For<IOperationTableRepository>();
        //    int testId = 55;
        //    operationTableRepository.ContainsById(Arg.Is(testId)).Returns(true);
        //    OperationsService operationsService = new OperationsService(operationTableRepository);

        //    // Act
        //    var sharesNumberInfo = operationsService.Get(testId);

        //    // Assert
        //    operationTableRepository.Received(1).Get(testId);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException), "I didn't get exception it's wrong!")]
        //public void ShouldThrowExceptionIfCantFindOperationItem()
        //{
        //    // Arrange
        //    var operationTableRepository = Substitute.For<IOperationTableRepository>();
        //    int testId = 55;
        //    operationTableRepository.ContainsById(Arg.Is(testId)).Returns(false); // Now Contains returns false (table don't contains operation with this Id)
        //    OperationsService operationsService = new OperationsService(operationTableRepository);

        //    // Act
        //    operationsService.ContainsById(testId); // Try to get operation and get exception

        //    // Assert
        //}

        //[TestMethod]
        //public void ShouldFillCustomerColumns()
        //{
        //    // Arrange
        //    var operationTableRepository = Substitute.For<IOperationTableRepository>();
        //    int testOperationId = 9;
        //    operationTableRepository.ContainsById(Arg.Is(testOperationId)).Returns(true);
        //    OperationsService operationsService = new OperationsService(operationTableRepository);
        //    int testBlockedMoneyId = 2;

        //    // Act
        //    operationsService.FillCustomerColumns(testOperationId, testBlockedMoneyId);

        //    // Assert
        //    operationTableRepository.Received(1).FillCustomerColumns(testOperationId, testBlockedMoneyId);
        //    operationTableRepository.Received(1).SaveChanges();
        //}

        //[TestMethod]
        //public void ShouldFillSellerColumns()
        //{
        //    // Arrange
        //    var operationTableRepository = Substitute.For<IOperationTableRepository>();
        //    int testOperationId = 5;
        //    operationTableRepository.ContainsById(Arg.Is(testOperationId)).Returns(true);
        //    OperationsService operationsService = new OperationsService(operationTableRepository);
        //    int testBlockedSharesNumberId = 7;

        //    // Act
        //    operationsService.FillSellerColumns(testOperationId, testBlockedSharesNumberId);

        //    // Assert
        //    operationTableRepository.Received(1).FillSellerColumns(testOperationId, testBlockedSharesNumberId);
        //    operationTableRepository.Received(1).SaveChanges();
        //}

        //[TestMethod]
        //public void ShouldFillChargeDate()
        //{
        //    // Arrange
        //    var operationTableRepository = Substitute.For<IOperationTableRepository>();
        //    int testOperationId = 55;
        //    operationTableRepository.ContainsById(Arg.Is(testOperationId)).Returns(true);
        //    OperationsService operationsService = new OperationsService(operationTableRepository);

        //    // Act
        //    operationsService.SetChargeDate(testOperationId);

        //    // Assert
        //    operationTableRepository.Received(1).SetChargeDate(testOperationId, Arg.Any<DateTime>());
        //    operationTableRepository.Received(1).SaveChanges();
        //}

        [TestMethod]
        public void ShouldGetClientOperations()
        {
            // Arrange
            var operationTableRepository = Substitute.For<IOperationTableRepository>();
            OperationsService operationsService = new OperationsService(operationTableRepository);
            int testClientId = 43;

            // Act
            var sharesNumbersOfClient = operationsService.GetByClient(testClientId);

            // Assert
            operationTableRepository.Received(1).GetByClient(testClientId);
        }

        /* Implemented in Sale service tests
         */
        //[TestMethod]
        //public void ShouldRemoveItem()
        //{
        //    // Arrange
        //    var operationTableRepository = Substitute.For<IOperationTableRepository>();            
        //    int testId = 55;
        //    operationTableRepository.ContainsById(Arg.Is(testId)).Returns(true);
        //    OperationsService operationsService = new OperationsService(operationTableRepository);

        //    // Act
        //    operationsService.Remove(testId);

        //    // Assert
        //    operationTableRepository.Received(1).Remove(testId);
        //    operationTableRepository.Received(1).SaveChanges();
        //}
    }
}
