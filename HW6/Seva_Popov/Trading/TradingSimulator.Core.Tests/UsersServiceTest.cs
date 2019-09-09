using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using TradingSimulator.Core.Dto;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Repositories;
using TradingSimulator.Core.Services;

namespace TradingSimulator.Core.Tests
{
    [TestClass]
    public class UsersServiceTest
    {
        [TestMethod]
        public void ShouldRegisterNewUser()
        {
            var userTableRepository = Substitute.For<IUserTableRepository>();
            UserService userService = new UserService(userTableRepository);
            UserRegistrationInfo args = new UserRegistrationInfo();
            args.Name = "Александр";
            args.Surname = "Пушкин";
            args.Phone = "222-22-22";

            var UserId = userService.RegisterNewUser(args);

            userTableRepository.Received(1).Add(Arg.Is<UserEntity>(w => w.Name == args.Name && w.Surname == args.Surname && w.Phone == args.Phone));
            userTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "This user has been registered. Can't continue")]
        public void ShouldCannotRegisterDuplicateUsers()
        {
            var userTableRepository = Substitute.For<IUserTableRepository>();
            UserService userService = new UserService(userTableRepository);
            UserRegistrationInfo args = new UserRegistrationInfo();
            args.Name = "Александр";
            args.Surname = "Пушкин";
            args.Phone = "222-22-22";

            userService.RegisterNewUser(args);

            userTableRepository.Contains(Arg.Is<UserEntity>(w => w.Name == args.Name && w.Surname == args.Surname && w.Phone == args.Phone)).Returns(true);

            userService.RegisterNewUser(args);
        }

        [TestMethod]
        public void ShouldGetUserInfo()
        {
            var userTableRepository = Substitute.For<IUserTableRepository>();
            userTableRepository.ContainsById(Arg.Is<int>(12)).Returns(true);
            UserService userService = new UserService(userTableRepository);

            var user = userService.GetUser(12);

            userTableRepository.Received(1).Get(12);
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
    }
}
