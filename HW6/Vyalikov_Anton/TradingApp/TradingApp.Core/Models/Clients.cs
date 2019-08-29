namespace TradingApp.Core.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Clients
    {
        [Key]
        public int ClientID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Balance { get; set; }
        public virtual ICollection<ClientsPortfolios> ClientPortfolios { get; set; }
    }
}
