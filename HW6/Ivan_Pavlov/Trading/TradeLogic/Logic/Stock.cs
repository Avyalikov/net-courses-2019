namespace Trading.Logic
{
    using System.Linq;
    using System.Text;
    using Trading.Infrastructure;
    using Trading.View;

    static class Stock
    {
        public static string ListStocks()
        {
            StringBuilder sb = new StringBuilder();

            using (AppDbContext db = new AppDbContext())
            {
                var InfoByStocks = db.Stocks.Include("TypeStock");

                foreach (var item in InfoByStocks)
                {
                    sb.AppendLine($"{item.Id}. {item.ToString()}");
                }
            }

            return sb.ToString();
        }

        public static void ChangeStockPrice()
        {            
            int stockId = SelectStock();
            using (AppDbContext db = new AppDbContext())
            {
                var stock = db.Stocks.Find(stockId);
                decimal oldPrice = stock.Price;
                stock.Price = NewPrice();
                db.SaveChanges();
                Logger.Log.Info($"ИЗМЕНЕНИЕ ЦЕНЫ: {stock.Name} имеет новую цену {stock.Price} вместо {oldPrice}");
            }            
        }

        private static int SelectStock(bool Valid = false)
        {            
            Models.Stock stock = null;
            if (int.TryParse(ChangeStock.ChooseStock(Valid), out int id))
            {
                using (AppDbContext db = new AppDbContext())
                {
                    stock = db.Stocks.Where(s => s.Id == id).FirstOrDefault();
                    if (stock != null)
                        return stock.Id;
                }
            }
            return SelectStock(true);
        }

        private static decimal NewPrice(bool Valid = false)
        {
            if (decimal.TryParse(ChangeStock.NewPrice(), out decimal newPrice))
                if (newPrice > 0)
                    return newPrice;
            return NewPrice(true);
        }
    }
}
