namespace stockSimulator.Core.DTO
{
    public class StocksOfClientInfo
    {
        public int StockID { get; set; }
        public string StockName { get; set; }
        public string StockType { get; set; }
        public int StockAmount { get; set; }
        public decimal Cost { get; set; }

        public override string ToString()
        {
            return $"ID: {StockID}, Name: {this.StockName}, Type: '{this.StockType}', Amount: {this.StockAmount}, Cost: {this.Cost}";
        }
    }
}
