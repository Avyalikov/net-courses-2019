using System;

namespace TradingApp.Core.Models
{
    public class UserEntity
    {
        public string ID { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
    }
}