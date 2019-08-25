using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Repositories;
using TradingSimulator.Core.Services;

namespace TradingSimulator.Core.Tests
{
    [TestClass]
    public class AddingSharesToThUserTest
    {
        [TestMethod]
        public void ShouldAddingSharesToTheUser()
        {
            var AddingSharesToThUserServiceTableRepository = Substitute.For<IAddingSharesToThUserServiceTableRepository>();
            AddingSharesToThUserService addingSharesToThUserService = new AddingSharesToThUserService(AddingSharesToThUserServiceTableRepository);
            AddingSharesToThUserEntity args = new AddingSharesToThUserEntity();
            args.UserId = 1;
            args.ShareId = 1;
            args.AmountStocks = 20;

            addingSharesToThUserService.RegisterNewSharesToTheUser(args);

            AddingSharesToThUserServiceTableRepository.Received(1).Add(Arg.Is<AddingSharesToThUserEntity>(w => w.UserId == args.UserId && w.ShareId == args.ShareId && w.AmountStocks == args.AmountStocks));
            AddingSharesToThUserServiceTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Can't get user by this id. May it has not been registred.")]
        public void ShouldThrowExceptionCantFindUser()
        {
            var userTableRepository = Substitute.For<IUserTableRepository>();
            userTableRepository.ContainsById(Arg.Is<int>(12)).Returns(false);
            UserService userService = new UserService(userTableRepository);

            var user = userService.GetUser(12);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Can't get shares by this id. May it has not been registred.")]
        public void ShouldThrowExceptionCantFindShares()
        {
            var sharesTableRepository = Substitute.For<ISharesTableRepository>();
            sharesTableRepository.ContainsById(Arg.Is<int>(12)).Returns(false);
            SharesService sharesService = new SharesService(sharesTableRepository);

            var shares = sharesService.GetShares(12);
        }
    }
}
