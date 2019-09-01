namespace TradingSoftware.Core.Tests
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using TradingSoftware.Core.Models;
    using TradingSoftware.Core.Repositories;
    using TradingSoftware.Core.Services;

    [TestClass]
    public class TransactionManagerTests
    {
        // Arrange
        [TestMethod]
        public void AddTransactionParametrsTest()
        {
            var clientManagerMock = Substitute.For<IClientManager>();
            var blockOfSharesRepositoryMock = Substitute.For<IBlockOfSharesRepository>();
            var clientRepositoryMock = Substitute.For<IClientRepository>();
            var sharesRepositoryMock = Substitute.For<ISharesRepository>();
            var transactionRepositoryMock = Substitute.For<ITransactionRepository>();

            var sut = new TransactionManager(
                clientManagerMock,
                blockOfSharesRepositoryMock,
                clientRepositoryMock,
                sharesRepositoryMock,
                transactionRepositoryMock);

            int sellerID = 3;
            int buyerID = 5;
            int shareID = 9;
            int shareAmount = 1;

            // Act
            sut.AddTransaction(sellerID, buyerID, shareID, shareAmount);

            // Asserts
            transactionRepositoryMock.Received(1).Insert(Arg.Is<Transaction>(t => t.BuyerID == buyerID &&
                                                                                t.SellerID == sellerID &&
                                                                                t.ShareID == shareID &&
                                                                                t.Amount == shareAmount));
        }

        [TestMethod]
        public void AddTransactionTest()
        {
            var clientManagerMock = Substitute.For<IClientManager>();
            var blockOfSharesRepositoryMock = Substitute.For<IBlockOfSharesRepository>();
            var clientRepositoryMock = Substitute.For<IClientRepository>();
            var sharesRepositoryMock = Substitute.For<ISharesRepository>();
            var transactionRepositoryMock = Substitute.For<ITransactionRepository>();

            var sut = new TransactionManager(
                clientManagerMock,
                blockOfSharesRepositoryMock,
                clientRepositoryMock,
                sharesRepositoryMock,
                transactionRepositoryMock);
            var transaction = new Transaction
            {
                SellerID = 3,
                BuyerID = 5,
                ShareID = 9,
                Amount = 1
            };

            // Act
            sut.AddTransaction(transaction);

            // Asserts
            transactionRepositoryMock.Received(1).Insert(Arg.Is<Transaction>(transaction));
        }

        [TestMethod]
        public void GetAllTransactionsTest()
        {
            var clientManagerMock = Substitute.For<IClientManager>();
            var blockOfSharesRepositoryMock = Substitute.For<IBlockOfSharesRepository>();
            var clientRepositoryMock = Substitute.For<IClientRepository>();
            var sharesRepositoryMock = Substitute.For<ISharesRepository>();
            var transactionRepositoryMock = Substitute.For<ITransactionRepository>();

            var sut = new TransactionManager(
                clientManagerMock,
                blockOfSharesRepositoryMock,
                clientRepositoryMock,
                sharesRepositoryMock,
                transactionRepositoryMock);

            // Act
            sut.GetAllTransactions();

            // Asserts
            transactionRepositoryMock.Received(1).GetAllTransaction();
        }

        [TestMethod]
        public void ValidateSuccessfulTest()
        {
            var clientManagerMock = Substitute.For<IClientManager>();
            var blockOfSharesRepositoryMock = Substitute.For<IBlockOfSharesRepository>();
            var clientRepositoryMock = Substitute.For<IClientRepository>();
            var sharesRepositoryMock = Substitute.For<ISharesRepository>();
            var transactionRepositoryMock = Substitute.For<ITransactionRepository>();

            var sut = new TransactionManager(
                clientManagerMock,
                blockOfSharesRepositoryMock,
                clientRepositoryMock,
                sharesRepositoryMock,
                transactionRepositoryMock);

            var transaction = new Transaction
            {
                SellerID = 5,
                BuyerID = 3,
                ShareID = 9,
                Amount = 12
            };
            blockOfSharesRepositoryMock
                .IsClientHasShareType(transaction.SellerID, transaction.ShareID)
                .Returns(true);

            blockOfSharesRepositoryMock
                .GetClientShareAmount(transaction.SellerID, transaction.ShareID)
                .Returns(14);

            clientRepositoryMock
                .GetClientBalance(transaction.BuyerID)
                .Returns(25);

            // Act
            bool sutOperationResult = sut.Validate(transaction);

            // Asserts
            Assert.AreEqual(true, sutOperationResult);
        }

        [TestMethod]
        public void ValidateBuyerAndSellerDifferentTest()
        {
            var clientManagerMock = Substitute.For<IClientManager>();
            var blockOfSharesRepositoryMock = Substitute.For<IBlockOfSharesRepository>();
            var clientRepositoryMock = Substitute.For<IClientRepository>();
            var sharesRepositoryMock = Substitute.For<ISharesRepository>();
            var transactionRepositoryMock = Substitute.For<ITransactionRepository>();

            var sut = new TransactionManager(
                clientManagerMock,
                blockOfSharesRepositoryMock,
                clientRepositoryMock,
                sharesRepositoryMock,
                transactionRepositoryMock);

            var transaction = new Transaction
            {
                SellerID = 5,
                BuyerID = 5,
                ShareID = 9,
                Amount = 12
            };
            blockOfSharesRepositoryMock
                .IsClientHasShareType(transaction.SellerID, transaction.ShareID)
                .Returns(true);

            blockOfSharesRepositoryMock
                .GetClientShareAmount(transaction.SellerID, transaction.ShareID)
                .Returns(14);

            clientRepositoryMock
                .GetClientBalance(transaction.BuyerID)
                .Returns(25);

            // Act
            bool sutOperationResult = sut.Validate(transaction);

            // Asserts
            Assert.AreEqual(false, sutOperationResult);
        }

        [TestMethod]
        public void ValidateSellerHasEnoughShareTest()
        {
            var clientManagerMock = Substitute.For<IClientManager>();
            var blockOfSharesRepositoryMock = Substitute.For<IBlockOfSharesRepository>();
            var clientRepositoryMock = Substitute.For<IClientRepository>();
            var sharesRepositoryMock = Substitute.For<ISharesRepository>();
            var transactionRepositoryMock = Substitute.For<ITransactionRepository>();

            var sut = new TransactionManager(
                clientManagerMock,
                blockOfSharesRepositoryMock,
                clientRepositoryMock,
                sharesRepositoryMock,
                transactionRepositoryMock);

            var transaction = new Transaction
            {
                SellerID = 1,
                BuyerID = 5,
                ShareID = 9,
                Amount = 12
            };
            blockOfSharesRepositoryMock
                .IsClientHasShareType(transaction.SellerID, transaction.ShareID)
                .Returns(true);

            blockOfSharesRepositoryMock
                .GetClientShareAmount(transaction.SellerID, transaction.ShareID)
                .Returns(7);

            clientRepositoryMock
                .GetClientBalance(transaction.BuyerID)
                .Returns(25);

            // Act
            bool sutOperationResult = sut.Validate(transaction);

            // Asserts
            Assert.AreEqual(false, sutOperationResult);
        }

        [TestMethod]
        public void ValidateIsBuyerHasNegativeBalanceTest()
        {
            var clientManagerMock = Substitute.For<IClientManager>();
            var blockOfSharesRepositoryMock = Substitute.For<IBlockOfSharesRepository>();
            var clientRepositoryMock = Substitute.For<IClientRepository>();
            var sharesRepositoryMock = Substitute.For<ISharesRepository>();
            var transactionRepositoryMock = Substitute.For<ITransactionRepository>();

            var sut = new TransactionManager(
                clientManagerMock,
                blockOfSharesRepositoryMock,
                clientRepositoryMock,
                sharesRepositoryMock,
                transactionRepositoryMock);

            var transaction = new Transaction
            {
                SellerID = 1,
                BuyerID = 5,
                ShareID = 9,
                Amount = 12
            };
            blockOfSharesRepositoryMock
                .IsClientHasShareType(transaction.SellerID, transaction.ShareID)
                .Returns(true);

            blockOfSharesRepositoryMock
                .GetClientShareAmount(transaction.SellerID, transaction.ShareID)
                .Returns(25);

            clientRepositoryMock
                .GetClientBalance(transaction.BuyerID)
                .Returns(-325);

            // Act
            bool sutOperationResult = sut.Validate(transaction);

            // Asserts
            Assert.AreEqual(false, sutOperationResult);
        }

        [TestMethod]
        public void TransactionAgentChangeSellerBlockOfShareTest()
        {
            var clientManagerMock = Substitute.For<IClientManager>();
            var blockOfSharesRepositoryMock = Substitute.For<IBlockOfSharesRepository>();
            var clientRepositoryMock = Substitute.For<IClientRepository>();
            var sharesRepositoryMock = Substitute.For<ISharesRepository>();
            var transactionRepositoryMock = Substitute.For<ITransactionRepository>();

            var sut = new TransactionManager(
                clientManagerMock,
                blockOfSharesRepositoryMock,
                clientRepositoryMock,
                sharesRepositoryMock,
                transactionRepositoryMock);

            var transaction = new Transaction
            {
                SellerID = 5,
                BuyerID = 3,
                ShareID = 9,
                Amount = 12
            };
            blockOfSharesRepositoryMock
                .IsClientHasShareType(transaction.BuyerID, transaction.ShareID)
                .Returns(true);

            // Act
            sut.TransactionAgent(transaction);

            // Asserts
            blockOfSharesRepositoryMock
                .Received(1)
                .ChangeShareAmountForClient(
                    Arg.Is<BlockOfShares>(b => b.ClientID == transaction.SellerID &&
                                             b.ShareID == transaction.ShareID &&
                                             b.Amount == (-1) * transaction.Amount));
        }

        [TestMethod]
        public void TransactionAgentChangeBuyerBlockOfShareTest()
        {
            // Arrange
            var clientManagerMock = Substitute.For<IClientManager>();
            var blockOfSharesRepositoryMock = Substitute.For<IBlockOfSharesRepository>();
            var clientRepositoryMock = Substitute.For<IClientRepository>();
            var sharesRepositoryMock = Substitute.For<ISharesRepository>();
            var transactionRepositoryMock = Substitute.For<ITransactionRepository>();

            var sut = new TransactionManager(
                clientManagerMock,
                blockOfSharesRepositoryMock,
                clientRepositoryMock,
                sharesRepositoryMock,
                transactionRepositoryMock);

            var transaction = new Transaction
            {
                SellerID = 5,
                BuyerID = 3,
                ShareID = 9,
                Amount = 12
            };
            blockOfSharesRepositoryMock
                .IsClientHasShareType(transaction.BuyerID, transaction.ShareID)
                .Returns(true);

            // Act
            sut.TransactionAgent(transaction);

            // Asserts
            blockOfSharesRepositoryMock
                .Received(1)
                .ChangeShareAmountForClient(
                    Arg.Is<BlockOfShares>(b => b.ClientID == transaction.BuyerID &&
                                             b.ShareID == transaction.ShareID &&
                                             b.Amount == transaction.Amount));
        }

        [TestMethod]
        public void TransactionAgentChangeBuyerBlockOfShareIfHeHasntStockTypeTest()
        {
            // Arrange
            var clientManagerMock = Substitute.For<IClientManager>();
            var blockOfSharesRepositoryMock = Substitute.For<IBlockOfSharesRepository>();
            var clientRepositoryMock = Substitute.For<IClientRepository>();
            var sharesRepositoryMock = Substitute.For<ISharesRepository>();
            var transactionRepositoryMock = Substitute.For<ITransactionRepository>();

            var sut = new TransactionManager(
                clientManagerMock,
                blockOfSharesRepositoryMock,
                clientRepositoryMock,
                sharesRepositoryMock,
                transactionRepositoryMock);

            var transaction = new Transaction
            {
                SellerID = 5,
                BuyerID = 3,
                ShareID = 9,
                Amount = 12
            };
            blockOfSharesRepositoryMock
                .IsClientHasShareType(transaction.BuyerID, transaction.ShareID)
                .Returns(false);

            // Act
            sut.TransactionAgent(transaction);

            // Asserts
            blockOfSharesRepositoryMock
                .DidNotReceive()
                .ChangeShareAmountForClient(
                    Arg.Is<BlockOfShares>(b => b.ClientID == transaction.BuyerID &&
                                             b.ShareID == transaction.ShareID &&
                                             b.Amount == transaction.Amount));
            blockOfSharesRepositoryMock
                .Received(1)
                .Insert(
                    Arg.Is<BlockOfShares>(b => b.ClientID == transaction.BuyerID &&
                                             b.ShareID == transaction.ShareID &&
                                             b.Amount == transaction.Amount));
        }

        [TestMethod]
        public void TransactionAgentChangeBuyerBalanceTest()
        {
            // Arrange
            var clientManagerMock = Substitute.For<IClientManager>();
            var blockOfSharesRepositoryMock = Substitute.For<IBlockOfSharesRepository>();
            var clientRepositoryMock = Substitute.For<IClientRepository>();
            var sharesRepositoryMock = Substitute.For<ISharesRepository>();
            var transactionRepositoryMock = Substitute.For<ITransactionRepository>();

            var sut = new TransactionManager(
                clientManagerMock,
                blockOfSharesRepositoryMock,
                clientRepositoryMock,
                sharesRepositoryMock,
                transactionRepositoryMock);

            var transaction = new Transaction
            {
                SellerID = 5,
                BuyerID = 3,
                ShareID = 9,
                Amount = 10
            };
            decimal sharePrice = 509;
            decimal calculatedSharesPrice = sharePrice * transaction.Amount;

            blockOfSharesRepositoryMock
                .IsClientHasShareType(transaction.BuyerID, transaction.ShareID)
                .Returns(true);
            sharesRepositoryMock
                .GetSharePrice(transaction.ShareID)
                .Returns(sharePrice);

            // Act
            sut.TransactionAgent(transaction);

            // Asserts
            clientManagerMock
                .Received(1)
                .ChangeBalance(transaction.BuyerID, (-1) * calculatedSharesPrice);
        }

        [TestMethod]
        public void TransactionAgentChangeSellerBalanceTest()
        {
            // Arrange
            var clientManagerMock = Substitute.For<IClientManager>();
            var blockOfSharesRepositoryMock = Substitute.For<IBlockOfSharesRepository>();
            var clientRepositoryMock = Substitute.For<IClientRepository>();
            var sharesRepositoryMock = Substitute.For<ISharesRepository>();
            var transactionRepositoryMock = Substitute.For<ITransactionRepository>();

            var sut = new TransactionManager(
                clientManagerMock,
                blockOfSharesRepositoryMock,
                clientRepositoryMock,
                sharesRepositoryMock,
                transactionRepositoryMock);

            var transaction = new Transaction
            {
                SellerID = 5,
                BuyerID = 3,
                ShareID = 9,
                Amount = 10
            };
            decimal sharePrice = 509;
            decimal calculatedSharesPrice = sharePrice * transaction.Amount;

            blockOfSharesRepositoryMock
                .IsClientHasShareType(transaction.BuyerID, transaction.ShareID)
                .Returns(true);
            sharesRepositoryMock
                .GetSharePrice(transaction.ShareID)
                .Returns(sharePrice);

            // Act
            sut.TransactionAgent(transaction);

            // Asserts
            clientManagerMock
                .Received(1)
                .ChangeBalance(transaction.SellerID, calculatedSharesPrice);
        }

        [TestMethod]
        public void GetTransactionWithClientTest()
        {
            // Arrange
            var clientManagerMock = Substitute.For<IClientManager>();
            var blockOfSharesRepositoryMock = Substitute.For<IBlockOfSharesRepository>();
            var clientRepositoryMock = Substitute.For<IClientRepository>();
            var sharesRepositoryMock = Substitute.For<ISharesRepository>();
            var transactionRepositoryMock = Substitute.For<ITransactionRepository>();

            var sut = new TransactionManager(
                clientManagerMock,
                blockOfSharesRepositoryMock,
                clientRepositoryMock,
                sharesRepositoryMock,
                transactionRepositoryMock);

            var transaction = new Transaction
            {
                SellerID = 1,
                BuyerID = 2,
                ShareID = 3,
                Amount = 4
            };
            int clientID = 1;
            decimal sharePrice = 509;

            transactionRepositoryMock
                .GetTransactionWithClient(clientID)
                .Returns(new Transaction[] { transaction });

            clientManagerMock
                .GetClientName(transaction.SellerID)
                .Returns("Yevgeny Zamyatin");

            clientManagerMock
                .GetClientName(transaction.BuyerID)
                .Returns("George Orwell");

            sharesRepositoryMock
                .GetShareType(transaction.ShareID)
                .Returns("We1984");

            sharesRepositoryMock
                .GetSharePrice(transaction.ShareID)
                .Returns(sharePrice);

            // Act
            var transactionsFullDataResult = sut.GetTransactionWithClient(clientID);

            // Asserts
            Assert.AreEqual(transactionsFullDataResult.ElementAt(0).SellerName, "Yevgeny Zamyatin");
            Assert.AreEqual(transactionsFullDataResult.ElementAt(0).BuyerName, "George Orwell");
            Assert.AreEqual(transactionsFullDataResult.ElementAt(0).ShareType, "We1984");
            Assert.AreEqual(transactionsFullDataResult.ElementAt(0).ShareAmount, 4);
        }
    }
}