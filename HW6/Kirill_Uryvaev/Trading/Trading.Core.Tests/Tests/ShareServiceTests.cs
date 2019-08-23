using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Trading.Core.Services;
using Trading.Core.Repositories;
using Trading.Core.DataTransferObjects;

namespace Trading.Core.Tests
{
    /// <summary>
    /// Summary description for ShareServiceTests
    /// </summary>
    [TestClass]
    public class ShareServiceTests
    {
        IShareRepository shareRepository;
        [TestInitialize]
        public void Initialize()
        {
            shareRepository = Substitute.For<IShareRepository>();
            shareRepository.When(w => w.Add(Arg.Any<ShareEntity>())).Do(x => x.Arg<ShareEntity>().ShareID = 1);
            shareRepository.LoadAllShares().Returns(new List<ShareEntity>()
            {
                new ShareEntity()
                {
                    ShareName = "Company1",
                    ShareCost = 1000
                },
                new ShareEntity()
                {
                    ShareName = "Company2",
                    ShareCost = 10
                },
                new ShareEntity()
                {
                    ShareName = "Company3",
                    ShareCost = 324
                },
            });
        }

        [TestMethod]
        public void ShouldRegisterShare()
        {
            //Arrange
            ShareService shareService = new ShareService(shareRepository);
            ShareRegistrationInfo shareInfo = new ShareRegistrationInfo()
            {
                Name = "Hardsoft",
                Cost = 10
            };
            //Act
            int id = shareService.RegisterShare(shareInfo);
            //Assert
            shareRepository.Received(1).Add(Arg.Is<ShareEntity>(
                w => w.ShareName == shareInfo.Name
                && w.ShareCost == shareInfo.Cost));
            shareRepository.Received(1).SaveChanges();
            Assert.AreEqual(1, id);
        }

        [TestMethod]
        public void ShouldGetAllShares()
        {
            //Arrange
            ShareService shareService = new ShareService(shareRepository);
            //Act
            var shares = shareService.GetAllShares();
            //Assert
            shareRepository.Received(1).LoadAllShares();
            Assert.AreEqual(3, shares.Count());
        }
    }
}
