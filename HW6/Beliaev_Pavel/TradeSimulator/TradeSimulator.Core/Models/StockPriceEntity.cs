namespace TradeSimulator.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class StockPriceEntity
    {
        public int Id { get; set; }

        public string TypeOfStock { get; set; }

        public decimal PriceOfStock { get; set; }
    }
}
