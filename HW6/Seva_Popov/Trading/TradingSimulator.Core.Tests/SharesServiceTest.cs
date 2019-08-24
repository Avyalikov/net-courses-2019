using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using TradingSimulator.Core.Dto;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Repositories;
using TradingSimulator.Core.Services;

namespace TradingSimulator.Core.Tests
{
    [TestClass]
    public class SharesServiceTest
    {
        [TestMethod]
        public void ShouldRegisterNewShares()
        {
            var sharesTableRepository = Substitute.For<ISharesTableRepository>();
            SharesService sharesService = new SharesService(sharesTableRepository);
            SharesRegistrationInfo args = new SharesRegistrationInfo();
            args.Name = "AAPL";
            args.Price = 201;
            

            var SharesId = sharesService.RegisterNewShares(args);

            sharesTableRepository.Received(1).Add(Arg.Is<SharesEntity>(w => w.Name == args.Name && w.Price == args.Price));
            sharesTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "This shares has been registered. Can't continue")]
        public void ShouldCannotRegisterDuplicateShares()
        {
            var sharesTableRepository = Substitute.For<ISharesTableRepository>();
            SharesService sharesService = new SharesService(sharesTableRepository);
            SharesRegistrationInfo args = new SharesRegistrationInfo();
            args.Name = "AAPL";
            args.Price = 201;

            sharesService.RegisterNewShares(args);

            sharesTableRepository.Contains(Arg.Is<SharesEntity>(w => w.Name == args.Name && w.Price == args.Price)).Returns(true);

            sharesService.RegisterNewShares(args);
        }

        //[TestMethod]
        //public void ShouldGetUserInfo()
        //{
        //    var userTableRepository = Substitute.For<ISharesTableRepository>();
        //    userTableRepository.ContainsById(Arg.Is<int>(12)).Returns(true);
        //    UserService userService = new UserService(userTableRepository);

        //    var user = userService.GetUser(12);

        //    userTableRepository.Received(1).Get(12);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException), "Can't get user by this id. May it has not been registred.")]
        //public void ShouldThrowExceptionCantFindUser()
        //{
        //    var userTableRepository = Substitute.For<ISharesTableRepository>();
        //    userTableRepository.ContainsById(Arg.Is<int>(12)).Returns(false);
        //    UserService userService = new UserService(userTableRepository);

        //    var user = userService.GetUser(12);
        //}
    }
}
