namespace TradingData
{
    using System;
    using System.Linq;
    using TradingData.Models;

    public class DbProvider : IDbProvider
    {
        private static AppDbContext db = new AppDbContext();

        public IQueryable ListUsers()
        {
            IQueryable InfoByUsers = db.Users.Include("UserStocks.Stock");

            return InfoByUsers;
        }

        public IQueryable ListStocks()
        {
            var InfoByStocks = db.Stocks.Include("TypeStock");

            return InfoByStocks;
        }

        public void AddUser(User user)
        {
            try
            {
                db.Users.Add(user);
                db.SaveChanges();
                Logger.Log.Info("НОВЫЙ ПОЛЬЗОВАТЕЛЬ: " + user.ToString());
            }
            catch (Exception ex)
            {
                Logger.Log.Error("ОШИБКА: НЕВЕРНЫЙ ПОЛЬЗОВАТЕЛЬ " + ex.Message);
            }
        }

        public void ChangeStockPrice(int idStock, decimal newPrice)
        {
            try
            {
                var stock = db.Stocks.Find(idStock);
                decimal oldPrice = stock.Price;
                stock.Price = newPrice;
                db.SaveChanges();
                Logger.Log.Info($"ИЗМЕНЕНИЕ ЦЕНЫ: {stock.Name} имеет новую цену {stock.Price} вместо {oldPrice}");
            }
            catch (Exception ex)
            {
                Logger.Log.Error("ОШИБКА: ИЗМЕНЕНИЕ ЦЕНЫ ОТМЕНЕНО " + ex.Message);
            }
        }

        public bool SelectStockId(int id)
        {
            Stock stock = null;
            stock = db.Stocks.Where(s => s.Id == id).FirstOrDefault();
            if (stock != null)
                return true;
            return false;
        }

        public void Transaction(TransactionStory transaction)
        {
            var seller = db.Users.Include("UserStocks.Stock")
                .Where(u => u.Id == transaction.SellerId).First();

            var customer = db.Users.Include("UserStocks.Stock")
                .Where(u => u.Id == transaction.CustomerId).First();

            if (customer.Balance < transaction.Sum)
            {
                Logger.Log.Info($"ТРАНЗАКЦИЯ ОТКЛОНЕНА: {customer.Name} {customer.SurName} не имеет достаточно средств");
                return;
            }

            db.TransactionStory.Add(transaction);
            Logger.Log.Info(transaction.ToString());
            seller.Balance += transaction.Sum;
            customer.Balance -= transaction.Sum;

            var sellerAfterTrade = seller.UserStocks
                .Where(us => us.Stock.Id == transaction.Stock.Id).First();
            sellerAfterTrade.AmountStocks -= transaction.AmountStocks;
            Logger.Log.Info($"{seller.SurName} продал {transaction.Stock.Name} в кол-ве {transaction.AmountStocks} на сумму {transaction.Sum}");

            var customerAfterTrade = customer.UserStocks.
                Where(us => us.Stock.Id == transaction.Stock.Id).FirstOrDefault();
            if (customerAfterTrade == null)
            {
                UserStocks us = new UserStocks
                {
                    AmountStocks = transaction.AmountStocks,
                    Stock = transaction.Stock,
                    StockId = transaction.Stock.Id,
                    UserId = customer.Id,
                    User = customer
                };
                db.UserStocks.Add(us);
                Logger.Log.Info($"{customer.SurName} {customer.Name} приобрел новый вид акций {us.Stock.Name}");
            }
            else
            {
                customerAfterTrade.AmountStocks += transaction.AmountStocks;
            }
            Logger.Log.Info($"{customer.SurName} купил {transaction.Stock.Name} в кол-ве {transaction.AmountStocks} на сумму {transaction.Sum}");
            db.SaveChanges();
        }

        public User ChooseUser(int LastUserId = 0)
        {
            Random random = new Random();
            int userId;
            if (LastUserId != 0) //return customer
            {
                while ((userId = random.Next(1, db.Users.Count() + 1)) == LastUserId) ;
                return db.Users.Include("UserStocks.Stock")
                    .Where(u => u.Id == userId).First();
            }
            else //return seller with stocks
            {
                userId = random.Next(1, db.Users.Count() + 1);
                var seller = db.Users.Include("UserStocks.Stock")
                    .Where(u => u.Id == userId && u.UserStocks.Count > 0).FirstOrDefault();
                if (seller != null)
                    return seller;
                else
                    return ChooseUser();
            }
        }

        public IQueryable OrangeZone()
        {
            return Zone(0);
        }

        public IQueryable BlackZone()
        {
            return Zone(1);
        }

        private IQueryable Zone(int idZone)
        {
            if (idZone == 0)
                return db.Users.Where(u => u.Balance == 0);
            else
                return db.Users.Where(u => u.Balance < 0);
        }

    }
}
