namespace TradingApp.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TransactionStory")]
    public class TransactionStory
    {
        [Key]

        public int Id { get; set; }
        public int SellerId { get; set; }
        public int CustomerId { get; set; }
        public int AmountShare { get; set; }
        public decimal Sum { get; set; }

        public virtual Share Share { get; set; }
    }
}
