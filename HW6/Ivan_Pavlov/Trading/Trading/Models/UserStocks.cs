namespace Trading.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UserStocks")]
    class UserStocks
    {
        [Key, Column(Order = 0)]
        public int UserId { get; set; }
        [Key, Column(Order = 1)]
        public int StockId { get; set; }
        public int AmountStocks { get; set; }

        public virtual Stock Stock { get; set; }
        public virtual User User { get; set; }

        public override string ToString()
        {
            return string.Format($"Stock Name {Stock.Name} by company {Stock.Company} amount = {AmountStocks}");
        }
    }
}
