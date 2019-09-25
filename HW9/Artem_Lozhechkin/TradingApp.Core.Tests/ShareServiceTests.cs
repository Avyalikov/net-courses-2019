namespace TradingApp.Core.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using NSubstitute.ReturnsExtensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TradingApp.Core.DTO;
    using TradingApp.Core.Models;
    using TradingApp.Core.Repositories;
    using TradingApp.Core.Services;

    [TestClass]
    public class ShareServiceTests
    {
        private IRepository<ShareEntity> shareRepository;
        private IRepository<ShareTypeEntity> shareTypeRepository;
        private IRepository<TraderEntity> traderRepository;
        private IRepository<StockEntity> stockRepository;

        [TestInitialize]
        public void Initialize()
        {
            this.shareRepository = Substitute.For<IRepository<ShareEntity>>();
            this.shareTypeRepository = Substitute.For<IRepository<ShareTypeEntity>>();
            this.traderRepository = Substitute.For<IRepository<TraderEntity>>();
            this.stockRepository = Substitute.For<IRepository<StockEntity>>();
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
                },
                new ShareEntity
                {
                    Id = 4,
                    Owner = new TraderEntity { Id = 1 }
                }
            });
        }
        [TestMethod]
        public void ShouldReturnSharesForExactId()
        {
            // Arrange
            var shareService = new ShareService(
                this.shareRepository,
                this.shareTypeRepository,
                this.stockRepository,
                this.traderRepository);

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
            var shareService = new ShareService(
                this.shareRepository,
                this.shareTypeRepository,
                this.stockRepository,
                this.traderRepository);
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
            var shareService = new ShareService(
                this.shareRepository,
                this.shareTypeRepository,
                this.stockRepository,
                this.traderRepository);
            this.shareRepository.GetById(Arg.Is<int>(1)).ReturnsNull();

            // Act
            shareService.ChangeShareType(1, 1);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "There is no share type with given Id.")]
        public void ShouldNotChangeShareTypeIfShareTypeDoesntExist()
        {
            // Arrange
            var shareService = new ShareService(
                this.shareRepository,
                this.shareTypeRepository,
                this.stockRepository,
                this.traderRepository);
            this.shareRepository.GetById(Arg.Is<int>(1)).Returns(new ShareEntity { });
            this.shareTypeRepository.GetById(Arg.Is<int>(1)).ReturnsNull();

            // Act
            shareService.ChangeShareType(1, 1);
        }
        [TestMethod]
        public void ShouldReturnSharesForExactTrader()
        {
            // Arrange
            var shareService = new ShareService(
                this.shareRepository,
                this.shareTypeRepository,
                this.stockRepository,
                this.traderRepository);
            int traderId = 1;
            // Act
            var shares = shareService.GetAllSharesByTraderId(traderId);
            // Assert
            Assert.IsTrue(shares.Count() == 2);
        }
        [TestMethod]
        public void ShouldNotReturnSharesForExactTrader()
        {
            // Arrange
            var shareService = new ShareService(
                this.shareRepository,
                this.shareTypeRepository,
                this.stockRepository,
                this.traderRepository);
            int traderId = 1;
            // Act
            var shares = shareService.GetAllSharesByTraderId(traderId);
            // Assert
            Assert.IsFalse(shares.Any(s => s.Owner.Id != 1));
        }
        [TestMethod]
        public void ShouldAddNewShare()
        {
            // Arrange
            var shareService = new ShareService(
                this.shareRepository,
                this.shareTypeRepository,
                this.stockRepository,
                this.traderRepository);
            ShareInfo shareInfo = new ShareInfo
            {
                Amount = 2,
                StockId = 1,
                ShareTypeId = 1,
                OwnerId = 1
            };
            this.shareTypeRepository.GetById(1)
                .Returns(new ShareTypeEntity { });
            this.stockRepository.GetById(1)
                .Returns(new StockEntity { });
            this.traderRepository.GetById(1)
                .Returns(new TraderEntity { });

            // Act
            shareService.AddNewShare(shareInfo);
            // Assert
            this.shareRepository.Received(1).Add(Arg.Any<ShareEntity>());
            this.shareRepository.Received(1).SaveChanges();
        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "There's no stock with given id in data source.")]
        public void ShouldNotAddNewShareIfStockDoesntExist()
        {
            // Arrange
            var shareService = new ShareService(
                this.shareRepository,
                this.shareTypeRepository,
                this.stockRepository,
                this.traderRepository);
            ShareInfo shareInfo = new ShareInfo
            {
                Amount = 2,
                StockId = 1,
                ShareTypeId = 1,
                OwnerId = 1
            };
            this.shareTypeRepository.GetById(1)
                .Returns(new ShareTypeEntity { });
            this.stockRepository.GetById(1)
                .ReturnsNull();
            this.traderRepository.GetById(1)
                .Returns(new TraderEntity { });

            // Act
            shareService.AddNewShare(shareInfo);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "There's no user with given id in data source.")]
        public void ShouldNotAddNewShareIfOwnerDoesntExist()
        {
            // Arrange
            var shareService = new ShareService(
                this.shareRepository,
                this.shareTypeRepository,
                this.stockRepository,
                this.traderRepository);
            ShareInfo shareInfo = new ShareInfo
            {
                Amount = 2,
                StockId = 1,
                ShareTypeId = 1,
                OwnerId = 1
            };
            this.shareTypeRepository.GetById(1)
                .Returns(new ShareTypeEntity { });
            this.stockRepository.GetById(1)
                .Returns(new StockEntity { });
            this.traderRepository.GetById(1)
                .ReturnsNull();

            // Act
            shareService.AddNewShare(shareInfo);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "There is no share type with given Id.")]
        public void ShouldNotAddNewShareIfShareTypeDoesntExist()
        {
            // Arrange
            var shareService = new ShareService(
                this.shareRepository,
                this.shareTypeRepository,
                this.stockRepository,
                this.traderRepository);
            ShareInfo shareInfo = new ShareInfo
            {
                Amount = 2,
                StockId = 1,
                ShareTypeId = 1,
                OwnerId = 1
            };
            this.stockRepository.GetById(1)
                .Returns(new StockEntity { });
            this.traderRepository.GetById(1)
                .ReturnsNull();
            this.shareTypeRepository.GetById(1)
                .ReturnsNull();

            // Act
            shareService.AddNewShare(shareInfo);
        }
        [TestMethod]
        public void ShouldUpdateShare()
        {
            // Arrange
            var shareService = new ShareService(
                this.shareRepository,
                this.shareTypeRepository,
                this.stockRepository,
                this.traderRepository);
            ShareInfo shareInfo = new ShareInfo
            {
                Id = 1,
                Amount = 2,
                StockId = 1,
                ShareTypeId = 1,
                OwnerId = 1
            };
            ShareEntity shareToUpdate = new ShareEntity { Id = 1 };
            this.shareRepository.GetById(1)
                .Returns(shareToUpdate);
            this.shareTypeRepository.GetById(1)
                .Returns(new ShareTypeEntity { Id = 1 });
            this.stockRepository.GetById(1)
                .Returns(new StockEntity { Id = 1 });
            this.traderRepository.GetById(1)
                .Returns(new TraderEntity { Id = 1 });

            // Act
            shareService.UpdateShare(shareInfo);
            // Assert
            Assert.IsTrue(
                shareToUpdate.Id == shareInfo.Id 
                && shareToUpdate.Amount == shareInfo.Amount
                && shareToUpdate.Owner.Id == shareInfo.OwnerId
                && shareToUpdate.Stock.Id == shareInfo.StockId
                && shareToUpdate.ShareType.Id == shareInfo.ShareTypeId);
            this.shareRepository.Received(1).Save(Arg.Any<ShareEntity>());
        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "There is no share with given Id.")]
        public void ShouldNotUpdateShareIfItDoesntExist()
        {
            // Arrange
            var shareService = new ShareService(
                this.shareRepository,
                this.shareTypeRepository,
                this.stockRepository,
                this.traderRepository);
            ShareInfo shareInfo = new ShareInfo
            {
                Id = 1,
                Amount = 2,
                StockId = 1,
                ShareTypeId = 1,
                OwnerId = 1
            };
            ShareEntity shareToUpdate = new ShareEntity { Id = 1 };
            this.shareRepository.GetById(1)
                .ReturnsNull();
            this.shareTypeRepository.GetById(1)
                .Returns(new ShareTypeEntity { Id = 1 });
            this.stockRepository.GetById(1)
                .Returns(new StockEntity { Id = 1 });
            this.traderRepository.GetById(1)
                .Returns(new TraderEntity { Id = 1 });

            // Act
            shareService.UpdateShare(shareInfo);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "There's no stock with given id in data source.")]
        public void ShouldNotUpdateShareIfStockDoesntExist()
        {
            // Arrange
            var shareService = new ShareService(
                this.shareRepository,
                this.shareTypeRepository,
                this.stockRepository,
                this.traderRepository);
            ShareInfo shareInfo = new ShareInfo
            {
                Id = 1,
                Amount = 2,
                StockId = 1,
                ShareTypeId = 1,
                OwnerId = 1
            };
            ShareEntity shareToUpdate = new ShareEntity { Id = 1 };
            this.shareRepository.GetById(1)
                .Returns(shareToUpdate);
            this.stockRepository.GetById(1)
                .ReturnsNull();

            // Act
            shareService.UpdateShare(shareInfo);
        }
    }
}
