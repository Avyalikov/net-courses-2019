namespace TradingSimulator.DataBase.Model
{
    using System.ComponentModel.DataAnnotations;

    public class TraderEntity
    {
        public int ID { get; set; }
        public decimal Money { get; set; }
        [Required]
        public virtual CardEntity Card { get; set; }
    }
}