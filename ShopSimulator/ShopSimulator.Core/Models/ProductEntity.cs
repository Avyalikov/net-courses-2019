namespace ShopSimulator.Core.Models
{
    public class ProductEntity
    {
        public int Id { get; set; }

        public int SupplierId { get; set; }

        public string Name { get; set; }

        public decimal PricePerItem { get; set; }

        public int Count { get; set; }
    }
}
