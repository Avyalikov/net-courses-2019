using System.ComponentModel.DataAnnotations;
namespace trading_software
{
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
