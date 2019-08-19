// <copyright file="StockModifier.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace TradingApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TradingApp.DAL;
    using TradingApp.Interfaces;
    using TradingApp.Model;

    /// <summary>
    /// StockModifier description
    /// </summary>
    public class StockModifier : IStockModifier
    {
        ExchangeContext db;
        public StockModifier(ExchangeContext db)
        {
            this.db = db;
        }
        public void AddStock(string prefix, int issuerID, StockType type)
        {
            var stock = new Stock {StockPrefix=prefix, IssuerID= issuerID, StockType=type };
            db.Stocks.Add(stock);
            db.SaveChanges();

        }
        public Stock GetStockByID(int id)
        {
            return db.Stocks.Where(i => i.StockID == id).Select(s => s).Single();
        }
        public Stock GetRandomStock()
        {
            Random random = new Random();
           Stock stock = null;
            var stocks = db.Stocks.Select(o => o).ToList();
            if (stocks != null)
            {
                int number = random.Next(stocks.Count() - 1);
                stock = stocks[number];
            }
            return stock;
        }
    }
}
