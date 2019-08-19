namespace TradingSimulator.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Broker
    {
        public int ID { get; set; }
        public decimal Money { get; set; }
        [Required]
        public virtual PersonalCard Card { get; internal set; }
        [InverseProperty("Owner")]
        public virtual List<Share> ShareList { get; internal set; }
    }
}