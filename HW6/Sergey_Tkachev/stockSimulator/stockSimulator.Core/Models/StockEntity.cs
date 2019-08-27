using System.Collections.Generic;

namespace stockSimulator.Core.Models
{
    public class StockEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Cost { get; set; }

        public virtual IEnumerable<StockOfClientsEntity> Stocks { get; set; }
    }
}
