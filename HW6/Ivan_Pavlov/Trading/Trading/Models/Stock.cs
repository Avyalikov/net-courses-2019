namespace Trading.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Stock")]
    class Stock
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public decimal Price { get; set; }
        public virtual TypeStock TypeStock { get; set; }

        public override string ToString()
        {
            return $"\"{Name}\" от компании {Company} с ценой {Price} и типом {TypeStock.Type}";
        }
    }
}
