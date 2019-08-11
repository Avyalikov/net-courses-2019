namespace Trading.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TypesStock")]
    class TypeStock
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
    }
}
