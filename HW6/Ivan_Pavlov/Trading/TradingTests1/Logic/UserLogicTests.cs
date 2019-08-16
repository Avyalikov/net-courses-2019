namespace Trading.Logic.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using TradingData;
    using TradingData.Models;
    using TradingView.Interface;

    [TestClass()]
    public class UserLogicTests
    {
        [TestMethod]
        public void ListUsers_print_all_users()
        {
            var viewMock = new Mock<IView>();
            var dbMock = new Mock<IDbProvider>();
            dbMock.Setup(m => m.ListUsers()).Returns(GetTestUser().AsQueryable);
            var user = new UserLogic(viewMock.Object, dbMock.Object);

            var result = user.ListUsers();
            StringBuilder sb = new StringBuilder();
            var InfoByUsers = GetTestUser();
            foreach (var item in InfoByUsers)
            {
                sb.AppendLine(item.ToString());
            }
            var resMock = sb.ToString();

            Assert.AreEqual(resMock, result);
        }

        [TestMethod]
        public void OrangeZone_print_users_with_zero_balance()
        {
            var viewMock = new Mock<IView>();
            var dbMock = new Mock<IDbProvider>();
            dbMock.Setup(m => m.OrangeZone()).Returns(GetTestUser()
                .Where(s => s.Balance == 0).AsQueryable);
            var user = new UserLogic(viewMock.Object, dbMock.Object);

            var result = user.OrangeZone();
            StringBuilder sb = new StringBuilder();
            var InfoByUsers = GetTestUser()
                .Where(u => u.Balance == 0);
            foreach (var item in InfoByUsers)
            {
                sb.AppendLine(item.ToString());
            }
            var resMock = sb.ToString();

            Assert.AreEqual(resMock, result);
        }

        [TestMethod]
        public void BlackZone_print_users_with_negativ_balance()
        {
            var viewMock = new Mock<IView>();
            var dbMock = new Mock<IDbProvider>();
            dbMock.Setup(m => m.OrangeZone()).Returns(GetTestUser()
                .Where(s => s.Balance < 0).AsQueryable);
            var user = new UserLogic(viewMock.Object, dbMock.Object);

            var result = user.OrangeZone();
            StringBuilder sb = new StringBuilder();
            var InfoByUsers = GetTestUser()
                .Where(u => u.Balance < 0);
            foreach (var item in InfoByUsers)
            {
                sb.AppendLine(item.ToString());
            }
            var resMock = sb.ToString();

            Assert.AreEqual(resMock, result);
        }

        [TestMethod]
        public void AddUser()
        {
            var viewMock = new Mock<IView>();
            var dbMock = new Mock<IDbProvider>();
            var user = new UserLogic(viewMock.Object, dbMock.Object);

            viewMock.Setup(m => m.EnterSurname(false)).Returns("Пупкин");
            viewMock.Setup(m => m.EnterName(false)).Returns("Вася");
            viewMock.Setup(m => m.EnterPhone(false)).Returns("8956533565");
            viewMock.Setup(m => m.EnterBalance(false)).Returns("2000");
            user.AddUser();

            dbMock.Verify(db => db.AddUser(It.IsAny<User>()), Times.Once());
        }

        private IEnumerable<User> GetTestUser()
        {
            var user = new List<User>()
            {
            new User { Id = 1, SurName = "Пупкин", Name = "Вася", Phone = "8956533565", Balance = 2000 },
            new User { Id = 1, SurName = "Смирнов", Name = "Измаил", Phone = "8111111111", Balance = 0 },
            new User { Id = 3, SurName = "Махмед", Name = "Измаил", Phone = "8111111111", Balance = -100 },
            };
            return user.Select(u => u);
        }
    }
}