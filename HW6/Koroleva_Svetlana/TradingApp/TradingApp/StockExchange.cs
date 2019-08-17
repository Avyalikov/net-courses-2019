// <copyright file="StockExchange.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace TradingApp
{
    using TradingApp.DAL;
    using TradingApp.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TradingApp.Model;
    using System.Threading;

    /// <summary>
    /// StockExchange description
    /// </summary>
    public class StockExchange : IStockExchange
    {
        ExchangeContext db;
        private IPriceModifier priceModifier;
        private IOrderModifier orderModifier;
        private ITransactionModifier transactionModifier;
        private IClientStocksModifier clientsStocksModifier;

        public StockExchange(IPriceModifier priceModifier, IOrderModifier orderModifier, ITransactionModifier transaction, IClientStocksModifier clientsStocksModifier )
        {
            this.db = new ExchangeContext();
            this.priceModifier = priceModifier;
            this.orderModifier = orderModifier;
            this.transactionModifier = transaction;
            this.clientsStocksModifier = clientsStocksModifier;
        }
      

        public void EditClientBalanse(Order custOrder, Order salerOrder, decimal price)
        {
            db.Clients.Where(c => c.ClientID == salerOrder.ClientID).Single().Balance += (decimal)(price * salerOrder.Quantity);
            db.Clients.Where(c => c.ClientID == custOrder.ClientID).Single().Balance -= (decimal)(price * custOrder.Quantity);
            db.SaveChanges();
        }

        public IEnumerable<Client> GetClientsFromOrangeZone()
        {
           return db.Clients.Where(c => c.Balance == 0).Select(c => c);
        }

        public IEnumerable<Client> GetClientsFromBlackZone()
        {
            return db.Clients.Where(c => c.Balance < 0).Select(c => c);
        }

        public void RunTraiding()
        {
           
            for(int i=0;i<5;i++)
            {
               orderModifier.GenerateOrder(OrderType.Sale);
                orderModifier.GenerateOrder(OrderType.Purchase);
                Order salerOrder = null;
                Order customerOrder = null;
                do
                {
                    salerOrder = orderModifier.GetRandomSalerOrder();
                    customerOrder = orderModifier.GetRandomCustomerOrder(salerOrder);
                }
                while (salerOrder == null || customerOrder == null||salerOrder.ClientID==customerOrder.ClientID);
                DateTime dateTime = DateTime.Now;
                decimal price = priceModifier.GetPriceByDateTime(dateTime, salerOrder.StockID);
                clientsStocksModifier.EditClientStocks(customerOrder, salerOrder);
                EditClientBalanse(customerOrder, salerOrder, price);
                var cl = db.Clients.Where(c => c.ClientID == customerOrder.ClientID).Single();
                var saler = db.Clients.Where(c => c.ClientID == salerOrder.ClientID).Single();
                transactionModifier.CommitTransaction(customerOrder, salerOrder, dateTime);
                orderModifier.SetIsExecuted(customerOrder, salerOrder);
                priceModifier.AddPriceInfo(price, salerOrder.StockID, dateTime);
                
                Thread.Sleep(10000);
            }





        }
    }
}
