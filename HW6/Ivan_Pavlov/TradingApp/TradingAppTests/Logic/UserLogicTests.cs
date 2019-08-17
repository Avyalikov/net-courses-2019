namespace TradingApp.Logic.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using TradingApp.Data;
    using TradingApp.Data.Models;

    [TestClass()]
    public class UserLogicTests
    {
        [TestMethod()]
        public void UserLogicTest_get_all_user()
        {
            var mockContext = GetContext();

            var userLogic = new UserLogic(mockContext.Object);
            var users = userLogic.ListUsers();

            Assert.AreEqual(3, users.Count());
            Assert.AreEqual(users.ToList()[0].SurName, "Пупкин");
            Assert.AreEqual(users.ToList()[2].Balance, 0);
        }

        [TestMethod()]
        public void UserLogicTest_get_all_user_in_orange()
        {
            var mockContext = GetContext();

            var userLogic = new UserLogic(mockContext.Object);
            var users = userLogic.OrangeZone();

            Assert.AreEqual(1, users.Count());
            Assert.AreEqual(users.ToList()[0].Balance, 0);
        }

        [TestMethod()]
        public void UserLogicTest_get_all_user_in_black()
        {
            var mockContext = GetContext();

            var userLogic = new UserLogic(mockContext.Object);
            var users = userLogic.BlackZone();

            Assert.AreEqual(1, users.Count());
            Assert.AreEqual(users.ToList()[0].Balance, -5);
        }

        [TestMethod()]
        public void UserLogicTest_add_user()
        {           
            var mockSet = new Mock<DbSet<User>>();
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.Users).Returns(mockSet.Object);
            var userLogic = new UserLogic(mockContext.Object);
            User user = new User()
            {
                SurName = "Махмед",
                Name = "Измаил",
                Phone = "88005553535",
                Balance = -20
            };

            string answer = userLogic.AddUser(user);

            Assert.AreEqual(answer, $"ДОБАВЛЕН НОВЫЙ ПОЛЬЗОВАТЕЛЬ: {user.SurName} {user.Name} с балансом {user.Balance} и телефоном {user.Phone}");
            mockSet.Verify(m => m.Add(It.IsAny<User>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        private Mock<AppDbContext> GetContext()
        {
            var Share = new List<Share>()
            {
             new Share { Name = "Сберегаем с Газпромом", Company = "Газпром", Price = 2000},
             new Share { Name = "Выслуга лет в EPAM", Company = "EPAM", Price = 1500 }
            }.AsQueryable();
            var UserShare = new List<UserShare>()
            {
            }.AsQueryable();
            var User = new List<User>()
            {
                new User { SurName = "Пупкин", Name = "Вася", Balance = 25000, Phone = "89992323265" },
                new User { SurName = "Пупкин", Name = "Коля", Balance = -5, Phone = "89992323265" },
                new User { SurName = "Пупкин", Name = "Петя", Balance = 0, Phone = "89992323265" }
            }.AsQueryable();

            var mockSetUser = new Mock<DbSet<User>>();
            mockSetUser.As<IQueryable<User>>().Setup(db => db.Provider).Returns(User.Provider);
            mockSetUser.As<IQueryable<User>>().Setup(db => db.Expression).Returns(User.Expression);
            mockSetUser.As<IQueryable<User>>().Setup(db => db.ElementType).Returns(User.ElementType);
            mockSetUser.As<IQueryable<User>>().Setup(db => db.GetEnumerator()).Returns(User.GetEnumerator());

            var mockSetShare = new Mock<DbSet<Share>>();
            mockSetShare.As<IQueryable<Share>>().Setup(db => db.Provider).Returns(Share.Provider);
            mockSetShare.As<IQueryable<Share>>().Setup(db => db.Expression).Returns(Share.Expression);
            mockSetShare.As<IQueryable<Share>>().Setup(db => db.ElementType).Returns(Share.ElementType);
            mockSetShare.As<IQueryable<Share>>().Setup(db => db.GetEnumerator()).Returns(Share.GetEnumerator());

            var mockSetUserShare = new Mock<DbSet<UserShare>>();
            mockSetUserShare.As<IQueryable<UserShare>>().Setup(db => db.Provider).Returns(UserShare.Provider);
            mockSetUserShare.As<IQueryable<UserShare>>().Setup(db => db.Expression).Returns(UserShare.Expression);
            mockSetUserShare.As<IQueryable<UserShare>>().Setup(db => db.ElementType).Returns(UserShare.ElementType);
            mockSetUserShare.As<IQueryable<UserShare>>().Setup(db => db.GetEnumerator()).Returns(UserShare.GetEnumerator());

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.Users).Returns(mockSetUser.Object);
            mockContext.Setup(m => m.Share).Returns(mockSetShare.Object);
            mockContext.Setup(m => m.UserShares).Returns(mockSetUserShare.Object);

            return mockContext;
        }
    }
}