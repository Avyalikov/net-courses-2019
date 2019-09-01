namespace TradingSoftware.Core.Tests
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using TradingSoftware.Core.Models;
    using TradingSoftware.Core.Repositories;
    using TradingSoftware.Core.Services;

    [TestClass]
    public class BlockOfSharesManagerTests
    {
        [TestMethod]
        public void ShouldAddShare()
        {
            // Arrange
            var blockOfSharesRepositoryMock = Substitute.For<IBlockOfSharesRepository>();
            var clientRepositoryMock = Substitute.For<IClientRepository>();
            var sharesRepositoryMock = Substitute.For<ISharesRepository>();

            var sut = new BlockOfSharesManager(
                blockOfSharesRepositoryMock,
                clientRepositoryMock,
                sharesRepositoryMock);

            BlockOfShares blockOfShares = new BlockOfShares();

            // Act
            sut.AddShare(blockOfShares);

            // Asserts
            blockOfSharesRepositoryMock.Received(1).Insert(Arg.Is<BlockOfShares>(blockOfShares));
        }

        [TestMethod]
        public void ShouldCheckIsClientHasStockType()
        {
            // Arrange
            var blockOfSharesRepositoryMock = Substitute.For<IBlockOfSharesRepository>();
            var clientRepositoryMock = Substitute.For<IClientRepository>();
            var sharesRepositoryMock = Substitute.For<ISharesRepository>();

            var sut = new BlockOfSharesManager(
                blockOfSharesRepositoryMock,
                clientRepositoryMock,
                sharesRepositoryMock);

            int clientID = 2;
            int shareID = 3;

            // Act
            sut.IsClientHasStockType(clientID, shareID);

            // Asserts
            blockOfSharesRepositoryMock.Received(1).IsClientHasShareType(Arg.Is<int>(clientID), Arg.Is<int>(shareID));
        }

        [TestMethod]
        public void ShouldChangeShareAmountForClient()
        {
            // Arrange
            var blockOfSharesRepositoryMock = Substitute.For<IBlockOfSharesRepository>();
            var clientRepositoryMock = Substitute.For<IClientRepository>();
            var sharesRepositoryMock = Substitute.For<ISharesRepository>();

            var sut = new BlockOfSharesManager(
                blockOfSharesRepositoryMock,
                clientRepositoryMock,
                sharesRepositoryMock);

            BlockOfShares blockOfShares = new BlockOfShares { ClientID = 3, ShareID = 6, Amount = 10 };

            // Act
            sut.ChangeShareAmountForClient(blockOfShares);

            // Asserts
            blockOfSharesRepositoryMock.Received(1).ChangeShareAmountForClient(Arg.Is<BlockOfShares>(blockOfShares));
        }

        [TestMethod]
        public void ShouldGetClientShareAmount()
        {
            // Arrange
            var blockOfSharesRepositoryMock = Substitute.For<IBlockOfSharesRepository>();
            var clientRepositoryMock = Substitute.For<IClientRepository>();
            var sharesRepositoryMock = Substitute.For<ISharesRepository>();

            var sut = new BlockOfSharesManager(
                blockOfSharesRepositoryMock,
                clientRepositoryMock,
                sharesRepositoryMock);
            int clientID = 3;
            int shareID = 5;

            // Act
            sut.GetClientShareAmount(clientID, shareID);

            // Asserts
            blockOfSharesRepositoryMock.Received(1).GetClientShareAmount(Arg.Is<int>(clientID), Arg.Is<int>(shareID));
        }

        [TestMethod]
        public void ShouldGetAllBlockOfShares()
        {
            // Arrange
            var blockOfSharesRepositoryMock = Substitute.For<IBlockOfSharesRepository>();
            var clientRepositoryMock = Substitute.For<IClientRepository>();
            var sharesRepositoryMock = Substitute.For<ISharesRepository>();

            var sut = new BlockOfSharesManager(
                blockOfSharesRepositoryMock,
                clientRepositoryMock,
                sharesRepositoryMock);

            // Act
            sut.GetAllBlockOfShares();

            // Asserts
            blockOfSharesRepositoryMock.Received(1).GetAllBlockOfShares();
        }

        [TestMethod]
        public void ShouldGetClientShares()
        {
            // Arrange
            var blockOfSharesRepositoryMock = Substitute.For<IBlockOfSharesRepository>();
            var clientRepositoryMock = Substitute.For<IClientRepository>();
            var sharesRepositoryMock = Substitute.For<ISharesRepository>();

            var sut = new BlockOfSharesManager(
                blockOfSharesRepositoryMock,
                clientRepositoryMock,
                sharesRepositoryMock);

            int clientID = 1;
            int firstShareID = 3;
            int secondShareID = 7;

            var blockOfShares = new[] 
            {
                new BlockOfShares { ClientID = clientID, ShareID = firstShareID, Amount = 1 },
                new BlockOfShares { ClientID = clientID, ShareID = secondShareID, Amount = 3 }
            };

            blockOfSharesRepositoryMock
                .GetClientShares(clientID)
                .Returns(blockOfShares);

            sharesRepositoryMock
                .GetShareType(3)
                .Returns("Umbrella");

            sharesRepositoryMock
                .GetSharePrice(3)
                .Returns(123);

            sharesRepositoryMock
                .GetShareType(7)
                .Returns("Weyland-Yutani");

            sharesRepositoryMock
               .GetSharePrice(7)
               .Returns(321);

            // Act
            var clientSharesResult = sut.GetClientShares(clientID);

            // Asserts
            Assert.AreEqual(clientSharesResult.ShareWithPrice.ElementAt(0).Key, "Umbrella");
            Assert.AreEqual(clientSharesResult.ShareWithPrice.ElementAt(0).Value, 123);

            Assert.AreEqual(clientSharesResult.ShareWithPrice.ElementAt(1).Key, "Weyland-Yutani");
            Assert.AreEqual(clientSharesResult.ShareWithPrice.ElementAt(1).Value, 321);
        }

        [TestMethod]
        public void ShouldCreateClientSharesInsteadOfUpdatingClientSharesThatDidntExist()
        {
            // Arrange
            var blockOfSharesRepositoryMock = Substitute.For<IBlockOfSharesRepository>();
            var clientRepositoryMock = Substitute.For<IClientRepository>();
            var sharesRepositoryMock = Substitute.For<ISharesRepository>();

            var sut = new BlockOfSharesManager(
                blockOfSharesRepositoryMock,
                clientRepositoryMock,
                sharesRepositoryMock);

            int clientID = 1;
            int shareID = 3;
            int amount = 13;
            var blockOfShareToUpdate =
                new BlockOfShares
                {
                    ClientID = clientID,
                    ShareID = shareID,
                    Amount = amount
                };

            blockOfSharesRepositoryMock
                .IsClientHasShareType(
                    blockOfShareToUpdate.ClientID,
                    blockOfShareToUpdate.ShareID)
                .Returns(false);

            // Act
            sut.UpdateClientShares(blockOfShareToUpdate);

            // Asserts
            blockOfSharesRepositoryMock
                .Received(1)
                .Insert(blockOfShareToUpdate);
        }

        [TestMethod]
        public void ShouldUpdateClientShares()
        {
            // Arrange
            var blockOfSharesRepositoryMock = Substitute.For<IBlockOfSharesRepository>();
            var clientRepositoryMock = Substitute.For<IClientRepository>();
            var sharesRepositoryMock = Substitute.For<ISharesRepository>();

            var sut = new BlockOfSharesManager(
                blockOfSharesRepositoryMock,
                clientRepositoryMock,
                sharesRepositoryMock);

            int clientID = 1;
            int shareID = 3;
            int amount = 13;
            var blockOfShareToUpdate =
                new BlockOfShares
                {
                    ClientID = clientID,
                    ShareID = shareID,
                    Amount = amount
                };

            blockOfSharesRepositoryMock
                .IsClientHasShareType(
                    blockOfShareToUpdate.ClientID,
                    blockOfShareToUpdate.ShareID)
                .Returns(true);

            // Act
            sut.UpdateClientShares(blockOfShareToUpdate);

            // Asserts
            blockOfSharesRepositoryMock
                .Received(1)
                .ChangeShareAmountForClient(blockOfShareToUpdate);
        }

        [TestMethod]
        public void ShouldDeleteBlockOfShares()
        {
            // Arrange
            var blockOfSharesRepositoryMock = Substitute.For<IBlockOfSharesRepository>();
            var clientRepositoryMock = Substitute.For<IClientRepository>();
            var sharesRepositoryMock = Substitute.For<ISharesRepository>();

            var sut = new BlockOfSharesManager(
                blockOfSharesRepositoryMock,
                clientRepositoryMock,
                sharesRepositoryMock);

            int clientID = 1;
            int shareID = 3;
            int amount = 13;
            var blockOfShareToUpdate =
                new BlockOfShares
                {
                    ClientID = clientID,
                    ShareID = shareID,
                    Amount = amount
                };

            // Act
            sut.Delete(blockOfShareToUpdate);

            // Asserts
            blockOfSharesRepositoryMock
                .Received(1)
                .Remove(blockOfShareToUpdate);
        }
    }
}