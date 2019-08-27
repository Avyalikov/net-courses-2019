using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using TradingApp.Core.Dto;
using TradingApp.Core.Models;
using TradingApp.Core.Repositories;
using TradingApp.Core.Services;

namespace TradingApp.Core.Tests
{
    [TestClass]
    public class UsersServiceTests
    {
        [TestMethod]
        public void ShouldRegisterNewUser()
        {
            //Arrange
            var userTableRepository = Substitute.For<IUserTableRepository>();
            UsersService userService = new UsersService(userTableRepository);
            UserRegistrationInfo args = new UserRegistrationInfo();
            args.Name = "John";
            args.Surname = "Smith";
            args.PhoneNumber = "+72394837";
            
            //Act
            var userId = userService.RegisterNewUser(args);

            //Assert
            userTableRepository.Received(1).Add(Arg.Is<UserEntity>(w =>
            w.Name == args.Name
            && w.Surname == args.Surname
            && w.PhoneNumber == args.PhoneNumber));
            userTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "This user has been already registered.Can't continue")]
        public void ShouldNotRegisterNewUserIfItExists()
        {
            //Arrange
            var userTableRepository = Substitute.For<IUserTableRepository>();
            UsersService userService = new UsersService(userTableRepository);
            UserRegistrationInfo args = new UserRegistrationInfo();
            args.Name = "John";
            args.Surname = "Smith";
            args.PhoneNumber = "+72394837";

            //Act
            userService.RegisterNewUser(args);

            userTableRepository.Contains(Arg.Is<UserEntity>(w =>
            w.Name == args.Name
            && w.Surname == args.Surname
            && w.PhoneNumber == args.PhoneNumber)).Returns(true);
            userService.RegisterNewUser(args);

        }

        [TestMethod]
        public void ShouldGetUserInfo()
        {
            //Arrange
            var userTableRepository = Substitute.For<IUserTableRepository>();
            userTableRepository.Contains(Arg.Is(88)).Returns(true);
            UsersService userService = new UsersService(userTableRepository);

            // Act
            var user = userService.GetUser(88);

            //Assert
            userTableRepository.Received(1).Get(88);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Can't find user with this Id. May it hasn't been registered")]
        public void ShouldGetExceptionIfUserNotRegistered()
        {
            //Arrange
            var userTableRepository = Substitute.For<IUserTableRepository>();
            userTableRepository.Contains(Arg.Is(88)).Returns(false);
            UsersService userService = new UsersService(userTableRepository);

            // Act
            var user = userService.GetUser(88);
        }
    }

}
