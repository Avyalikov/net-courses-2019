namespace TradeSimulator.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class StockOfClientEntity
    {
        public int Id { get; set; }

        public string TypeOfStocks { get; set; }

        public int quantityOfStocks { get; set; }

        public int AccountId { get; set; }

        public AccountEntity AccountForStock { get; set; }
    }
}
