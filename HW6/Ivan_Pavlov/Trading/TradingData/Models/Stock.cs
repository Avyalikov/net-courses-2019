namespace TradingData.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Stock")]
    public class Stock
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public decimal Price { get; set; }
        public virtual TypeStock TypeStock { get; set; }

        public override string ToString()
        {
            return $"{Id}. \"{Name}\" от компании {Company} с ценой {Price} и типом {TypeStock.Type}";
        }
    }
}
