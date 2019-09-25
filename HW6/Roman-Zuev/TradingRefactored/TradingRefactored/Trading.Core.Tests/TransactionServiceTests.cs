using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Trading.Core.Dto;
using Trading.Core.Models;
using Trading.Core.Repositories;
using Trading.Core.Services;

namespace Trading.Core.Tests
{

    [TestClass]
    public class TransactionServiceTests
    {
        IClientTableRepository clientTableRepository;
        ISharesTableRepository sharesTableRepository;
        IClientSharesTableRepository clientSharesTableRepository;
        ITransactionHistoryTableRepository transactionHistoryTableRepository;
        TransactionService transactionService;

        [TestInitialize]
        public void Initialize()
        {
            clientTableRepository = Substitute.For<IClientTableRepository>();
            sharesTableRepository = Substitute.For<ISharesTableRepository>();
            clientSharesTableRepository = Substitute.For<IClientSharesTableRepository>();
            transactionHistoryTableRepository = Substitute.For<ITransactionHistoryTableRepository>();
            clientTableRepository.ContainsById(1).Returns(true);
            clientTableRepository.ContainsById(2).Returns(true);
            clientTableRepository.ContainsById(3).Returns(false);
            sharesTableRepository.ContainsById(1).Returns(true);
            sharesTableRepository.ContainsById(2).Returns(true);
            sharesTableRepository.ContainsById(3).Returns(true);
            sharesTableRepository.ContainsById(4).Returns(false);
            clientTableRepository.GetById(1).Returns(new ClientEntity()
            {
                Id = 1,
                Balance = 0M,
                Name = "Seller",
                Portfolio = new List<ClientSharesEntity>()
                {
                    new ClientSharesEntity()
                    {
                        Id = 1,
                        Quantity = 50,
                        Shares = new SharesEntity
                        {
                            Id = 1,
                            Price = 10M,
                            SharesType = "TypeA"
                        }
                    },

                    new ClientSharesEntity()
                    {
                        Id = 2,
                        Quantity = 10,
                        Shares = new SharesEntity
                        {
                            Id = 3,
                            Price = 15M,
                            SharesType = "TypeC"
                        }
                    }
                }
            });
            clientTableRepository.GetById(2).Returns(new ClientEntity()
            {
                Id = 2,
                Balance = 100M,
                Name = "Buyer",
                Portfolio = new List<ClientSharesEntity>()
                {
                    new ClientSharesEntity()
                    {
                        Id = 2,
                        Quantity = 0,
                        Shares = new SharesEntity
                        {
                            Id = 2,
                            Price = 5M,
                            SharesType = "TypeB"
                        }
                    },

                    new ClientSharesEntity()
                    {
                        Id = 3,
                        Quantity = 15,
                        Shares = new SharesEntity
                        {
                            Id = 1,
                            Price = 10M,
                            SharesType = "TypeA"
                        }
                    }
                }
            });
            sharesTableRepository.GetById(1).Returns(new SharesEntity()
            {
                Id = 1,
                Price = 10M,
                SharesType = "TypeA"
            });
            sharesTableRepository.GetById(2).Returns(new SharesEntity()
            {
                Id = 2,
                Price = 5M,
                SharesType = "TypeB"
            });
            sharesTableRepository.GetById(3).Returns(new SharesEntity()
            {
                Id = 3,
                Price = 15M,
                SharesType = "TypeC"
            });

            transactionService = new TransactionService(
                clientTableRepository,
                clientSharesTableRepository,
                sharesTableRepository,
                transactionHistoryTableRepository);
        }

        [TestMethod]
        public void ShouldChangeSellersBalance()
        {
            //Arrange
            TransactionArguments args = new TransactionArguments();
            args.SellerId = 1;
            args.BuyerId = 2;
            args.SharesId = 1;
            args.Quantity = 5;
            var balanceBeforeChange = clientTableRepository.GetById(1).Balance;
            var sum = sharesTableRepository.GetById(args.SharesId).Price * args.Quantity;

            //Act
            transactionService.MakeTransaction(args);
            //Assert
            clientTableRepository.Received(1).Change(Arg.Is<ClientEntity>(s =>
            s.Name == "Seller"
            && s.Balance == balanceBeforeChange + sum));
            clientTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        public void ShouldChangeBuyersBalance()
        {
            //Arrange
            TransactionArguments args = new TransactionArguments();
            args.SellerId = 1;
            args.BuyerId = 2;
            args.SharesId = 1;
            args.Quantity = 5;
            var balanceBeforeChange = clientTableRepository.GetById(2).Balance;
            var sum = sharesTableRepository.GetById(args.SharesId).Price * args.Quantity;

            //Act
            transactionService.MakeTransaction(args);
            //Assert
            clientTableRepository.Received(1).Change(Arg.Is<ClientEntity>(s =>
            s.Name == "Buyer"
            && s.Balance == balanceBeforeChange - sum));
            clientTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        public void ShouldChangeSellersSharesQuantity()
        {
            //Arrange
            TransactionArguments args = new TransactionArguments();
            args.SellerId = 1;
            args.BuyerId = 2;
            args.SharesId = 1;
            args.Quantity = 5;
            var seller = clientTableRepository.GetById(1);
            ClientSharesEntity sellersItem = null;
            foreach (var item in seller.Portfolio)
            {
                if (item.Shares.Id == args.SharesId)
                {
                    sellersItem = item;
                }
            }
            var quantityBeforeChange = sellersItem.Quantity;

            //Act
            transactionService.MakeTransaction(args);
            //Assert
            clientSharesTableRepository.Received(1).Change(Arg.Is<ClientSharesEntity>(s =>
            s.Shares.Id == 1
            && s.Quantity == quantityBeforeChange - args.Quantity));
            clientTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        public void ShouldChangeBuyersSharesQuantity()
        {
            //Arrange
            TransactionArguments args = new TransactionArguments();
            args.SellerId = 1;
            args.BuyerId = 2;
            args.SharesId = 1;
            args.Quantity = 5;
            var buyer = clientTableRepository.GetById(2);
            ClientSharesEntity buyersItem = null;
            foreach (var item in buyer.Portfolio)
            {
                if (item.Shares.Id == args.SharesId)
                {
                    buyersItem = item;
                }
            }
            var quantityBeforeChange = buyersItem.Quantity;

            //Act
            transactionService.MakeTransaction(args);
            //Assert
            clientSharesTableRepository.Received(1).Change(Arg.Is<ClientSharesEntity>(s =>
            s.Shares.Id == 1
            && s.Quantity == quantityBeforeChange + args.Quantity));
            clientTableRepository.Received(1).SaveChanges();
        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "Transaction failed : Wrong quantity : 0")]
        public void ShouldThrowExceptionWrongQuantity()
        {
            //Arrange
            TransactionArguments args = new TransactionArguments();
            args.SellerId = 1;
            args.BuyerId = 2;
            args.SharesId = 1;
            args.Quantity = 0;

            //Act
            transactionService.MakeTransaction(args);
            //Assert

        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "Transaction failed : Client with Id -1 doesn't exist")]
        public void ShouldThrowExceptionSellerDoesntExists()
        {
            //Arrange
            TransactionArguments args = new TransactionArguments();
            args.SellerId = -1;
            args.BuyerId = 2;
            args.SharesId = 1;
            args.Quantity = 10;

            //Act
            transactionService.MakeTransaction(args);
            //Assert

        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "Transaction failed : Client with Id -2 doesn't exist")]
        public void ShouldThrowExceptionBuyerDoesntExists()
        {
            //Arrange
            TransactionArguments args = new TransactionArguments();
            args.SellerId = 1;
            args.BuyerId = -2;
            args.SharesId = 1;
            args.Quantity = 10;

            //Act
            transactionService.MakeTransaction(args);
            //Assert

        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "Transaction failed : Shares with Id -1 don't exist")]
        public void ShouldThrowExceptionSharesDoesntExists()
        {
            //Arrange
            TransactionArguments args = new TransactionArguments();
            args.SellerId = 1;
            args.BuyerId = 2;
            args.SharesId = -1;
            args.Quantity = 10;

            //Act
            transactionService.MakeTransaction(args);
            //Assert
            
        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "Transaction failed : Buyer is in the Black zone")]
        public void ShouldThrowExceptionBuyerInTheBlackZone()
        {
            //Arrange
            TransactionArguments args = new TransactionArguments();
            args.SellerId = 1;
            args.BuyerId = 2;
            args.SharesId = 1;
            args.Quantity = 10;
            clientTableRepository.GetById(2).Returns(new ClientEntity()
            {
                Id = 2,
                Balance = -100M
            });


            //Act
            transactionService.MakeTransaction(args);
            //Assert

        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "Transaction failed : Buyer is in the Orange zone")]
        public void ShouldThrowExceptionBuyerInTheOrangeZone()
        {
            //Arrange
            TransactionArguments args = new TransactionArguments();
            args.SellerId = 1;
            args.BuyerId = 2;
            args.SharesId = 1;
            args.Quantity = 10;
            clientTableRepository.GetById(2).Returns(new ClientEntity()
            {
                Id = 2,
                Balance = 0M
            });


            //Act
            transactionService.MakeTransaction(args);
            //Assert

        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "Transaction failed : Not enough shares to sell")]
        public void ShouldThrowExceptionNotEnoughShares()
        {
            //Arrange
            TransactionArguments args = new TransactionArguments();
            args.SellerId = 1;
            args.BuyerId = 2;
            args.SharesId = 1;
            args.Quantity = 10;
            clientTableRepository.GetById(1).Returns(new ClientEntity()
            {
                Id = 1,
                Balance = 0M,
                Name = "Seller",
                Portfolio = new List<ClientSharesEntity>()
                {
                    new ClientSharesEntity()
                    {
                        Id = 1,
                        Quantity = 0,
                        Shares = new SharesEntity
                        {
                            Id = 1,
                            Price = 10M,
                            SharesType = "TypeA"
                        }
                    }
                }
            });


            //Act
            transactionService.MakeTransaction(args);
            //Assert

        }
        [TestMethod]
        public void ShouldWriteHistoryOfSuccessfulTransactions()
        {
            //Arrange
            TransactionArguments args = new TransactionArguments();
            args.SellerId = 1;
            args.BuyerId = 2;
            args.SharesId = 1;
            args.Quantity = 5;

            //Act
            transactionService.MakeTransaction(args);

            //Assert
            transactionHistoryTableRepository.Received(1).Add(Arg.Is<TransactionHistoryEntity>(s =>
            s.Seller == clientTableRepository.GetById(args.SellerId)
            && s.Buyer == clientTableRepository.GetById(args.BuyerId)
            && s.SelledItem == sharesTableRepository.GetById(args.SharesId)
            && s.Quantity == args.Quantity
            && s.Total == sharesTableRepository.GetById(args.SharesId).Price * args.Quantity));
            transactionHistoryTableRepository.Received(1).SaveChanges();
        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "Transaction failed : Client with Id -1 doesn't exist") ]
        public void ShouldNotWriteHistoryOfTransactions()
        {
            //Arrange
            TransactionArguments args = new TransactionArguments();
            args.SellerId = -1;
            args.BuyerId = 2;
            args.SharesId = 1;
            args.Quantity = 5;

            //Act
            transactionService.MakeTransaction(args);

            //Assert
            transactionHistoryTableRepository.Received(0);
        }
    }
}
