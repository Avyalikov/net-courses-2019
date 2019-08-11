namespace Trading.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Stocks")]
    class Stock
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public decimal Price { get; set; }
        public virtual TypeStock Type { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
