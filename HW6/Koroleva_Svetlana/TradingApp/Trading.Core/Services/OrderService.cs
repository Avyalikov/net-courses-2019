// <copyright file="OrderModifier.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace Trading.Core.Modifiers
{
    using Trading.Core.Interfaces;
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
        private ITableRepository tableRepository;
            

        public  OrderService(
            ITableRepository tableRepository
        )
        {
            this.tableRepository = tableRepository;
           
        }
        public void AddOrder(OrderInfo args)
        {
            Order order = new Order { ClientID = args.ClientId, StockID = args.StockId, Quantity = args.Quantity, OrderType = (OrderType)args.ordType, IsExecuted = false };
            tableRepository.Add(order);
            tableRepository.SaveChanges();
        }

        public Order GetEntityByID(int orderId)
        {
            if (this.tableRepository.ContainsByID(orderId))
            {
                throw new ArgumentException("Order doesn't exist");
            }
            return (Order)tableRepository.GetEntityByID(orderId);
        }

   
        public void SetIsExecuted(int clientId)
        {
            Order order = this.GetEntityByID(clientId);
            order.IsExecuted = true;
            tableRepository.SaveChanges();
          
        }

        /*public Order GetRandomCustomerOrder(Order salersOrder)
        {
            Random random = new Random();

            {
                var purchaseOrders = db.Orders.Select(o => o).Where(or => or.IsExecuted == false && (int)or.OrderType == 1 && or.StockID == salersOrder.StockID);

               
                if (purchaseOrders.Count()==0)
                {
                    Client client = null;
                    do
                    {
                        client = clientModifier.GetRandomClient();
                    }
                    while (client.ClientID == salersOrder.ClientID);

                    AddOrder(OrderType.Purchase, salersOrder.StockID, client.ClientID, salersOrder.Quantity);
                    return db.Orders.OrderByDescending(o=>o.OrderID).Select(o=>o).First();
                }
                var purachaseOrdersIds = purchaseOrders.Select(o => o.OrderID).ToArray();
                    int purchaseOrderNumber = random.Next(purachaseOrdersIds.Count()-1);
                    var porderId = purachaseOrdersIds[purchaseOrderNumber];
                    var order= purchaseOrders.Select(o => o).Where(or => or.OrderID == porderId).Single();
                    return order;
               
            }
        }*/

      
    }
}
