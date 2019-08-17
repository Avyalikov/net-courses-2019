namespace TradingApp.Logic
{
    using System;
    using System.Linq;
    using TradingApp.Data;
    using TradingApp.Data.Models;

    class Transaction
    {
        private readonly IAppDbContext dbProvider;

        public Transaction(IAppDbContext dbProvider)
        {
            this.dbProvider = dbProvider;
        }

        public string TransactionRun()
        {
            Random random = new Random();

            int sellerId = ChooseUser();
            int customerId = ChooseUser(sellerId);

            var seller = dbProvider.Users.Include("UserShare.Share")
                .Where(u => u.Id == sellerId).First();

            var customer = dbProvider.Users.Include("UserShare.Share")
                .Where(u => u.Id == customerId).First();

            var share = dbProvider.Users.Where(u => u.Id == sellerId)
                .Select(u => u.UserShare).ToList();
            var shareForTrade = share[random.Next(0, share.Count() - 1)].First();

            int AmountSharesForTrade = random.Next(0, shareForTrade.AmountStocks / 10);

            if (customer.Balance < AmountSharesForTrade * shareForTrade.Share.Price)
            {
                throw new Exception($"ТРАНЗАКЦИЯ ОТКЛОНЕНА: {customer.Name} {customer.SurName} не имеет достаточно средств");
            }

            TransactionStory transaction = new TransactionStory
            {
                CustomerId = customer.Id,
                SellerId = seller.Id,
                AmountShare = AmountSharesForTrade,
                Share = shareForTrade.Share,
                Sum = shareForTrade.Share.Price * AmountSharesForTrade
            };
            dbProvider.TransactionStory.Add(transaction);

            seller.Balance += transaction.Sum;
            customer.Balance -= transaction.Sum;
            shareForTrade.AmountStocks -= AmountSharesForTrade;

            var customerAfterTrade = customer.UserShare.
                Where(us => us.Share.Id == transaction.Share.Id).FirstOrDefault();
            if (customerAfterTrade == null)
            {
                UserShare us = new UserShare
                {
                    AmountStocks = transaction.AmountShare,
                    Share = transaction.Share,
                    ShareId = transaction.Share.Id,
                    UserId = customer.Id,
                    User = customer
                };
                dbProvider.UserShares.Add(us);                
            }
            else
            {
                customerAfterTrade.AmountStocks += transaction.AmountShare;
            }
            dbProvider.SaveChanges();
            return $"{customer.SurName} купил у {seller.SurName} акции {transaction.Share.Name} {transaction.AmountShare} шт. на сумму {transaction.Sum}";
        }

        private int ChooseUser(int LastUserId = 0)
        {
            Random random = new Random();
            int userId;
            if (LastUserId != 0) //return customer
            {
                while ((userId = random.Next(1, dbProvider.Users.Count() + 1)) == LastUserId) ;
                return dbProvider.Users.Where(u => u.Id == userId).First().Id;
            }
            else //return seller with stocks
            {
                userId = random.Next(1, dbProvider.Users.Count() + 1);
                var seller = dbProvider.Users.Include("UserShare.Share")
                    .Where(u => u.Id == userId && u.UserShare.Count > 0).FirstOrDefault();
                if (seller != null)
                    return seller.Id;
                else
                    return ChooseUser();
            }
        }
    }
}
