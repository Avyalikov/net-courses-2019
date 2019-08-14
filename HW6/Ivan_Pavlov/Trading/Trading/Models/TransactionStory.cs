namespace Trading.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    [Table("TransactionStory")]
    class TransactionStory
    {
        [Key]

        public int Id { get; set; }
        public int SellerId { get; set; }
        public int CustomerId { get; set; }
        public int AmountStocks { get; set; }
        public decimal Sum { get; set; }

        public virtual Stock Stock { get; set; }

        public override string ToString()
        {
            return $"{CustomerId} купил у {SellerId} {Stock.Name} в кол-ве {AmountStocks} шт. на сумму {Sum}";
        }
    }
}
