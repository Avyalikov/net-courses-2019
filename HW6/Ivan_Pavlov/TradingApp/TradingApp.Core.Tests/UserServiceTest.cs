namespace TradingApp.Core.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using System;
    using System.Collections.Generic;
    using TradingApp.Core.Dto;
    using TradingApp.Core.Models;
    using TradingApp.Core.Repositories;
    using TradingApp.Core.Services;

    [TestClass]
    public class UserServiceTest
    {
        [TestMethod]
        public void ShouldAddNewUser()
        {
            var userTableRepository = Substitute.For<IUserTableRepository>();
            UsersService usersService = new UsersService(userTableRepository);
            UserInfo args = new UserInfo
            {
                Name = "Vasya",
                SurName = "Pypckin",
                Balance = 50000,
                Phone = "88005553535"
            };

            int userId = usersService.AddNewUser(args);

            userTableRepository.Received(1).Add(Arg.Is<UserEntity>(
                u => u.Name == args.Name &&
                u.SurName == args.SurName &&
                u.Balance == args.Balance &&
                u.Phone == args.Phone));
            userTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Не уникальный пользователь")]
        public void ShouldNotAddUserIfItExists()
        {
            var userTableRepository = Substitute.For<IUserTableRepository>();
            UsersService usersService = new UsersService(userTableRepository);
            UserInfo args = new UserInfo
            {
                Name = "Vasya",
                SurName = "Pypckin",
                Balance = 50000,
                Phone = "88005553535"
            };

            usersService.AddNewUser(args);
            userTableRepository.Contains(Arg.Is<UserEntity>(
                u => u.Name == args.Name &&
                u.SurName == args.SurName &&
                u.Balance == args.Balance &&
                u.Phone == args.Phone)).Returns(true);

            usersService.AddNewUser(args);
        }

        [TestMethod]
        public void ShouldGetUserById()
        {
            var userTableRepository = Substitute.For<IUserTableRepository>();
            userTableRepository.GetUserById(Arg.Is(1)).Returns(new UserEntity());
            UsersService usersService = new UsersService(userTableRepository);

            var user = usersService.GetUserById(1);

            userTableRepository.Received(1).GetUserById(1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Данный пользователь не найден")]
        public void ShouldThrowExceptIfUserDontFind()
        {
            var userTableRepository = Substitute.For<IUserTableRepository>();
            UserEntity user = null;
            userTableRepository.GetUserById(Arg.Is(1)).Returns(user);
            UsersService usersService = new UsersService(userTableRepository);

            var client = usersService.GetUserById(1);
        }

        [TestMethod]
        public void ShouldChangeUserBalance()
        {
            var userTableRepository = Substitute.For<IUserTableRepository>();
            UserEntity args = new UserEntity
            {
                Name = "Vasya",
                SurName = "Pypckin",
                Balance = 50000,
                Phone = "88005553535"
            };
            userTableRepository.GetUserById(Arg.Is(1)).Returns(args);
            UsersService usersService = new UsersService(userTableRepository);

            usersService.ChangeUserBalance(1, -2000);

            userTableRepository.Contains(Arg.Is<UserEntity>(
                u => u.Name == args.Name &&
                u.SurName == args.SurName &&
                u.Balance == args.Balance - 2000 &&
                u.Phone == args.Phone)).Returns(true);
            userTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        public void ShouldGetAllUsersInfo()
        {
            var userTableRepository = Substitute.For<IUserTableRepository>();
            UsersService usersService = new UsersService(userTableRepository);

            var users = usersService.GetAllUsers();

            userTableRepository.Received(1).GetAllUsers();
        }

        [TestMethod]
        public void ShouldGetAllUsersWithZeroBalansInfo()
        {
            var userTableRepository = Substitute.For<IUserTableRepository>();
            UsersService usersService = new UsersService(userTableRepository);

            var users = usersService.GetAllUsersWithZero();

            userTableRepository.Received(1).GetAllUsersWithZero();
        }

        [TestMethod]
        public void ShouldGetAllUsersWithNegativeBalansInfo()
        {
            var userTableRepository = Substitute.For<IUserTableRepository>();
            UsersService usersService = new UsersService(userTableRepository);

            var users = usersService.GetAllUsersWithNegativeBalance();

            userTableRepository.Received(1).GetAllUsersWithNegativeBalance();
        }

        [TestMethod]
        public void ShouldGetSeller()
        {
            var userTableRepository = Substitute.For<IUserTableRepository>();
            UsersService usersService = new UsersService(userTableRepository);
            UserEntity args = new UserEntity()
            {
                Id = 1,
                Name = "Vasya",
                SurName = "Pypckin",
                Balance = 5000,
                UsersShares = new List<PortfolioEntity>()
                {
                    new PortfolioEntity() {
                        UserEntityId = 1, ShareId = 1, Amount = 50,
                        Share = new ShareEntity(){Name = "test", CompanyName = "test", Price = 100 }
                    }
                }
            };
            userTableRepository.Count().Returns(1);
            userTableRepository.GetUserById(args.Id).Returns(args);

            var seller = usersService.GetSeller(args.Id);

            userTableRepository.Received(1).GetUserById(args.Id);
            Assert.AreEqual(seller, args);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Нет зарегестрированных пользователей")]
        public void ShouldThrowExcepIfDBbDontHasUser()
        {
            var userTableRepository = Substitute.For<IUserTableRepository>();
            UsersService usersService = new UsersService(userTableRepository);
            UserEntity args = new UserEntity()
            {
                Id = 1,
                Name = "Vasya",
                SurName = "Pypckin",
                Balance = 5000
            };

            userTableRepository.Count().Returns(1);

            usersService.GetSeller(1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Нет пользователя с таким Id")]
        public void ShouldThrowExceptionIfSellerIsNull()
        {
            var userTableRepository = Substitute.For<IUserTableRepository>();
            UsersService usersService = new UsersService(userTableRepository);
            UserEntity args = null;

            userTableRepository.Count().Returns(1);
            userTableRepository.GetUserById(1).Returns(args);

            usersService.GetSeller(1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "У пользователя нет акций")]
        public void ShouldSellerHasShares()
        {
            var userTableRepository = Substitute.For<IUserTableRepository>();
            UsersService usersService = new UsersService(userTableRepository);

            userTableRepository.Count().Returns(0);

            usersService.GetSeller(1);
        }


        [TestMethod]
        public void ShouldGetCustomer()
        {
            var userTableRepository = Substitute.For<IUserTableRepository>();
            UsersService usersService = new UsersService(userTableRepository);
            UserEntity args = new UserEntity()
            {
                Id = 2,
                Name = "Petay",
                SurName = "Pypckin",
            };
            int sellerId = 1;
            int userId = 2;

            userTableRepository.GetUserById(userId).Returns(args);

            var customer = usersService.GetCustomer(userId, sellerId);

            userTableRepository.Received(1).GetUserById(userId);
            Assert.AreEqual(customer, args);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Покупатель и продавец - один персонаж")]
        public void ShouldCustomerDontBeSeller()
        {
            var userTableRepository = Substitute.For<IUserTableRepository>();
            UsersService usersService = new UsersService(userTableRepository);

            int userId = 2;
            int sellerId = 2;
          
            var customer = usersService.GetCustomer(userId, sellerId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Нет пользователя с таким Id")]
        public void ShouldThrowExceptionIfCusttomerIsNull()
        {
            var userTableRepository = Substitute.For<IUserTableRepository>();
            UsersService usersService = new UsersService(userTableRepository);
            UserEntity args = null;

            int userId = 1;
            int sellerId = 2;
            userTableRepository.GetUserById(userId).Returns(args);

            var customer = usersService.GetCustomer(userId, sellerId);
        }
    }
}
