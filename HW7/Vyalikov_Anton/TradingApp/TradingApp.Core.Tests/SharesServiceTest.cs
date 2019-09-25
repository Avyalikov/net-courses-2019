namespace TradingApp.Core.Tests
{
    using TradingApp.Core.DTO;
    using TradingApp.Core.Models;
    using TradingApp.Core.Services;
    using TradingApp.Core.Repos;
    using NSubstitute;
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SharesServiceTest
    {
        [TestMethod]
        public void RegisterShareTest()
        {
            // Arrange
            var shareRepositoryMock = Substitute.For<ISharesRepository>();

            var sut = new SharesService(shareRepositoryMock);

            ShareRegistrationData share = new ShareRegistrationData
            {
                ShareType = "Toyota",
                SharePrice = (decimal)6021023
            };

            // Act
            sut.RegisterShare(share);

            // Asserts
            shareRepositoryMock.Received(1).Insert(Arg.Is<Share>(x => x.ShareType == share.ShareType && x.Price == share.SharePrice));
        }

        [TestMethod]
        public void GetShareTypeTest()
        {
            // Arrange
            var shareRepositoryMock = Substitute.For<ISharesRepository>();

            var sut = new SharesService(shareRepositoryMock);

            int shareID = 10;
            shareRepositoryMock
                .DoesShareExists(Arg.Is<int>(shareID))
                .Returns(true);

            // Act
            sut.GetShareType(shareID);

            // Asserts
            shareRepositoryMock.Received(1).GetShareType(Arg.Is<int>(shareID));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),
           "There is no shares with id = 123")]
        public void GetShareTypeDidntExistTest()
        {
            // Arrange
            var shareRepositoryMock = Substitute.For<ISharesRepository>();

            var sut = new SharesService(shareRepositoryMock);

            int shareID = 12;
            shareRepositoryMock
                .DoesShareExists(Arg.Is<int>(shareID))
                .Returns(false);

            // Act
            sut.GetShareType(shareID);

            // Asserts
            shareRepositoryMock.DidNotReceive().GetShareType(Arg.Is<int>(shareID));
        }

        [TestMethod]
        public void GetShareIDTest()
        {
            // Arrange
            var shareRepositoryMock = Substitute.For<ISharesRepository>();

            var sut = new SharesService(shareRepositoryMock);

            string shareType = "Nissan";
            shareRepositoryMock
                .DoesShareExists(Arg.Is<string>(shareType))
                .Returns(true);

            // Act
            sut.GetShareIDByType(shareType);

            // Asserts
            shareRepositoryMock.Received(1).GetShareIDByType(Arg.Is<string>(shareType));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),
           "There is no shares with type = Nissan")]
        public void GetShareIDDidntExistTest()
        {
            // Arrange
            var shareRepositoryMock = Substitute.For<ISharesRepository>();

            var sut = new SharesService(shareRepositoryMock);

            string shareType = "Nissan";
            shareRepositoryMock
                .DoesShareExists(Arg.Is<string>(shareType))
                .Returns(false);

            // Act
            sut.GetShareIDByType(shareType);

            // Asserts
            shareRepositoryMock.DidNotReceive().GetShareIDByType(Arg.Is<string>(shareType));
        }

        [TestMethod]
        public void GetSharePriceTest()
        {
            // Arrange
            var shareRepositoryMock = Substitute.For<ISharesRepository>();

            var sut = new SharesService(shareRepositoryMock);

            int shareID = 135;
            shareRepositoryMock
                .DoesShareExists(Arg.Is<int>(shareID))
                .Returns(true);

            // Act
            sut.GetSharePrice(shareID);

            // Asserts
            shareRepositoryMock.Received(1).GetSharePrice(Arg.Is<int>(shareID));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),
           "There is no shares with id = 123")]
        public void GetSharePriceDidntExistTest()
        {
            // Arrange
            var shareRepositoryMock = Substitute.For<ISharesRepository>();

            var sut = new SharesService(shareRepositoryMock);

            int shareID = 135;
            shareRepositoryMock
                .DoesShareExists(Arg.Is<int>(shareID))
                .Returns(false);

            // Act
            sut.GetSharePrice(shareID);

            // Asserts
            shareRepositoryMock.DidNotReceive().GetSharePrice(Arg.Is<int>(shareID));
        }

        [TestMethod]
        public void GetAllSharesTest()
        {
            // Arrange
            var shareRepositoryMock = Substitute.For<ISharesRepository>();

            var sut = new SharesService(shareRepositoryMock);

            // Act
            sut.GetAllShares();

            // Asserts
            shareRepositoryMock.Received(1).GetAllShares();
        }
    }
}
