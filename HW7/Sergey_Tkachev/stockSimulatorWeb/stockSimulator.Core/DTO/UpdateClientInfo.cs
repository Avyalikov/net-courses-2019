using System;
using System.Collections.Generic;
using System.Text;

namespace stockSimulator.Core.DTO
{
    public class UpdateClientInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public decimal AccountBalance { get; set; }
    }
}
