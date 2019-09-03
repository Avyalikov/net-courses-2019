using System.Collections.Generic;

namespace stockSimulator.Core.Models
{
    public class StockEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Cost { get; set; }

        public virtual ICollection<StockOfClientsEntity> Clients { get; set; }
    }
}
