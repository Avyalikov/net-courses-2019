namespace TradingApp.Core.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using NSubstitute.ReturnsExtensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TradingApp.Core.Models;
    using TradingApp.Core.Repositories;
    using TradingApp.Core.Services;

    [TestClass]
    public class ShareServiceTests
    {
        private IRepository<ShareEntity> shareRepository;
        private IRepository<ShareTypeEntity> shareTypeRepository;
        [TestInitialize]
        public void Initialize()
        {
            this.shareRepository = Substitute.For<IRepository<ShareEntity>>();
            this.shareTypeRepository = Substitute.For<IRepository<ShareTypeEntity>>();
            this.shareRepository.GetAll().Returns(new List<ShareEntity>
            {
                new ShareEntity
                {
                    Id = 1,
                    Owner = new TraderEntity { Id = 1 }
                },
                new ShareEntity
                {
                    Id = 2,
                    Owner = new TraderEntity { Id = 2 }
                },
                new ShareEntity
                {
                    Id = 3,
                    Owner = new TraderEntity { Id = 3 }
                }
            });
        }
        [TestMethod]
        public void ShouldReturnSharesForExactId()
        {
            // Arrange
            var shareService = new ShareService(this.shareRepository, this.shareTypeRepository);

            // Act
            var tradersShares = shareService.GetAllSharesByTraderId(1);

            // Assert
            shareRepository.Received(2).GetAll();
            Assert.IsFalse(tradersShares.Any(s => s.Owner.Id != 1));
        }
        [TestMethod]
        public void ShouldChangeShareType()
        {
            // Arrange
            var shareService = new ShareService(this.shareRepository, this.shareTypeRepository);
            ShareTypeEntity firstShareType = new ShareTypeEntity { Id = 1 };
            ShareTypeEntity secondShareType = new ShareTypeEntity { Id = 2 };
            ShareEntity shareToChange = new ShareEntity { };

            this.shareRepository.GetById(Arg.Is<int>(1)).Returns(shareToChange);
            this.shareTypeRepository.GetById(Arg.Is<int>(1)).Returns(firstShareType);
            this.shareTypeRepository.GetById(Arg.Is<int>(2)).Returns(secondShareType);

            // Act
            shareService.ChangeShareType(1, 2);

            // Assert
            this.shareRepository.Received(1).Save(shareToChange);
            Assert.IsTrue(shareToChange.ShareType == secondShareType);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "There is no share with given Id.")]
        public void ShouldNotChangeShareTypeIfShareDoesntExist()
        {
            // Arrange
            var shareService = new ShareService(this.shareRepository, this.shareTypeRepository);
            this.shareRepository.GetById(Arg.Is<int>(1)).ReturnsNull();

            // Act
            shareService.ChangeShareType(1, 1);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "There is no share type with given Id.")]
        public void ShouldNotChangeShareTypeIfShareTypeDoesntExist()
        {
            // Arrange
            var shareService = new ShareService(this.shareRepository, this.shareTypeRepository);
            this.shareRepository.GetById(Arg.Is<int>(1)).Returns(new ShareEntity { });
            this.shareTypeRepository.GetById(Arg.Is<int>(1)).ReturnsNull();

            // Act
            shareService.ChangeShareType(1, 1);
        }
    }
}
