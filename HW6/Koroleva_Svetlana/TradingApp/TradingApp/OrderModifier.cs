// <copyright file="OrderModifier.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace TradingApp
{
    using TradingApp.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TradingApp.DAL;
    using TradingApp.Model;

    /// <summary>
    /// OrderModifier description
    /// </summary>
    public class OrderModifier : IOrderModifier

    {
        public ExchangeContext db;
        private IClientStocksModifier clientStockModifier;
        private IClientModifier clientModifier;
        private ILogger logger;

        public  OrderModifier(ExchangeContext db, IClientStocksModifier clientStockModifier, IClientModifier clientModifier, ILogger logger)
        {
            this.db = db;
            this.clientStockModifier = clientStockModifier;
            this.clientModifier = clientModifier;
            this.logger = logger;
        }
        public void AddOrder(OrderType orderType, int stockId, int clientId, int amount)
        {
            Order order = new Order { ClientID = clientId, StockID = stockId, Quantity = amount, OrderType = orderType, IsExecuted = false };
            db.Orders.Add(order);
            db.SaveChanges();
            logger.Info($"Order {order.OrderID} for stock {order.StockID} purchase for client {order.ClientID} has been added to DB");         
        }

        public bool IsExists(int clientId, int stockId, int amount, OrderType orderType, bool isExecuted)
        {
            bool isExists = false;
            var orders = db.Orders.Where(w => w.OrderType == orderType &&
            w.ClientID == clientId&&
            w.StockID==stockId&&
            w.Quantity==amount&&
            w.IsExecuted==isExecuted).Select(o=>o);
            if (orders.Count()>0)
            {
                isExists = true;
            }
            return isExists;
        }

            
        public Order GetOrder(int id)
        {
            var order = db.Orders.Select(o => o).Where(or => or.OrderID == id).Single();
            return order;
        }

        
        public int GetOrderId(Order order)
        {
            var orderId = order.OrderID;
            return orderId;
        }

        public void SetIsExecuted(Order custOrder, Order salerOrder)
        {
            db.Orders.Where(or => or.OrderID == custOrder.OrderID).Select(o => o).Single().IsExecuted = true;
            db.Orders.Where(or => or.OrderID == salerOrder.OrderID).Select(o => o).Single().IsExecuted = true;
        }

        public Order GetRandomCustomerOrder(Order salersOrder)
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
        }

      
    }
}
