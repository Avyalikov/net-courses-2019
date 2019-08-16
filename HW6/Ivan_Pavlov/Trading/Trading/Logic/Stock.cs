namespace Trading.Logic
{
    using System.Text;
    using TradingData;
    using TradingView.Interface;

    internal class Stock
    {
        private readonly IView viewProvider;
        private readonly IDbProvider dbProvider;

        public Stock(IView viewProvider, IDbProvider dbProvider)
        {
            this.viewProvider = viewProvider;
            this.dbProvider = dbProvider;
        }

        public string ListStocks()
        {
            StringBuilder sb = new StringBuilder();
            var InfoByStocks = dbProvider.ListStocks();

            foreach (var item in InfoByStocks)
                sb.AppendLine(item.ToString());

            return sb.ToString();
        }

        public void ChangeStockPrice()
        {
            int stockId = SelectStock();
            decimal newPrice = NewPrice();
            dbProvider.ChangeStockPrice(stockId, newPrice);
        }

        private int SelectStock(bool Valid = false)
        {
            if (int.TryParse(viewProvider.ChooseStock(Valid), out int id))
            {
                if (dbProvider.SelectStockId(id))
                    return id;
            }
            return SelectStock(true);
        }

        private decimal NewPrice(bool Valid = false)
        {
            if (decimal.TryParse(viewProvider.NewPrice(), out decimal newPrice))
                if (newPrice > 0)
                    return newPrice;
            return NewPrice(true);
        }
    }
}
