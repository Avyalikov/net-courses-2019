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

    /// <summary>
    /// OrderModifier description
    /// </summary>
    public class OrderService

    {
        private ITableRepository<Order> tableRepository;
        
        public OrderService(ITableRepository<Order> tableRepository  )
        {
            this.tableRepository = tableRepository;
         
        }
        public void AddOrder(OrderInfo args)
        {
            Order order = new Order
            { ClientID = args.ClientId,
                StockID = args.StockId,
                Quantity = args.Quantity,
                OrderType = (OrderType)args.OrderType,
                IsExecuted = false }
            ;
         
            tableRepository.Add(order);
            tableRepository.SaveChanges();
        }

        public Order GetEntityByID(int orderId)
        {
            if (!this.tableRepository.ContainsByPK(orderId))
            {
                throw new ArgumentException("Order doesn't exist");
            }
            return this.tableRepository.FindByPK(orderId);
        }

        public Order LastOrder()
        {
            int orderAmount = this.tableRepository.Count();
            return tableRepository.GetElementAt(orderAmount);

        }
   
        public void SetIsExecuted(Order order)
        {
            order.IsExecuted = true;
            tableRepository.SaveChanges();
          
        }
             
    }
}
