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

        public  OrderModifier(ExchangeContext db, IClientStocksModifier clientStockModifier)
        {
            this.db = db;
            this.clientStockModifier = clientStockModifier;
        }

            public void GenerateOrder(OrderType orderType)
        {
            bool isGenerated = false;
            Order order;
            do
            {
                if (orderType == OrderType.Sale)
                {
                    var clientstock = db.ClientStocks.Select(o=>o).ToList();
                    if (clientstock!= null)
                    {  Random random = new Random();
                    int number= random.Next(clientstock.Count()-1);
                    var clientStock = clientstock[number];
                    order = new Order { ClientID = clientStock.ClientID, StockID = clientStock.StockID, Quantity = 10, OrderType = orderType, IsExecuted = false };
                    isGenerated = true;
                    AddOrder(order);
                    }
                }
                if (orderType == OrderType.Purchase)
                {var client = SelectClient();
                 var stock = SelectStock();
                 order = new Order { ClientID = client.ClientID, StockID = stock.StockID, Quantity = 10, OrderType = orderType, IsExecuted = false };
                 isGenerated = true;
                 AddOrder(order);
                }
            }
            while (isGenerated == false);
        }

       
        public void AddOrder(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
        }

        public Client SelectClient()
        {Random random = new Random();
            var clients = db.Clients.Select(o => o).ToList();
            int clientNumber = random.Next(clients.Count()-1);
            return  clients[clientNumber];
        }

        public Stock SelectStock()
        {
            Random random = new Random();
            var stocks = db.Stocks.Select(o => o).ToList();
            int stockNumber = random.Next(stocks.Count()-1);
            return stocks[stockNumber];
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
                Order order = null;
                    var purchaseOrders = db.Orders.Select(o => o).Where(or => or.IsExecuted == false && (int)or.OrderType == 1 && or.StockID == salersOrder.StockID);

                if (purchaseOrders.Count()>0)
                {
                    var purachaseOrdersIds = purchaseOrders.Select(o => o.OrderID).ToArray();
                    int purchaseOrderNumber = random.Next(purachaseOrdersIds.Count()-1);
                    var porderId = purachaseOrdersIds[purchaseOrderNumber];
                    var purchaseOrder = purchaseOrders.Select(o => o).Where(or => or.OrderID == porderId).Single();
                    return purchaseOrder;
                }
                return order;
            }
        }

        public Order GetRandomSalerOrder()
        {
            Random random = new Random();

            var saleOrders = db.Orders.Select(o => o).Where(or => or.IsExecuted == false && or.OrderType == 0);
            var saleOrdersIds = saleOrders.Select(o => o.OrderID).ToArray();
            int saleorderNumber = random.Next(saleOrdersIds.Count()-1);
            var orderid = saleOrdersIds[saleorderNumber];
            var saleOrder = saleOrders.Select(o => o).Where(or => or.OrderID == orderid).Single();
            return saleOrder;

        }
    }
}
