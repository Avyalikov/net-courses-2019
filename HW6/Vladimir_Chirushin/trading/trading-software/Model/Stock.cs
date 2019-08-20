namespace trading_software
{
    using System.ComponentModel.DataAnnotations;
    public class Stock
    {
        [Key]
        public int StockID { get; set; }
        [Required]
        public string StockType { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}