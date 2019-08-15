namespace Trading.Logic
{
    using System;
    using System.Linq;
    using TradingData;

    internal static class Transaction
    {
        private static readonly IDbProvider dbProvider = SettingsByLayers.dbProvider;

        public static void Run()
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

        private static TradingData.Models.User ChooseUser(int LastUserId = 0)
        {
            return dbProvider.ChooseUser(LastUserId);
        }
    }
}
