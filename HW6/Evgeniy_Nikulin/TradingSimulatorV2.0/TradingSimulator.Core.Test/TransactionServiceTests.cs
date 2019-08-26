namespace TradingSimulator.Core.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TradingSimulator.Core.Dto;
    using TradingSimulator.Core.Interfaces;
    using TradingSimulator.Core.Repositories;
    using TradingSimulator.Core.Services;

    [TestClass]
    public class TransactionServiceTests
    {
        private ITraderRepository traderRep;
        private IShareRepository shareRep;
        private ITransactionRepository transactionRep;
        private ILoggerService logger;
        List<Trader> traders;
        List<Share> shares;

        [TestInitialize]
        public void Initialize()
        {
            this.traderRep = Substitute.For<ITraderRepository>();
            this.traderRep.GetTrader(Arg.Any<int>()).Returns((callInfo) =>
            {
                var TraderId = callInfo.Arg<int>();
                return traders.Where(t => t.Id == TraderId).SingleOrDefault();
            });

            this.shareRep = Substitute.For<IShareRepository>();
            this.shareRep.GetShare(Arg.Any<int>(), Arg.Any<string>()).Returns((callInfo) =>
            {
                var TraderId = callInfo.Arg<int>();
                var shareName = callInfo.Arg<string>();

                return shares.Where(t => t.name == shareName && t.ownerId == TraderId).SingleOrDefault();
            });

            this.transactionRep = Substitute.For<ITransactionRepository>();
            this.logger = Substitute.For<ILoggerService>();
        }

        [TestMethod]
        public void ShouldMakeDealWithCreatingShare()
        {
            // Arrange
            TransactionService transactionService = new TransactionService(
                this.traderRep,
                this.shareRep,
                this.transactionRep,
                this.logger);

            this.traders = new List<Trader>()
            {
                new Trader()
                {
                    Id = 1,
                    name = "Brian",
                    surname = "Kelly",
                    phone = "+1()",
                    money = 5000m,
                },
                new Trader()
                {
                    Id = 2,
                    name = "Yves",
                    surname = "Guillemot",
                    phone = "+33()",
                    money = 5000m,
                }
            };
            this.shares = new List<Share>()
            {
                new Share()
                {
                    name = "Ubisoft",
                    price = 10m,
                    quantity = 10,
                    ownerId = 1,
                }
            };


            // Act
            var result = transactionService.MakeDeal(1, 2, "Ubisoft", 5);

            // Assert
            Assert.AreEqual(1, result.seller.Id);
            Assert.AreEqual("Brian", result.seller.name);
            Assert.AreEqual("Kelly", result.seller.surname);
            Assert.AreEqual(5050m, result.seller.money);

            Assert.AreEqual(2, result.buyer.Id);
            Assert.AreEqual("Yves", result.buyer.name);
            Assert.AreEqual("Guillemot", result.buyer.surname);
            Assert.AreEqual(4950m, result.buyer.money);

            Assert.AreEqual("Ubisoft", result.sellerShare.name);
            Assert.AreEqual(5, result.sellerShare.quantity);
            Assert.AreEqual(1, result.sellerShare.ownerId);

            Assert.AreEqual("Ubisoft", result.buyerShare.name);
            Assert.AreEqual(5, result.buyerShare.quantity);
            Assert.AreEqual(2, result.buyerShare.ownerId);
        }
        [TestMethod]
        public void ShouldMakeDealWithoutCreatingShare()
        {
            // Arrange
            TransactionService transactionService = new TransactionService(
                this.traderRep,
                this.shareRep,
                this.transactionRep,
                this.logger);

            this.traders = new List<Trader>()
            {
                new Trader()
                {
                    Id = 1,
                    name = "Brian",
                    surname = "Kelly",
                    phone = "+1()",
                    money = 5000m,
                },
                new Trader()
                {
                    Id = 2,
                    name = "Yves",
                    surname = "Guillemot",
                    phone = "+33()",
                    money = 5000m,
                }
            };
            this.shares = new List<Share>()
            {
                new Share()
                {
                    name = "Ubisoft",
                    price = 10m,
                    quantity = 10,
                    ownerId = 1,
                },
                new Share()
                {
                    name = "Ubisoft",
                    price = 10m,
                    quantity = 10,
                    ownerId = 2,
                }
            };


            // Act
            var result = transactionService.MakeDeal(1, 2, "Ubisoft", 5);

            // Assert
            Assert.AreEqual(1, result.seller.Id);
            Assert.AreEqual("Brian", result.seller.name);
            Assert.AreEqual("Kelly", result.seller.surname);
            Assert.AreEqual(5050m, result.seller.money);

            Assert.AreEqual(2, result.buyer.Id);
            Assert.AreEqual("Yves", result.buyer.name);
            Assert.AreEqual("Guillemot", result.buyer.surname);
            Assert.AreEqual(4950m, result.buyer.money);

            Assert.AreEqual("Ubisoft", result.sellerShare.name);
            Assert.AreEqual(5, result.sellerShare.quantity);
            Assert.AreEqual(1, result.sellerShare.ownerId);

            Assert.AreEqual("Ubisoft", result.buyerShare.name);
            Assert.AreEqual(15, result.buyerShare.quantity);
            Assert.AreEqual(2, result.buyerShare.ownerId);
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Incorrect trader ID 1")]
        public void ShouldNotMakeDealWithIncorrectFirstTradersIDcase1()
        {// Arrange
            TransactionService transactionService = new TransactionService(
                this.traderRep,
                this.shareRep,
                this.transactionRep,
                this.logger);

            this.traders = new List<Trader>();
            this.shares = new List<Share>();

            // Act
            var result = transactionService.MakeDeal(1, 2, "Ubisoft", 5);
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Incorrect trader ID 2")]
        public void ShouldNotMakeDealWithIncorrectSecondTraderID()
        {// Arrange
            TransactionService transactionService = new TransactionService(
                this.traderRep,
                this.shareRep,
                this.transactionRep,
                this.logger);

            this.traders = new List<Trader>()
            {
                new Trader()
                {
                    Id = 1,
                    name = "Brian",
                    surname = "Kelly",
                    phone = "+1()",
                    money = 5000m,
                }
            };
            this.shares = new List<Share>();

            // Act
            var result = transactionService.MakeDeal(1, 2, "Ubisoft", 5);
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Can't find Ubisoft share at trader with ID 1")]
        public void ShouldNotMakeDealWithIncorrectShareName()
        {// Arrange
            TransactionService transactionService = new TransactionService(
                this.traderRep,
                this.shareRep,
                this.transactionRep,
                this.logger);

            this.traders = new List<Trader>()
            {
                new Trader()
                {
                    Id = 1,
                    name = "Brian",
                    surname = "Kelly",
                    phone = "+1()",
                    money = 5000m,
                },
                new Trader()
                {
                    Id = 2,
                    name = "Yves",
                    surname = "Guillemot",
                    phone = "+33()",
                    money = 5000m,
                }
            };
            this.shares = new List<Share>();

            // Act
            var result = transactionService.MakeDeal(1, 2, "Ubisoft", 5);
        }

        [TestMethod]
        public void ShouldSaveTransaction()
        {// Arrange
            TransactionService transactionService = new TransactionService(
                this.traderRep,
                this.shareRep,
                this.transactionRep,
                this.logger);

            Transaction transaction = new Transaction()
            {
                seller = new Trader()
                {
                    Id = 1,
                    name = "Brian",
                    surname = "Kelly",
                    phone = "+1()",
                    money = 5050m,
                },
                buyer =
                new Trader()
                {
                    Id = 2,
                    name = "Yves",
                    surname = "Guillemot",
                    phone = "+33()",
                    money = 4050m,
                },
                sellerShare = new Share()
                {
                    name = "Ubisoft",
                    price = 10m,
                    quantity = 5,
                    ownerId = 1,
                },
                buyerShare = new Share()
                {
                    name = "Ubisoft",
                    price = 10m,
                    quantity = 5,
                    ownerId = 2,
                },
            };

            // Act
            transactionService.Save(transaction);

            // Assert
            traderRep.Received(1).Push(transaction.seller);
            traderRep.Received(1).Push(transaction.buyer);
            shareRep.Received(1).Push(transaction.sellerShare);
            shareRep.Received(1).Push(transaction.buyerShare);
            transactionRep.Received(1).Push(transaction);
            transactionRep.Received(1).SaveChanges();
            logger.Received(1).Info(transaction.ToString());
        }
        [TestMethod]
        public void ShouldSaveTransactionWithZeroSellerShare()
        {// Arrange
            TransactionService transactionService = new TransactionService(
                this.traderRep,
                this.shareRep,
                this.transactionRep,
                this.logger);

            Transaction transaction = new Transaction()
            {
                seller = new Trader()
                {
                    Id = 1,
                    name = "Brian",
                    surname = "Kelly",
                    phone = "+1()",
                    money = 5050m,
                },
                buyer =
                new Trader()
                {
                    Id = 2,
                    name = "Yves",
                    surname = "Guillemot",
                    phone = "+33()",
                    money = 4050m,
                },
                sellerShare = new Share()
                {
                    name = "Ubisoft",
                    price = 10m,
                    quantity = 0,
                    ownerId = 1,
                },
                buyerShare = new Share()
                {
                    name = "Ubisoft",
                    price = 10m,
                    quantity = 5,
                    ownerId = 2,
                },
            };

            // Act
            transactionService.Save(transaction);

            // Assert
            traderRep.Received(1).Push(transaction.seller);
            traderRep.Received(1).Push(transaction.buyer);
            shareRep.Received(1).Remove(transaction.sellerShare);
            shareRep.Received(1).Push(transaction.buyerShare);
            transactionRep.Received(1).Push(transaction);
            transactionRep.Received(1).SaveChanges();
            logger.Received(1).Info(transaction.ToString());
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Transaction must not be null or continence field with null")]
        public void ShouldNotSaveTransaction()
        {// Arrange
            TransactionService transactionService = new TransactionService(
                this.traderRep,
                this.shareRep,
                this.transactionRep,
                this.logger);

            Transaction transaction = null;

            // Act
            transactionService.Save(transaction);
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Transaction must not be null or continence field with null")]
        public void ShouldNotSaveTransactionWithoutSeller()
        {// Arrange
            TransactionService transactionService = new TransactionService(
                this.traderRep,
                this.shareRep,
                this.transactionRep,
                this.logger);

            Transaction transaction = new Transaction()
            {
                seller = null,
                buyer =
                new Trader()
                {
                    Id = 2,
                    name = "Yves",
                    surname = "Guillemot",
                    phone = "+33()",
                    money = 4050m,
                },
                sellerShare = new Share()
                {
                    name = "Ubisoft",
                    price = 10m,
                    quantity = 5,
                    ownerId = 1,
                },
                buyerShare = new Share()
                {
                    name = "Ubisoft",
                    price = 10m,
                    quantity = 5,
                    ownerId = 2,
                },
            };

            // Act
            transactionService.Save(transaction);
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Transaction must not be null or continence field with null")]
        public void ShouldNotSaveTransactionWithoutBuyer()
        {// Arrange
            TransactionService transactionService = new TransactionService(
                this.traderRep,
                this.shareRep,
                this.transactionRep,
                this.logger);

            Transaction transaction = new Transaction()
            {
                seller = new Trader()
                {
                    Id = 1,
                    name = "Brian",
                    surname = "Kelly",
                    phone = "+1()",
                    money = 5050m,
                },
                buyer = null,
                sellerShare = new Share()
                {
                    name = "Ubisoft",
                    price = 10m,
                    quantity = 5,
                    ownerId = 1,
                },
                buyerShare = new Share()
                {
                    name = "Ubisoft",
                    price = 10m,
                    quantity = 5,
                    ownerId = 2,
                },
            }; ;

            // Act
            transactionService.Save(transaction);
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Transaction must not be null or continence field with null")]
        public void ShouldNotSaveTransactionWithoutSellerShare()
        {// Arrange
            TransactionService transactionService = new TransactionService(
                this.traderRep,
                this.shareRep,
                this.transactionRep,
                this.logger);

            Transaction transaction = new Transaction()
            {
                seller = new Trader()
                {
                    Id = 1,
                    name = "Brian",
                    surname = "Kelly",
                    phone = "+1()",
                    money = 5050m,
                },
                buyer =
                new Trader()
                {
                    Id = 2,
                    name = "Yves",
                    surname = "Guillemot",
                    phone = "+33()",
                    money = 4050m,
                },
                sellerShare = null,
                buyerShare = new Share()
                {
                    name = "Ubisoft",
                    price = 10m,
                    quantity = 5,
                    ownerId = 2,
                },
            }; ;

            // Act
            transactionService.Save(transaction);
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Transaction must not be null or continence field with null")]
        public void ShouldNotSaveTransactionWithoutBuyerShare()
        {// Arrange
            TransactionService transactionService = new TransactionService(
                this.traderRep,
                this.shareRep,
                this.transactionRep,
                this.logger);

            Transaction transaction = new Transaction()
            {
                seller = new Trader()
                {
                    Id = 1,
                    name = "Brian",
                    surname = "Kelly",
                    phone = "+1()",
                    money = 5050m,
                },
                buyer =
                new Trader()
                {
                    Id = 2,
                    name = "Yves",
                    surname = "Guillemot",
                    phone = "+33()",
                    money = 4050m,
                },
                sellerShare = new Share()
                {
                    name = "Ubisoft",
                    price = 10m,
                    quantity = 5,
                    ownerId = 1,
                },
                buyerShare = null,
            };

            // Act
            transactionService.Save(transaction);
        }
    }
}