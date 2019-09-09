// <copyright file="OrderModifier.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace Trading.Core.Services
{
   
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Trading.Core.Model;
    using Trading.Core.Repositories;
    using Trading.Core.DTO;
    using Trading.Core.IServices;

    /// <summary>
    /// OrderModifier description
    /// </summary>
    public class OrderService: IOrderService

    {
        private readonly IUnitOfWork unitOfWork;
        
        public OrderService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
         
        }
        public void AddOrder(OrderInfo args)
        {
            Order order = new Order
            { ClientID = args.ClientId,
                StockID = args.StockId,
                Quantity = args.Quantity,
                OrderType = (OrderType)args.OrderType,
                IsExecuted = false,
                TransactionHistoryID=null
            }
            ;
         
            unitOfWork.Orders.Add(order);
            unitOfWork.Save();
        }

        public void Delete(int id)
        {
            if (this.unitOfWork.Orders.Get(c => c.OrderID == id) == null)
            {
                throw new ArgumentException("Order doesn't exist");
            }
            var orderToDelete = this.GetEntityByID(id);
            this.unitOfWork.Orders.Delete(orderToDelete);
        }

        public Order GetEntityByID(int orderId)
        {
            if (this.unitOfWork.Orders.Get(o=>o.OrderID==orderId)==null)
            {
                throw new ArgumentException("Order doesn't exist");
            }
            return this.unitOfWork.Orders.Get(o => o.OrderID == orderId).Single();
        }

        public Order LastOrder()
        {
            var orders = this.unitOfWork.Orders.GetAll().OrderByDescending(o=>o.OrderID).ToList();
            return orders.First();

        }
   
        public void SetIsExecuted(int orderId, int transactionId)
        {
            var order = this.unitOfWork.Orders.Get(o => o.OrderID == orderId).Single();           
            order.IsExecuted = true;
            order.TransactionHistoryID = transactionId;
            this.unitOfWork.Orders.Update(order);
            this.unitOfWork.Save();
          
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return this.unitOfWork.Orders.GetAll();
        }

        public IEnumerable<Order> GetTopOrdersForClient(int amount, int clientid)
        {
            return this.GetAllOrders().Where(c=>c.ClientID==clientid).Take(amount);
        }


        public void Update(int id, OrderInfo orderInfo)
        {
            var orderToUpdate = this.GetEntityByID(id);
            orderToUpdate.ClientID = orderInfo.ClientId;
            orderToUpdate.IsExecuted = orderInfo.IsExecuted;
            orderToUpdate.StockID = orderInfo.StockId;
            orderToUpdate.OrderType =(OrderType)orderInfo.OrderType;
            orderToUpdate.Quantity = orderInfo.Quantity;
            this.unitOfWork.Orders.Update(orderToUpdate);
            this.unitOfWork.Save();
        }


    }
}
