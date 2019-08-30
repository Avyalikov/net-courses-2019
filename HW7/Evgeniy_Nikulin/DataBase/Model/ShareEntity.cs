namespace TradingSimulator.DataBase.Model
{
    using System.ComponentModel.DataAnnotations;

    public class ShareEntity
    {
        public int ID { get; set; }
        [MaxLength(128)]
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public virtual TraderEntity Owner { get; set; }
    }
}