namespace TradingSoftware.Core.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Client
    {
        [Key]
        public int ClientID { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public decimal Balance { get; set; }

        public virtual List<BlockOfShares> blockOfShares { get; set; }
    }
}
