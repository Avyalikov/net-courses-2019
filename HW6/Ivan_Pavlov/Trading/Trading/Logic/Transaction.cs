﻿namespace Trading.Logic
{
    using System;
    using System.Linq;
    using Trading.Infrastructure;

    static class Transaction
    {
        public static void Run()
        {
            using (AppDbContext db = new AppDbContext())
            {
                Random random = new Random();

                int sellerId = ChooseUser();
                int customerId = ChooseUser(sellerId);

                var seller = db.Users.Include("UserStocks.Stock")
                    .Where(u => u.Id == sellerId).First();

                var customer = db.Users.Include("UserStocks.Stock")
                    .Where(u => u.Id == customerId).First();

                var stockForTrade = seller.UserStocks.ToList()[random.Next(0, seller.UserStocks.Count - 1)];

                int AmountStocksForTrade = random.Next(0, stockForTrade.AmountStocks / 5);

                var sellerStocksAfterTrade = seller.UserStocks
                    .Where(us => us.Stock == stockForTrade.Stock).First();

                if ((customer.Balance -= AmountStocksForTrade * stockForTrade.Stock.Price) < 0)
                    return; //нехватка денег в логи
                else
                {
                    var customerStocksAfterTrade = customer.UserStocks
                        .Where(us => us.Stock == sellerStocksAfterTrade.Stock).FirstOrDefault();

                    sellerStocksAfterTrade.AmountStocks -= AmountStocksForTrade;
                    seller.Balance += AmountStocksForTrade * stockForTrade.Stock.Price;

                    if (customerStocksAfterTrade != null)
                    {
                        customerStocksAfterTrade.AmountStocks += AmountStocksForTrade;
                    }
                    else
                    {
                        Models.UserStocks us = new Models.UserStocks
                        {
                            AmountStocks = AmountStocksForTrade,
                            Stock = sellerStocksAfterTrade.Stock,
                            User = customer,
                            UserId = customer.Id,
                            StockId = sellerStocksAfterTrade.Stock.Id
                        };
                        db.UserStocks.Add(us);
                    }        
                    db.SaveChanges();
                }
            }
        }

        private static int ChooseUser(int LastUserId = 0)
        {
            using (AppDbContext db = new AppDbContext())
            {
                Random random = new Random();
                int userId;
                if (LastUserId != 0) //return customer
                {
                    while ((userId = random.Next(1, db.Users.Count() + 1)) == LastUserId) ;
                    return db.Users.Where(u => u.Id == userId).First().Id;
                }
                else //return seller with stocks
                {
                    userId = random.Next(1, db.Users.Count() + 1);
                    Models.User seller = db.Users.Include("UserStocks.Stock")
                        .Where(u => u.Id == userId).First();
                    if (seller.UserStocks.Count > 0)
                        return seller.Id;
                    else
                        return ChooseUser();
                }
            }                  
        }       
    }
}
