namespace Trading.Logic
{
    using System;
    using System.Linq;
    using TradingData;

    internal class Transaction
    {
        private readonly IDbProvider dbProvider;

        public Transaction(IDbProvider dbProvider)
        {
            this.dbProvider = dbProvider;
        }

        public void Run()
        {
            Random random = new Random();

            var seller = ChooseUser();
            var customer = ChooseUser(seller.Id);

            var stockForTrade = seller.UserStocks.ToList()[random.Next(0, seller.UserStocks.Count - 1)];
            int AmountStocksForTrade = random.Next(0, stockForTrade.AmountStocks / 10);

            TradingData.Models.TransactionStory transaction = new TradingData.Models.TransactionStory
            {
                CustomerId = customer.Id,
                SellerId = seller.Id,
                AmountStocks = AmountStocksForTrade,
                Stock = stockForTrade.Stock,
                Sum = stockForTrade.Stock.Price * AmountStocksForTrade
            };
            dbProvider.Transaction(transaction);        
        }

        private TradingData.Models.User ChooseUser(int LastUserId = 0)
        {
            return dbProvider.ChooseUser(LastUserId);
        }
    }
}
