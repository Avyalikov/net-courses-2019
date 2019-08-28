namespace TradingSoftware.Core.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Share
    {
        [Key]
        public int ShareID { get; set; }

        public string ShareType { get; set; }

        public decimal Price { get; set; }
    }
}
