using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Trading.Core.Repositories;
using Trading.Core.Services;
using Trading.Core.DTO;
using Trading.Core.Model;


namespace TradingApp.Tests
{
    [TestClass]
    public class TransactionHistoryServiceTest
    {
        [TestMethod]
        public void ShouldAddTransactiont()
        {
            //Arrange
            var transactionTableRepository = Substitute.For<ITableRepository<TransactionHistory>>();
            TransactionHistoryService transactionService = new TransactionHistoryService(transactionTableRepository);
            TransactionInfo transactInfo = new TransactionInfo
            {
                CustomerOrderId=1,
                SalerOrderId=1,
                TrDateTime=new DateTime(2019,08,21)
            };
            //Act
            transactionService.AddTransactionInfo(transactInfo);
            //Assert
            transactionTableRepository.Received(1).Add(Arg.Is<TransactionHistory>(
                w => w.CustomerOrderID==1&&
                w.SalerOrderID==1&&
                w.TransactionDateTime==new DateTime(2019,08,21)

                ));
        }

       

        [TestMethod]
        public void ShouldGetTransactionInfo()
        {
            // Arrange
            var transactionTableRepository = Substitute.For<ITableRepository<TransactionHistory>>();
            TransactionHistoryService transactionService = new TransactionHistoryService(transactionTableRepository);
           transactionTableRepository.ContainsByPK(1).Returns(true);


            // Act
            var transaction = transactionService.GetTransactionByID(1);

            // Assert
            transactionTableRepository.Received(1).FindByPK(1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Transaction doesn't exist")]
        public void ShouldThrowExceptionIfCantFindTransaction()
        {
            // Arrange
            var transactionTableRepository = Substitute.For<ITableRepository<TransactionHistory>>();
            TransactionHistoryService transactionService = new TransactionHistoryService(transactionTableRepository);
            transactionTableRepository.ContainsByPK(1).Returns(false);

            // Act
            var transaction = transactionService.GetTransactionByID(1);


        }
    }
}
