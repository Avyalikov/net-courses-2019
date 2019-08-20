namespace trading_software
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class BlockOfShares
    {
        [Required]
        [Key, Column(Order = 0)]
        public virtual int ClientID { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public virtual int StockID { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}