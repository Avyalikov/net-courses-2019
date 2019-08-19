using System;

namespace stockSimulator.Core.Models
{
    public class ClientEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreateAt { get; set; }
        public decimal AccountBalance { get; set; }
        public int ZoneID { get; set; }

    }
}
