namespace TradingApp.Core.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Share
    {
        [Key]
        public int ShareID { get; set; }
        public string ShareType { get; set; }
        public decimal Price { get; set; }
        public virtual ICollection<ClientPortfolio> ClientsPortfolios { get; set; }
    }
}
