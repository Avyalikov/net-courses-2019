namespace Trading.Logic
{
    using System.Linq;
    using System.Text;
    using Trading.Infrastructure;

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
                    sb.AppendLine(item.ToString());
                }
            }

            return sb.ToString();
        }
    }
}
