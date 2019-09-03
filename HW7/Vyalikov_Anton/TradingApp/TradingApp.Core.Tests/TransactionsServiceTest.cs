namespace TradingApp.Core.Tests
{
    using TradingApp.Core.Interfaces;
    using TradingApp.Core.Models;
    using TradingApp.Core.Services;
    using TradingApp.Core.Repos;
    using NSubstitute;
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TransactionsServiceTest
    {
        [TestMethod]
        public void AddTransactionTest()
        {
            var clientServiceMock = Substitute.For<IClientService>();
            var sharesServiceMock = Substitute.For<ISharesService>();
            var portfolioServiceMock = Substitute.For<IPortfoliosService>();
            var transactionRepositoryMock = Substitute.For<ITransactionsRepository>();

            var sut = new TransactionsService(
                clientServiceMock,
                sharesServiceMock,
                portfolioServiceMock,               
                transactionRepositoryMock);

            var transaction = new Transaction
            {
                SellerID = 3,
                BuyerID = 5,
                ShareID = 9,
                AmountOfShares = 1
            };

            // Act
            sut.AddTransaction(transaction);

            // Asserts
            transactionRepositoryMock.Received(1).Insert(Arg.Is<Transaction>(transaction));
        }

        [TestMethod]
        public void GetAllTransactionsTest()
        {
            var clientServiceMock = Substitute.For<IClientService>();
            var sharesServiceMock = Substitute.For<ISharesService>();
            var portfolioServiceMock = Substitute.For<IPortfoliosService>();
            var transactionRepositoryMock = Substitute.For<ITransactionsRepository>();

            var sut = new TransactionsService(
                clientServiceMock,
                sharesServiceMock,
                portfolioServiceMock,
                transactionRepositoryMock);

            // Act
            sut.GetAllTransactions();

            // Asserts
            transactionRepositoryMock.Received(1).GetAllTransactions();
        }

        [TestMethod]
        public void SellOrBuySharesTest()
        {
            //Arrange
            var clientServiceMock = Substitute.For<IClientService>();
            var sharesServiceMock = Substitute.For<ISharesService>();
            var portfolioServiceMock = Substitute.For<IPortfoliosService>();
            var transactionRepositoryMock = Substitute.For<ITransactionsRepository>();

            var sut = new TransactionsService(
                clientServiceMock,
                sharesServiceMock,
                portfolioServiceMock,
                transactionRepositoryMock);

            Transaction transaction = new Transaction()
            {
                BuyerID = 5,
                SellerID = 6,
                ShareID = 7,
                AmountOfShares = 10
            };

            //Act
            sut.SellOrBuyShares(transaction);

            //Assert
            portfolioServiceMock.Received(2).ChangeAmountOfShares(Arg.Any<ClientPortfolio>());
            clientServiceMock.Received(2).ChangeBalance(Arg.Any<int>(), Arg.Any<decimal>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Seller and buyer are the same person.")]
        public void ShouldNotBuyAndSellSharesYourself()
        {
            //Arrange
            var clientServiceMock = Substitute.For<IClientService>();
            var sharesServiceMock = Substitute.For<ISharesService>();
            var portfolioServiceMock = Substitute.For<IPortfoliosService>();
            var transactionRepositoryMock = Substitute.For<ITransactionsRepository>();

            var sut = new TransactionsService(
                clientServiceMock,
                sharesServiceMock,
                portfolioServiceMock,
                transactionRepositoryMock);

            Transaction transaction = new Transaction()
            {
                SellerID = 1,
                BuyerID = 1,
                ShareID = 2,
                AmountOfShares = 10
            };

            //Act
            sut.SellOrBuyShares(transaction);

            //Assert
            portfolioServiceMock.DidNotReceive().ChangeAmountOfShares(Arg.Any<ClientPortfolio>());
            clientServiceMock.DidNotReceive().ChangeBalance(Arg.Any<int>(), Arg.Any<decimal>());
        }
    }
}
