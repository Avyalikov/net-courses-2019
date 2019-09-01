namespace TradingApp.Core.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using System;
    using TradingApp.Core.Dto;
    using TradingApp.Core.Models;
    using TradingApp.Core.Repositories;
    using TradingApp.Core.Services;

    [TestClass]
    public class ShareServiceTest
    {
        [TestMethod]
        public void ShouldAddNewShare()
        {
            var shareTableRepository = Substitute.For<IShareTableRepository>();
            ShareServices shareServices = new ShareServices(shareTableRepository);
            ShareInfo args = new ShareInfo
            {
                Name = "Печенюшка",
                CompanyName = "Nescaffe",
                Price = 2000
            };

            int shareId = shareServices.AddNewShare(args);

            shareTableRepository.Received(1).Add(Arg.Is<ShareEntity>(
                s => s.Name == args.Name &&
                s.CompanyName == args.CompanyName &&
                s.Price == args.Price));
            shareTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Данная акция уже существвует")]
        public void ShouldNotAddShareIfItExists()
        {
            var shareTableRepository = Substitute.For<IShareTableRepository>();
            ShareServices shareServices = new ShareServices(shareTableRepository);
            ShareInfo args = new ShareInfo
            {
                Name = "Печенюшка",
                CompanyName = "Nescaffe",
                Price = 2000
            };

            shareServices.AddNewShare(args);
            shareTableRepository.Contains(Arg.Is<ShareEntity>(
                s => s.Name == args.Name &&
                s.CompanyName == args.CompanyName &&
                s.Price == args.Price)).Returns(true);

            shareServices.AddNewShare(args);
        }

        [TestMethod]
        public void ShouldGetAllShares()
        {
            var shareTableRepository = Substitute.For<IShareTableRepository>();
            ShareServices shareServices = new ShareServices(shareTableRepository);

            var shares = shareServices.GetAllShares();

            shareTableRepository.Received(1).GetAllShares();
        }

        [TestMethod]
        public void ShouldChangeSharePrice()
        {
            var shareTableRepository = Substitute.For<IShareTableRepository>();
            ShareServices shareServices = new ShareServices(shareTableRepository);
            ShareEntity args = new ShareEntity
            {
                Name = "Печенюшка",
                CompanyName = "Nescaffe",
                Price = 2000
            };
            shareTableRepository.GetShareById(Arg.Is(1)).Returns(args);

            shareServices.ChangeSharePrice(1, 150);

            shareTableRepository.Contains(Arg.Is<ShareEntity>(
                s => s.Name == args.Name &&
                s.CompanyName == args.CompanyName &&
                s.Price == 150));
            shareTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Недопустимая цена акции")]
        public void ShouldDontChangeSharePriceIfIncorrectPrice()
        {
            var shareTableRepository = Substitute.For<IShareTableRepository>();
            ShareServices shareServices = new ShareServices(shareTableRepository);
            shareTableRepository.GetShareById(Arg.Is(1)).Returns(new ShareEntity());

            shareServices.ChangeSharePrice(1, -150);
        }

        [TestMethod]
        public void ShouldUpdateShare()
        {
            var shareTableRepository = Substitute.For<IShareTableRepository>();
            ShareServices shareServices = new ShareServices(shareTableRepository);
            ShareEntity baseShare = new ShareEntity()
            {
                Id = 1,
                Name = "Pechenyshka"
            };
            shareTableRepository.GetShareById(1).Returns(baseShare);
            ShareInfo args = new ShareInfo()
            {
                Name = "test"
            };

            shareServices.Update(1, args);

            shareTableRepository.Received(1).GetShareById(1);
            shareTableRepository.Received(1).SaveChanges();
            Assert.AreEqual(baseShare.Name, args.Name);
        }

        [TestMethod]
        public void ShouldRemoveShare()
        {
            var shareTableRepository = Substitute.For<IShareTableRepository>();
            ShareServices shareServices = new ShareServices(shareTableRepository);
            ShareEntity share = null;
            shareTableRepository.GetShareById(1).Returns(share);

            shareServices.Remove(1);

            shareTableRepository.Received(1).Remove(1);
            shareTableRepository.Received(1).SaveChanges();
        }
    }
}
