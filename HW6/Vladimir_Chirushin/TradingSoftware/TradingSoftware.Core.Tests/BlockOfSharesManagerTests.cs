namespace TradingSoftware.Core.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using TradingSoftware.Core.Models;
    using TradingSoftware.Core.Repositories;
    using TradingSoftware.Core.Services;

    [TestClass]
    public class BlockOfSharesManagerTests
    {
        [TestMethod]
        public void AddShareTest()
        {
            // Arrange
            var blockOfSharesRepositoryMock = Substitute.For<IBlockOfSharesRepository>();
            
            var sut = new BlockOfSharesManager(blockOfSharesRepositoryMock);

            BlockOfShares blockOfShares = new BlockOfShares();

            // Act
            sut.AddShare(blockOfShares);

            // Asserts
            blockOfSharesRepositoryMock.Received(1).Insert(Arg.Is<BlockOfShares>(blockOfShares));
        }

        [TestMethod]
        public void IsClientHasStockTypeTest()
        {
            // Arrange
            var blockOfSharesRepositoryMock = Substitute.For<IBlockOfSharesRepository>();

            var sut = new BlockOfSharesManager(blockOfSharesRepositoryMock);

            int clientID = 2;
            int shareID = 3;

            // Act
            sut.IsClientHasStockType(clientID, shareID);

            // Asserts
            blockOfSharesRepositoryMock.Received(1).IsClientHasShareType(Arg.Is<int>(clientID), Arg.Is<int>(shareID));
        }

        [TestMethod]
        public void ChangeShareAmountForClientTest()
        {
            // Arrange
            var blockOfSharesRepositoryMock = Substitute.For<IBlockOfSharesRepository>();

            var sut = new BlockOfSharesManager(blockOfSharesRepositoryMock);

            BlockOfShares blockOfShares = new BlockOfShares { ClientID = 3, ShareID = 6, Amount = 10 };

            // Act
            sut.ChangeShareAmountForClient(blockOfShares);

            // Asserts
            blockOfSharesRepositoryMock.Received(1).ChangeShareAmountForClient(Arg.Is<BlockOfShares>(blockOfShares));
        }

        [TestMethod]
        public void GetClientShareAmountTest()
        {
            // Arrange
            var blockOfSharesRepositoryMock = Substitute.For<IBlockOfSharesRepository>();
            var sut = new BlockOfSharesManager(blockOfSharesRepositoryMock);
            int clientID = 3;
            int shareID = 5;

            // Act
            sut.GetClientShareAmount(clientID, shareID);

            // Asserts
            blockOfSharesRepositoryMock.Received(1).GetClientShareAmount(Arg.Is<int>(clientID), Arg.Is<int>(shareID));
        }

        [TestMethod]
        public void GetAllBlockOfSharesTest()
        {
            // Arrange
            var blockOfSharesRepositoryMock = Substitute.For<IBlockOfSharesRepository>();
            var sut = new BlockOfSharesManager(blockOfSharesRepositoryMock);

            // Act
            sut.GetAllBlockOfShares();

            // Asserts
            blockOfSharesRepositoryMock.Received(1).GetAllBlockOfShares();
        }
    }
}