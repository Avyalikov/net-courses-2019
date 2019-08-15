namespace Trading.Logic
{
    using System.Text;
    using TradingData;
    using TradingView.Interface;

    internal static class Stock
    {
        private static readonly IView viewProvider = SettingsByLayers.viewProvider;
        private static readonly IDbProvider dbProvider = SettingsByLayers.dbProvider;

        public static string ListStocks()
        {
            StringBuilder sb = new StringBuilder();
            var InfoByStocks = dbProvider.ListStocks();

            foreach (var item in InfoByStocks)
                sb.AppendLine(item.ToString());

            return sb.ToString();
        }

        public static void ChangeStockPrice()
        {
            int stockId = SelectStock();
            decimal newPrice = NewPrice();
            dbProvider.ChangeStockPrice(stockId, newPrice);
        }

        private static int SelectStock(bool Valid = false)
        {
            if (int.TryParse(viewProvider.ChooseStock(Valid), out int id))
            {
                if (dbProvider.SelectStockId(id))
                    return id;
            }
            return SelectStock(true);
        }

        private static decimal NewPrice(bool Valid = false)
        {
            if (decimal.TryParse(viewProvider.NewPrice(), out decimal newPrice))
                if (newPrice > 0)
                    return newPrice;
            return NewPrice(true);
        }
    }
}
