namespace TradingApp.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UserShare")]
    public class UserShare
    {
        [Key, Column(Order = 0)]
        public int UserId { get; set; }
        [Key, Column(Order = 1)]
        public int ShareId { get; set; }
        public int AmountStocks { get; set; }

        public virtual Share Share { get; set; }
        public virtual User User { get; set; }
    }
}
