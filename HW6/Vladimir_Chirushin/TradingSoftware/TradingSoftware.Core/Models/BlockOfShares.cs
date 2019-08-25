namespace TradingSoftware.Core.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class BlockOfShares
    {
        [Key, Column(Order = 0)]
        public virtual int ClientID { get; set; }

        [Key, Column(Order = 1)]
        public virtual int ShareID { get; set; }

        public int Amount { get; set; }
    }
}
