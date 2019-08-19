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
        private IClientModifier clientModifier;
        private IStockModifier stockModifier;


        public StockExchange(IPriceModifier priceModifier,
            IOrderModifier orderModifier,
            ITransactionModifier transaction,
            IClientStocksModifier clientsStocksModifier,
            IClientModifier clientModifier,
            IStockModifier stockModifier
            )
        {
            this.db = new ExchangeContext();
            this.priceModifier = priceModifier;
            this.orderModifier = orderModifier;
            this.transactionModifier = transaction;
            this.clientsStocksModifier = clientsStocksModifier;
            this.stockModifier = stockModifier;
            this.clientModifier = clientModifier;
        }





        public void RunTraiding()
        {
            log4net.Config.XmlConfigurator.Configure();
            var logger = new Logger(log4net.LogManager.GetLogger("Logger"));
            for (int i = 0; i < 10; i++)
            {

                int amountForSale = 10;

                Client saler = clientModifier.GetRandomClient();
                ClientStock clstock = clientsStocksModifier.GetRandomClientStock(saler.ClientID);
                if (clstock == null)
                {
                    throw new NullReferenceException("This client has no stocks, select another saler");

                }
               
                
                if (!orderModifier.IsExists(clstock.ClientID, clstock.StockID, amountForSale, OrderType.Sale, false))
                    {
                    orderModifier.AddOrder(OrderType.Sale, clstock.StockID, clstock.ClientID, amountForSale);
                 
                   }
                    
                 var salerOrder = db.Orders.Where(o => o.ClientID==clstock.ClientID&&o.StockID == clstock.StockID && o.Quantity == amountForSale && o.OrderType == OrderType.Sale&&o.IsExecuted==false).Select(o => o).First();
                        var purchaseOrders = db.Orders.Where(o => o.StockID == clstock.StockID && o.Quantity == amountForSale && o.OrderType == OrderType.Purchase).Select(o => o);
                        if (purchaseOrders.Count() == 0)
                        {
                            throw new NullReferenceException("There are no orders for purhcase");
                        }
                        var customerOrder = orderModifier.GetRandomCustomerOrder(salerOrder);
                        DateTime dateTime = DateTime.Now;
                        decimal price = priceModifier.GetPriceByDateTime(dateTime, salerOrder.StockID);
                        clientsStocksModifier.EditClientStocks(customerOrder, salerOrder);
                        clientModifier.EditClientBalanse(customerOrder, salerOrder, price);
                        var cl = db.Clients.Where(c => c.ClientID == customerOrder.ClientID).Single();
                        transactionModifier.CommitTransaction(customerOrder, salerOrder, dateTime);
                        logger.Info($"Transaction has been commited on {dateTime} with price {price} for stock {salerOrder.StockID}" +
                            $" Order { salerOrder.OrderID}  saler { salerOrder.ClientID} {saler.FirstName} balance: {saler.Balance}" +
                            $" Order {customerOrder.OrderID}  customer {customerOrder.ClientID} {cl.FirstName} balance: {cl.Balance}");
                        orderModifier.SetIsExecuted(customerOrder, salerOrder);
                        priceModifier.AddPriceInfo(price, salerOrder.StockID, dateTime);
                    Thread.Sleep(10000);
                }

            }
        }
    }

