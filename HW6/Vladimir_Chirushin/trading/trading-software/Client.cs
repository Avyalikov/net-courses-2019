using System.Collections.Generic;
namespace trading_software
{
    public class Client
    {
        public int ClientID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Balance { get; set; }
        public virtual List<Stock> AvailableStocks { get; set; }
    }
}
