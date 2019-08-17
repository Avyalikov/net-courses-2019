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
    public class ShareLogicTests
    {
        [TestMethod()]
        public void ShareLogicTest_get_all_share()
        {
            var mockContext = GetContext();

            var shareLogic = new ShareLogic(mockContext.Object);
            var shares = shareLogic.ListStocks();

            Assert.AreEqual(2, shares.Count());
            Assert.AreEqual(shares.ToList()[0].Name, "Сберегаем с Газпромом");
        }

        [TestMethod()]
        public void ShareLogicTest_change_price()
        {
            var mockContext = GetContext();          
            var shareLogic = new ShareLogic(mockContext.Object);

            string answer = shareLogic.ChangeStockPrice(1, 2000);

            var resShoe = mockContext.Object.Share.Where(s => s.Id == 1).First();

            Assert.AreEqual(resShoe.Price, 2000);
            Assert.AreEqual(answer, $"ИЗМЕНЕНИЕ ЦЕНЫ: Акция {resShoe.Name} имеет новую цену {resShoe.Price} вместо 1000");
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        private Mock<AppDbContext> GetContext()
        {
            var Share = new List<Share>()
            {
             new Share {Id = 1, Name = "Сберегаем с Газпромом", Company = "Газпром", Price = 1000},
             new Share {Id = 2, Name = "Сберегаем с Газпромом", Company = "Газпром", Price = 2000 }
            }.AsQueryable();

            var mockSetShare = new Mock<DbSet<Share>>();
            mockSetShare.As<IQueryable<Share>>().Setup(db => db.Provider).Returns(Share.Provider);
            mockSetShare.As<IQueryable<Share>>().Setup(db => db.Expression).Returns(Share.Expression);
            mockSetShare.As<IQueryable<Share>>().Setup(db => db.ElementType).Returns(Share.ElementType);
            mockSetShare.As<IQueryable<Share>>().Setup(db => db.GetEnumerator()).Returns(Share.GetEnumerator());

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.Share).Returns(mockSetShare.Object);

            return mockContext;
        }
    }
}
