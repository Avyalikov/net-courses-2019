using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Trading.Core.Repositories;
using Trading.Core.Services;


namespace Trading.Core.Tests
{
    [TestClass]
    public class TransactionHistoryServiceTests
    {
        ITransactionHistoryTableRepository transactionHistoryTableRepository;
        ITransactionHistoryService transactionHistoryService;
        [TestInitialize]
        public void Initialize()
        {
            transactionHistoryTableRepository = Substitute.For<ITransactionHistoryTableRepository>();
            transactionHistoryService = new TransactionHistoryService(transactionHistoryTableRepository);
        }

        [TestMethod]
        public void ShouldGetTransactionsList()
        {
            //Arrange
            int clientId = 1;
            int top = 10;
            //Act
            transactionHistoryService.GetTopByClientId(clientId, top);
            //Assert
            transactionHistoryTableRepository.Received(1).GetTopById(clientId, top);
        }
    }
}