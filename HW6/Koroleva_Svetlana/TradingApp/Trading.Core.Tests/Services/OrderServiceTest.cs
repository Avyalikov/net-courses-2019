using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Trading.Core.Repositories;
using Trading.Core.Services;
using Trading.Core.DTO;
using Trading.Core.Model;


namespace TradingApp.Tests
{
    [TestClass]
    public class OrderServiceTest
    {
        

        [TestMethod]
        public void ShouldAddNewOrder()
        {
            //Arrange
            var orderTableRepository = Substitute.For<ITableRepository<Order>>();
            OrderService orderService = new OrderService(orderTableRepository);
            OrderInfo orderInfo = new OrderInfo
            {
                ClientId = 3,
                StockId = 5,
                OrderType = (OrderInfo.OrdType)OrderType.Sale,
                Quantity = 10,
                IsExecuted = false
            };
            //Act
            orderService.AddOrder(orderInfo);
            //Assert
            orderTableRepository.Received(1).Add(Arg.Is<Order>(
                w => w.ClientID == 3 &&
                w.StockID == 5 &&
                w.OrderType == OrderType.Sale &&
                w.Quantity == 10&&
                w.IsExecuted==false
                ));
        }

       

        [TestMethod]
        public void ShouldGetOrderInfo()
        {
            // Arrange
            var orderTableRepository = Substitute.For<ITableRepository<Order>>();
            OrderService orderService = new OrderService(orderTableRepository);
            orderTableRepository.ContainsByPK(1).Returns(true);


            // Act
            var order = orderService.GetEntityByID(1);

            // Assert
            orderTableRepository.Received(1).FindByPK(1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Order doesn't exist")]
        public void ShouldThrowExceptionIfCantFindOrder()
        {
            // Arrange
            var orderTableRepository = Substitute.For<ITableRepository<Order>>();
            OrderService orderService = new OrderService(orderTableRepository);
            orderTableRepository.ContainsByPK(1).Returns(false);

            // Act
            var order = orderService.GetEntityByID(1);


        }

        [TestMethod]
        public void ShouldGetLastOrder()
        {
            // Arrange
            var orderTableRepository = Substitute.For<ITableRepository<Order>>();
            OrderService orderService = new OrderService(orderTableRepository);
            orderTableRepository.Count().Returns(5);


            // Act
            var order = orderService.LastOrder();

            // Assert
            int amount = orderTableRepository.Count();
            orderTableRepository.GetElementAt(amount);
        }

        [TestMethod]
        public void ShouldSetIsExecuted()
        {
            // Arrange
            var orderTableRepository = Substitute.For<ITableRepository<Order>>();
            OrderService orderService = new OrderService(orderTableRepository);
            Order order = new Order
            {
                OrderID = 1,
                ClientID = 3,
                StockID = 5,
                OrderType = OrderType.Sale,
                Quantity = 10,
                IsExecuted = false
            };


            // Act
            orderService.SetIsExecuted(order);

            // Assert
            order.IsExecuted = true;
            orderTableRepository.SaveChanges();
        }
    }
}
