namespace Core.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Trader
    {
        public int ID { get; set; }
        public decimal Money { get; set; }
        [Required]
        public virtual PersonalCard Card { get; set; }
        [InverseProperty("Owner")]
        public virtual List<Share> ShareList { get; set; }
        [Required]
        public string Reputation { get; set; }

        public override string ToString() => $"{ID}. {Card.Name} {Card.Surname},    {Money}$";
    }
}