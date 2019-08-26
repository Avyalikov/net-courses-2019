namespace TradingSoftware.Core.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using TradingSoftware.Core.Models;
    using TradingSoftware.Core.Repositories;
    using TradingSoftware.Core.Services;

    [TestClass]
    public class ShareManagerTests
    {
        [TestMethod]
        public void AddTest()
        {
            // Arrange
            var shareRepositoryMock = Substitute.For<ISharesRepository>();

            var sut = new ShareManager(shareRepositoryMock);

            Share share = new Share
            {
                ShareType = "Umbrella",
                Price = (decimal)6021023
            };

            // Act
            sut.AddShare(share);

            // Asserts
            shareRepositoryMock.Received(1).Insert(Arg.Is<Share>(share));
        }

        [TestMethod]
        public void AddParametrsTest()
        {
            // Arrange
            var shareRepositoryMock = Substitute.For<ISharesRepository>();

            var sut = new ShareManager(shareRepositoryMock);

            string shareType = "Weyland-Yutani";
            decimal sharePrice = (decimal)642134;

            // Act
            sut.AddShare(shareType, sharePrice);

            // Asserts
            shareRepositoryMock.Received(1).Insert(Arg.Is<Share>(s => s.ShareType == shareType &&
                                                                      s.Price == sharePrice));
        }

        [TestMethod]
        public void GetShareTypeTest()
        {
            // Arrange
            var shareRepositoryMock = Substitute.For<ISharesRepository>();

            var sut = new ShareManager(shareRepositoryMock);

            int shareID = 12;
            shareRepositoryMock
                .IsShareExist(Arg.Is<int>(shareID))
                .Returns(true);

            // Act
            sut.GetShareType(shareID);

            // Asserts
            shareRepositoryMock.Received(1).GetShareType(Arg.Is<int>(shareID));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),
           "There is no shares with id = 135")]
        public void GetShareTypeDidntExistTest()
        {
            // Arrange
            var shareRepositoryMock = Substitute.For<ISharesRepository>();

            var sut = new ShareManager(shareRepositoryMock);

            int shareID = 12;
            shareRepositoryMock
                .IsShareExist(Arg.Is<int>(shareID))
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

            var sut = new ShareManager(shareRepositoryMock);

            string shareType = "Umbrella";
            shareRepositoryMock
                .IsShareExist(Arg.Is<string>(shareType))
                .Returns(true);

            // Act
            sut.GetShareID(shareType);

            // Asserts
            shareRepositoryMock.Received(1).GetShareID(Arg.Is<string>(shareType));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),
           "There is no shares with type = Umbrella")]
        public void GetShareIDDidntExistTest()
        {
            // Arrange
            var shareRepositoryMock = Substitute.For<ISharesRepository>();

            var sut = new ShareManager(shareRepositoryMock);

            string shareType = "Umbrella";
            shareRepositoryMock
                .IsShareExist(Arg.Is<string>(shareType))
                .Returns(false);

            // Act
            sut.GetShareID(shareType);
            
            // Asserts
            shareRepositoryMock.DidNotReceive().GetShareID(Arg.Is<string>(shareType));
        }

        [TestMethod]
        public void GetNumberOfSharesTest()
        {
            // Arrange
            var shareRepositoryMock = Substitute.For<ISharesRepository>();

            var sut = new ShareManager(shareRepositoryMock);

            // Act
            sut.GetNumberOfShares();

            // Asserts
            shareRepositoryMock.Received(1).GetNumberOfShares();
        }

        [TestMethod]
        public void GetSharePriceTest()
        {
            // Arrange
            var shareRepositoryMock = Substitute.For<ISharesRepository>();

            var sut = new ShareManager(shareRepositoryMock);

            int shareID = 135;
            shareRepositoryMock
                .IsShareExist(Arg.Is<int>(shareID))
                .Returns(true);

            // Act
            sut.GetSharePrice(shareID);

            // Asserts
            shareRepositoryMock.Received(1).GetSharePrice(Arg.Is<int>(shareID));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),
           "There is no shares with id = 135")]
        public void GetSharePriceDidntExistTest()
        {
            // Arrange
            var shareRepositoryMock = Substitute.For<ISharesRepository>();

            var sut = new ShareManager(shareRepositoryMock);

            int shareID = 135;
            shareRepositoryMock
                .IsShareExist(Arg.Is<int>(shareID))
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

            var sut = new ShareManager(shareRepositoryMock);

            // Act
            sut.GetNumberOfShares();

            // Asserts
            shareRepositoryMock.Received(1).GetNumberOfShares();
        }

        [TestMethod]
        public void IsShareExistTest()
        {
            // Arrange
            var shareRepositoryMock = Substitute.For<ISharesRepository>();

            var sut = new ShareManager(shareRepositoryMock);

            int shareID = 135;
            
            // Act
            sut.IsShareExist(shareID);

            // Asserts
            shareRepositoryMock.Received(1).IsShareExist(Arg.Is<int>(shareID));
        }

        [TestMethod]
        public void IsShareExistShareTypeTest()
        {
            // Arrange
            var shareRepositoryMock = Substitute.For<ISharesRepository>();

            var sut = new ShareManager(shareRepositoryMock);

            string shareType = "Umbrella";

            // Act
            sut.IsShareExist(shareType);

            // Asserts
            shareRepositoryMock.Received(1).IsShareExist(Arg.Is<string>(shareType));
        }

        [TestMethod]
        public void ChangeSharePriceTest()
        {
            // Arrange
            var shareRepositoryMock = Substitute.For<ISharesRepository>();

            var sut = new ShareManager(shareRepositoryMock);

            int shareID = 135;
            decimal sharePrice = 1314095;
            shareRepositoryMock
               .IsShareExist(Arg.Is<int>(shareID))
               .Returns(true);

            // Act
            sut.ChangeSharePrice(shareID, sharePrice);
            
            // Asserts
            shareRepositoryMock.Received(1).ChangeSharePrice(Arg.Is<int>(shareID), Arg.Is<decimal>(sharePrice));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),
           "There is no clients with id 135")]
        public void ChangeSharePriceDidntExistTest()
        {
            // Arrange
            var shareRepositoryMock = Substitute.For<ISharesRepository>();

            var sut = new ShareManager(shareRepositoryMock);

            int shareID = 135;
            decimal sharePrice = 1314095;
            shareRepositoryMock
               .IsShareExist(Arg.Is<int>(shareID))
               .Returns(false);

            // Act
            sut.ChangeSharePrice(shareID, sharePrice);

            // Asserts
            shareRepositoryMock.DidNotReceive().ChangeSharePrice(Arg.Is<int>(shareID), Arg.Is<decimal>(sharePrice));
        }
    }
}
