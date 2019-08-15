namespace TradingData.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TypesStock")]
    public class TypeStock
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
    }
}
