namespace stockSimulator.Core.Models
{
    public class StockOfClientsEntity
    {
        public int ID { get; set; }
        public int ClientID { get; set; }
        public int StockID { get; set; }
        public int Amount { get; set; }

        public virtual ClientEntity Client { get; set; }
        public virtual StockEntity Stock { get; set; }
    }
}
