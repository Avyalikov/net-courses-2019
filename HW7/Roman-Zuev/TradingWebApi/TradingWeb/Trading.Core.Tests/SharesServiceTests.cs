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
    public class SharesServiceTests
    {
        ISharesTableRepository sharesTableRepository;
        ISharesService sharesService;
        [TestInitialize]
        public void Initialize()
        {
            sharesTableRepository = Substitute.For<ISharesTableRepository>();
            sharesService = new SharesService(sharesTableRepository);
        }
        [TestMethod]
        public void ShouldAddNewShares()
        {
            //Arrange
            SharesEntity shares = new SharesEntity() { SharesType = "AAA", Price = 100 };
            //Act
            sharesService.Add(shares);
            //Assert
            sharesTableRepository.Received(1).Add(Arg.Is<SharesEntity>(s =>
            s.SharesType == shares.SharesType
            && s.Price == shares.Price));
            sharesTableRepository.Received(1).SaveChanges();
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "This shares type is already exists")]
        public void ShouldNotAddNewSharesThatExists()
        {
            //Arrange
            SharesEntity shares = new SharesEntity() { SharesType = "AAA", Price = 100 };
            sharesTableRepository.Contains(shares).Returns(true);
            //Act
            sharesService.Add(shares);
            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Wrong data")]
        public void ShouldNotAddNewSharesWithWrongData()
        {
            //Arrange
            SharesEntity shares = new SharesEntity() { SharesType = "a", Price = 100 };
            //Act
            sharesService.Add(shares);
            //Assert
        }

        [TestMethod]
        public void ShouldRemoveShares()
        {
            //Arrange
            SharesEntity shares = new SharesEntity() { Id = 1, SharesType = "AAA", Price = 100 };
            sharesTableRepository.Contains(shares).Returns(true);
            sharesTableRepository.GetById(shares.Id).Returns(shares);
            //Act
            sharesService.Remove(shares);
            //Assert
            sharesTableRepository.Received(1).Remove(Arg.Is<SharesEntity>(s =>
            s.SharesType == shares.SharesType
            && s.Price == shares.Price));
            sharesTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "This shares type doesn't exist")]
        public void ShouldnotRemoveSharesThatDontExist()
        {
            //Arrange
            SharesEntity shares = new SharesEntity() { Id = 1, SharesType = "AAA", Price = 100 };
            sharesTableRepository.Contains(shares).Returns(false);
            //Act
            sharesService.Remove(shares);
        }

        [TestMethod]
        public void ShouldUpdateShares()
        {
            //Arrange
            SharesEntity shares = new SharesEntity() { Id = 1, SharesType = "AAA", Price = 100 };
            sharesTableRepository.Contains(shares).Returns(true);
            sharesTableRepository.GetById(shares.Id).Returns(shares);
            //Act
            sharesService.Update(shares);
            //Assert
            sharesTableRepository.Received(1).Update(Arg.Is<SharesEntity>(s =>
            s.SharesType == shares.SharesType
            && s.Price == shares.Price));
            sharesTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "This shares type doesn't exist")]
        public void ShouldNotUpdateSharesThatDontExist()
        {
            //Arrange
            SharesEntity shares = new SharesEntity() { Id = 1, SharesType = "AAA", Price = 100 };
            sharesTableRepository.Contains(shares).Returns(false);
            //Act
            sharesService.Update(shares);
        }
    }
}
