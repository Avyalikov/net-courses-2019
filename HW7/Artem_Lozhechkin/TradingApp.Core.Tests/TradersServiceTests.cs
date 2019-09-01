namespace TradingApp.Core.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TradingApp.Core.DTO;
    using TradingApp.Core.Models;
    using TradingApp.Core.Repositories;
    using TradingApp.Core.Services;
    
    [TestClass]
    public class TradersServiceTests
    {
        private IRepository<TraderEntity> repository;
        [TestInitialize]
        public void Initialize()
        {
            repository = Substitute.For<IRepository<TraderEntity>>();
            repository.GetAll().Returns(new List<TraderEntity>
            {
                new TraderEntity
                {
                    FirstName = "Igor",
                    LastName = "Igorev",
                    PhoneNumber = "124142",
                    Balance = 100
                },
                new TraderEntity
                {
                    FirstName = "Alexey",
                    LastName = "Alexeev",
                    PhoneNumber = "122222",
                    Balance = 0
                },
                new TraderEntity
                {
                    FirstName = "Viktor",
                    LastName = "Viktorov",
                    PhoneNumber = "1234214",
                    Balance = 0
                },
                new TraderEntity
                {
                    FirstName = "Vasiliy",
                    LastName = "Vasiliev",
                    PhoneNumber = "1234557",
                    Balance = -100
                },
            }
            );
        }
        [TestMethod]
        public void ShouldRegisterNewTrader()
        {
            // Arrange
            TraderService tradersService = new TraderService(this.repository);
            TraderInfo info = new TraderInfo
            {
                FirstName = "Artem",
                LastName = "Lozhechkin",
                PhoneNumber = "79998887766",
                Balance = 100
            };

            // Act
            tradersService.RegisterNewUser(info);

            // Assert
            this.repository.Received(1).Add(Arg.Is<TraderEntity>
                    (w => w.FirstName == info.FirstName
                    && w.LastName == info.LastName
                    && w.PhoneNumber == info.PhoneNumber
                    && w.Balance == info.Balance));
            this.repository.Received(1).SaveChanges();
        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "This user is already in database.")]
        public void ShouldNotRegisterTraderIfExists()
        {
            // Arrange
            TraderService tradersService = new TraderService(this.repository);
            TraderInfo info = new TraderInfo
            {
                FirstName = "Vasiliy",
                LastName = "Vasiliev",
                PhoneNumber = "1234557",
                Balance = -100
            };

            // Act
            tradersService.RegisterNewUser(info);

            this.repository.Contains(Arg.Is<TraderEntity>(
                t => t.FirstName == info.FirstName
                && t.LastName == info.LastName
                && t.PhoneNumber == info.PhoneNumber
                && t.Balance == info.Balance)).Returns(true);

            tradersService.RegisterNewUser(info);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "User can't be added. Check your input please..")]
        public void ShouldNotRegisterNewTraderWithEmptyName()
        {
            // Arrange
            TraderService tradersService = new TraderService(this.repository);
            TraderInfo info = new TraderInfo
            {
                FirstName = string.Empty,
                LastName = "Lozhechkin",
                PhoneNumber = "79998887766",
                Balance = 100
            };

            // Act
            tradersService.RegisterNewUser(info);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "User can't be added. Check your input please..")]
        public void ShouldNotRegisterNewTraderWithIncorrectName()
        {
            // Arrange
            TraderService tradersService = new TraderService(this.repository);
            TraderInfo info = new TraderInfo
            {
                FirstName = "Ar1em",
                LastName = "Lozhechkin",
                PhoneNumber = "79998887766",
                Balance = 100
            };

            // Act
            tradersService.RegisterNewUser(info);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "User can't be added. Check your input please..")]
        public void ShouldNotRegisterNewTraderWithEmptySurname()
        {
            // Arrange
            TraderService tradersService = new TraderService(this.repository);
            TraderInfo info = new TraderInfo
            {
                FirstName = "Artem",
                LastName = string.Empty,
                PhoneNumber = "79998887766",
                Balance = 100
            };

            // Act
            tradersService.RegisterNewUser(info);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "User can't be added. Check your input please..")]
        public void ShouldNotRegisterNewTraderWithIncorrectSurname()
        {
            // Arrange
            TraderService tradersService = new TraderService(this.repository);
            TraderInfo info = new TraderInfo
            {
                FirstName = "Artem",
                LastName = "Lozhechk1n",
                PhoneNumber = "79998887766",
                Balance = 100
            };

            // Act
            tradersService.RegisterNewUser(info);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "User can't be added. Check your input please..")]
        public void ShouldNotRegisterNewTraderWithEmptyPhoneNumber()
        {
            // Arrange
            TraderService tradersService = new TraderService(this.repository);
            TraderInfo info = new TraderInfo
            {
                FirstName = "Artem",
                LastName = "Lozhechkin",
                PhoneNumber = string.Empty,
                Balance = 100
            };

            // Act
            tradersService.RegisterNewUser(info);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "User can't be added. Check your input please..")]
        public void ShouldNotRegisterNewTraderWithIncorrectPhoneNumber()
        {
            // Arrange
            TraderService tradersService = new TraderService(this.repository);
            TraderInfo info = new TraderInfo
            {
                FirstName = "Artem",
                LastName = "Lozhechkin",
                PhoneNumber = "999asd9213",
                Balance = 100
            };

            // Act
            tradersService.RegisterNewUser(info);
        }
        [TestMethod]
        public void ShouldReturnOnlyUsersFromOrangeZone()
        {
            // Arrange
            TraderService tradersService = new TraderService(this.repository);

            // Act
            var orangeZoneTraders = tradersService.GetTradersFromOrangeZone();

            // Assert
            this.repository.Received(1).GetAll();
            Assert.IsTrue(orangeZoneTraders.Count == this.repository.GetAll().Where(t => t.Balance == 0).Count());
        }
        [TestMethod]
        public void ShouldReturnOnlyUsersFromBlackZone()
        {
            // Arrange
            TraderService tradersService = new TraderService(this.repository);

            // Act
            var orangeZoneTraders = tradersService.GetTradersFromBlackZone();

            // Assert
            this.repository.Received(1).GetAll();
            Assert.IsTrue(orangeZoneTraders.Count == this.repository.GetAll().Where(t => t.Balance < 0).Count());
        }
        [TestMethod]
        public void ShouldReturnAllTraders()
        {
            // Arrange
            TraderService tradersService = new TraderService(this.repository);

            // Act
            var allTraders = tradersService.GetAllTraders();

            // Assert
            this.repository.Received(1).GetAll();
            Assert.IsTrue(allTraders.Count == this.repository.GetAll().Count());
        }
    }
}
