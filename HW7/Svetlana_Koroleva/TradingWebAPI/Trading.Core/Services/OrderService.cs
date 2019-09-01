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
            var orders = this.unitOfWork.Orders.GetAll().OrderByDescending(o=>o.OrderID);
            return orders.First();

        }
   
        public void SetIsExecuted(int orderId, int transactionId)
        {
            var order = this.unitOfWork.Orders.Get(o => o.OrderID == orderId).Single();
            order.IsExecuted = true;
            //this.unitOfWork.Orders.Update(order);
            this.unitOfWork.Save();
          
        }

       
        public void Update(int id)
        {
            throw new NotImplementedException();
        }
    }
}
