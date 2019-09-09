using System;

namespace TradingSimulator.Core.Models
{
    public class TraderEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Balance { get; set; }
    }
}